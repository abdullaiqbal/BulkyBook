using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repositry.IRepositry;
using BulkyBook.Models;
//using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositry
{
    public class CompanyRepositry : Repositry<Company>, ICompanyRepositry
    {
        private ApplicationDbContext _Db;
        public CompanyRepositry(ApplicationDbContext db) : base(db)
        {
            _Db = db;
        }
        //public void Save()
        //{
        //    _Db.SaveChanges();
        //}

        public void Update(Company obj)
        {
            _Db.Companies.Update(obj);
        }
    }
}
