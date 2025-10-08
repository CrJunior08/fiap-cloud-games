using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCG.Domain.Entities.Search;

namespace FCG.Domain.Entities.Search {
    public static class GameMappings {
        public static GameDocument ToGameDocument(this Game g) => new() {
            Id = g.Id.ToString(),
            Title = g.Name,
            Genre = g.Gender,
            Description = null,
            Platform = null,
            Rating = null,
            ReleaseDate = g.CreationDate,
            Tags = Array.Empty<string>()
        };
    }
}
