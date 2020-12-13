using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreApp.Authentication;
using WebApiCoreApp.Models;

namespace WebApiCoreApp.Repos
{
    public class SQLProductRepo : IproductRepo
    {
        private readonly AppDBcontext db;
        private readonly IHostingEnvironment hostingEnvironment;

        public SQLProductRepo(AppDBcontext db, IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
        }
        public product Addproduct(product model)
        {
            product pro = new product
            {
                Name = model.Name,
                Price = model.Price
            };
            db.products.Add(pro);
            db.SaveChanges();
            return pro;
        }

        public void Delete(product pro)
        {
            if(pro != null)
            {
                db.Remove(pro);
                db.SaveChanges();
            }
        }

        public IEnumerable<product> GetAllproducts()
        {
            IEnumerable<product> productList = db.products.ToList();
            return productList;
        }

        public product GetproductByID(int id)
        {
            product pro = db.products.Where(n => n.Id == id).FirstOrDefault();
            return pro;
        }

        public product Update(product proToUpdate)
        {
            product pro = db.products.Find(proToUpdate.Id);
            pro.Name = proToUpdate.Name;
            pro.Price = proToUpdate.Price;

            db.SaveChanges();
            return pro;
        }
    }
}
