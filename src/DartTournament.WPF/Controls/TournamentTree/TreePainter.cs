using DartTournament.Domain.Entities;
using DartTournament.WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DartTournament.WPF.Controls.TournamentTree
{
    internal class TreePainter
    {

        public List<UIElement> CreateTournamentTree(int maxPlayers, List<DartPlayerUI> players)
        {
            List<UIElement> uIElements = new List<UIElement>();
            // TODO add parameter
            int score = 501;
            int teamsInFirstRound = maxPlayers;
            int rounds = RoundCalculator.GetRoundCount(teamsInFirstRound);
            int teamCount = teamsInFirstRound;
            for (int i = 0; i < rounds; i++)
            {
                if (i + 1 == rounds)
                {
                    teamCount = 1; // lastRound
                }
                var control = GetTournamentRoundControl(teamCount, score);
                uIElements.Add(control);
                teamCount = teamCount / 2;
            }

            RoundControl first = uIElements[0] as RoundControl;
            if(first == null)
            {
                throw new ArgumentException("First round control cannot be null.");
            }
            first.FillNames(players.Select(p => p.Name).ToList());

            return uIElements;
        }

        private RoundControl GetTournamentRoundControl(int teamCount, int score)
        {
            var list1 = GetControls(teamCount, score);
            RoundControl roundControl = new RoundControl();
            roundControl.FillGrid(list1);
            return roundControl;
        }

        private List<TeamTournamentControl> GetControls(int amount, int score)
        {
            List<TeamTournamentControl> controls = new List<TeamTournamentControl>();
            for (int i = 0; i < amount; i++)
            {
                controls.Add(CreateTeamTournamentControl(i, score));
            }
            return controls;
        }

        private TeamTournamentControl CreateTeamTournamentControl(int i, int score)
        {
            // TODO: replace with some kind of team source
            DartPlayer team1 = new DartPlayer($"Team {i + 1}");
            // TODO: implement when DTO is ready
            //TeamTournament teamTournament1 = new TeamTournament(team1, score);
            TeamTournamentControl control1 = new TeamTournamentControl();
            return control1;
        }
    }
}
