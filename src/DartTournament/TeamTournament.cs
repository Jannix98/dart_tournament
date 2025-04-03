using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTournament
{
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

    public class Team
    {
        public Team(string name)
        {
            Name = name;
        }

        public string Name { get; set; }


    }
}
