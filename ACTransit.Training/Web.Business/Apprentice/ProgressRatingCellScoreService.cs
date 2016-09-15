using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ProgressRatingCellScoreService : TrainingServiceBase<ProgressRatingCellScore>
    {
        public ProgressRatingCellScoreService(string currentUserName) : base(currentUserName) { }

        public ProgressRatingCellScore GetProgressRatingCellScore(int progressId, int ratingCellScoreId, params Expression<Func<ProgressRatingCellScore, object>>[] paths)
        {
            return Get(p => p.ProgressId == progressId && p.RatingCellScoreId == ratingCellScoreId, paths).FirstOrDefault();
        }

        public IQueryable<ProgressRatingCellScore> GetProgressRatingCellScores(int progressId, params Expression<Func<ProgressRatingCellScore, object>>[] paths)
        {
            return Get(p => p.ProgressId == progressId, paths);
        }

        public ProgressRatingCellScore Add(ProgressRatingCellScore entity)
        {
            ProgressRatingCellScore result;
            var resultInternal = (EntityKeyMember[])AddInternal(entity);
            result = new ProgressRatingCellScore
            {
                ProgressId = (int) resultInternal[0].Value,
                RatingCellScoreId = (int) resultInternal[1].Value
            };
            return result;
        }

        public ProgressRatingCellScore Update(ProgressRatingCellScore entity)
        {
            ProgressRatingCellScore result;
            var resultInternal = (EntityKeyMember[])UpdateInternal(entity);
            result = new ProgressRatingCellScore
            {
                ProgressId = (int)resultInternal[0].Value,
                RatingCellScoreId = (int)resultInternal[1].Value
            };
            return result;
        }

        public void Delete(Progress progress)
        {
            var items = GetProgressRatingCellScores(progress.ProgressId).ToList();
            using (var transaction = new TransactionScope())
            {
                foreach (var item in items)
                    UnitOfWork.Delete(item);
                UnitOfWork.SaveChanges();
                transaction.Complete();
            }
        }

        public override void RefreshCache()
        {
            Common.Cache.ClearAll();
        }

    }
}
