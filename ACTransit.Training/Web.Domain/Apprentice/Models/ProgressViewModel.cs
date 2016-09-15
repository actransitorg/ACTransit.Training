using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.UI;
using ACTransit.Entities.Employee;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ACTransit.Training.Web.Domain.Apprentice.Models
{
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ProgressViewModel : ViewModelBase
    {
        public ProgressViewModel() { }

        public static ProgressViewModel Create(int? id = null)
        {
            return new ProgressViewModel
            {
                Progress = new Progress
                {
                    ProgressId = id.GetValueOrDefault()
                },
                ProgressDayViewModels = new Dictionary<ProgressDay, ProgressDayViewModel>()
            };
        }

        [DataMember, JsonProperty]
        public Progress Progress { get; set; }

        [JsonIgnore]
        public ParticipantProgramLevelGroup ParticipantProgramLevelGroup { get; set; }

        [JsonIgnore]
        public ProgramLevelGroup ProgramLevelGroup { get; set; }

        [JsonIgnore]
        public List<DailyPerformanceProgramLevelGroup> DailyPerformanceProgramLevelGroups { get; set; }

        [JsonIgnore]
        public Participant Participant { get; set; }

        [JsonIgnore]
        public ParticipantStatus ParticipantStatus { get; set; }

        [JsonIgnore]
        public Program Program { get; set; }

        [JsonIgnore]
        public ICollection<ParticipantWork> ParticipantWork { get; set; }

        [DataMember, JsonProperty]
        public ICollection<ProgressDay> ProgressDays { get; set; }

        [JsonIgnore]
        public ICollection<RatingCell> RatingCells { get; set; }

        [JsonIgnore]
        public ICollection<ProgressRatingCellScore> ProgressRatingCellScores { get; set; }

        [JsonIgnore]
        public Dictionary<ProgressDay, ProgressDayViewModel> ProgressDayViewModels { get; set; }

        [JsonIgnore]
        public List<WorkCategory> WorkCategory { get; set; }

        [JsonIgnore]
        public bool SignAsApprentice { get; set; }

        [JsonIgnore]
        public bool SignAsSupervisor { get; set; }

        [JsonIgnore]
        public bool SignAsSuperintendent { get; set; }

        [JsonIgnore]
        public List<EmployeeAll> Employees { get; set; }

        [JsonIgnore]
        public string AclUserName { get; set; }

        [JsonIgnore]
        public EmployeeAll CurrentUser { get; set; }

        public EmployeeAll Employee(string badge)
        {
            return badge != null ? Employees.FirstOrDefault(s => s.Badge == badge) : new EmployeeAll();
        }

        public string EmployeeName(string badge)
        {
            var employee = Employee(badge);
            if (employee == null) return null;
            return employee.FirstName + " " + employee.LastName;
        }

        public string ParticipantName
        {
            get { return Participant != null ? EmployeeName(Participant.Badge) : null; }
        }

        public string SupervisorName
        {
            get { return Progress != null ? EmployeeName(Progress.SupervisorBadge) : null; }
        }

        public string SuperintendentName
        {
            get { return Progress != null ? EmployeeName(Progress.SuperintendentBadge) : null; }
        }

        public string StartDate
        {
            get { return Progress != null ? Progress.StartDate.ToString("M/d") : null; }
        }

        public string EndDate
        {
            get { return Progress != null ? Progress.EndDate.ToString("M/d") : null; }
        }

        public bool IsLastProgress { get; set; }

        [JsonProperty]
        public bool AreDailyEvaluationsDone
        {
            get
            {
                return Progress != null && (Progress.SuperintendentApprovalDate.HasValue
                    || ProgressDays.All(IsDailyEvaluationDone));
            }
        }

        public bool IsDailyEvaluationDone(ProgressDay progressDay)
        {
            return progressDay.IsDayOff|| (progressDay.DailyPerformanceId > 0 && !string.IsNullOrEmpty(progressDay.Comment));
        }

        public bool ShowProgressEvaluationSection
        {
            get
            {
                if (ProgressDays == null) return false;
                var lastProgressDay = ProgressDays != null ? ProgressDays.OrderBy(pd => pd.CalendarDate).LastOrDefault() : null;
                var lastDate = lastProgressDay != null ? lastProgressDay.CalendarDate : DateTime.MaxValue;
                return (Progress != null && Progress.ScoreTotal > 0) || (AreDailyEvaluationsDone && (!IsLastProgress || DateTime.Now >= lastDate));
            }
        }

        public string ProgramLevelGroupName
        {
            get
            {
                if (ProgramLevelGroup == null)
                    return null;
                var result = ProgramLevelGroup.Program != null ? ProgramLevelGroup.Program.Name : "";
                if (ProgramLevelGroup.StartLevel > 0 && ProgramLevelGroup.EndLevel > 0)
                    result += (!string.IsNullOrEmpty(result) ? ": " : "") + ProgramLevelGroup.Description;
                return result;
            }
        }

        public bool CanPrint
        {
            get { return !string.IsNullOrEmpty(Progress.SupervisorBadge); }
        }

        public bool CanSubmit
        {
            get
            {
                return ((SignAsSuperintendent || SignAsSupervisor) && !ShowProgressEvaluationSection)
                    || (ShowProgressEvaluationSection && (SignAsSuperintendent || SignAsSupervisor) && !Progress.SuperintendentApprovalDate.HasValue);
            }
        }

        // fix hierachy to: Category > Area > Cell > Scores (data model flattens graph modeling)
        private readonly Dictionary<RatingArea, RatingCategory> ratingAreaCategories = new Dictionary<RatingArea, RatingCategory>();
        public RatingCategory RatingAreaCategory(RatingArea ratingArea)
        {
            if (!ratingAreaCategories.ContainsKey(ratingArea))
                ratingAreaCategories.Add(ratingArea, (ratingArea.RatingCells.First().RatingCategory));
            return ratingAreaCategories[ratingArea];
        }

        private readonly Dictionary<RatingArea, List<RatingCell>> ratingAreaCells = new Dictionary<RatingArea, List<RatingCell>>();
        public List<RatingCell> RatingAreaCells(RatingArea ratingArea)
        {
            if (!ratingAreaCells.ContainsKey(ratingArea))
                ratingAreaCells.Add(ratingArea, (RatingCells.Where(rc => rc.RatingAreaId == ratingArea.RatingAreaId).OrderBy(rc => rc.SortOrderCell).ToList()));
            return ratingAreaCells[ratingArea];
        }

        public List<RatingCellScore> RatingCellScores(RatingCell ratingCell)
        {
            return ratingCell.RatingCellScores.OrderByDescending(rcs => rcs.Score).ToList();
        }

        private List<RatingArea> ratingAreas;
        public List<RatingArea> RatingAreas
        {
            get
            {
                if (RatingCells == null) return null;
                var ratingCells = RatingCells.ToList();
                var ra = ratingCells.Select(rc => rc.RatingArea).Distinct().ToList();
                return ratingAreas ?? (ratingAreas = ra.ToList());
            }
        }

        public bool IsLastRatingArea(RatingArea ratingArea)
        {
            return ratingAreas != null && ratingAreas.Last().RatingAreaId == ratingArea.RatingAreaId;
        }

        public RatingCellScore RatingAreaScore(RatingArea ratingArea)
        {
            var ratingAreaCells = RatingAreaCells(ratingArea);
            var ratingCellScores = Progress.ProgressRatingCellScores;

            var result =
                (from rac in ratingAreaCells
                 from racs in rac.RatingCellScores
                 from rcs in ratingCellScores
                 where racs.RatingCellScoreId == rcs.RatingCellScoreId
                 select racs).FirstOrDefault();
            return result;
        }

        public List<RatingCellScore> RatingCellScores()
        {
            var ratingCellScores = Progress.ProgressRatingCellScores;

            var result =
                (from rcs1 in ratingCellScores
                    from rc in RatingCells
                    from rcs2 in rc.RatingCellScores
                    where rcs1.RatingCellScoreId == rcs2.RatingCellScoreId
                    select rcs2);
            return result.ToList();
        }

        public bool IsRatingAreaScoreChecked(RatingArea ratingArea, RatingCellScore ratingCellScore)
        {
            var rcScore = RatingAreaScore(ratingArea);
            var result = rcScore != null && rcScore.RatingCellScoreId == ratingCellScore.RatingCellScoreId;
            return result;
        }

        public bool HasSupervisorApproved
        {
            get { return Progress.EvaluationDate != null; }
        }

        public bool CanSupervisorSign
        {
            get { return AreDailyEvaluationsDone
                && RatingAreas != null 
                && ProgressRatingCellScores != null
                && !HasSupervisorApproved; }
        }

        public bool HasApprenticeApproved
        {
            get { return Progress.EvaluationDate != null; }
        }

        public bool CanApprenticeSign
        {
            get { return HasSupervisorApproved && !HasApprenticeApproved; }
        }

        public bool HasSuperintendentApproved
        {
            get { return Progress.SuperintendentApprovalDate != null; }
        }

        public bool CanSuperintendentSign
        {
            get { return SignAsSuperintendent && !HasSuperintendentApproved; }
        }

        public string DaysOff
        {
            get
            {
                if (!string.IsNullOrEmpty(Progress.ScheduledDaysOff))
                    return Progress.ScheduledDaysOff;

                var days = ProgressDays.Where(p=>p.IsDayOff);
                string result = null;
                foreach (var day in days)
                {
                    if (result != null) result += "/";
                    result += day.CalendarDate.Date.ToString("ddd");
                }
                return result;
            }
        }

        public string LevelTitle
        {
            get
            {
                return string.Format("(Level {0} through {1})", ProgramLevelGroup?.StartLevel, ProgramLevelGroup?.EndLevel);
            }
        }

        public string Serialize
        {
            get
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented, serializationSettings());
            }
        }

        private JsonSerializerSettings serializationSettings()
        {
            var jsonResolver = new IgnorableSerializerContractResolver();
            jsonResolver.Ignore(typeof(ProgressDay), "DailyPerformance");
            jsonResolver.Ignore(typeof(ProgressDay), "ProgramLevel");
            jsonResolver.Ignore(typeof(ProgressDay), "Progress");
            jsonResolver.Ignore(typeof(ProgressDay), "Division");
            jsonResolver.Ignore(typeof(Progress), "ParticipantProgramLevelGroup");
            jsonResolver.Ignore(typeof(Progress), "ProgressDay");
            jsonResolver.Ignore(typeof(ProgressRatingCellScore), "RatingCellScore");
            //jsonResolver.Ignore(typeof(System.Data.Objects.DataClasses.EntityObject));  // ignore single datatype
            return new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore, 
                ContractResolver = jsonResolver
            };
        }
    }
}