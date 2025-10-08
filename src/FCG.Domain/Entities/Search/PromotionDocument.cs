using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCG.Domain.Entities.Search;

namespace FCG.Domain.Entities.Search {
    public sealed class PromotionDocument {
        public string Id { get; init; } = default!;
        public string GameId { get; init; } = default!;
        public string Title { get; init; } = default!;
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public decimal? OldPrice { get; init; }
        public DateTime StartAt { get; init; }
        public DateTime EndAt { get; init; }
    }
}
