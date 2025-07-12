using DartTournament.WPF.NotifyPropertyChange;
using System.Collections.ObjectModel;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public class GameTreeViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<RoundViewModel> Rounds { get; set; } = new();

        public ObservableCollection<MatchViewModel> AllMatches
        {
            get
            {
                var all = new ObservableCollection<MatchViewModel>();
                for (int round = 0; round < Rounds.Count; round++)
                {
                    for (int match = 0; match < Rounds[round].Matches.Count; match++)
                    {
                        var m = Rounds[round].Matches[match];
                        m.RoundIndex = round;
                        m.MatchIndex = match;
                        all.Add(m);
                    }
                }
                return all;
            }
        }
    }

    public class RoundViewModel
    {
        public ObservableCollection<MatchViewModel> Matches { get; set; } = new();
    }

    public class MatchViewModel
    {
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public int RoundIndex { get; set; }
        public int MatchIndex { get; set; }
    }
}