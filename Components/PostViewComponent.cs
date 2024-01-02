using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Reader.Models;

namespace Reader.Components
{
    [ViewComponent(Name = "PostView")]
    public class PostViewComponent : ViewComponent
    {

        private DataContext _dbContext;

        public PostViewComponent(DataContext DbReaderContext)
        {
            _dbContext = DbReaderContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var listOfCat = (from cat in _dbContext.TblCategories

                             where cat.IsActive == true && cat.HomeFlag == true
                             orderby cat.CategoryOrder ascending
                             select new ViewCategory
                             {
                                 Title = cat.Title,
                                 Description = cat.Description,
                                 Thumb = cat.Thumb,
                                 CategoryId = cat.CategoryId,
                                 CategoryName = cat.CategoryName,
                                 Posts = (from p in _dbContext.TblPosts
                                          where p.CategoryId == cat.CategoryId && p.IsActive == true
                                          orderby p.PostId
                                          select new ViewPost
                                          {
                                              PostId = p.PostId,
                                              Title = p.Title,
                                              Thumb = p.Thumb,
                                              SubContents = p.SubContents,
                                              CreatedDate = p.CreatedDate,
                                              Sview = p.Sview,
                                              Contents = p.Contents,
                                          }


                                         ).Take(6).ToList()

                             });




            



            var listOfCategoriesWithPosts = listOfCat.Where(c => c.Posts.Count > 0);



            return await Task.FromResult((IViewComponentResult)View("Default", listOfCategoriesWithPosts.ToList()));
        }

    }
}
