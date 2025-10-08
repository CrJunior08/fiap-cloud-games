using FCG.Domain.Entities;
using FCG.Domain.Interfaces.Repository;
using FCG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FCG.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GameRepository> _logger;

        public GameRepository(AppDbContext context, ILogger<GameRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateGameAsync(Game game)
        {
            try
            {
                await _context.Games.AddAsync(game);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Jogo criado no banco: Id={game.Id}, Name={game.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao criar jogo: Name={game.Name}");
                throw;
            }
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            try
            {
                var games = await _context.Games.ToListAsync();
                _logger.LogInformation($"Total de jogos no banco: {games.Count}");
                return games;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar jogos.");
                throw;
            }
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            try
            {
                var game = await _context.Games.FindAsync(id);
                if (game != null)
                {
                    _logger.LogInformation($"Jogo encontrado: Id={game.Id}, Name={game.Name}");
                }
                else
                {
                    _logger.LogWarning($"Jogo não encontrado: Id={id}");
                }
                return game;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar jogo por Id={id}");
                throw;
            }
        }

        public async Task UpdateGameAsync(Game game)
        {
            try
            {
                _context.Games.Update(game);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Jogo atualizado no banco: Id={game.Id}, Name={game.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar jogo: Id={game.Id}");
                throw;
            }
        }

        public async Task DeleteGameAsync(Game game)
        {
            try
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Jogo deletado do banco: Id={game.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar jogo: Id={game.Id}");
                throw;
            }
        }
    }
}
