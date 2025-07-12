using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DartTournament.WPF.Controls.GameTreeControl.PositionCalculator;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public class GameTreePanel : Panel
    {
        public GameTreePanel()
        { 
            Background = new SolidColorBrush(Colors.Transparent);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double totalWidth = 0;
            double totalHeight = 0;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                totalWidth += child.DesiredSize.Width;
                totalHeight += child.DesiredSize.Height;
            }

            return new Size(totalWidth, totalHeight);
        }

        // Helper to get the first child of a specific type in the visual tree
        static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild)
                    return tChild;
                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var matchControls = InternalChildren
                .OfType<ContentPresenter>()
                .Select(cp => FindVisualChild<MatchControl.MatchControl>(cp))
                .Where(mc => mc != null)
                .ToList();

            // 1. Group children by round
            var rounds = matchControls
                .Where(mc => mc.Match != null)
                .GroupBy(mc => mc.Match.RoundIndex)
                .OrderBy(g => g.Key)
                .ToList();

            MatchControlPositionManager positionManager = new MatchControlPositionManager(rounds, finalSize.Height);
            positionManager.PositionControls();

            return finalSize;
        }
    }
}