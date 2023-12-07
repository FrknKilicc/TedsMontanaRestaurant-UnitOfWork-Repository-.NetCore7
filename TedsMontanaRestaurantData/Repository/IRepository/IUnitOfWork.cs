using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedsMontanaRestaurantData.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IMealRepository Meal { get; }
        ICustomerRepository Customer { get; }
        IMenuRepository Menu { get; }
        IOrderRepository Orderr { get; }
        IPaymentRepository Paymentt { get; }
        IRestaurantRepository Restaurant { get; }
        IOrderItemRepository OrderItem { get; }
        void save();
    }
}
