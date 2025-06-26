using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    public class DartPlayer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } // TODO: change back to private when DTO is implemented

        [JsonConstructor]
        public DartPlayer(Guid id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public DartPlayer(string name)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

    }
}
