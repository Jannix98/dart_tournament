using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DartTournament.WPF.Controls.TournamentTree
{
    internal class LineManager
    {
        Grid _grid;
        Canvas _canvas;

        public LineManager(Grid grid, Canvas canvas)
        {
            _grid = grid;
            _canvas = canvas;
        }

        public void CreateLinesBetweenMatches(UIElementCollection uIElementCollection)
        {
            List<Point> startingPoints = new List<Point>();
            List<Point> endingPoints = new List<Point>();

            for (int i = 0; i < uIElementCollection.Count - 1; i++)
            {
                startingPoints.Clear();
                endingPoints.Clear();
                var control1 = uIElementCollection[i];
                var control2 = uIElementCollection[i + 1];

                if (!(control1 is RoundControl leftControl))
                    throw new Exception("Can't be. Wrong type");

                if (!(control2 is RoundControl rightControl))
                    throw new Exception("Can't be. Wrong type");

                FillStartingPoints(leftControl, startingPoints);
                FillEndingPoints(rightControl, endingPoints);

                if (endingPoints.Count != 1)
                {
                    if (startingPoints.Count != endingPoints.Count * 2)
                    {
                        throw new Exception("UnEven X Points");
                    }
                }

                int count = startingPoints.Count;
                for (int j = 0; j < count; j += 2)
                {
                    Point startPoint1 = startingPoints[j];
                    Point startPoint2 = startingPoints[j + 1];

                    Point endPoint = GetEndPoint(endingPoints, j);

                    _canvas.Children.Add(CreatePolyLineControl(startPoint1, endPoint));
                    _canvas.Children.Add(CreatePolyLineControl(startPoint2, endPoint));

                }
            }
        }

        private static Line CreateLine(Point startPoint1, Point endPoint)
        {
            Line line = new Line();
            line.X1 = startPoint1.X;
            line.Y1 = startPoint1.Y;
            line.X2 = endPoint.X;
            line.Y2 = endPoint.Y;
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.StrokeThickness = 1;
            return line;
        }

        private static Polyline CreatePolyLineControl(Point startPoint1, Point endPoint)
        {
            int value = 48;
            Polyline path = new Polyline();
            path.Points.Add(startPoint1);
            path.Points.Add(new Point(startPoint1.X + value, startPoint1.Y));
            path.Points.Add(new Point(endPoint.X - value, endPoint.Y));
            path.Points.Add(endPoint);
            path.Stroke = new SolidColorBrush(Colors.Black);
            path.StrokeThickness = 1;
            return path;
        }

        private static Point GetEndPoint(List<Point> endingPoints, int j)
        {
            if (j == 0 || j == 1)
                return endingPoints[0];
            else if (j >= 2)
                return endingPoints[j / 2];

            throw new Exception("No Ending Point found");
        }

        private void FillStartingPoints(RoundControl control, List<Point> points)
        {
            List<TeamTournamentControl> roundControls = control.GetTeamTournamentControls();

            foreach (var uiElement in roundControls)
            {
                Point point = uiElement.TransformToAncestor(_grid).Transform(new Point(uiElement.ActualWidth, uiElement.ActualHeight / 2));
                points.Add(point);
            }
        }

        private void FillEndingPoints(RoundControl control, List<Point> points)
        {
            List<TeamTournamentControl> roundControls = control.GetTeamTournamentControls();

            foreach (var uiElement in roundControls)
            {
                Point point = uiElement.TransformToAncestor(_grid).Transform(new Point(0, uiElement.ActualHeight / 2));
                points.Add(point);
            }
        }
    }
}
