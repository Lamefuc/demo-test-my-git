using Microsoft.AspNetCore.Mvc;
using Reader.Models;

namespace Reader.Components
{
    [ViewComponent(Name = "SlidersFooterView")]
    public class SlidersFooterViewComponent : ViewComponent
    {
        private DataContext _DbReaderContext;

        public SlidersFooterViewComponent(DataContext DbReaderContext)
        {
            _DbReaderContext = DbReaderContext;
        }

        public IViewComponentResult Invoke()
        {
            var listOfPost = (from post in _DbReaderContext.TblPosts
                              where (post.IsActive == true)
                              orderby post.CreatedDate descending
                              select post).ToList();

            var listOfCategory = (from Cat in _DbReaderContext.TblCategories
                                  where (Cat.IsActive == true)
                                  select Cat).ToList();

            var listOfAcc = (from Cat in _DbReaderContext.TblAccounts
                             where (Cat.IsActive == true)
                             select Cat).ToList();

            ViewBag.listOfCategory = listOfCategory;
            ViewBag.listOfAcc = listOfAcc;

            return View("Default", listOfPost);
        }
    }
}
