using SourceControlFinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using log4net;
using System.Web.Security;

namespace SourceControlFinalAssignment.Controllers
{
    public class UserController : Controller
    {
        // GET: User


        MyDatabaseEntities dbobj = new MyDatabaseEntities();

        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(UserController));      //type of class




        public ActionResult Register()
        {
            log.Debug("Debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");
            return View();
        }

        [HttpPost]
        public ActionResult Register(HttpPostedFileBase uploadfile, tbl_userdata obj)
        {

            String filename = Path.GetFileName(uploadfile.FileName);
            String filename1 = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(uploadfile.FileName);

            String path = Path.Combine(Server.MapPath("/Images/"), filename1);

            obj.Profile = "/Images/" + filename1;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (uploadfile.ContentLength <= 10000000)
                {
                    dbobj.tbl_userdata.Add(obj);
                    if (dbobj.SaveChanges() > 0)
                    {
                        uploadfile.SaveAs(path);
                        ModelState.AddModelError("", "Inserted Successfully.");
                        ModelState.Clear();

                        RedirectToAction("Login");

                    }
                }
                else
                {

                    ModelState.AddModelError("", "Upload Image file in proper size.");
                    RedirectToAction("Register");

                }
            }
            else
            {
                ModelState.AddModelError("", "Upload image in jpg, jpeg or png format.");
                RedirectToAction("Register");

            }
            return View();
        }











        public ActionResult Login()
        {

            log.Debug("Debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_userdata model)
        {
            using (var data = new MyDatabaseEntities())
            {
                bool isvalid = data.tbl_userdata.Any(x => x.Email == model.Email && x.Password == model.Password);
                if (isvalid)
                {

                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    TempData["loginModel1"] = model;
                    return RedirectToAction("List");
                }

                else
                {
                    ModelState.AddModelError("", "Invalid email or Password.");
                    return View();
                }
            }
        }







        [Authorize]
        public ActionResult List()
        {
            var model1 = TempData["loginModel1"] as tbl_userdata;

            var model = dbobj.tbl_userdata.Where(x => x.Email == model1.Email && x.Password == model1.Password).FirstOrDefault();

            if (model != null)
            {
                Session["id"] = model.Id;
                Session["Username"] = model.Username;
                Session["Email"] = model.Email;
                Session["Age"] = model.Age;
                Session["Phoneno"] = model.Phoneno;
                Session["Profile"] = model.Profile;

                var img = Session["Profile"];
                ViewData["Img"] = img;
            }
            else
            {
                RedirectToAction("Login");
            }

            return View();



        }







        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }
}