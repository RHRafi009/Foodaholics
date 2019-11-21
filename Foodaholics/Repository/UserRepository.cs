using Foodaholics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodaholics.Repository
{
    public class UserRepository:Repository<User>
    {
        public void ProfilePictureUpdate(int id, string path)
        {
            User u = GetById(id);
            u.ProfilePicturePath = path;
            faEntity.SaveChanges();
        }

        public void PasswordUpdate(int id, string pass)
        {
            User u = GetById(id);
            u.Password = pass;
            faEntity.SaveChanges();
        }

        public User getByEmailnPass(string email, string pass)
        {
            List<User> uList = GetAll();
            foreach (User u in uList)
            {
                if (u.Email == email && u.Password == pass)
                    return u;
            }
            return null;

        }
    }
}