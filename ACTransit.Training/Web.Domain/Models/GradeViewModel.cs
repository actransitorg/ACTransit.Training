using System.ComponentModel.DataAnnotations;

namespace ACTransit.Training.Web.Domain.Models
{
    public class GradeViewModel
    {
        [MaxLength(2)]
        public string LetterGrade { get; set; }
        [MaxLength(25)]
        public string Description { get; set; }
        public bool IsPassing { get; set; }
    }
}
