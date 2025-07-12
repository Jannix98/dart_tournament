using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DartTournament.WPF.Controls.GameTreeControl.PositionCalculator;

namespace DartTournament.WPF.Controls.GameTreeControl
{
    public class BracketPanel : Panel
    {
        public static readonly DependencyProperty RoundIndexProperty =
            DependencyProperty.RegisterAttached(
                "RoundIndex",
                typeof(int),
                typeof(BracketPanel),
                new FrameworkPropertyMetadata(default(int)));

        public static readonly DependencyProperty MatchIndexProperty =
            DependencyProperty.RegisterAttached(
                "MatchIndex",
                typeof(int),
                typeof(BracketPanel),
                new FrameworkPropertyMetadata(default(int)));

        public static int GetRoundIndex(UIElement element)
        {
            return (int)element.GetValue(RoundIndexProperty);
        }

        public static void SetRoundIndex(UIElement element, int value)
        {
            element.SetValue(RoundIndexProperty, value);
        }

        public static int GetMatchIndex(UIElement element)
        {
            return (int)element.GetValue(MatchIndexProperty);
        }

        public static void SetMatchIndex(UIElement element, int value)
        {
            element.SetValue(MatchIndexProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double maxWidth = 0;
            double totalHeight = 0;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                maxWidth = Math.Max(maxWidth, child.DesiredSize.Width);
                totalHeight += child.DesiredSize.Height;
            }

            // Add spacing if needed, or calculate based on your layout logic
            // For a bracket, you may want to calculate width/height based on rounds and matches

            return new Size(maxWidth, totalHeight);
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