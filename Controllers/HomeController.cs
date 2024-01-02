using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Reader.Models;

namespace Reader.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var listOfPost = (from post in _dbContext.TblPosts
                              join category in _dbContext.TblCategories
                              on post.CategoryId equals category.CategoryId
                              where post.IsActive == true && post.Status == 1 && category.IsActive == true
                              orderby post.Sview descending
                              select new ViewPost
                              {
                                  PostId = post.PostId,
                                  Title = post.Title,
                                  Thumb = post.Thumb,
                                  SubContents = post.SubContents,
                                  CreatedDate = post.CreatedDate,
                                  Sview = post.Sview,
                                  CategoryId = category.CategoryId,
                                  CategoryName = category.CategoryName,
                              }).ToList();

            ViewBag.listOfPost = listOfPost;

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}