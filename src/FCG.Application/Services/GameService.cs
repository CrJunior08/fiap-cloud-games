using FCG.Application.Interfaces;
using FCG.Application.Requests;
using FCG.Application.Responses;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;

namespace FCG.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<CreateGameResponse> CreateGameAsync(CreateGameRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Name)) {
                throw new ArgumentException("Nome do jogo é obrigatório");
            }

            var game = new Game
            {
                Name = request.Name,
                Price = request.Price,
                Gender = request.Gender
            };

            try
            {
                await _gameRepository.CreateGameAsync(game);

                return new CreateGameResponse
                {
                    Id = game.Id,
                    Name = game.Name,
                    Price = game.Price,
                    Gender = game.Gender,
                    CreatedAt = game.CreationDate
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            try
            {
                var games = await _gameRepository.GetAllGamesAsync();
                return games;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UpdateGameResponse> UpdateGameAsync(int id, UpdateGameRequest request)
        {
            try
            {
                var game = await _gameRepository.GetGameByIdAsync(id);

                if (game == null)
                {
                    throw new KeyNotFoundException("Jogo não encontrado.");
                }

                game.Name = request.Name;
                game.Price = request.Price;
                game.Gender = request.Gender;
                game.UpdateDate = DateTime.UtcNow;

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

        public async Task DeleteGameAsync(int id)
        {
            try
            {
                var game = await _gameRepository.GetGameByIdAsync(id);

                if (game == null)
                {
                    throw new KeyNotFoundException("Jogo não encontrado.");
                }

                await _gameRepository.DeleteGameAsync(game);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
