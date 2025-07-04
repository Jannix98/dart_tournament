﻿using System;
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

namespace DartTournament.WPF
{
    /// <summary>
    /// Interaction logic for RoundControl.xaml
    /// </summary>
    public partial class RoundControl : UserControl
    {
        public RoundControl(List<TeamTournamentControl> controls) : this()
        {
            FillGrid(controls);
        }

        public RoundControl()
        {
            InitializeComponent();

        }

        public void FillGrid(List<TeamTournamentControl> controls)
        {
            for (int i = 0; i < controls.Count; i++) 
            {
                roundGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Grid.SetRow(controls[i], i); // Weist das UserControl der jeweiligen Spalte zu
                roundGrid.Children.Add(controls[i]);
            }
        }

        public UIElementCollection GetControls()
        {
            return roundGrid.Children;
        }

        public List<TeamTournamentControl> GetTeamTournamentControls()
        {
            var controls = GetControls();
            List<TeamTournamentControl> roundControls = new List<TeamTournamentControl>();
            foreach (UIElement element in controls)
            {
                if (element is TeamTournamentControl myControl)
                {
                    roundControls.Add(myControl);
                }
            }

            if (roundControls.Count != 1 && roundControls.Count % 2 != 0)
                throw new Exception("Uneven controls");
            return roundControls;
        }
    }
}
