using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Mvc;
using ACTransit.Entities.Employee;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Extensions;
using System;

namespace ACTransit.Training.Web.Domain.Apprentice.Models
{
    [DataContract]
    public class ParticipantViewModel : ViewModelBase
    {
        public ParticipantViewModel() { }

        public static ParticipantViewModel Create(int? id = null)
        {
            return new ParticipantViewModel
            {
                Participant = new Participant
                {
                    ParticipantId = id.GetValueOrDefault(),
                },
                ParticipantStatuses = new List<SelectListItem>()
            };
        }

        [DataMember]
        public Participant Participant { get; set; }

        [DataMember]
        public ProgramLevelGroup ProgramLevelGroup { get; set; }

        [DataMember]
        public List<ParticipantProgramLevelGroup> ParticipantProgramLevelGroups { get; set; }

        [DataMember]
        public ParticipantProgramLevelGroup ParticipantProgramLevelGroup { get; set; }

        [DataMember]
        public EmployeeAll EmployeeDetails { get; set; }

        [DataMember]
        public List<Progress> Items { get; set; }

        public Program Program { get; set; }

        public List<WorkCategory> WorkCategory { get; set; }

        public List<WorkCategoryHour> WorkCategoryCompleted { get; set; }

        public List<WorkCategoryHour> WorkCategoryRemaining { get; set; }

        public string WorkCategoryCompletedHours(int index)
        {
            var result = WorkCategoryCompleted != null ? WorkCategoryCompleted[index].Hours : 0;
            return MathUtil.RoundShort(result);
        }

        public string WorkCategoryRemainingHours(int index)
        {
            var result = WorkCategoryRemaining != null ? WorkCategoryRemaining[index].Hours : 0;
            return MathUtil.RoundShort(result);
        }

        public IEnumerable<SelectListItem> ParticipantStatuses { get; set; }

        [DataMember]
        public ProgramLevel ProgramLevel { get; set; }

        [DataMember]
        public ProgramLevel NewProgramLevel { get; set; }

        public IEnumerable<SelectListItem> ProgramLevels { get; set; }

        public bool AllowChangeApprenticeLevel { get; set; }

    }
}