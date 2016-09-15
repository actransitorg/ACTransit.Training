using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class RatingCellScoreService : TrainingServiceBase<RatingCellScore>
    {
        public RatingCellScoreService(string currentUserName) : base(currentUserName) { }

        public RatingCellScore GetRatingCellScore(int id, params Expression<Func<RatingCellScore, object>>[] paths)
        {
            return Get(ra => ra.RatingCellScoreId == id, paths).FirstOrDefault();
        }

        public IQueryable<RatingCellScore> GetRatingCellScores(int ratingCellId, params Expression<Func<RatingCellScore, object>>[] paths)
        {
            return Get(rc => rc.RatingCellId == ratingCellId, paths);
        }

        public int Add(RatingCellScore entity)
        {
            return (int)AddInternal(entity);
        }

        public int Update(RatingCellScore entity)
        {
            return (int)UpdateInternal(entity);
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

    }
}
