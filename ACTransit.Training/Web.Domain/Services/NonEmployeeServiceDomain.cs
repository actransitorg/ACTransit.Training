using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;


namespace ACTransit.Training.Web.Domain.Services
{
    public class NonEmployeeServiceDomain:BaseService
    {
        public NonEmployeeViewModel GetNonEmployeeViewModel(long? nonEmployeeId)
        {
            if (!nonEmployeeId.HasValue)
                return new NonEmployeeViewModel { State = ViewModelState.New, IsActive = true };

            var nonEmployee = NonEmployeeService.GetNonEmployee(nonEmployeeId.Value);
            if (nonEmployee == null)
                throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);

            return new NonEmployeeViewModel
            {
                NonEmployeeId = nonEmployee.NonEmployeeId,
                Name = Trim(nonEmployee.Name),
                IsActive = nonEmployee.IsActive,
                State = ViewModelState.Edit
            };
        }

        public NonEmployeesPageViewModel GetNonEmployeesViewModel(NonEmployeesPageViewModel model)
        {
            if (model == null)
                model = new NonEmployeesPageViewModel();
            var nonEmployees = NonEmployeeService.Get(m => !model.JustShowActive || m.IsActive).OrderBy(m => m.Name).ToList();

            if (nonEmployees.Any())
            {
                model.NonEmployees = nonEmployees.Select(m => new NonEmployeeViewModel
                {
                    NonEmployeeId = m.NonEmployeeId,
                    Name = Trim(m.Name),
                    IsActive = m.IsActive
                }).ToList();
            }
            return model;
        }

        public List<NonEmployeeViewModel> GetNonEmployees()
        {
            return NonEmployeeService.Get(m => m.IsActive).OrderBy(m => m.Name).ToList().Select(Converter.ToViewModel).ToList();
        }

        public NonEmployeeViewModel SaveNonEmployeeViewModel(NonEmployeeViewModel model)
        {
            if (model == null)
                throw new Exception("Parameter model can't be null.");

            var nonEmployee = new NonEmployee
            {
                IsActive = model.IsActive,
                Name = Trim(model.Name),
                NonEmployeeId = model.NonEmployeeId,
            };
            long nonEmployeeId;
            if (model.State == ViewModelState.New)
                nonEmployeeId = NonEmployeeService.Add(nonEmployee);
            else
                nonEmployeeId = NonEmployeeService.Update(nonEmployee);
            return GetNonEmployeeViewModel(nonEmployeeId);
        }

        public void DeleteNonEmployee(long nonEmployeeId)
        {
            var couseEnrollments = CourseEnrollmentService.GetCourseEnrollmentsOfTrainee(nonEmployeeId);
            if (couseEnrollments.Any())
                throw new FriendlyException(FriendlyExceptionType.InUseCanNotDelete);
            var instructor = InstructorService.GetInstructorFromNonEmployeeId(nonEmployeeId);
            if (instructor != null)
                throw new FriendlyException(FriendlyExceptionType.InUseCanNotDelete);

            NonEmployeeService.Delete(nonEmployeeId);

        }

        public bool ValidateModel(NonEmployeeViewModel model, ModelStateDictionary modelState)
        {
            bool isValid = true;
            if (model == null)
                throw new Exception("Parameter model can't be null.");
            model.Name = Trim(model.Name);
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                modelState.AddModelError("Name", "Name can't be empty.");
                isValid = false;
            }
            else
            {
                var nonEmployees = NonEmployeeService.Get(m => m.Name == model.Name).ToList();
                if (nonEmployees.Any())
                {
                    //todo needs to check the state of the viewmodel
                    if (model.NonEmployeeId == 0)
                    {
                        modelState.AddModelError("Name", "Name already exist.");
                        isValid = false;
                    }
                    else if (model.NonEmployeeId != 0 && nonEmployees[0].NonEmployeeId != model.NonEmployeeId)
                    {
                        modelState.AddModelError("Name", "Name already exist.");
                        isValid = false;
                    }
                }
            }
            return isValid;
        }
    }
}
