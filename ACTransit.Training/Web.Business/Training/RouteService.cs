using System.Linq;
using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Business.Training
{
    public class RouteService : TrainingServiceBase<RouteList>
    {
        public RouteService(string currentUserName) : base(currentUserName) { }

        public RouteList GetRoute(string routeAlpha)
        {
            return Get(m => m.RouteAlpha == routeAlpha).FirstOrDefault();
        }

        public override void RefreshCache()
        {

        }
    }
}
