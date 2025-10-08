using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCG.Domain.Entities.Search;

namespace FCG.Domain.Entities.Search {
    public sealed class GameDocument {
        public string Id { get; init; } = default!;
        public string Title { get; init; } = default!;
        public string Genre { get; init; } = default!;
        public string? Description { get; init; }
        public string? Platform { get; init; }
        public double? Rating { get; init; }
        public DateTime? ReleaseDate { get; init; }
        public string[] Tags { get; init; } = Array.Empty<string>();
    }
}
