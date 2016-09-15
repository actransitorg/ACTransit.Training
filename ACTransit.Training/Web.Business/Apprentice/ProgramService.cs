using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ProgramService : TrainingServiceBase<Program>
    {
        public ProgramService(string currentUserName) : base(currentUserName) { }

        public Program GetProgram(int id, params Expression<Func<Program, object>>[] paths)
        {
            return Get(p => p.ProgramId == id, paths).FirstOrDefault();
        }

        public Program GetProgram(string nameOrType, params Expression<Func<Program, object>>[] paths)
        {
            return Get(p => p.Name == nameOrType || p.ProgramType == nameOrType, paths).FirstOrDefault();
        }

        public IQueryable<Program> GetPrograms(bool? isActive, params Expression<Func<Program, object>>[] paths)
        {
            var checkActive = isActive.HasValue;
            var active = isActive.GetValueOrDefault();
            return Get(p => p.Name != null && (!checkActive || (checkActive && p.IsActive == active)), paths);
        }

        public int Add(Program entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(Program entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

    }
}
