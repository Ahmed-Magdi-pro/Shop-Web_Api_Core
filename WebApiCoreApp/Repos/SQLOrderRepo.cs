using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreApp.Authentication;
using WebApiCoreApp.Models;
using WebApiCoreApp.Repos;

namespace WebApiCoreApp.ViewModel
{
    public class SQLOrderRepo : IOrderRepo
    {
        private readonly AppDBcontext db;

        public SQLOrderRepo(AppDBcontext db)
        {
            this.db = db;
        }
        public void Delete(order o)
        {
            db.Remove(o);
            db.SaveChanges();
        }

        public IEnumerable<order> GetAllOrders()
        {
            IEnumerable<order> orders = db.orders.ToList();
            return orders;
        }

        public order GetOrderById(int id)
        {
            order order = db.orders.Find(id);
            return order;
        }

        public order orderRequest(OrderRequestVM order)
        {
            throw new NotImplementedException();
        }

        //public order orderRequest(OrderRequestVM order)
        //{

        //        int numOfItems = order.ItemNumbers;
        //        int cost = order.ItemNumbers * order.product.Price;

        //        order orderRequest = new order()
        //        {

        //            TotallCost = cost,
        //            ItemName = order.product.Name,
        //            ItemNumbers = numOfItems,
        //            CustomerName = order.CustomerName,
        //            Phone = order.Phone,

        //        };

        //        db.orders.Add(orderRequest);
        //        db.SaveChanges();
        //        return orderRequest;

        //}


    }
}
