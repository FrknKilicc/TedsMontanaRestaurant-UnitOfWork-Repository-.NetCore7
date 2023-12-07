using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedsMontanaRestaurantData.Repository.IRepository;

namespace TedsMontanaRestaurantData.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;  // veritabanı tablosunu temsil edecek.

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); //tablonun içine setledim 
        }

       
        public void add(T entity) 
        {
            _dbSet.Add(entity); // aynı tabloya ekleme yaptım.
            
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet; // aşağıda ki linq sorgularını uygulamak için kullandım, alternatif olarak tablonun kendisine de yazabilirdim.
            if (filter != null)
            {
                query = query.Where(filter); // parametreden gelen koşulu query aracılığıyla veritabanında yürütüyorum .
            }if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)) //include property  virgülle ayırdım diziye dönüştürdüm
                {
                    query=query.Include(item); // her bir değeri query ye include ettim
                }
            }
            return query.ToList();

        }

        public T GetById(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach(var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(item);
                }

            }
            return query.FirstOrDefault();
        }

        public void remove(T entity)
        {
           _dbSet.Remove(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void removeRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities); //entites de ki  birden fazla satırı aynı anda silebilirim.
        }
    }
}
