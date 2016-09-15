using System.Collections.Generic;
using System.Linq;
using ACTransit.Entities.Employee;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Models;

namespace ACTransit.Training.Web.Domain.Apprentice.Models
{
    public class ProgramLevelGroupViewModel : ViewModelBase
    {
        public ProgramLevelGroupViewModel() { }

        public static ProgramLevelGroupViewModel Create(int? id = null)
        {
            return new ProgramLevelGroupViewModel
            {
                ProgramLevelGroup = new ProgramLevelGroup
                {
                    ProgramLevelGroupId = id.GetValueOrDefault(),
                },
            };
        }

        public ProgramLevelGroup ProgramLevelGroup { get; set; }

        public List<Participant> Items { get; set; }

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

        public static string GetName(ProgramLevelGroup programLevelGroup)
        {
            if (programLevelGroup == null)
                return null;
            var result = programLevelGroup.Program != null ? programLevelGroup.Program.Name : "";
            if (programLevelGroup.StartLevel > 0 && programLevelGroup.EndLevel > 0)
                result += (!string.IsNullOrEmpty(result) ? ": " : "") + programLevelGroup.Description;
            return result;
        }

    }
}