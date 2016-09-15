using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Training;
using System.Collections.Generic;

namespace ACTransit.Training.Web.Business.Apprentice
{
    public class ActionItemsService: TrainingServiceBase<GetActionItems_Result>
    {
        public ActionItemsService(string currentUserName) : base(currentUserName) { }

        public List<GetActionItems_Result> GetActionItems(string Badge)
        {
            return UnitOfWork.GetActionItems(Badge);
        }

        public override void RefreshCache()
        {
        }
    }
}
