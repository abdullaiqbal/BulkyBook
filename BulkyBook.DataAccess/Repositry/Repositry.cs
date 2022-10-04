using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repositry.IRepositry;
//using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositry
{
    public class Repositry<T> : IRepositry<T> where T : class
    {
        private readonly ApplicationDbContext _Db;

        internal DbSet<T> dbSet;
        public Repositry(ApplicationDbContext Db)
        {
            _Db = Db;
            this.dbSet = _Db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T GetFirstorDefault(Expression<Func<T, bool>> Filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(Filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
