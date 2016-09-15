using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Models;

namespace ACTransit.Training.Web.Domain.Apprentice.Models
{
    [DataContract]
    public class ParticipantProgressDaysViewModel : ViewModelBase
    {
        public ParticipantProgressDaysViewModel() { }

        public static ParticipantProgressDaysViewModel Create(int? id = null)
        {
            return new ParticipantProgressDaysViewModel
            {
                Progress = new Progress
                {
                    ProgressId = id.GetValueOrDefault()
                }
            };
        }

        public ProgramLevelGroup ProgramLevelGroup { get; set; }

        public ParticipantProgramLevelGroup ParticipantProgramLevelGroup { get; set; }

        public Participant Participant { get; set; }

        public Progress Progress { get; set; }

        public List<ProgressDay> Items { get; set; }

        public bool AreDailyEvaluationsDone
        {
            get
            {
                return Items.All(IsDailyEvaluationDone);
            }
        }

        public bool IsDailyEvaluationDone(ProgressDay progressDay)
        {
            return progressDay.IsDayOff 
                || progressDay.ParticipantWork == null 
                || !progressDay.ParticipantWork.Any() 
                || (progressDay.DailyPerformanceId > 0 && !string.IsNullOrEmpty(progressDay.Comment));
        }

        public string HighlightButton(ProgressDay item)
        {
            return IsDailyEvaluationDone(item) ? "btn-default" : "btn-warning";
        }

        public string EvaluationText(ProgressDay item)
        {
            return IsDailyEvaluationDone(item) ? "View" : "Pending";
        }

        public string HideButton(ProgressDay item)
        {
            return item.IsDayOff || (!string.IsNullOrEmpty(item.Comment) && item.DailyPerformanceId.GetValueOrDefault() > 0) ? "hidden" : "";
        }
    }
}