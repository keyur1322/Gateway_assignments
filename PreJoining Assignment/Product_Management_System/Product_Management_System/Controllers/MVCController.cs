using log4net;
using Newtonsoft.Json;
using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Product_Management_System.Controllers
{

    public class MVCController : Controller
    {

        // GET: MVC
        //TODO: Register action method 
        
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(UserController));      //type of class

        public ActionResult Register()
        {
            log.Debug("Debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");

            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(HttpPostedFileBase imagefile, tbl_Users data1)
        {
            String filename = Path.GetFileName(imagefile.FileName);
            String filename1 = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(imagefile.FileName);

            String path = Path.Combine(Server.MapPath("/User_Image/"), filename1);

            data1.Profile = "/User_Image/" + filename1;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (imagefile.ContentLength <= 10000000)
                {
                    imagefile.SaveAs(path);


                    HttpClient hc = new HttpClient();
                    hc.BaseAddress = new Uri("https://localhost:44393/api/User/");

                    var registerdata = hc.PostAsJsonAsync<tbl_Users>("Register", data1);
                    registerdata.Wait();

                    var savedata = registerdata.Result;
                    if (savedata.IsSuccessStatusCode)
                    {
                        ModelState.Clear();
                        return RedirectToAction("Login");

                    }
                }
            }

            ModelState.Clear();
            return View("Register");
        }







        //TODO: Login action method 

        public ActionResult Login()
        {
            log.Debug("Debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");

            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(String email, String password)
        {
            tbl_Users obj = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44393/");

            var logindata = hc.GetAsync("api/User/UserLogin/" + email + "/" + password);
            logindata.Wait();

            var savedata = logindata.Result;
            if (savedata.IsSuccessStatusCode)
            {
                FormsAuthentication.SetAuthCookie(email, false);

                var displaydata = savedata.Content.ReadAsAsync<tbl_Users>();
                displaydata.Wait();
                obj = displaydata.Result;


                Session["useremail"] = obj.Email;
                Session["userimg"] = obj.Profile;

                return RedirectToAction("Dashboard");

            }
            else
            {
                ModelState.Clear();
                return RedirectToAction("Login");
            }

        }







        //TODO: Dashboard action method 

        [Authorize]
        public ActionResult Dashboard()
        {

            return View();

        }






        //TODO: ProductList action method 

        [Authorize]
        public ActionResult ProductList()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44393/api/Product/");

            var consume = hc.GetAsync("GetProduct");
            consume.Wait();

            var result = consume.Result;
            if (result.IsSuccessStatusCode)
            {
                var displaydata = result.Content.ReadAsAsync<List<tbl_Products>>();
                displaydata.Wait();

                // var obj = JsonConvert.DeserializeObject<List<tbl_Products>>(displaydata.Result);
                //var obj = displaydata.Result;
                List<tbl_Products> list = displaydata.Result;

                return View(list);

            }
            else
            {
                return View();

            }
        }








        //TODO: Add product action method 

        [Authorize]
        public ActionResult AddProduct()
        {
            // IEnumerable<tbl_Products> obj = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44393/api/Product/");

            var consume = hc.GetAsync("GetCategory");
            consume.Wait();

            var savedata = consume.Result;
            if (savedata.IsSuccessStatusCode)
            {
                var displaydata = savedata.Content.ReadAsAsync<List<tbl_Categories>>();
                displaydata.Wait();
                // ViewBag.obj = JsonConvert.DeserializeObject(displaydata.Result);

                List<tbl_Categories> list = displaydata.Result;
                ViewBag.obj = list;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(HttpPostedFileBase smallimage, HttpPostedFileBase largeimage, tbl_Products data1)
        {

            String smallfile = Path.GetFileName(smallimage.FileName);
            String smallfile1 = DateTime.Now.ToString("yymmssfff") + smallfile;
            string smallextension = Path.GetExtension(smallimage.FileName);
            String smallpath = Path.Combine(Server.MapPath("/Product_Image/Small_Image/"), smallfile1);
            data1.SmallImage = "/Product_Image/Small_Image/" + smallfile1;


            String largefile = Path.GetFileName(largeimage.FileName);
            String largefile1 = DateTime.Now.ToString("yymmssfff") + largefile;
            string largeextension = Path.GetExtension(largeimage.FileName);
            String largepath = Path.Combine(Server.MapPath("/Product_Image/Large_Image/"), largefile1);
            data1.LargeImage = "/Product_Image/Large_Image/" + largefile1;


            if (smallextension.ToLower() == ".jpg" || smallextension.ToLower() == ".jpeg" || smallextension.ToLower() == ".png" || largeextension.ToLower() == ".jpg" || largeextension.ToLower() == ".jpeg" || largeextension.ToLower() == ".png")
            {
                if (smallimage.ContentLength <= 10000000 || largeimage.ContentLength <= 10000000)
                {
                    smallimage.SaveAs(smallpath);
                    largeimage.SaveAs(largepath);


                    HttpClient hc = new HttpClient();
                    hc.BaseAddress = new Uri("https://localhost:44393/api/Product/");

                    var registerdata = hc.PostAsJsonAsync<tbl_Products>("AddProduct", data1);
                    registerdata.Wait();

                    var savedata = registerdata.Result;
                    if (savedata.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Dashboard");
                    }
                }

            }
            return View();

        }









        //TODO: Edit product action method 

        [Authorize]
        public ActionResult EditProduct(int id)
        {


            HttpClient hc1 = new HttpClient();
            hc1.BaseAddress = new Uri("https://localhost:44393/api/Product/");

            var consume1 = hc1.GetAsync("GetCategory");
            consume1.Wait();

            var savedata1 = consume1.Result;
            if (savedata1.IsSuccessStatusCode)
            {
                var displaydata = savedata1.Content.ReadAsAsync<List<tbl_Categories>>();
                displaydata.Wait();
                // ViewBag.obj = JsonConvert.DeserializeObject(displaydata.Result);

                List<tbl_Categories> list = displaydata.Result;
                ViewBag.obj = list;
            }


            tbl_Products obj = null;
            HttpClient hc = new HttpClient();

            hc.BaseAddress = new Uri("https://localhost:44393/");

            var consume = hc.GetAsync("api/Product/EditOrViewProduct/" + id.ToString());
            consume.Wait();

            var savedata = consume.Result;
            if (savedata.IsSuccessStatusCode)
            {
                var displaydata = savedata.Content.ReadAsAsync<tbl_Products>();
                displaydata.Wait();
                obj = displaydata.Result;
            }
            return View(obj);
        }


        [HttpPost]
        public ActionResult EditProduct(HttpPostedFileBase smallimage, HttpPostedFileBase largeimage, tbl_Products data1)
        {
            String smallfile = Path.GetFileName(smallimage.FileName);
            String smallfile1 = DateTime.Now.ToString("yymmssfff") + smallfile;
            string smallextension = Path.GetExtension(smallimage.FileName);
            String smallpath = Path.Combine(Server.MapPath("/Product_Image/Small_Image/"), smallfile1);
            data1.SmallImage = "/Product_Image/Small_Image/" + smallfile1;


            String largefile = Path.GetFileName(largeimage.FileName);
            String largefile1 = DateTime.Now.ToString("yymmssfff") + largefile;
            string largeextension = Path.GetExtension(largeimage.FileName);
            String largepath = Path.Combine(Server.MapPath("/Product_Image/Large_Image/"), largefile1);
            data1.LargeImage = "/Product_Image/Large_Image/" + largefile1;


            if (smallextension.ToLower() == ".jpg" || smallextension.ToLower() == ".jpeg" || smallextension.ToLower() == ".png" || largeextension.ToLower() == ".jpg" || largeextension.ToLower() == ".jpeg" || largeextension.ToLower() == ".png")
            {
                if (smallimage.ContentLength <= 10000000 || largeimage.ContentLength <= 10000000)
                {
                    smallimage.SaveAs(smallpath);
                    largeimage.SaveAs(largepath);


                    HttpClient hc = new HttpClient();


                    hc.BaseAddress = new Uri("https://localhost:44393/api/Product/");

                    var registerdata = hc.PutAsJsonAsync<tbl_Products>("PutProduct", data1);
                    registerdata.Wait();

                    var savedata = registerdata.Result;
                    if (savedata.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Dashboard");
                    }
                }
            }

            return View();

        }








        //TODO: View product action method 

        [Authorize]
        public ActionResult ViewProduct(int id)
        {
            tbl_Products obj = null;
            HttpClient hc = new HttpClient();

            hc.BaseAddress = new Uri("https://localhost:44393/");

            var consume = hc.GetAsync("api/Product/EditOrViewProduct/" + id.ToString());
            consume.Wait();

            var savedata = consume.Result;
            if (savedata.IsSuccessStatusCode)
            {
                var displaydata = savedata.Content.ReadAsAsync<tbl_Products>();
                displaydata.Wait();
                obj = displaydata.Result;
            }

            Session["Name"] = obj.Name;
            Session["Category"] = obj.tbl_Categories.Name;
            Session["Price"] = obj.Price;
            Session["Quantity"] = obj.Quantity;
            Session["ShortDescription"] = obj.ShortDescription;
            Session["LongDescription"] = obj.LongDescription;
            Session["SmallImage"] = obj.SmallImage;
            Session["LargeImage"] = obj.LargeImage;

            var img = Session["SmallImage"];
            ViewData["small"] = img;


            var img1 = Session["LargeImage"];
            ViewData["large"] = img1;


            return View();


        }







        //TODO: Delete product action method 

        public ActionResult DeleteProduct(int id)
        {
            HttpClient hc = new HttpClient();

            hc.BaseAddress = new Uri("https://localhost:44393/");

            var consume = hc.GetAsync("api/Product/DeleteProduct/" + id.ToString());
            consume.Wait();

            var savedata = consume.Result;
            if (savedata.IsSuccessStatusCode)
            {
                //ViewBag.deletemsg = "Successfull";
            }

            return RedirectToAction("ProductList");

        }







        //TODO: Logout action method 

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }




    }
}
    

