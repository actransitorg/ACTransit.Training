using System.Collections.Generic;
using System.Linq;
using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Domain.Services
{
    public class MenuServiceDomain : BaseService
    {

        public List<Menu> GetResources()
        {
            var res=MenuService.Get(m => m.ParentMenuId == 1).OrderBy(m=>m.SortOrder).ToList();
            return res;
        }
    }
}
