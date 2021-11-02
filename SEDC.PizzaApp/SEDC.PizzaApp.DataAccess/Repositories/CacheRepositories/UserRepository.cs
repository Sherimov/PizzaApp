using SEDC.PizzaApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.PizzaApp.DataAccess.Repositories.CacheRepositories
{
    public class UserRepository : IRepository<User>
    {
        public List<User> GetAll()
        {
            return CacheDb.Users;
        }

        public User GetById(int id)
        {
            return CacheDb.Users.FirstOrDefault(u => u.Id == id);
        }

        public int Insert(User entity)
        {
            CacheDb.UserId++;
            entity.Id = CacheDb.UserId;
            CacheDb.Users.Add(entity);
            return entity.Id;
        }

        public void Update(User entity)
        {
            var user = CacheDb.Users.FirstOrDefault(u => u.Id == entity.Id);
            if(user != null)
            {
                int index = CacheDb.Users.IndexOf(user);
                CacheDb.Users[index] = entity;
            }
        }
        public void Delete(int id)
        {
            var user = CacheDb.Users.FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                CacheDb.Users.Remove(user);
            }
        }
    }
}
