using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DartTournament.WPF.Controls.GameSession
{
    public class GameSessionVM : INotifyPropertyChanged
    {
        private string _title;
        private bool _showLooserRound;
        private int _selectedTabIndex;
        private UserControl _gameContent;
        private UserControl _looserRoundContent;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool ShowLooserRound
        {
            get => _showLooserRound;
            set => SetProperty(ref _showLooserRound, value);
        }

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                if (SetProperty(ref _selectedTabIndex, value))
                {
                    UpdateCurrentContent();
                }
            }
        }

        public UserControl CurrentContent
        {
            get
            {
                return SelectedTabIndex switch
                {
                    0 => _gameContent,
                    1 => _looserRoundContent,
                    _ => _gameContent
                };
            }
        }

        public GameSessionVM(string title, bool showLooserRound)
        {
            _title = title;
            _showLooserRound = showLooserRound;
            _selectedTabIndex = 0;
        }

        public void SetGameContent(UserControl content)
        {
            _gameContent = content;
            UpdateCurrentContent();
        }

        public void SetLooserRoundContent(UserControl content)
        {
            _looserRoundContent = content;
            UpdateCurrentContent();
        }

        private void UpdateCurrentContent()
        {
            OnPropertyChanged(nameof(CurrentContent));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
