using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Models;

namespace ACTransit.Training.Web.Domain.Apprentice.Models
{
    [DataContract]
    public class ProgramsViewModel : ViewModelBase
    {
        public ProgramsViewModel()
        {
            OnlyActive = true;
        }

        public static ProgramsViewModel Create(bool OnlyActive = true, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            return new ProgramsViewModel
            {
                OnlyActive = OnlyActive,
                StartDate = StartDate ?? DateTime.Now.Date.AddMonths(-1),
                EndDate = EndDate ?? DateTime.Now.Date
            };
        }

        [DataMember]
        public List<Program> Items { get; set; }

        [DataMember]
        public bool OnlyActive { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }
    }
}
