using SEDC.PizzaApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.PizzaApp.DataAccess.Repositories.CacheRepositories
{
    public class PizzaRepository : IRepository<Pizza>
    {
        public List<Pizza> GetAll()
        {
            return CacheDb.Menu;
        }

        public Pizza GetById(int id)
        {
            return CacheDb.Menu.FirstOrDefault(p => p.Id == id);
        }

        public int Insert(Pizza entity)
        {
            CacheDb.PizzaId++;
            entity.Id = CacheDb.PizzaId;
            CacheDb.Menu.Add(entity);
            return entity.Id;
        }

        public void Update(Pizza entity)
        {
            var pizza = CacheDb.Menu.FirstOrDefault(p => p.Id == entity.Id);
            if(pizza != null)
            {
                int index = CacheDb.Menu.IndexOf(pizza);
                CacheDb.Menu[index] = entity;
            }
        }

        public void Delete(int id)
        {
            var pizza = CacheDb.Menu.FirstOrDefault(p => p.Id == id);
            if(pizza != null)
            {
                CacheDb.Menu.Remove(pizza);
            }
        }
    }
}
