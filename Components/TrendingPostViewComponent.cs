using Microsoft.AspNetCore.Mvc;
using Reader.Models;

namespace Reader.Components
{
    [ViewComponent(Name = "TrendingPostView")]
    public class TrendingPostViewComponent : ViewComponent
    {
        private DataContext _DbReaderContext;

        public TrendingPostViewComponent(DataContext DbReaderContext)
        {
            _DbReaderContext = DbReaderContext;
        }

        public IViewComponentResult Invoke()
        {
            var listOfPost = (from post in _DbReaderContext.TblPosts
                              where (post.IsActive == true) && (post.IsHot == true)
                              orderby post.PostId descending
                              select post).Take(4).ToList();

           
            return View("Default", listOfPost);
        }
    }
}
