using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repositry.IRepositry;
using BulkyBook.Models;
//using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositry
{
    public class CoverTypeRepositry : Repositry<CoverType>, ICoverTypeRepositry
    {
        private ApplicationDbContext _Db;
        public CoverTypeRepositry(ApplicationDbContext db) : base(db)
        {
            _Db = db;
        }
        //public void Save()
        //{
        //    _Db.SaveChanges();
        //}

        public void Update(CoverType obj)
        {
            _Db.CoverType.Update(obj);
        }
    }
}
