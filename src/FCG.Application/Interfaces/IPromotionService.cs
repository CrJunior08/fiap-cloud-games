using FCG.Application.Responses;

namespace FCG.Application.Interfaces
{
    public interface IPromotionService
    {
        Task<UpdateGameResponse> ApplyDiscountAsync(int gameId, double discountPercentage);
    }
}
