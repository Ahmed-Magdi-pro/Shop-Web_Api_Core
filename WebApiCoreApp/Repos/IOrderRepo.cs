using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreApp.Models;
using WebApiCoreApp.ViewModel;

namespace WebApiCoreApp.Repos
{
   public interface IOrderRepo
    {
        order orderRequest(OrderRequestVM order);
        order GetOrderById(int id);
        IEnumerable<order> GetAllOrders();
        void Delete(order o);
    }
}
