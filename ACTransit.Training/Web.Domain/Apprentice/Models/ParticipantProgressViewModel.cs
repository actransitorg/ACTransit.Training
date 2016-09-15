using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Models;
using Newtonsoft.Json;

namespace ACTransit.Training.Web.Domain.Apprentice.Models
{
    [DataContract]
    public class ParticipantProgressViewModel : ViewModelBase
    {
        public ParticipantProgressViewModel() { }

        public static ParticipantProgressViewModel Create(int? id = null, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            return new ParticipantProgressViewModel
            {
                Participant = new Participant
                {
                    ParticipantId = id.GetValueOrDefault()
                },
                IsDailyEvaluationDone = new Dictionary<Progress, bool>(),
                StartDate = StartDate ?? DateTime.Now.Date.AddMonths(-1),
                EndDate = EndDate ?? DateTime.Now.Date
            };
        }

        public Participant Participant { get; set; }

        public List<ParticipantWork> ParticipantWork { get; set; }
        
        public List<Progress> Items { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ProgressLevel(Progress progress)
        {
            var lastProgressDay = progress.ProgressDays.OrderByDescending(pd => pd.CalendarDate).FirstOrDefault();
            return lastProgressDay != null ? lastProgressDay.ProgramLevel.Level.ToString() : (Participant != null && Participant.ProgramLevel != null ? Participant.ProgramLevel.Level.ToString() : "n/a");
        }

        public string ProgressTitle(Progress progress)
        {
            return string.Format("Week {0} to {1}: Score {2}, Level {3}",
                progress.StartDate.ToShortDateString(),
                progress.EndDate.ToShortDateString(),
                progress.ScoreTotal,
                ProgressLevel(progress));
        }

        public string HighlightEvaluationButton(Progress item)
        {
            if (!HasWorkOrders[item] || IsHistorical[item])
                return "btn-disable";
            if (item.SuperintendentApprovalDate != null)
                return "btn-default";
            if (item.EvaluationDate == null)
                return "btn-warning";
            return "btn-success";
        }

        public string DisableEvaluationButton(Progress item)
        {
            return !HasWorkOrders[item] ? "disabled=disabled" : "";
        }

        public string IsClickableButton(Progress item)
        {
            return !HasWorkOrders[item] ? "" : "clickable";
        }

        public string EvaluationText(Progress item)
        {
            if (IsHistorical[item])
                return "Historical";
            if (!HasWorkOrders[item])
                return "No Work Orders";
            if (item.SuperintendentApprovalDate != null)
                return "View";
            if (item.EvaluationDate == null || item.SuperintendentApprovalDate == null)
                return "Pending";
            return "Pending";
        }

        public Dictionary<Progress, bool> IsDailyEvaluationDone { get; set; }
        public Dictionary<Progress, bool> HasWorkOrders { get; set; }
        public Dictionary<Progress, bool> IsHistorical { get; set; }

        public string ShowEvaluationButton(Progress item)
        {
            return item.StartDate <= DateTime.Now.Date && IsDailyEvaluationDone != null && IsDailyEvaluationDone[item] ? "" : "hidden";
        }

    }
}