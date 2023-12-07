using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TedsMontanaRestaurantData.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void add(T entity);
        void Update(T entity);
        void remove(T entity);
        T GetById(Expression<Func<T,bool>>filter,string?includeProperties=null);
        IEnumerable<T> GetAll(Expression<Func<T,bool>>?filter=null, string? includeProperties = null);
        void removeRange(IEnumerable<T> entities);

    }
}
