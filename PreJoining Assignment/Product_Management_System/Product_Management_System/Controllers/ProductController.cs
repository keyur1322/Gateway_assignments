using Newtonsoft.Json;
using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Product_Management_System.Controllers
{
    public class ProductController : ApiController
    {
        AddDatabaseEntities dbobj = new AddDatabaseEntities();
        
        public IHttpActionResult GetCategory()
        {
            //var data = dbobj.tbl_Categories.Select(x => new CategoryClass{Id= x.Id, Name = x.Name }).ToList();
            //return Ok(JsonConvert.SerializeObject(data));

            List<tbl_Categories> list = dbobj.tbl_Categories.ToList();
            return Ok(list);

        }

        public IHttpActionResult GetProduct()
        {
            //var data = dbobj.tbl_Products.Select(x => new ProductClass { Id = x.Id, Name = x.Name, Category = x.Category, Price = x.Price, Quantity = x.Quantity, ShortDescription = x.ShortDescription, LongDescription =x.LongDescription, SmallImage = x.SmallImage, LargeImage = x.LargeImage }).ToList();
            //return Ok(JsonConvert.SerializeObject(data));


            List<tbl_Products> list = dbobj.tbl_Products.ToList();
            return Ok(list);
        }



        [HttpPost]
        public IHttpActionResult AddProduct(tbl_Products data)
        {
            
            dbobj.tbl_Products.Add(data);
            dbobj.SaveChanges();
            return Ok();

        }


        [HttpGet]
        public IHttpActionResult EditOrViewProduct(int id)
        {
            tbl_Products obj = dbobj.tbl_Products.Where(x => x.Id == id).First<tbl_Products>();
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);

        }


        public IHttpActionResult PutProduct(tbl_Products obj)
        {
            var data = dbobj.tbl_Products.Where(x => x.Id == obj.Id).First<tbl_Products>();
            if (data != null)
            {
                data.Name = obj.Name;
                data.Category = obj.Category;
                data.Price = obj.Price;
                data.Quantity = obj.Quantity;
                data.ShortDescription = obj.ShortDescription;
                data.LongDescription = obj.LongDescription;
                data.SmallImage = obj.SmallImage;
                data.LargeImage = obj.LargeImage;

                dbobj.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        public IHttpActionResult SearchData(String searchtext)
        {
            tbl_Products obj = dbobj.tbl_Products.Where(x => x.Name == searchtext).First<tbl_Products>();
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }


        [HttpGet]
        public IHttpActionResult DeleteProduct(int id)
        {
            var data = dbobj.tbl_Products.Where(x => x.Id == id).FirstOrDefault();
            dbobj.Entry(data).State = System.Data.Entity.EntityState.Deleted;
            dbobj.SaveChanges();
            return Ok();
        }




    }
}
