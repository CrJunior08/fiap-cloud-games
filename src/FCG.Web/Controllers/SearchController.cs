using Elastic.Clients.Elasticsearch;
using FCG.Domain.Entities.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using FCG.Domain.Entities.Search;

namespace FCG.Web.Controllers;

[ApiController]
[Route("search")]

public class SearchController : ControllerBase {
    private readonly ElasticsearchClient _es;
    public SearchController(ElasticsearchClient es) => _es = es;

    [HttpPost("games/reindex")]

    public async Task<IActionResult> ReindexGames(CancellationToken ct) {
        var sample = new[]
        {
            new GameDocument { Id="1", Title="Zelda", Genre="RPG", Description="Explore Hyrule", Rating=4.9, Platform="Switch", ReleaseDate=new DateTime(2023,5,12), Tags=new[]{"open-world","adventure"} },
            new GameDocument { Id="2", Title="Forza Horizon 5", Genre="Racing", Description="Arcade racing", Rating=4.7, Platform="Xbox",  ReleaseDate=new DateTime(2021,11,9), Tags=new[]{"cars","arcade"} },
        };

        var count = 0;
        foreach (var g in sample) {
            var resp = await _es.IndexAsync(g, i => i.Index("games").Id(g.Id), ct);
            if (resp.Result == Result.Created || resp.Result == Result.Updated) count++;
        }
        return Ok(new { Created = count });
    }

    [HttpPost("promotions/reindex")]

    public async Task<IActionResult> ReindexPromotions(CancellationToken ct) {
        var sample = new[]
        {
            new PromotionDocument { Id="p1", GameId="2", Title="FH5 - Week Sale", Description="-30%", Price=199.90m, OldPrice=289.90m, StartAt=DateTime.UtcNow.Date, EndAt=DateTime.UtcNow.Date.AddDays(7) }
        };

        var count = 0;
        foreach (var p in sample) {
            var resp = await _es.IndexAsync(p, i => i.Index("promotions").Id(p.Id), ct);
            if (resp.Result == Result.Created || resp.Result == Result.Updated) count++;
        }
        return Ok(new { Created = count });
    }

    [HttpGet("games")]

    public async Task<IActionResult> SearchGames(
     [FromQuery] string? q,
     [FromQuery] string? genre,
     [FromQuery] string? platform,
     [FromQuery] int page = 1,
     [FromQuery] int size = 10,
     CancellationToken ct = default) {
        page = Math.Max(page, 1);
        size = size is > 0 and <= 100 ? size : 10;

        var res = await _es.SearchAsync<GameDocument>(s => s
            .Index("games")
            .From((page - 1) * size)
            .Size(size)
            .Query(qd => qd.Bool(b => {
                if (!string.IsNullOrWhiteSpace(q)) {
                    b.Must(m => m.MultiMatch(mm => mm
                        .Query(q!)
                        .Fields(new[] { "title^3", "description", "genre^2", "tags" })
                        .Fuzziness(new Fuzziness("AUTO"))));
                }

                if (!string.IsNullOrWhiteSpace(genre))
                    b.Filter(f => f.Term(t => t.Field("genre.keyword").Value(genre!)));

                if (!string.IsNullOrWhiteSpace(platform))
                    b.Filter(f => f.Term(t => t.Field("platform.keyword").Value(platform!)));
            })), ct);

        return Ok(new {
            res.HitsMetadata.Total,
            Items = res.Hits.Select(h => new { h.Source!.Id, h.Source!.Title, Score = h.Score })
        });
    }


    [HttpGet("promotions")]
    public async Task<IActionResult> SearchPromotions(
        [FromQuery] string? q,
        [FromQuery] DateTime? at,
        CancellationToken ct = default) {
        var when = at ?? DateTime.UtcNow;

        var res = await _es.SearchAsync<PromotionDocument>(s => s
            .Index("promotions")
            .Size(20)
            .Query(qd => qd.Bool(b => {
                if (!string.IsNullOrWhiteSpace(q)) {
                    b.Must(m => m.MultiMatch(mm => mm
                        .Query(q!)
                        .Fields(new[] { "title^2", "description" })));
                }
            })), ct);

        return Ok(res.Documents);
    }

}