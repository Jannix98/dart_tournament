using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament.Domain.Entities
{
    // TODO: possible a DTO
    public class TeamTournament
    {
        public Team Team { get; set; }
        public int Score { get; set; }

        public TeamTournament(Team team, int score)
        {
            Team = team;
            Score = score;
        }
    }
}
