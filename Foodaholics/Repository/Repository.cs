using Foodaholics.Interfaces;
using Foodaholics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodaholics.Repository
{
    public class Repository<T> : IRepository<T> where T: class
    {
        protected FoodaholicDBEntities faEntity = new FoodaholicDBEntities();

        public List<T> GetAll()
        {
            return faEntity.Set<T>().ToList<T>();
        }

        public T GetById(int id)
        {
            return faEntity.Set<T>().Find(id);
        }

        public void Insert(T t)
        {
            faEntity.Set<T>().Add(t);
            faEntity.SaveChanges();
        }

        public void Delete(T t)
        {
            faEntity.Set<T>().Remove(t);
            faEntity.SaveChanges();
        }
    }
}