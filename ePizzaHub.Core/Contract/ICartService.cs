
namespace ePizzaHub.Core.Contract
{
    public interface ICartService
    {
        Task<int> GetCartItemsCountAsync(Guid cartId);
    }
}
