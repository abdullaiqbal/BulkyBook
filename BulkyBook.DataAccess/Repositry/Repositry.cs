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
            //_Db.Products.Include(u => u.Category).Include(u => u.CoverType);
            this.dbSet = _Db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        //IncludeProp (Category,CoverType)
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public T GetFirstorDefault(Expression<Func<T, bool>> Filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
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
