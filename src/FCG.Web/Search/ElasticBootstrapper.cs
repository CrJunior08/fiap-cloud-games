using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Hosting;
using FCG.Domain.Entities.Search;

namespace FCG.Web.Search;

public sealed class ElasticBootstrapper(ElasticsearchClient client) : IHostedService {
    private const string GamesIndex = "games";
    private const string PromotionsIndex = "promotions";

    public async Task StartAsync(CancellationToken ct) {
        // cria índices vazios (mapping dinâmico cuida do resto em dev)
        if (!(await client.Indices.ExistsAsync(GamesIndex, ct)).Exists)
            await client.Indices.CreateAsync(GamesIndex, c => c
                .Settings(s => s.NumberOfShards(1).NumberOfReplicas(0)), ct);

        if (!(await client.Indices.ExistsAsync(PromotionsIndex, ct)).Exists)
            await client.Indices.CreateAsync(PromotionsIndex, c => c
                .Settings(s => s.NumberOfShards(1).NumberOfReplicas(0)), ct);
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}