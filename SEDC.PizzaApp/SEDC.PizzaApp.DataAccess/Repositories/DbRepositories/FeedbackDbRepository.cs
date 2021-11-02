using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.PizzaApp.DataAccess.Repositories.DbRepositories
{
    public class FeedbackDbRepository : IRepository<Feedback>
    {
        private readonly PizzaAppContext _context;

        public FeedbackDbRepository(PizzaAppContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            Feedback feedback = _context.Feedbacks.FirstOrDefault(x => x.Id == id);
            if (feedback != null) _context.Feedbacks.Remove(feedback);
            _context.SaveChanges();
        }

        public List<Feedback> GetAll()
        {
            return _context.Feedbacks.ToList();
        }

        public Feedback GetById(int id)
        {
            return _context.Feedbacks.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Feedback entity)
        {
            _context.Feedbacks.Add(entity);
            int id = _context.SaveChanges();
            return id;
        }

        public void Update(Feedback entity)
        {
            Feedback feedback = _context.Feedbacks.FirstOrDefault(x => x.Id == entity.Id);
            if (feedback != null)
            {
                entity.Id = feedback.Id;
                _context.Feedbacks.Update(entity);
            }
        }
    }
}
