using System.Linq;
using ACTransit.Entities.Training;

namespace ACTransit.Training.Web.Business.Training
{
    public class MenuService: TrainingServiceBase<Menu>
    {
        public MenuService(string currentUserName) : base(currentUserName) { }

        public Menu GetMenu(long menuId)
        {
            return Get(m => m.MenuId == menuId).FirstOrDefault();
        }
        public IQueryable<Menu> GetMenus()
        {
            return Get(null);
        }
        public IQueryable<Menu> GetMenus(long? parentMenuId)
        {
            return Get(m => m.ParentMenuId == parentMenuId);
        }

        public override void RefreshCache()
        {
            
        }
    }
}
