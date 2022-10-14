using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repositry.IRepositry;
//using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositry
{
    public class UnitOfWork : IUnitOfWork
    {

        public ICategoryRepositry Category { get; private set; }
        public ICoverTypeRepositry CoverType { get; private set; }
        public IProductRepositry Product { get; private set; }
        public ICompanyRepositry Company { get; private set; }

        private readonly ApplicationDbContext _Db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _Db=db;
            Category = new CategoryRepositry(_Db);
            CoverType = new CoverTypeRepositry(_Db);
            Product = new ProductRepositry(_Db);
            Company = new CompanyRepositry(_Db);
        }

        public void Save()
        {
            _Db.SaveChanges();
        }
    }
}
