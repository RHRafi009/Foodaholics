using Foodaholics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodaholics.Repository
{
    public class BlogRepository : Repository<Blog>
    {


        public void UpdateStatusId(Blog b, int id)
        {
            Blog ub = GetById(b.id);
            ub.StatusID = id;
            faEntity.SaveChanges();
        }

        public void UpdateWatch(int id)
        {
            Blog b = GetById(id);
            b.Watch = b.Watch + 1;
            faEntity.SaveChanges();
        }
        public string WriterName(int id)
        {
            Blog b = GetById(id);
            UserRepository uRepo = new UserRepository();
            User u = uRepo.GetById(b.WriterID);
            return u.Name;
        }
    }
}