using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;
using ACTransit.Entities.Employee;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Extensions;
using Newtonsoft.Json;
using System.Web;

namespace ACTransit.Training.Web.Domain.Apprentice.Models
{
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ProgressDayViewModel : ViewModelBase
    {
        public ProgressDayViewModel() { }

        public ProgressDayViewModel(ProgressDay model = null, ProgressViewModel parent = null)
        {
            ProgressDay = model;
            if (parent != null)
            {
                Progress = parent.Progress;
                ParticipantProgramLevelGroup = parent.ParticipantProgramLevelGroup;
                Participant = parent.Participant;
                ProgramLevelGroup = parent.ProgramLevelGroup;
                Program = parent.Program;
                if (parent.ParticipantWork != null)
                    ParticipantWork = parent.ParticipantWork.Where(pw => pw.WorkDate.Date == model.CalendarDate.Date).ToList();
                DailyPerformanceProgramLevelGroups = parent.DailyPerformanceProgramLevelGroups;
                ParticipantStatus = parent.ParticipantStatus;
                Employees = parent.Employees;
                WorkCategory = parent.WorkCategory;
                AclUserName = parent.AclUserName;
                CurrentUser = parent.CurrentUser;
                if (model != null)
                {
                    var firstProgressDay = parent.ProgressDays.FirstOrDefault();
                    if (firstProgressDay != null)
                        IsFirstProgressDay = firstProgressDay.ProgressDayId == ProgressDay.ProgressDayId;
                    var lastProgressDay = parent.ProgressDays.LastOrDefault();
                    if (lastProgressDay != null)
                        IsLastProgressDay = lastProgressDay.ProgressDayId == ProgressDay.ProgressDayId;
                }
            }
            else
            {
                IsFirstProgressDay = true;
            }

            if (model != null)
            {
                DailyPerformance = model.DailyPerformance;
                ProgramLevel = model.ProgramLevel;
                Division = model.Division;
            }
            AllowSubmit = true; 
        }

        public static ProgressDayViewModel Create(int? id = null)
        {
            return new ProgressDayViewModel
            {
                ProgressDay = new ProgressDay
                {
                    ProgressDayId = id.GetValueOrDefault()
                }
            };
        }

        public static ProgressDayViewModel Create(int? id = null, ProgressViewModel parent = null)
        {
            return new ProgressDayViewModel(new ProgressDay
            {
                ProgressDayId = id.GetValueOrDefault()
            }, parent);
        }

        [DataMember, JsonProperty]
        public ProgressDay ProgressDay { get; set; }

        [JsonIgnore]
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

        [JsonIgnore]
        public DailyPerformance DailyPerformance { get; set; }

        [JsonIgnore]
        public Division Division { get; set; }

        [JsonIgnore]
        public ProgramLevel ProgramLevel { get; set; }

        [JsonIgnore]
        public bool AllowSubmit { get; set; }

        [JsonIgnore]
        public bool AllowChangeHours { get; set; }

        [JsonIgnore]
        public bool IsFirstProgressDay { get; set; }

        [JsonIgnore]
        public bool IsLastProgressDay { get; set; }

        [JsonIgnore]
        public bool SignAsSupervisor { get; set; }

        [JsonIgnore]
        public bool SignAsSuperintendent { get; set; }

        [JsonIgnore]
        public bool CanChangeDailyPerformance 
        {
            get
            {
                return Progress == null ? false : !Progress.SuperintendentApprovalDate.HasValue 
                    && !ProgressDay.IsDayOff 
                    && !Progress.EvaluationDate.HasValue
                    && (SignAsSuperintendent 
                        || (!IsDailyEvaluationDone && SignAsSupervisor) 
                        || (IsDailyEvaluationDone && CurrentUser != null && ProgressDay.CommentBadge == CurrentUser.Badge));
            } 
        }

        [JsonIgnore]
        public bool CanChangeDayOff
        {
            get
            {
                return Progress == null ? false : !Progress.SuperintendentApprovalDate.HasValue
                    && !Progress.EvaluationDate.HasValue
                    && (SignAsSuperintendent
                        || (!IsDailyEvaluationDone && SignAsSupervisor)
                        || (IsDailyEvaluationDone && CurrentUser != null && ProgressDay.CommentBadge == CurrentUser.Badge));
            }
        }

        public bool CanSubmit
        {
            get { return (SignAsSuperintendent || SignAsSupervisor) && ParticipantWork != null && ParticipantWork.Any(); }
        }

        public SelectList SelectedHours(int hours)
        {
            return new SelectList(Enumerable.Range(0, 12), hours);
        }

        public bool IsDailyPerformanceChecked(DailyPerformance dailyPerformance)
        {
            var result = dailyPerformance.DailyPerformanceId == ProgressDay.DailyPerformanceId;
            return result;
        }
        
        public bool IsDailyEvaluationDone
        {
            get
            {
                return ProgressDay.IsDayOff || (ProgressDay.DailyPerformanceId.GetValueOrDefault() > 0 && !string.IsNullOrEmpty(ProgressDay.Comment));
            }
        }

        public object AsRadioButtonDisable
        {
            get { return IsDailyEvaluationDone ? new { disabled = "disabled" } : null; }
        }

        public object AsTextAreaDisable
        {
            get { return IsDailyEvaluationDone ? new { disabled = "disabled" } : null; }
        }

        public List<WorkCategory> WorkCategory { get; set; }

        public ParticipantWork ParticipantWorkItem(int index)
        {
            var result = ParticipantWork.FirstOrDefault(pw => pw.WorkCategoryId == WorkCategory[index].WorkCategoryId);
            return result ?? new ParticipantWork { WorkCategory = new WorkCategory() };
        }

        public string ParticipantWorkItemHours(int index)
        {
            var result = ParticipantWork != null ? ParticipantWork.Where(pw => pw.WorkCategoryId == WorkCategory[index].WorkCategoryId).Sum(pw => pw.WorkHour) : 0;
            return MathUtil.RoundShort(result);
        }

        public string ParticipantWorkItemEmphasis(int index)
        {
            var result = ParticipantWork != null ? ParticipantWork.Where(pw => pw.WorkCategoryId == WorkCategory[index].WorkCategoryId).Sum(pw => pw.WorkHour) : 0;
            return result > 0 ? "emphasis" : "";
        }

        public string WorkDetails
        {
            get
            {
                if (ProgressDay.IsDayOff)
                    return "Day Off";

                var result = "";
                var vehicleIds = (from pw in ParticipantWork
                                  where !string.IsNullOrEmpty(pw.VehicleId)
                                  select pw.VehicleId).Distinct();
                var otherWork = (from pw in ParticipantWork
                                 where string.IsNullOrEmpty(pw.VehicleId) && !string.IsNullOrWhiteSpace(pw.WorkOrderNum)
                                 select pw.WorkOrderNum).Distinct();

                foreach (var vehicleId in vehicleIds)
                {
                    if (result != "") result += ", ";
                    result += "<a class=\"work-task\" data-title=\"Vehicle " + vehicleId + "\" data-work=\"" + VehicleWork(vehicleId) + "\">Vehicle " + vehicleId + "</a>";
                }

                foreach (var workOrderNum in otherWork)
                {
                    if (result != "") result += ", ";
                    result += "<a class=\"work-task\" data-title=\"" + workOrderNum + "\" data-work=\"" + WorkOrder(workOrderNum) + "\">" + workOrderNum + "</a>";
                }

                return result;
            }
        }

        public string WorkDescription(Func<IEnumerable<ParticipantWork>> workItems)
        {
            var result = "";
            foreach (var item in workItems())
            {
                var category = WorkCategory.First(wc => wc.WorkCategoryId == item.WorkCategoryId).Description;
                result += string.Format("<p>Work Order {0}, Job Code: {1}, Comp Code: {2}, Comp Description: {3}, Hours: {4}<br/>Task {5}: {6}</p>",
                    item.WorkOrderNum, category, item.CompCode, item.CompDescription, MathUtil.RoundShort(item.WorkHour), item.TaskNum, item.TaskComment);
            }
            return HttpContext.Current.Server.HtmlEncode(result);
        }

        public string VehicleWork(string vehicleId)
        {
            return WorkDescription(() => ParticipantWork.Where(pw => pw.VehicleId == vehicleId));
        }

        public string WorkOrder(string workOrderNum)
        {
            return WorkDescription(() => ParticipantWork.Where(pw => pw.WorkOrderNum == workOrderNum));
        }

        public string TotalHours
        {
            get
            {
                var result = ParticipantWork != null ? ParticipantWork.Sum(hours => hours.WorkHour) : 0;
                return MathUtil.RoundShort(result);
            }
        }

        public string TotalHoursEmphasis
        {
            get
            {
                return (Convert.ToDecimal(TotalHours) > 0 ) ? "emphasis" : "";
            }
        }

        [JsonIgnore]
        public List<EmployeeAll> Employees { get; set; }

        public EmployeeAll Employee(string badge)
        {
            return badge != null && Employees  != null ? Employees.FirstOrDefault(s => s.Badge == badge) : new EmployeeAll();
        }

        [JsonIgnore]
        public string AclUserName { get; set; }

        [JsonIgnore]
        public EmployeeAll CurrentUser { get; set; }

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

        public string SupervisorName
        {
            get { return ProgressDay != null ? EmployeeName(ProgressDay.SupervisorBadge) : null; }
        }

        public string CommentsName
        {
            get { return ProgressDay != null ? EmployeeName(ProgressDay.CommentBadge) : null; }
        }


        public string CommentSignature
        {
            get { return ProgressDay.IsDayOff 
                    ? "" 
                    : (HasSupervisorSigned && HasSigned 
                        ? SupervisorName + "/" + CommentsName 
                        : (HasSupervisorSigned ? SupervisorName : "Not Yet Rated")); }
        }

        public string CommentHeight
        {
            get { return !IsFirstProgressDay || (IsFirstProgressDay && !string.IsNullOrEmpty(ProgressDay.Comment)) ? "42px" : ""; }
        }

        public bool HasSupervisorSigned
        {
            get 
            { 
                return ProgressDay.SupervisorBadge != null
                    && Employee(ProgressDay.SupervisorBadge) != null
                    && Employee(ProgressDay.SupervisorBadge).Badge != null
                    && ProgressDay.DailyPerformanceId.GetValueOrDefault() > 0;
            }
        }

        public bool HasSigned
        {
            get
            {
                return ProgressDay.CommentBadge != null
                    && Employee(ProgressDay.CommentBadge) != null
                    && Employee(ProgressDay.CommentBadge).Badge != null;
            }
        }

        public string Serialize
        {
            get
            {
                var result = JsonConvert.SerializeObject(this, Formatting.Indented, serializationSettings());
                return result;
            }
        }

        private JsonSerializerSettings serializationSettings()
        {
            var jsonResolver = new IgnorableSerializerContractResolver();
            jsonResolver.Ignore(typeof(ProgressDay), "DailyPerformance");
            jsonResolver.Ignore(typeof(ProgressDay), "ProgramLevel");
            jsonResolver.Ignore(typeof(ProgressDay), "Progress");
            jsonResolver.Ignore(typeof(ProgressDay), "Division");
            return new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = jsonResolver
            };
        }
    }
}