using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedsMontanaRestaurantData.Repository.IRepository;
using TedsMontanaRestaurantModel;

namespace TedsMontanaRestaurantData.Repository
{
    internal class RestaurantRepository:Repository<Restaurant>,IRestaurantRepository
    {
        private readonly ApplicationDbContext _context;
        public RestaurantRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
