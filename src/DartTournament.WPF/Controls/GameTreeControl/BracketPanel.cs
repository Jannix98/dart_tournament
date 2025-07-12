using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            foreach (UIElement child in InternalChildren)
                child.Measure(new Size(250, 2550));
            return new Size(250, 250);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double itemWidth = 200; // TODO remove
            double itemHeight = 100; // TODO remove
            double horizontalSpacing = 40;

            // 1. Group children by round
            var rounds = InternalChildren
                .OfType<FrameworkElement>()
                .Where(fe => fe.DataContext is MatchViewModel)
                .GroupBy(fe => ((MatchViewModel)fe.DataContext).RoundIndex)
                .OrderBy(g => g.Key)
                .ToList();

            foreach (var roundGroup in rounds)
            {
                int roundIndex = roundGroup.Key;
                int matchCount = roundGroup.Count();

                // Calculate total height occupied by all matches
                double matchesHeight = matchCount * itemHeight;
                // Calculate the space to distribute between matches (including top and bottom)
                double totalSpacing = finalSize.Height - matchesHeight;
                double spacing = matchCount > 1
                    ? totalSpacing / (matchCount + 1)
                    : totalSpacing / 2; // Center if only one

                int i = 0;
                foreach (var child in roundGroup)
                {
                    double x = roundIndex * (itemWidth + horizontalSpacing);
                    // y = spacing + (itemHeight + spacing) * i
                    double y = spacing + i * (itemHeight + spacing);

                    child.Arrange(new Rect(new Point(x, y), new Size(itemWidth, itemHeight)));
                    i++;
                }
            }

            return finalSize;
        }
    }
}