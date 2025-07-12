using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            double itemWidth = 200; // TODO remove
            double itemHeight = 100; // TODO remove
            double horizontalSpacing = 40;

            

            // Get MatchControl instances
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

            var firstRound = rounds[0].Select(x => x).ToList();

            PositionFirstRound(firstRound, finalSize);

            //foreach (var roundGroup in rounds)
            //{
            //    int roundIndex = roundGroup.Key;
            //    int matchCount = roundGroup.Count();

            //    // Calculate total height occupied by all matches
            //    double matchesHeight = matchCount * itemHeight;
            //    // Calculate the space to distribute between matches (including top and bottom)
            //    double totalSpacing = finalSize.Height - matchesHeight;
            //    double spacing = matchCount > 1
            //        ? totalSpacing / (matchCount + 1)
            //        : totalSpacing / 2; // Center if only one

            //    int i = 0;
            //    foreach (var child in roundGroup)
            //    {
            //        double x = roundIndex * (itemWidth + horizontalSpacing);
            //        // y = spacing + (itemHeight + spacing) * i
            //        double y = spacing + i * (itemHeight + spacing);

            //        child.Arrange(new Rect(new Point(x, y), new Size(itemWidth, itemHeight)));
            //        i++;
            //    }
            //}

            return finalSize;
        }

        private void PositionFirstRound(List<MatchControl.MatchControl> firstRound, Size finalSize)
        {
            int matchesInFirstRound = firstRound.Count();
            if(firstRound.Select(x => x.Height).Distinct().Count() > 1)
                throw new InvalidOperationException("All matches in the first round must have the same height.");

            if(firstRound.Select(x => x.Width).Distinct().Count() > 1)
                throw new InvalidOperationException("All matches in the first round must have the same width.");

            double itemHeight = firstRound.First().Height;
            double itemWidth = firstRound.First().Width;
            double totalHeight = itemHeight * matchesInFirstRound;
            double verticalSpacing = (finalSize.Height - totalHeight) / (matchesInFirstRound + 1);
            if(verticalSpacing < 20)
                verticalSpacing = 20; // Minimum spacing

            int i = 0;
            int horizontalSpacing = 40; // TODO remove
            int roundIndex = 0; // Assuming first round is at index 0
            foreach (var child in firstRound)
            {
                double x = roundIndex * (itemWidth + horizontalSpacing);
                // y = spacing + (itemHeight + spacing) * i
                double y = verticalSpacing + i * (itemHeight + verticalSpacing);

                child.Arrange(new Rect(new Point(x, y), new Size(itemWidth, itemHeight)));
                i++;
            }
        }
    }
}