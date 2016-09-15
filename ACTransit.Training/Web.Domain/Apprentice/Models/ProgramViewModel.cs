using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ACTransit.Entities.Employee;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Models;
using Newtonsoft.Json;

namespace ACTransit.Training.Web.Domain.Apprentice.Models
{
    [DataContract]
    public class ProgramViewModel : ViewModelBase
    {
        public ProgramViewModel() { }

        public static ProgramViewModel Create(int? id = null, bool OnlyActive = true, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            return new ProgramViewModel
            {
                Program = new Program
                {
                    ProgramId = id.GetValueOrDefault(),
                },
                ItemLevel = new Dictionary<Participant, ProgramLevel>(),
                OnlyActive = OnlyActive,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }

        [DataMember]
        public Program Program { get; set; }

        [DataMember]
        public List<Participant> Items { get; set; }

        [JsonIgnore]
        public bool OnlyActive { get; set; }

        [JsonIgnore]
        public DateTime? StartDate { get; set; }

        [JsonIgnore]
        public DateTime? EndDate { get; set; }

        public Dictionary<Participant, ProgramLevel> ItemLevel { get; set; }

        public List<ProgramLevelGroup> ProgramLevelGroups { get; set; }

        public List<EmployeeAll> Employees { get; set; }

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

        public string EmployeeLastName(string badge)
        {
            var employee = Employee(badge);
            if (employee == null) return null;
            return employee.LastName + ", " + employee.FirstName;
        }

        public string ParticipantTitle(Participant item)
        {
            var level = ItemLevel.ContainsKey(item) && ItemLevel[item] != null ? ItemLevel[item].Level : 0;
            if (level == 0)
                return string.Format("{0} - {1} (Level:{2})",
                    EmployeeLastName(item.Badge),
                    item.Badge,
                    "NO LEVEL SET");
            if (OnlyActive)
                return string.Format("{0} - {1} (Level:{2})",
                    EmployeeLastName(item.Badge),
                    item.Badge,
                    level);
            return string.Format("{0} - {1} (Level:{2}) <span class=\"{3}\">({4})</span>",
                EmployeeLastName(item.Badge),
                item.Badge,
                level,
                item.ParticipantStatus.Name == "Active" ? "" : "red",
                item.ParticipantStatus.Name == "Graduated" 
                    ? string.Format("Graduated on {0:MM-dd-yyyy}", 
                        (item.CompletedDate.HasValue 
                            ? item.CompletedDate.Value 
                            : DateTime.Now)) 
                    : item.ParticipantStatus.Name);
        }

        public Dictionary<int, string> ParticipantsActionItems { get; set; }

        public string ParticipantActionItems(Participant item)
        {
            if (ParticipantsActionItems == null)
                return null;
            var actionItems = ParticipantsActionItems.ContainsKey(item.ParticipantId) ? ParticipantsActionItems[item.ParticipantId] : "&nbsp;";
            return string.Format("{0}", actionItems);
        }
    }
}
