using Reader.Areas.Admin.Models;
//using Reader.Extension;
using Reader.Models;
using Reader.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Security.Principal;
using Reader.Extension;

namespace Reader.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountsController : Controller
    {
        private readonly DataContext _context;

        public AdminAccountsController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, int RoleID = 0)
        {
            var pageNumber = page;
            var pageSize = Functions.PAGE_SIZE;

            List<TblAccount> lsAccounts = new List<TblAccount>();

            if (RoleID != 0)
            {
                lsAccounts = (from ac in _context.TblAccounts
                              join rl in _context.TblRoles on ac.RoleId equals rl.RoleId
                              where ac.RoleId == RoleID
                              orderby ac.AccountId descending
                              select ac).ToList();
            }
            else
            {
                lsAccounts = (from ac in _context.TblAccounts
                              join rl in _context.TblRoles on ac.RoleId equals rl.RoleId
                              orderby ac.AccountId descending
                              select ac).ToList();
            }

            PagedList<TblAccount> lsModels = new PagedList<TblAccount>(lsAccounts.AsQueryable(), pageNumber, pageSize);

            ViewBag.CurrentRoleID = RoleID;
            ViewBag.CurrentPage = pageNumber;

            ViewData["QuyenTruyCap"] = new SelectList(_context.TblRoles, "RoleId", "RoleName");

            return View(lsModels);
        }

        // Lọc quyền
        public IActionResult Filtter(int RoleID = 0)
        {
            var url = $"/Admin/AdminAccounts?RoleId={RoleID}";
            if (RoleID == 0)
            {
                url = $"/Admin/AdminAccounts";
            }
            return Json(new { status = "success", redirectUrl = url });
        }


        // ===============================================================================
        public IActionResult Details(int? accountID)
        {
            if (accountID == null || accountID == 0)
            {
                return NotFound();
            }
            // Lấy danh sách quyền truy cập
            var RoleList = (from role in _context.TblRoles
                            select new SelectListItem()
                            {
                                Text = role.RoleName,
                                Value = role.RoleId.ToString(),
                            }).ToList();
            RoleList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn quyền ----",
                Value = "0"
            });

            ViewBag.RoleList = RoleList;
            var tblAccounts = _context.TblAccounts
                            .Include(x => x.Role)
                            .FirstOrDefault(x => x.AccountId == accountID);

            if (tblAccounts == null)
            {
                return NotFound();
            }

            return View(tblAccounts);
        }


        // ====================================================================================

        public IActionResult Create()
        {
            // Lấy danh sách quyền truy cập
            var RoleList = (from role in _context.TblRoles
                            select new SelectListItem()
                            {
                                Text = role.RoleName,
                                Value = role.RoleId.ToString(),
                            }).ToList();
            RoleList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn quyền ----",
                Value = "0"
            });

            ViewBag.RoleList = RoleList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblAccount model, IFormFile? fThumb)
        {
            if (ModelState.IsValid)
            {


                model.Password = (model.Phone.Trim()).ToMD5();
                model.CreatedDate = DateTime.Now;

                // Tạo hình ảnh
                // xử lý Thumb 

                model.FullName = Functions.ToTitleCase(model.FullName);


                if (fThumb != null) // nếu có chọn hình ảnh
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Functions.SEOUrl(model.FullName + model.AccountId.ToString()) + extension;
                    model.Thumb = await Functions.UploadFile(fThumb, @"account", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(model.Thumb)) model.Thumb = "default.jpg"; // 


                _context.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // ================================================================

        public IActionResult Edit(int? accountID)
        {
            if (accountID == null || accountID == 0)
            {
                return NotFound();

            }

            var tblAccounts = _context.TblAccounts.Include(x => x.Role).FirstOrDefault(x => x.AccountId == accountID);

            if (tblAccounts == null)
            {
                return NotFound();
            }

            var RoleList = (from role in _context.TblRoles
                            select new SelectListItem()
                            {
                                Text = role.RoleName,
                                Value = role.RoleId.ToString(),
                            }).ToList();
            RoleList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn quyền ----",
                Value = "0"
            });

            ViewBag.RoleList = RoleList;

            return View(tblAccounts);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TblAccount tblAccounts)
        {
            if (ModelState.IsValid)
            {


                // Tạo ngẫu nhiên laị mật khẩu trong database
                string salt = Functions.GetRandomKey();

                tblAccounts.Email = salt;
                tblAccounts.Password = (tblAccounts.Phone.Trim()).ToMD5(); // lấy lại số điện thoại làm mật khẩu

                _context.Update(tblAccounts);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tblAccounts);
        }

        // =========================================================

        public IActionResult Delete(int? accountID)
        {
            if (accountID == null || accountID == 0)
            {
                return NotFound();
            }

            var tblAccounts = _context.TblAccounts.Find(accountID);

            if (tblAccounts == null)
            {
                return NotFound();
            }

            var RoleList = (from role in _context.TblRoles
                            select new SelectListItem()
                            {
                                Text = role.RoleName,
                                Value = role.RoleId.ToString(),
                            }).ToList();
            RoleList.Insert(0, new SelectListItem()
            {
                Text = "----Chọn quyền ----",
                Value = "0"
            });

            ViewBag.RoleList = RoleList;

            return View(tblAccounts);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int accountID)
        {
            var tblAccounts = _context.TblAccounts.Find(accountID);

            if (tblAccounts == null)
            {
                return NotFound();
            }

            _context.TblAccounts.Remove(tblAccounts);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // ============================================ 

       
    }
}
