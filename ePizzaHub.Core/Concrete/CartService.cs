using ePizzaHub.Core.Contract;
using ePizzaHub.Repositories.Concrete;
using ePizzaHub.Repositories.Contract;

namespace ePizzaHub.Core.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<int> GetCartItemsCountAsync(Guid cartId)
        {
            return await _cartRepository.GetCartItemQuantityAsync(cartId);
        }
    }
}
