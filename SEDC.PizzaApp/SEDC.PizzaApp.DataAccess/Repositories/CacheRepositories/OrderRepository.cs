using SEDC.PizzaApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.PizzaApp.DataAccess.Repositories.CacheRepositories
{
    public class OrderRepository : IRepository<Order>
    {
        public List<Order> GetAll()
        {
            return CacheDb.Orders;
        }

        public Order GetById(int id)
        {
            return CacheDb.Orders.FirstOrDefault(o => o.Id == id);
        }

        public int Insert(Order entity)
        {
            CacheDb.OrderId++;
            entity.Id = CacheDb.OrderId;
            CacheDb.Orders.Add(entity);
            return entity.Id;
        }

        public void Update(Order entity)
        {
            var order = CacheDb.Orders.FirstOrDefault(o => o.Id == entity.Id);
            if(order != null)
            {
                int index = CacheDb.Orders.IndexOf(order);
                CacheDb.Orders[index] = entity;
            }
        }

        public void Delete(int id)
        {
            var order = CacheDb.Orders.FirstOrDefault(o => o.Id == id);
            if(order != null)
            {
                CacheDb.Orders.Remove(order);
            }
        }
    }
}
