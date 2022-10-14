using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositry.IRepositry
{
    public interface ICompanyRepositry : IRepositry<Company>
    {
        void Update(Company obj);
        //void Save();
    }
}
