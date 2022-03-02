using Lunimedia.Models;
using Lunimedia.Services;
using System;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        readonly IUserService _userService = new UserService();
        public ActionResult Index()
        {
            if (Session["UserPK"] != null)
            {
                UserModel model = _userService.GetUserByUserPk((Guid)Session["UserPK"]);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }
    }
}