using Reader.Models;
using Microsoft.AspNetCore.Mvc;

namespace Reader.Areas.Admin.Components
{
    [ViewComponent(Name ="AdminMenuView")]
    public class AdminMenuViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public AdminMenuViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList = (from mn in _context.TblAdminMenus
                          where (mn.IsActive == true)
                          select mn).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
