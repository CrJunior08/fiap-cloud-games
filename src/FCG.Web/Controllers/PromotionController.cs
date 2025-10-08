using FCG.Application.Interfaces;
using FCG.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        private readonly ILogger<PromotionController> _logger;

        public PromotionController(IPromotionService promotionService, ILogger<PromotionController> logger)
        {
            _promotionService = promotionService;
            _logger = logger;
        }

        [HttpPost("ApplyDiscount")]
        public async Task<IActionResult> ApplyDiscount([FromBody] ApplyDiscountRequest request)
        {
            try
            {
                var updatedGame = await _promotionService.ApplyDiscountAsync(request.GameId, request.DiscountPercentage);
                _logger.LogInformation($"Desconto de {request.DiscountPercentage}% aplicado ao jogo {updatedGame.Name}.");
                return Ok(updatedGame);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning($"Jogo não encontrado: {request.GameId}");
                return NotFound("Jogo não encontrado.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"Erro de validação ao aplicar desconto ao jogo {request.GameId}.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro interno ao aplicar desconto ao jogo {request.GameId}.");
                return StatusCode(500, "Erro interno ao aplicar desconto. Tente novamente.");
            }
        }
    }
}
