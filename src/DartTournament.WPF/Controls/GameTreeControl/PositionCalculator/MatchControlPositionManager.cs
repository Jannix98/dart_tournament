using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DartTournament.WPF.Controls.GameTreeControl.PositionCalculator
{
    internal class MatchControlPositionManager
    {
        List<IGrouping<int, MatchControl.MatchControl>> _rounds;
        double _availableHeight;

        public MatchControlPositionManager(List<IGrouping<int, MatchControl.MatchControl>> rounds, double availableHeight)
        {
            _rounds = rounds;
            _availableHeight = availableHeight;
        }

        public void PositionControls()
        {
            if (_rounds == null || _rounds.Count == 0)
                return;

            var firstRound = _rounds[0].Select(x => x).ToList();

            var firstRoundPositions = GetFirstRoundPositions(firstRound);
            PositionControl(firstRound, firstRoundPositions);

            PositionOtherRounds(_rounds, firstRoundPositions);

        }

        private static void PositionOtherRounds(List<IGrouping<int, MatchControl.MatchControl>> rounds, Rect[] firstRoundPositions)
        {
            Rect[] previousRoundPositions = firstRoundPositions;

            for (int i = 1; i < rounds.Count; i++)
            {
                var controls = rounds[i].Select(x => x).ToList();
                GetMatchControlVariables(controls, out int itemCount, out double itemHeight, out double itemWidth);

                FollowingRoundCalculator positionCalculator = new FollowingRoundCalculator(
                    previousRoundPositions,
                    itemHeight,
                    itemWidth,
                    itemCount,
                    i);

                var positions = positionCalculator.CalculatePositions();

                PositionControl(controls, positions);
                previousRoundPositions = positions;
            }
        }

        private Rect[] GetFirstRoundPositions(List<MatchControl.MatchControl> firstRound)
        {
            int matchesInFirstRound;
            double itemHeight, itemWidth;
            GetMatchControlVariables(firstRound, out matchesInFirstRound, out itemHeight, out itemWidth);
            FirstRoundPositionCalculator positionCalculator = new FirstRoundPositionCalculator(
                _availableHeight, itemHeight, itemWidth, matchesInFirstRound);

            var rects = positionCalculator.CalculatePositions();
            return rects;
        }

        private static void GetMatchControlVariables(List<MatchControl.MatchControl> controls, out int controlCount, out double controlHeigth, out double controlWidth)
        {
            controlCount = controls.Count();
            if (controls.Select(x => x.Height).Distinct().Count() > 1)
                throw new InvalidOperationException("All matches in the first round must have the same height.");

            if (controls.Select(x => x.Width).Distinct().Count() > 1)
                throw new InvalidOperationException("All matches in the first round must have the same width.");

            controlHeigth = controls.First().Height;
            controlWidth = controls.First().Width;
        }

        private static void PositionControl(List<MatchControl.MatchControl> controls, Rect[] rects)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                var child = controls[i];
                child.Arrange(rects[i]);
            }
        }
    }
}
