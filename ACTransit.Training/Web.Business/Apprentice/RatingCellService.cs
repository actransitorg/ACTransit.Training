using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class RatingCellService : TrainingServiceBase<RatingCell>
    {
        public RatingCellService(string currentUserName) : base(currentUserName) { }

        public RatingCell GetRatingCell(int id, params Expression<Func<RatingCell, object>>[] paths)
        {
            return Get(ra => ra.RatingCellId == id, paths).FirstOrDefault();
        }

        public IQueryable<RatingCell> GetRatingCells(int programLevelGroupId, params Expression<Func<RatingCell, object>>[] paths)
        {
            var cells = UnitOfWork.Get<RatingCell>().Where(rc => rc.ProgramLevelGroupId == programLevelGroupId);
            if (cells == null)
                throw new Exception(string.Format("Invalid parameter programLevelGroupId: {0}", programLevelGroupId));
            return
                (from rc in cells
                 where rc.ProgramLevelGroupId == programLevelGroupId
                 orderby rc.SortOrderCategory, rc.SortOrderArea, rc.SortOrderCell
                 select rc).Distinct();
        }

        public int Add(RatingCell entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(RatingCell entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

    }
}
