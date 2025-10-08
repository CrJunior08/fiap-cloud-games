using FCG.Application.Requests;
using FCG.Application.Responses;
using FCG.Domain.Entities;

namespace FCG.Application.Interfaces
{
    public interface IGameService
    {
        Task<CreateGameResponse> CreateGameAsync(CreateGameRequest request);
        Task<List<Game>> GetAllGamesAsync();
        Task<UpdateGameResponse> UpdateGameAsync(int id, UpdateGameRequest request);
        Task DeleteGameAsync(int id);
    }
}
