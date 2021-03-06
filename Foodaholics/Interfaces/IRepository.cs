﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodaholics.Interfaces
{
    interface IRepository<T> where T : class
    {
        List<T> GetAll();

        T GetById(int id);
        void Insert(T t);
        void Delete(T t);
    }
}
