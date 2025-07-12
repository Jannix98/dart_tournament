using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DartTournament.WPF.Controls.GameTreeControl.PositionCalculator
{
    public class FirstRoundPositionCalculator
    {
        double _maxScreenHeigth;
        double _singleItemHeight;
        double _singleItemWidth;
        int _itemCount;

        public FirstRoundPositionCalculator(double maxScreenHeigth, double singleItemHeight, double singleItemWidth, int itemCount)
        {
            _maxScreenHeigth = maxScreenHeigth;
            _singleItemHeight = singleItemHeight;
            _singleItemWidth = singleItemWidth;
            _itemCount = itemCount;
        }

        public Rect[] CalculatePositions()
        {
            Rect[] rects = new Rect[_itemCount];

            double totalHeight = _singleItemHeight * _itemCount;
            double verticalSpacing = (_maxScreenHeigth - totalHeight) / (_itemCount + 1);
            if (verticalSpacing < PositionConstants.VERTICAL_SPACING)
                verticalSpacing = PositionConstants.VERTICAL_SPACING; // Minimum spacing

            double horizontalSpacing = PositionConstants.HORIZONTAL_SPACING; // TODO remove
            int roundIndex = 0; // Assuming first round is at index 0
            for (int i = 0; i < _itemCount; i++)
            {
                double x = roundIndex * (_singleItemWidth + horizontalSpacing);
                // y = spacing + (itemHeight + spacing) * i
                double y = verticalSpacing + i * (_singleItemHeight + verticalSpacing);
                var rect = new Rect(new Point(x, y), new Size(_singleItemWidth, _singleItemHeight));
                rects[i] = rect;
            }

            return rects;
        }
    }
}
