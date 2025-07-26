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

            if (rounds.Count == 0)
                return finalSize;

            MatchControlPositionManager positionManager = new MatchControlPositionManager(rounds, finalSize.Height);
            positionManager.PositionControls();

            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            var matchControls = InternalChildren
                .OfType<ContentPresenter>()
                .Select(cp => FindVisualChild<MatchControl.MatchControl>(cp))
                .Where(mc => mc != null && mc.Match != null)
                .ToList();

            var rounds = matchControls
                .GroupBy(mc => mc.Match.RoundIndex)
                .OrderBy(g => g.Key)
                .ToList();

            if (rounds.Count < 2)
                return;

            for (int roundIndex = 0; roundIndex < rounds.Count - 1; roundIndex++)
            {
                var currentRound = rounds[roundIndex].ToList();
                var nextRound = rounds[roundIndex + 1].ToList();

                for (int i = 0; i < currentRound.Count; i += 2)
                {
                    var matchA = currentRound[i];
                    var matchB = (i + 1 < currentRound.Count) ? currentRound[i + 1] : null;
                    int nextIndex = i / 2;
                    if (nextIndex >= nextRound.Count)
                        continue;
                    var nextMatch = nextRound[nextIndex];

                    Point a = matchA.TransformToAncestor(this)
                        .Transform(new Point(matchA.ActualWidth, matchA.ActualHeight / 2));
                    Point b = matchB != null
                        ? matchB.TransformToAncestor(this).Transform(new Point(matchB.ActualWidth, matchB.ActualHeight / 2))
                        : a;
                    Point next = nextMatch.TransformToAncestor(this)
                        .Transform(new Point(0, nextMatch.ActualHeight / 2));

                    if (matchB != null)
                        DrawBracketLineNoOverlap(dc, a, b, next);
                    else
                        DrawBracketLine(dc, a, next);
                }
            }
        }

        // Draws a bracket line from two matches to a single next match, avoiding overlap
        private void DrawBracketLineNoOverlap(DrawingContext dc, Point a, Point b, Point to)
        {
            var pen = new Pen(Brushes.Black, 0.5);

            if (a.Y > b.Y)
            {
                var temp = a;
                a = b;
                b = temp;
            }

            double midX = a.X + (to.X - a.X) / 2;
            double midY = (a.Y + b.Y) / 2;

            Point aMid = new Point(midX, a.Y);
            Point bMid = new Point(midX, b.Y);

            dc.DrawLine(pen, a, aMid);
            dc.DrawLine(pen, b, bMid);
            dc.DrawLine(pen, aMid, bMid);

            Point midTo = new Point(midX, midY);
            Point horizontalTo = new Point(to.X, midY);
            dc.DrawLine(pen, midTo, horizontalTo);

            if (!horizontalTo.Equals(to))
                dc.DrawLine(pen, horizontalTo, to);
        }

        // Fallback for single match (no pair)
        private void DrawBracketLine(DrawingContext dc, Point from, Point to)
        {
            var pen = new Pen(Brushes.Blue, 1);

            double midX = from.X + (to.X - from.X) / 2;
            Point p1 = new Point(midX, from.Y);
            Point p2 = new Point(midX, to.Y);

            dc.DrawLine(pen, from, p1);
            dc.DrawLine(pen, p1, p2);
            dc.DrawLine(pen, p2, to);
        }
    }
}