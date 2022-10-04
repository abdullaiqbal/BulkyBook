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
    public class ProductRepositry : Repositry<Product>, IProductRepositry
    {
        private ApplicationDbContext _Db;
        public ProductRepositry(ApplicationDbContext db) : base(db)
        {
            _Db = db;
        }
        //public void Save()
        //{
        //    _Db.SaveChanges();
        //}

        public void Update(Product obj)
        {
            var objFromDb = _Db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title=obj.Title;
                objFromDb.Description=obj.Description;
                objFromDb.ISBN=obj.ISBN;
                objFromDb.Price=obj.Price;
                objFromDb.Price50=obj.Price50;
                objFromDb.Price100=obj.Price100;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.CategoryId=obj.CategoryId;
                objFromDb.CoverTypeId=obj.CoverTypeId;
                objFromDb.Author=obj.Author;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl=obj.ImageUrl;
                }

            }
            //_Db.Products.Update(obj);
        }
    }
}
