using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LineTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Line connectionLine; // Die Linie

        public MainWindow()
        {
            InitializeComponent();
           
            this.SizeChanged += (s, e) => UpdateLine(); // Update bei Größenänderung
            this.Loaded += (s, e) => InitializeLine(); // Erst nach dem Laden starten
        }

        private void InitializeLine()
        {
            CreatePath();
            UpdateLine();
            this.SizeChanged += (s, e) => UpdateLine(); // Linie bei Fensteränderung aktualisieren
        }

        private void CreatePath()
        {
            connectionLine = new Line
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            // Path ins Grid einfügen
            MyGrid.Children.Add(connectionLine);
        }

        private void UpdateLine()
        {
            if (Button1 == null || Button2 == null || connectionLine == null) return;

            // Positionen der Buttons relativ zum Grid berechnen
            //Point startPoint = Button1.TransformToAncestor(MyGrid).Transform(new Point(Button1.ActualWidth / 2, Button1.ActualHeight / 2));
            //Point endPoint = Button2.TransformToAncestor(MyGrid).Transform(new Point(Button2.ActualWidth / 2, Button2.ActualHeight / 2));

            Point startPoint = Button1.TransformToAncestor(MyGrid).Transform(new Point(Button1.ActualWidth / 2, Button1.ActualHeight / 2));
            Point endPoint = Button2.TransformToAncestor(MyGrid).Transform(new Point(Button2.ActualWidth / 2, Button2.ActualHeight / 2));

            //Point startPoint = new Point() { X = 100, Y = 100 };
            //Point endPoint = new Point() { X = 500, Y = 500 };

            //// Knickpunkt berechnen (z. B. in der Mitte, 50 Pixel nach unten)
            //double midX = (startPoint.X + endPoint.X) / 2;
            //double midY = (startPoint.Y + endPoint.Y) / 2;

            //// PathGeometry mit Linien-Segmenten erzeugen
            //PathGeometry geometry = new PathGeometry();
            //PathFigure figure = new PathFigure { StartPoint = startPoint, IsClosed = false };
            //figure.Segments.Add(new LineSegment(new Point(midX, midY), true)); // Knickpunkt
            //figure.Segments.Add(new LineSegment(endPoint, true)); // Endpunkt
            //geometry.Figures.Add(figure);

            //// Linie setzen
            //connectionLine.Data = geometry;
            connectionLine.X1 = startPoint.X;
            connectionLine.Y1 = startPoint.Y;
            connectionLine.X2 = endPoint.X;
            connectionLine.Y2 = endPoint.Y;
        }
    }
}