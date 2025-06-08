
using ePizzaHub.Models.ApiModels.Response;

namespace ePizzaHub.Core.Contract
{
    public interface IItemService
    {
        Task <IEnumerable<GetItemResponse>> GetItemsAsync();
    }
}
