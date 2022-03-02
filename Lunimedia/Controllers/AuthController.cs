using Lunimedia.Models;
using Lunimedia.Services;
using System.Web.Mvc;
using System.Web.Security;

namespace Lunimedia.Controllers
{
    public class AuthController : Controller
    {
        readonly IUserService _userService = new UserService();

        public JsonResult VerifyId(string term)
        {
            return Json(!_userService.VerifyId(term), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Register()
        {
            RegisterModel model = new RegisterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userService.Register(model))
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = _userService.GetUserByUserId(model.Id);
                if (user.Id != null)
                {
                    if (_userService.ValidatePassword(user, model))
                    {
                        Session["UserPK"] = user.Pk;
                        return RedirectToAction("Index", "Home");
                    }
                }
                ViewData["Login"] = "아이디 또는 비밀번호가 일치하지 않습니다.";
                return View();
            }
            return View();
        }

        public ActionResult Logout()
        {

            Session.Remove("UserPK");
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

    }
}