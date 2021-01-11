using Newtonsoft.Json;
using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Product_Management_System.Controllers
{
    public class UserController : ApiController
    {

        AddDatabaseEntities dbobj = new AddDatabaseEntities();

        [HttpGet]
        [Route("api/User/UserLogin/{email}/{password}")]
        public IHttpActionResult UserLogin(String email, string password)
        {
            tbl_Users obj = dbobj.tbl_Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault<tbl_Users>();
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);

        }

        [HttpPost]
        public IHttpActionResult Register(tbl_Users data)
        {
            dbobj.tbl_Users.Add(data);
            dbobj.SaveChanges();
            return Ok();

        }      
    }
}