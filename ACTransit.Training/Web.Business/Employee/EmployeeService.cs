using System;
using System.Linq;
using ACTransit.DataAccess.Employee.Repositories;
using ACTransit.Training.Web.Business.Infrastructure;
using EM = ACTransit.Entities.Employee;

namespace ACTransit.Training.Web.Business.Employee
{
    public class EmployeeService : BaseService
    {
        private const string ClassName = "ACTransit.Training.Web.Business.Employee.EmployeeService";

        private readonly EmployeeRepository _repository;

        public EmployeeService()
        {
            _repository = new EmployeeRepository();
        }

        public IQueryable<EM.Employee> GetEmployees(string[] lanIds)
        {
            return _repository.GetEmployees(lanIds);
        }

        public IQueryable<EM.Employee> GetEmployeesByBadges(string[] badges)
        {
            return _repository.GetEmployeesByBadges(badges);
        }

        public IQueryable<EM.Employee> GetEmployees(string badge = null,
                    string name = null, string firstName = null, string lastName = null, string middleName = null, DateTime? birthDate = null,
                    string address = null,
                    string homePhone = null, string workPhone = null, string cellPhone = null, string email = null,
                    string deptName = null, string jobTitle = null, string ntLogin = null)
        {
            using (var repository = new EmployeeRepository())
            {
                return repository.GetEmployees(badge,
                                        name,
                                        firstName,
                                        lastName,
                                        middleName,
                                        birthDate,
                                        address,
                                        homePhone,
                                        workPhone,
                                        cellPhone,
                                        email,
                                        deptName,
                                        jobTitle,
                                        ntLogin
                                        );
            }
        }


        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

        protected override void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                if (_repository != null)
                    _repository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
