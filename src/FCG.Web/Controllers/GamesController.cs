using FCG.Application.Interfaces;
using FCG.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGameService gameService, ILogger<GamesController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest request)
        {
            try
            {
                var game = await _gameService.CreateGameAsync(request);
                _logger.LogInformation($"Jogo criado com sucesso: Id={game.Id}, Name={game.Name}");
                return Ok(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao criar jogo. Dados: Name={request.Name}");
                return StatusCode(500, "Erro interno ao criar jogo. Tente novamente.");
            }
        }

        [HttpGet("List")]
        public async Task<IActionResult> ListGames()
        {
            try
            {
                var games = await _gameService.GetAllGamesAsync();
                _logger.LogInformation($"Total de jogos encontrados: {games.Count}");
                return Ok(games);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar jogos.");
                return StatusCode(500, "Erro interno ao listar jogos. Tente novamente.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] UpdateGameRequest request)
        {
            try
            {
                var updatedGame = await _gameService.UpdateGameAsync(id, request);
                _logger.LogInformation($"Jogo atualizado com sucesso: Id={id}");
                return Ok(updatedGame);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Tentativa de atualizar jogo inexistente: Id={id}");
                return NotFound("Jogo não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar jogo: Id={id}");
                return StatusCode(500, "Erro interno ao atualizar jogo. Tente novamente.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                await _gameService.DeleteGameAsync(id);
                _logger.LogInformation($"Jogo deletado com sucesso: Id={id}");
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Tentativa de deletar jogo inexistente: Id={id}");
                return NotFound("Jogo não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar jogo: Id={id}");
                return StatusCode(500, "Erro interno ao deletar jogo. Tente novamente.");
            }
        }
    }
}
