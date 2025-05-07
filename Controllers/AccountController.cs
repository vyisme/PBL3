using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Data;
using PBL3.Entities;
using PBL3.Models;

namespace PBL3.Controllers
{
    public class AccountController : Controller
    {
        private readonly BMContext _bMContext;

        public AccountController(BMContext bmcontext)
        {
            _bMContext = bmcontext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                if (_bMContext.Users.Any(u => u.Sdt == model.Sdt))
                {
                    ModelState.AddModelError("Sdt", "Số điện thoại đã tồn tại.");
                    return View(model);
                }
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                    return View(model);
                }
                //Kiểm tra số điện thoại đã tồn tại trong cơ sở dữ liệu
                User user = new User();
                user.Sdt = model.Sdt;
                user.Hoten = model.Hoten;
                user.NS = model.NS;
                user.SetPassword(model.Password);
                //Console.WriteLine($"Sdt: {model.Sdt}, Hoten: {model.Hoten}, Password: {model.Password}";
                try
                {
                    _bMContext.Users.Add(user);
                    _bMContext.SaveChanges();
                    if (model.AccountType == "Tiết kiệm")
                    {
                        _bMContext.BankAccounts.Add(new SavingAccount { Sdt = user.Sdt, AccountId = BankAccount.GenerateAccountId(_bMContext) });
                    }
                    else if (model.AccountType == "Vay")
                    {
                        _bMContext.BankAccounts.Add(new LoanAccount { Sdt = user.Sdt, AccountId = BankAccount.GenerateAccountId(_bMContext) });
                    }
                    else if (model.AccountType == "Thường")
                    {
                        _bMContext.BankAccounts.Add(new RegularAccount { Sdt = user.Sdt, AccountId = BankAccount.GenerateAccountId(_bMContext) });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Loại tài khoản không hợp lệ.");
                        return View(model);
                    }
                    _bMContext.SaveChanges();

                    ModelState.Clear(); // Xóa ModelState để tránh lỗi khi đăng ký lại
                    ViewBag.Message = "Đăng ký thành công.";
                }
                catch (DbUpdateException e)
                {
                    ModelState.AddModelError("", "Số điện thoại đã tồn tại.");
                    return View(model);

                }
                return RedirectToAction("Login","Home");
            }
            return View(model);
        }
        // Action tr? v? view ??ng nh?p (Login)
        [HttpGet]
        public IActionResult Login()
        {
            return View();  // Tr? v? Login.cshtml
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var account = _bMContext.Users.FirstOrDefault(a => a.Sdt == model.Sdt);
            if (account == null || !account.VerifyPassword(model.Password))
            {
                ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không chính xác.");
                return View(model);
            }
            //var  account = _bMContext.Users.Where(a => a.Sdt == model.Sdt && a.PasswordHash == model.Password);
            TempData["Username"] = model.Sdt;
            TempData["Password"] = model.Password;
            return RedirectToAction("user","User");
        }
        //public IActionResult SecurePage()
        //{

        //}
        public IActionResult LogOut()
        {
            return RedirectToAction("Index");
        }
    }
}
