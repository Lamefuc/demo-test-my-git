using Microsoft.AspNetCore.Mvc;
using Reader.Models;

namespace Reader.Components
{
    [ViewComponent(Name = "PopularPostView")]
    public class PopularPostViewComponent : ViewComponent
    {
        private DataContext _DbReaderContext;

        public PopularPostViewComponent(DataContext DbReaderContext)
        {
            _DbReaderContext = DbReaderContext;
        }

        public IViewComponentResult Invoke()
        {
            var listOfPost = (from post in _DbReaderContext.TblPosts
                              where (post.IsActive == true)
                              orderby post.CreatedDate descending
                              select post).Take(1).ToList();


            return View("Default", listOfPost);
        }
    }
}
