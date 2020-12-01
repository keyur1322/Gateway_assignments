using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SourceControlAssignment1.Models;

namespace SourceControlAssignment1.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserView(UserClass userClass)
        {
            return View();
        }
    }
}