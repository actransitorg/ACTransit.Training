using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class RatingAreaService : TrainingServiceBase<RatingArea>
    {
        public RatingAreaService(string currentUserName) : base(currentUserName) { }

        public RatingArea GetRatingArea(int id, params Expression<Func<RatingArea, object>>[] paths)
        {
            return Get(ra => ra.RatingAreaId == id, paths).FirstOrDefault();
        }

        public RatingArea GetRatingArea(string name, params Expression<Func<RatingArea, object>>[] paths)
        {
            return Get(ra => ra.Name == name, paths).FirstOrDefault();
        }

        public IQueryable<RatingArea> GetRatingAreas(int programLevelGroupId, params Expression<Func<RatingArea, object>>[] paths)
        {
            var cells = UnitOfWork.Get<RatingCell>().Where(rc => rc.ProgramLevelGroupId == programLevelGroupId);
            if (cells == null)
                throw new Exception(string.Format("Invalid parameter programLevelGroupId: {0}", programLevelGroupId));
            return
                (from rc in cells
                 where rc.ProgramLevelGroupId == programLevelGroupId
                 orderby rc.SortOrderArea
                 select rc.RatingArea).Distinct();
        }

        public int Add(RatingArea entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(RatingArea entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

    }
}
