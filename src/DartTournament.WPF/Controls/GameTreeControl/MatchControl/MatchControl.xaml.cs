using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DartTournament.WPF.Controls.GameTreeControl.MatchControl
{
    /// <summary>
    /// Interaction logic for MatchControl.xaml
    /// </summary>
    public partial class MatchControl : UserControl
    {
        public static readonly DependencyProperty Player1Property =
            DependencyProperty.Register(
                "Player1",
                typeof(string),
                typeof(MatchControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty Player2Property =
            DependencyProperty.Register(
                "Player2",
                typeof(string),
                typeof(MatchControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MatchRoundIndexProperty =
            DependencyProperty.Register(
                "MatchRoundIndex",
                typeof(int),
                typeof(MatchControl),
                new PropertyMetadata(-1));

        public static readonly DependencyProperty ChooseWinnerCommandProperty =
            DependencyProperty.Register(
                "ChooseWinnerCommand",
                typeof(ICommand),
                typeof(MatchControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ChooseWinnerCommandParameterProperty =
            DependencyProperty.Register(
                "ChooseWinnerCommandParameter",
                typeof(object),
                typeof(MatchControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty Player1IdProperty =
            DependencyProperty.Register(
                "Player1Id",
                typeof(Guid),
                typeof(MatchControl),
                new PropertyMetadata(Guid.Empty));

        public static readonly DependencyProperty Player2IdProperty =
            DependencyProperty.Register(
                "Player2Id",
                typeof(Guid),
                typeof(MatchControl),
                new PropertyMetadata(Guid.Empty));

        public static readonly DependencyProperty WinnerIdProperty =
            DependencyProperty.Register(
                "WinnerId",
                typeof(Guid),
                typeof(MatchControl),
                new PropertyMetadata(Guid.Empty));

        public string Player1
        {
            get => (string)GetValue(Player1Property);
            set => SetValue(Player1Property, value);
        }

        public string Player2
        {
            get => (string)GetValue(Player2Property);
            set => SetValue(Player2Property, value);
        }

        public int MatchRoundIndex
        {
            get => (int)GetValue(MatchRoundIndexProperty);
            set => SetValue(MatchRoundIndexProperty, value);
        }

        public ICommand ChooseWinnerCommand
        {
            get => (ICommand)GetValue(ChooseWinnerCommandProperty);
            set => SetValue(ChooseWinnerCommandProperty, value);
        }

        public object ChooseWinnerCommandParameter
        {
            get => GetValue(ChooseWinnerCommandParameterProperty);
            set => SetValue(ChooseWinnerCommandParameterProperty, value);
        }

        public Guid Player1Id
        {
            get => (Guid)GetValue(Player1IdProperty);
            set => SetValue(Player1IdProperty, value);
        }

        public Guid Player2Id
        {
            get => (Guid)GetValue(Player2IdProperty);
            set => SetValue(Player2IdProperty, value);
        }

        public Guid WinnerId
        {
            get => (Guid)GetValue(WinnerIdProperty);
            set => SetValue(WinnerIdProperty, value);
        }

        public MatchControl()
        {
            InitializeComponent();
        }

    }
}
