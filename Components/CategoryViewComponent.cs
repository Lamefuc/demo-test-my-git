using Microsoft.AspNetCore.Mvc;
using Reader.Models;

namespace Reader.Components
{
    [ViewComponent(Name = "CategoryView")]
    public class CategoryViewComponent : ViewComponent
    {
        private DataContext _DbReaderContext;

        public CategoryViewComponent(DataContext DbReaderContext)
        {
            _DbReaderContext = DbReaderContext;
        }
        public IViewComponentResult Invoke()
        {
            var listOfCat = (from Cat in _DbReaderContext.TblCategories
                              where (Cat.IsActive == true)
                              select Cat).Take(6).ToList();
            return View("Default", listOfCat);
        }
    }
}
