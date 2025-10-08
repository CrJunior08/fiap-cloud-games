using FCG.Application.Interfaces;
using FCG.Application.Responses;
using FCG.Domain.Interfaces.Repository;

namespace FCG.Application.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IGameRepository _gameRepository;

        public PromotionService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<UpdateGameResponse> ApplyDiscountAsync(int gameId, double discountPercentage)
        {
            try
            {
                var game = await _gameRepository.GetGameByIdAsync(gameId);

                if (game == null)
                {
                    throw new KeyNotFoundException("Jogo não encontrado.");
                }

                if (discountPercentage < 0 || discountPercentage > 100)
                {
                    throw new ArgumentException("Porcentagem de desconto inválida.");
                }

                if (!game.IsOnPromotion)
                {
                    game.OriginalPrice = game.Price;
                }

                var discountAmount = game.OriginalPrice.Value * (discountPercentage / 100);
                game.Price = game.OriginalPrice.Value - discountAmount;

                game.IsOnPromotion = true;
                game.UpdateDate = DateTime.Now;

                await _gameRepository.UpdateGameAsync(game);


                return new UpdateGameResponse
                {
                    Id = game.Id,
                    Name = game.Name,
                    Price = game.Price,
                    Gender = game.Gender,
                    UpdatedAt = (DateTime)game.UpdateDate
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
