using DartTournament.WPF.Models;
using DartTournament.WPF.NotifyPropertyChange;

namespace DartTournament.WPF.Models;

internal class SelectableDartPlayerUI : NotifyPropertyChanged
{
    private DartPlayerUI _player;
    private bool _isSelected;

    public SelectableDartPlayerUI(DartPlayerUI player)
    {
        _player = player;
    }

    public DartPlayerUI Player
    {
        get => _player;
        set => SetProperty(ref _player, value);
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public string Name => Player?.Name ?? string.Empty;
}