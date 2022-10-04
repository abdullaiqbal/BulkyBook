﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositry.IRepositry
{
    public interface IUnitOfWork
    {
        ICategoryRepositry Category { get; }
        ICoverTypeRepositry CoverType { get; }
        IProductRepositry Product { get; }
        void Save();
    }
}
