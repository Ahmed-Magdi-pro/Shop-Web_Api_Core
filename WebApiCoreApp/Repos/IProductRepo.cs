using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreApp.Models;

namespace WebApiCoreApp.Repos
{
    public interface IproductRepo
    {
        product Addproduct(product product);
        product GetproductByID(int id);
        IEnumerable<product> GetAllproducts();
        void Delete(product pro);
        product Update(product proToUpdate);
       
    }
}
