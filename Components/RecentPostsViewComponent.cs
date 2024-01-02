using Microsoft.AspNetCore.Mvc;
using Reader.Models;

namespace Reader.Components
{
    [ViewComponent(Name = "RecentPostsView")]
    public class RecentPostsViewComponent : ViewComponent
    {
        private DataContext _DbReaderContext;

        public RecentPostsViewComponent(DataContext DbReaderContext)
        {
            _DbReaderContext = DbReaderContext;
        }

        public IViewComponentResult Invoke()
        {
            var listOfPost= (from post in _DbReaderContext.TblPosts
                              where (post.IsActive == true)
                              orderby post.CreatedDate descending
                              select post).Take(5).ToList();

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
