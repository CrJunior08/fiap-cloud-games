using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces.Repository
{
    public interface IGameRepository
    {
        Task CreateGameAsync(Game game);
        Task<List<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(Game game);
    }
}
