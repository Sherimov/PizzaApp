using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.PizzaApp.DataAccess.Repositories.DbRepositories
{
    public class PizzaDbRepository : IRepository<Pizza>
    {
        private readonly PizzaAppContext _context;

        public PizzaDbRepository(PizzaAppContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            Pizza pizza = _context.Pizzas.FirstOrDefault(x => x.Id == id);
            if (pizza != null) _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
        }

        public List<Pizza> GetAll()
        {
            return _context.Pizzas.ToList();
        }

        public Pizza GetById(int id)
        {
            return _context.Pizzas.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Pizza entity)
        {
            _context.Pizzas.Add(entity);
            int id = _context.SaveChanges();
            return id;
        }

        public void Update(Pizza entity)
        {
            Pizza pizza = _context.Pizzas.FirstOrDefault(x => x.Id == entity.Id);
            if (pizza != null)
            {
                entity.Id = pizza.Id;
                _context.Pizzas.Update(entity);
            }
        }
    }
}
