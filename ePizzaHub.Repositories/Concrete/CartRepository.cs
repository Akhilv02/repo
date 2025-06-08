using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Repositories.Contract;
using Microsoft.EntityFrameworkCore;

namespace ePizzaHub.Repositories.Concrete
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(PizzaHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetCartItemQuantityAsync(Guid guid)
        {
            int itemCount = await _dbContext.CartItems.Where(x => x.CartId == guid).CountAsync();

            return itemCount;
        }
    }
}
