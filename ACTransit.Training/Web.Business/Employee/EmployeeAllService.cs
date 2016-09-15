using System.Linq;
using ACTransit.DataAccess.Employee.Repositories;
using ACTransit.Training.Web.Business.Infrastructure;
using EM = ACTransit.Entities.Employee;


namespace ACTransit.Training.Web.Business.Employee
{
    public class EmployeeAllService:BaseService
    {
        private const string ClassName = "ACTransit.Training.Web.Business.Employee.EmployeeService";

        private readonly EmployeeAllRepository _repository;

        public EmployeeAllService()
        {
            _repository = new EmployeeAllRepository();
        }

        public IQueryable<EM.EmployeeAll> GetEmployeesByBadges(string[] badges)
        {
            return _repository.GetEmployeesByBadges(badges);
        }


        public IQueryable<EM.EmployeeAll> GetEmployees(
            string badge = null, 
            string name = null, 
            string firstName = null, 
            string lastName = null, 
            string location = null, 
            string division = null,
            string deptName = null, 
            string ntLogin=null,
            bool? inEmployeeTable = null,
            string jobTitle=null)
        {
                return _repository.GetEmployees(badge,
                                        name,
                                        firstName,
                                        lastName,
                                        location,
                                        division,
                                        deptName,
                                        ntLogin,
                                        inEmployeeTable,
                                        jobTitle);
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

