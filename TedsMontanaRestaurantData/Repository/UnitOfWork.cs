using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedsMontanaRestaurantData.Repository.IRepository;
using TedsMontanaRestaurantModel;

namespace TedsMontanaRestaurantData.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IMealRepository Meal => new MealRepository(_context);

        public ICustomerRepository Customer => new CustomerRepository(_context);

        public IMenuRepository Menu => new MenuRepository(_context);

        public IOrderRepository Orderr => new OrderRepository(_context);

        public IPaymentRepository Paymentt => new PaymentRepository(_context);

        public IRestaurantRepository Restaurant => new RestaurantRepository(_context);
        public IOrderItemRepository OrderItem => new OrderItemRepository (_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void save()
        {
           _context.SaveChanges();
        }
    }
}
