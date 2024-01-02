using Microsoft.AspNetCore.Mvc;
using Reader.Models;

namespace Reader.Components
{
    [ViewComponent(Name = "HeaderView")]
    public class HeaderViewComponent : ViewComponent
    {
        private DataContext _DbReaderContext;

        public HeaderViewComponent(DataContext DbReaderContext)
        {
            _DbReaderContext = DbReaderContext;
        }

        public IViewComponentResult Invoke()
        {
            var listOfMenu = (from menu in _DbReaderContext.TblMenus
                              where (menu.IsActive == true) && menu.Position == 1
                              select menu).ToList();
            return View("Default", listOfMenu);
        }
    }
}
