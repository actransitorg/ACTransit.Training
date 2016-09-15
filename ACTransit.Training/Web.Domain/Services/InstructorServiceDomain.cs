using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;
using System.Web.Mvc;

namespace ACTransit.Training.Web.Domain.Services
{
    public class InstructorServiceDomain:BaseService
    {
        public InstructorsPageViewModel GetInstructorsPageViewModel(InstructorsPageViewModel model)
        {
            if (model == null)
                model = new InstructorsPageViewModel();
            model = (InstructorsPageViewModel)base.PrepareModel(model);

            var instructors = InstructorService.Get(m => !model.JustShowActive || m.IsActive == true, m => m.NonEmployee);
            if (model.JustShowNonEmployees)
                instructors = instructors.Where(m => m.NonEmployeeId.HasValue);
            var selectedCourseTypes = model.CourseTypes.Where(m => m.Selected).Select(m => m.Value).ToList();
            instructors = instructors.Where(m => selectedCourseTypes.Contains(m.CourseTypeId.ToString()));

            model.Instructors = Converter.ToViewModels(instructors.ToList());
            return model;
        }

        public InstructorPageViewModel GetInstructorPageViewMode(long? instructorId)
        {
            var model = new InstructorPageViewModel { State = instructorId.HasValue ? ViewModelState.Edit : ViewModelState.New };
            if (!instructorId.HasValue)
            {
                model.Instructor.IsActive = true; //set the default
                return PrepareModel(model, null);
            }


            var instructor = InstructorService.Get(m => m.InstructorId == instructorId, m => m.NonEmployee).FirstOrDefault();
            if (instructor == null)
                throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);

            var instructors = Converter.ToViewModels(new List<Instructor>(new[] { instructor }));
            if (instructors != null && instructors.Count == 1)
            {
                return PrepareModel(model, instructors[0]);
            }


            throw new Exception("Could not get InstructorViewModel.");
        }

        public InstructorPageViewModel SaveInstructorPageViewMode(InstructorPageViewModel model)
        {
            if (model == null)
                throw new Exception("Parameter model can't be null.");

            var instructor = new Instructor
            {
                Badge = Trim(model.Instructor.Badge),
                InstructorId = model.Instructor.InstructorId,
                NonEmployeeId = model.Instructor.NonEmployeeId,
                IsActive = model.Instructor.IsActive,
                CourseTypeId = model.Instructor.CourseTypeId
            };
            NonEmployee nonEmployee = null;
            var isNonEmployee = string.IsNullOrWhiteSpace(model.Instructor.Badge) && !string.IsNullOrWhiteSpace(model.Instructor.Instructor);
            if ((model.Instructor.NonEmployeeId.GetValueOrDefault(0) > 0 && isNonEmployee) || (isNonEmployee && model.Instructor.InstructorId == 0))
            {
                nonEmployee = new NonEmployee { NonEmployeeId = model.Instructor.NonEmployeeId.GetValueOrDefault(0), Name = model.Instructor.Instructor, IsActive = model.Instructor.IsActive.GetValueOrDefault(false) };
            }
            long instructorId;
            if (model.State == ViewModelState.New)
                instructorId = InstructorService.Add(instructor, nonEmployee);
            else
                instructorId = InstructorService.Update(instructor, nonEmployee);
            return GetInstructorPageViewMode(instructorId);
        }

        public void DeleteInstructor(long instructorId)
        {
            var courseSchedules = CourseScheduleService.GetCourseSchedulesOfInstructor(instructorId);
            if (courseSchedules != null && courseSchedules.Any())
                throw new FriendlyException(FriendlyExceptionType.InUseCanNotDelete);
            InstructorService.Delete(instructorId);
        }

        public bool ValidateModel(InstructorPageViewModel model, ModelStateDictionary modelState)
        {
            bool isValid = true;
           
            if (string.IsNullOrWhiteSpace(model.Instructor.Badge))
            {
                if (!model.Instructor.IsNonEmployee)
                {
                    modelState.AddModelError("Instructor.Badge", "Employee can't be empty.");
                    isValid = false;
                }
                else if (model.Instructor.NonEmployeeId.GetValueOrDefault(0) == 0)
                {
                    modelState.AddModelError("Instructor.NonEmployeeId", "External Instructor can't be empty.");
                    isValid = false;
                }
            }
            else
            {
                if (model.Instructor.IsNonEmployee)
                {
                    modelState.AddModelError("Instructor.NonEmployeeId", "External Instructor should not be checked.");
                    isValid = false;
                }
                else if (model.Instructor.NonEmployeeId.GetValueOrDefault(0) != 0)
                {
                    modelState.AddModelError("Instructor.NonEmployeeId", "External Instructor should be empty.");
                    isValid = false;
                }
            }
            if (isValid)
            {
                if (model.Instructor.IsNonEmployee)
                {
                    var instructor = InstructorService.GetInstructorFromNonEmployeeId(model.Instructor.NonEmployeeId.GetValueOrDefault(0));
                    if ((model.Instructor.InstructorId == 0 && instructor != null) ||
                        (model.Instructor.InstructorId != 0 && instructor != null &&
                         instructor.InstructorId != model.Instructor.InstructorId))
                    {
                        modelState.AddModelError("Instructor.NonEmployeeId", "External Instructor already selected.");
                        isValid = false;
                    }
                }
                else
                {
                    var instructor = InstructorService.GetInstructor(model.Instructor.Badge);
                    if ((model.Instructor.InstructorId == 0 && instructor != null) ||
                        (model.Instructor.InstructorId != 0 && instructor != null &&
                         instructor.InstructorId != model.Instructor.InstructorId))
                    {
                        modelState.AddModelError("Instructor.Badge", "Badge already selected.");
                        isValid = false;
                    }

                }

            }
            if (model.Instructor.CourseTypeId==null || model.Instructor.CourseTypeId == 0)
            {
                modelState.AddModelError("Instructor.CourseTypeId", "Course Type must be selected.");
                isValid = false;
            }
            
            PrepareModel(model, null);
            return isValid;
        }

        private InstructorPageViewModel PrepareModel(InstructorPageViewModel model, InstructorViewModel instructor)
        {
            if (model == null)
                model = new InstructorPageViewModel();
            model = (InstructorPageViewModel)base.PrepareModel(model);
            if (model.CourseTypes.Count(m => m.Selected) > 1)  //nothing should be selected by default.
                model.CourseTypes.ForEach(m => m.Selected = false);
                            
            bool fetchSeperate = false;
            var nonEmployees = NonEmployeeService.Get(m => m.IsActive).OrderBy(m => m.Name).Select(m => new { m.Name, m.NonEmployeeId }).ToList();
            if (instructor != null)
            {
                if (instructor.NonEmployeeId.HasValue && !nonEmployees.Any(m => m.NonEmployeeId == instructor.NonEmployeeId.Value))
                    fetchSeperate = true;
            }

            if (fetchSeperate)
            {
                var nonEmployee = NonEmployeeService.GetNonEmployee(instructor.NonEmployeeId.Value);
                if (nonEmployee != null)
                    nonEmployees.Add(new { nonEmployee.Name, nonEmployee.NonEmployeeId });
            }

            model.NonEmployees = nonEmployees.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.NonEmployeeId.ToString(CultureInfo.InvariantCulture),
            }).ToList();

            if (instructor != null)
            {
                AddIfNotExistCourseType(instructor.CourseTypeId.GetValueOrDefault(0), model.CourseTypes);
                model.Instructor = instructor;
            }
            return model;
        }
        
    }
}
