using Microsoft.AspNetCore.Mvc;
using Elastic.Clients.Elasticsearch;

namespace FCG.Web.Controllers {

    [ApiController]
    [Route("health")]

    public class HealthController : ControllerBase {
        private readonly ElasticsearchClient _es;
        public HealthController(ElasticsearchClient es) => _es = es;

        [HttpGet("es")]

        public async Task<IActionResult> Es() {
            var info = await _es.InfoAsync();
            return Ok(new { info.Version.Number, info.Tagline });
        }
    }
}