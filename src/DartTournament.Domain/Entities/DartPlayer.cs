using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    public class DartPlayer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public DartPlayer(string name)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

    }
}
