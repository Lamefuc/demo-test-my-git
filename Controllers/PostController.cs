using Reader.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using PagedList.Core;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Reader.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Reader.Controllers
{
    public class PostController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _DbReaderContext;

        public PostController(ILogger<HomeController> logger, DataContext DbReaderContext)
        {
            _logger = logger;
            _DbReaderContext = DbReaderContext;
        }

        //Danh sách bài viết   
        [Route("/list/{slug}-{id:long}.html", Name = "list")]
        [HttpGet]
        public IActionResult list(long? id, int page = 1)
        {

            if (id == null)
            {
                return NotFound();

            }
            var listOfCat = (from cat in _DbReaderContext.TblCategories
                             join mn in _DbReaderContext.TblMenus on cat.CategoryId equals mn.CategoryId
                             where cat.IsActive == true && mn.MenuId == id
                             orderby cat.CategoryId descending
                             select new ViewCategory
                             {
                                 CategoryName = cat.CategoryName,
                                 Posts = (from p in _DbReaderContext.TblPosts
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


                                         ).ToList()

                             });


            var listOfCategoriesWithPosts = listOfCat.Where(c => c.Posts.Count > 0);
            if (listOfCategoriesWithPosts == null)
            {
                return NotFound();
            }
            return View(listOfCategoriesWithPosts.ToList());
        }

        //Xem chi tiết bài viết   
        [Route("/post/{slug}-{id:long}.html", Name = "postdetail")]
        [HttpGet]
        public IActionResult detail(long? id, int page = 1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _DbReaderContext.TblPosts
                .Include(x => x.Category)
                
                .FirstOrDefault(m => (m.PostId == id) && (m.IsActive == true));

            if (post == null)
            {
                return NotFound();
            }

            if (post.Sview != null)
            {
                post.Sview++;
            }
            else
            {
                post.Sview = 0;
            }

            _DbReaderContext.Update(post);
            _DbReaderContext.SaveChanges();

            var listAcc = (from item in _DbReaderContext.TblAccounts
                           where item.IsActive == true
                           select item).ToList();
            ViewBag.page = page;
            ViewBag.listAcc = listAcc;

            return View(post);
        }


        //Danh sách bài viết   
        [Route("/list-cat/{slug}-{id:long}.html", Name = "listCat")]
        [HttpGet]
        public IActionResult listCat(long? id, int page = 1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _DbReaderContext.TblCategories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            var posts = (from p in _DbReaderContext.TblPosts
                         where p.CategoryId == id && p.IsActive == true
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
                         }).ToList();

            var viewCategory = new ViewCategory
            {
                CategoryName = category.CategoryName,
                Posts = posts
            };

            return View(viewCategory);
        }
    }
}
