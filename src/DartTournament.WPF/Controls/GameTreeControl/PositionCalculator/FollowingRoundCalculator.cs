using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DartTournament.WPF.Controls.GameTreeControl.PositionCalculator
{
    internal class FollowingRoundCalculator
    {
        Rect[] _positionsOfPreviousRound;
        double _singleItemHeight;
        double _singleItemWidth;
        int _itemCount;
        int _roundIndex;

        public FollowingRoundCalculator(Rect[] positionsOfPreviousRound, double singleItemHeight, double singleItemWidth, int itemCount, int roundIndex)
        {
            _positionsOfPreviousRound = positionsOfPreviousRound;
            _singleItemHeight = singleItemHeight;
            _singleItemWidth = singleItemWidth;
            _itemCount = itemCount;
            _roundIndex = roundIndex;
        }

        public Rect[] CalculatePositions()
        {
            Rect[] rects = new Rect[_itemCount];
            for (int i = 0; i < _itemCount; i++)
            {
                (double minY, double maxY) = GetPreviosControlPosition(i); 
                double availableHeigth = maxY - minY;
                double restHeigth = availableHeigth - _singleItemHeight;
                double topSpacing = restHeigth / 2; // Center the item vertically in the available space
                double yPos = minY + topSpacing;
                double x = _roundIndex * (_singleItemWidth + PositionConstants.HORIZONTAL_SPACING);
                var rect = new Rect(new Point(x, yPos), new Size(_singleItemWidth, _singleItemHeight));
                rects[i] = rect;
            }
            return rects;
        }

        private (double minY, double maxY) GetPreviosControlPosition(int currentControlIndex)
        {
            int firstIndex = currentControlIndex * 2;
            int secondIndex = firstIndex + 1;

            Rect firstRect = _positionsOfPreviousRound[firstIndex];
            Rect secondRect = _positionsOfPreviousRound[secondIndex];

            double minY = firstRect.Y;
            double maxY = secondRect.Y + secondRect.Height;

            return (minY, maxY);
        }
    }
}
