using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeometryClasses;

namespace GeometryClassesInterface
{
    public partial class MainWindow : Window
    {
        private Grid currentGrid;
        private System.Windows.Point canvasCenter;
        private Dictionary<Shape, IShape> shapesMap;
        private int numberOfPoints;
        private double radiusValue;

        public MainWindow()
        {
            InitializeComponent();
            numberOfPoints = 1;
            shapesMap = new Dictionary<Shape, IShape>();
            currentGrid = MainFormGrid;
            canvasCenter = new System.Windows.Point(Width * 5.0 / 8.0 / 2.0, Height * 51.0 / 116.0);
            CreateAxis();
            CreatePointFields();
        }

        private void CreateAxis()
        {
            Line element1 = new Line();
            Line element2 = new Line();
            element2.X1 = Width * 5.0 / 8.0 / 2.0;
            element2.X2 = Width * 5.0 / 8.0 / 2.0;
            element2.Y1 = Height * 2.0 / 29.0;
            element2.Y2 = Height * 49.0 / 58.0;
            element2.StrokeThickness = 2.0;
            element2.Visibility = Visibility.Visible;
            element2.Stroke = (Brush)Brushes.Black;
            element1.X1 = Width * 1.0 / 32.0;
            element1.X2 = Width * 39.0 / 64.0;
            element1.Y1 = Height * 51.0 / 116.0;
            element1.Y2 = Height * 51.0 / 116.0;
            element1.StrokeThickness = 2.0;
            element1.Visibility = Visibility.Visible;
            element1.Stroke = (Brush)Brushes.Black;
            MainCanvas.Children.Add((UIElement)element1);
            MainCanvas.Children.Add((UIElement)element2);
        }

        private void CreatePointFields()
        {
            CreateSixPointFields(FirstToSixthPointsGrid, 1);
            CreateSixPointFields(SeventhToTwlelvthPointsGrid, 7);
            CreateSixPointFields(ThirteenthToEighteenthPointsGrid, 13);
        }

        private void CreateSixPointFields(Grid parentalGrid, int start)
        {
            for (int index = 0; index < 6; ++index)
            {
                TextBlock element1 = new TextBlock();
                element1.Text = (index + start).ToString() + " point";
                Grid.SetColumn((UIElement)element1, 1);
                Grid.SetRow((UIElement)element1, 2 * index);
                Grid.SetColumnSpan((UIElement)element1, 3);
                element1.VerticalAlignment = VerticalAlignment.Center;
                TextBlock element2 = new TextBlock();
                element2.Text = "x:";
                Grid.SetColumn((UIElement)element2, 0);
                Grid.SetRow((UIElement)element2, 2 * index + 1);
                element2.HorizontalAlignment = HorizontalAlignment.Center;
                element2.VerticalAlignment = VerticalAlignment.Center;
                TextBox element3 = new TextBox();
                Grid.SetColumn((UIElement)element3, 1);
                Grid.SetRow((UIElement)element3, 2 * index + 1);
                element3.VerticalContentAlignment = VerticalAlignment.Center;
                element3.IsEnabled = false;
                TextBlock element4 = new TextBlock();
                element4.Text = "y:";
                Grid.SetColumn((UIElement)element4, 2);
                Grid.SetRow((UIElement)element4, 2 * index + 1);
                element4.HorizontalAlignment = HorizontalAlignment.Center;
                element4.VerticalAlignment = VerticalAlignment.Center;
                TextBox element5 = new TextBox();
                Grid.SetColumn((UIElement)element5, 3);
                Grid.SetRow((UIElement)element5, 2 * index + 1);
                element5.VerticalContentAlignment = VerticalAlignment.Center;
                element5.IsEnabled = false;
                parentalGrid.Children.Add((UIElement)element1);
                parentalGrid.Children.Add((UIElement)element2);
                parentalGrid.Children.Add((UIElement)element3);
                parentalGrid.Children.Add((UIElement)element4);
                parentalGrid.Children.Add((UIElement)element5);
            }
        }

        private void AddFigureGrid_Click(object sender, RoutedEventArgs e)
        {
            if (AddFigureGrid.Visibility != Visibility.Hidden)
                return;
            currentGrid.Visibility = Visibility.Hidden;
            AddFigureGrid.Visibility = Visibility.Visible;
            currentGrid = AddFigureGrid;
        }

        private void MainGrid_Click(object sender, RoutedEventArgs e)
        {
            if (MainFormGrid.Visibility != Visibility.Hidden)
                return;
            currentGrid.Visibility = Visibility.Hidden;
            MainFormGrid.Visibility = Visibility.Visible;
            currentGrid = MainFormGrid;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (shapesMap.Count != 0)
            {
                foreach (UIElement key in shapesMap.Keys)
                    MainCanvas.Children.Remove(key);
                shapesMap.Clear();
                int num = (int)MessageBox.Show("Canvas cleared.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                int num1 = (int)MessageBox.Show("Error: No shapes found.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Perimeter_Click(object sender, RoutedEventArgs e)
        {
            if (shapesMap.Count != 0)
            {
                double num1 = 0.0;
                foreach (Shape key in shapesMap.Keys)
                    num1 += shapesMap[key].length();
                PerimeterOrArea.Text = "Perimeter:";
                PerimeterOrAreaField.Text = num1.ToString();
                PerimeterOrArea.Visibility = Visibility.Visible;
                PerimeterOrAreaField.Visibility = Visibility.Visible;
                int num2 = (int)MessageBox.Show("Perimeter has been calculated.\nCheck the right corner.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                int num = (int)MessageBox.Show("Error: No shapes to measure.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Area_Click(object sender, RoutedEventArgs e)
        {
            if (shapesMap.Count != 0)
            {
                double num1 = 0.0;
                foreach (Shape key in shapesMap.Keys)
                    num1 += shapesMap[key].square();
                PerimeterOrArea.Text = "Area:";
                PerimeterOrAreaField.Text = num1.ToString();
                PerimeterOrArea.Visibility = Visibility.Visible;
                PerimeterOrAreaField.Visibility = Visibility.Visible;
                int num2 = (int)MessageBox.Show("Area has been calculated.\nCheck the right corner.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                int num = (int)MessageBox.Show("Error: No shapes to measure.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void AddShape_Click(object sender, RoutedEventArgs e)
        {
            if (ShapeType.Text == "")
            {
                MessageBox.Show("Error: Shape type haven't been chosen", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                string str = ((ContentControl)ShapeType.SelectedItem).Content.ToString();
                bool num;
                switch (str)
                {
                    case "Polyline": 
                        if (numberOfPoints < 3) MessageBox.Show("Error: Not enough points to form a shape", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        else SpawnPolyline();
                        break;
                    case "Polygon":
                        if (numberOfPoints < 3) MessageBox.Show("Error: Not enough points to form a shape", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        else SpawnPolygon();
                        break;
                    case "Circle":
                        SpawnCircle(new double[2]
                        {
                          double.Parse((FirstToSixthPointsGrid.Children[2] as TextBox).Text),
                          double.Parse((FirstToSixthPointsGrid.Children[4] as TextBox).Text)
                        });
                        break;
                    case "Segment":
                        SpawnSegment(new double[2]
                        {
                          double.Parse((FirstToSixthPointsGrid.Children[2] as TextBox).Text),
                          double.Parse((FirstToSixthPointsGrid.Children[4] as TextBox).Text)
                        }, new double[2]
                        {
                          double.Parse((FirstToSixthPointsGrid.Children[7] as TextBox).Text),
                          double.Parse((FirstToSixthPointsGrid.Children[9] as TextBox).Text)
                        }, false);
                        break;
                    case "Triangle":
                        SpawnTriangle();
                        break;
                    case "Quadrilateral":
                        SpawnQuadrilateral();
                        break;
                    case "Rectangle":
                        SpawnRectangle();
                        break;
                    case "Trapeze":
                        SpawnTrapeze();
                        break;
                }
                MessageBox.Show("Item successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void SpawnPolyline()
        {
            System.Windows.Shapes.Polyline visualPolyline = new System.Windows.Shapes.Polyline();
            visualPolyline.StrokeThickness = 2.0;
            visualPolyline.Visibility = Visibility.Visible;
            visualPolyline.Stroke = (Brush)Brushes.Black;
            visualPolyline.Points = FormPointCollection();
            List<Point2D> points = new List<Point2D>();
            foreach (var point in visualPolyline.Points)
                points.Add(new Point2D(new double[2] { point.X, point.Y }));
            GeometryClasses.Polyline polyline = new GeometryClasses.Polyline(points.ToArray());
            shapesMap.Add((Shape)visualPolyline, (IShape)polyline);
            MainCanvas.Children.Add((UIElement)visualPolyline);

        }

        private Polygon SpawnPolygon()
        {
            Polygon visualPolygon = new Polygon();
            visualPolygon.StrokeThickness = 2.0;
            visualPolygon.Visibility = Visibility.Visible;
            visualPolygon.Stroke = (Brush)Brushes.Black;
            visualPolygon.Points = FormPointCollection();
            List<Point2D> points = new List<Point2D>();
            foreach (var point in visualPolygon.Points)
                points.Add(new Point2D(new double[2] { point.X, point.Y }));
            NGon polygon = new NGon(points.ToArray());
            shapesMap.Add((Shape)visualPolygon, (IShape)polygon);
            MainCanvas.Children.Add((UIElement)visualPolygon);
            return visualPolygon;
        }

        private void SpawnTriangle()
        {
            Polygon visualPolygon = SpawnPolygon(); 
            List<Point2D> points = new List<Point2D>();
            foreach (var point in visualPolygon.Points)
                points.Add(new Point2D(new double[2] { point.X, point.Y }));
            shapesMap[(Shape)visualPolygon] = new TGon(points.ToArray());
        }

        private void SpawnQuadrilateral()
        {
            Polygon visualPolygon = SpawnPolygon();
            List<Point2D> points = new List<Point2D>();
            foreach (var point in visualPolygon.Points)
                points.Add(new Point2D(new double[2] { point.X, point.Y }));
            shapesMap[(Shape)visualPolygon] = new QGon(points.ToArray());
        }

        private void SpawnRectangle()
        {
            Polygon visualPolygon = SpawnPolygon();
            List<Point2D> points = new List<Point2D>();
            foreach (var point in visualPolygon.Points)
                points.Add(new Point2D(new double[2] { point.X, point.Y }));
            shapesMap[(Shape)visualPolygon] = new GeometryClasses.Rectangle(points.ToArray());
        }

        private void SpawnTrapeze()
        {
            Polygon visualPolygon = SpawnPolygon();
            List<Point2D> points = new List<Point2D>();
            foreach (var point in visualPolygon.Points)
                points.Add(new Point2D(new double[2] { point.X, point.Y }));
            shapesMap[(Shape)visualPolygon] = new Trapeze(points.ToArray());
        }

        private PointCollection FormPointCollection()
        {
            PointCollection pointCollection = new PointCollection();
            for (int i = 0; i < 5 * numberOfPoints && i < 30; i += 5)
            {
                System.Windows.Point p = new System.Windows.Point(canvasCenter.X + double.Parse((FirstToSixthPointsGrid.Children[i + 2] as TextBox).Text),
                                                                    canvasCenter.Y - double.Parse((FirstToSixthPointsGrid.Children[i + 4] as TextBox).Text));
                pointCollection.Add(p);
            }
            for (int i = 0; i < 5 * numberOfPoints - 30 && i < 30; i += 5)
            {
                System.Windows.Point p = new System.Windows.Point(canvasCenter.X + double.Parse((SeventhToTwlelvthPointsGrid.Children[i + 2] as TextBox).Text),
                                                                    canvasCenter.Y - double.Parse((SeventhToTwlelvthPointsGrid.Children[i + 4] as TextBox).Text));
                pointCollection.Add(p);
            }
            for (int i = 0; i < 5 * numberOfPoints - 60 && i < 30; i += 5)
            {
                System.Windows.Point p = new System.Windows.Point(canvasCenter.X + double.Parse((ThirteenthToEighteenthPointsGrid.Children[i + 2] as TextBox).Text),
                                                                    canvasCenter.Y - double.Parse((ThirteenthToEighteenthPointsGrid.Children[i + 4] as TextBox).Text));
                pointCollection.Add(p);
            }
            return pointCollection;
        }

        private void SpawnCircle(double[] center)
        {
            Circle circle = new Circle(new Point2D(center), radiusValue);
            Ellipse ellipse = new Ellipse();
            ellipse.Height = 2.0 * radiusValue;
            ellipse.Width = 2.0 * radiusValue;
            ellipse.StrokeThickness = 2.0;
            ellipse.Margin = new Thickness(canvasCenter.X + center[0] - radiusValue, canvasCenter.Y - center[1] - radiusValue, 0.0, 0.0);
            ellipse.Visibility = Visibility.Visible;
            ellipse.Stroke = (Brush)Brushes.Black;
            shapesMap.Add((Shape)ellipse, (IShape)circle);
            MainCanvas.Children.Add((UIElement)ellipse);
        }

        private void SpawnSegment(double[] start, double[] end, bool asAPart)
        {
            Line line = new Line();
            line.X1 = canvasCenter.X + start[0];
            line.X2 = canvasCenter.X + end[0];
            line.Y1 = canvasCenter.Y - start[1];
            line.Y2 = canvasCenter.Y - end[1];
            line.StrokeThickness = 2.0;
            line.Visibility = Visibility.Visible;
            line.Stroke = (Brush)Brushes.Black;
            shapesMap.Add((Shape)line, (IShape)new Segment(new Point2D(start), new Point2D(end)));
            MainCanvas.Children.Add((UIElement)line);
        }

        private void ShapeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = ((ContentControl)ShapeType.SelectedItem).Content.ToString();
            radiusValue = 0.0;
            ShapeRadius.Text = radiusValue.ToString();
            numberOfPoints = 1;
            PointsNumber.Text = numberOfPoints.ToString();
            ShapeRadius.IsEnabled = false;
            PointsDown.IsEnabled = false;
            PointsUp.IsEnabled = false;
            for (int index = 0; index < 30; index += 5)
            {
                (FirstToSixthPointsGrid.Children[index + 2] as TextBox).Text = "";
                (FirstToSixthPointsGrid.Children[index + 4] as TextBox).Text = "";
                FirstToSixthPointsGrid.Children[index + 2].IsEnabled = false;
                FirstToSixthPointsGrid.Children[index + 4].IsEnabled = false;
            }
            for (int index = 0; index < 30; index += 5)
            {
                (SeventhToTwlelvthPointsGrid.Children[index + 2] as TextBox).Text = "";
                (SeventhToTwlelvthPointsGrid.Children[index + 4] as TextBox).Text = "";
                SeventhToTwlelvthPointsGrid.Children[index + 2].IsEnabled = false;
                SeventhToTwlelvthPointsGrid.Children[index + 4].IsEnabled = false;
            }
            for (int index = 0; index < 30; index += 5)
            {
                (ThirteenthToEighteenthPointsGrid.Children[index + 2] as TextBox).Text = "";
                (ThirteenthToEighteenthPointsGrid.Children[index + 4] as TextBox).Text = "";
                ThirteenthToEighteenthPointsGrid.Children[index + 2].IsEnabled = false;
                ThirteenthToEighteenthPointsGrid.Children[index + 4].IsEnabled = false;
            }
            if (str == "Polyline" || str == "Polygon")
            {
                numberOfPoints = 3;
                PointsNumber.Text = "3";
                PointsNumber.IsEnabled = true;
                PointsUp.IsEnabled = true;
                PointsDown.IsEnabled = true;
                (FirstToSixthPointsGrid.Children[2] as TextBox).Text = "0";
                (FirstToSixthPointsGrid.Children[4] as TextBox).Text = "0";
                FirstToSixthPointsGrid.Children[2].IsEnabled = true;
                FirstToSixthPointsGrid.Children[4].IsEnabled = true;
            }
            else
            {
                int num;
                switch (str)
                {
                    case "Circle":
                        ShapeRadius.IsEnabled = true;
                        (FirstToSixthPointsGrid.Children[2] as TextBox).Text = "0";
                        (FirstToSixthPointsGrid.Children[4] as TextBox).Text = "0";
                        FirstToSixthPointsGrid.Children[2].IsEnabled = true;
                        FirstToSixthPointsGrid.Children[4].IsEnabled = true;
                        return;
                    case "Segment":
                        numberOfPoints = 2;
                        PointsNumber.Text = "2";
                        for (int index = 0; index < 10; index += 5)
                        {
                            (FirstToSixthPointsGrid.Children[index + 2] as TextBox).Text = "0";
                            (FirstToSixthPointsGrid.Children[index + 4] as TextBox).Text = "0";
                            FirstToSixthPointsGrid.Children[index + 2].IsEnabled = true;
                            FirstToSixthPointsGrid.Children[index + 4].IsEnabled = true;
                        }
                        return;
                    case "Triangle":
                        numberOfPoints = 3;
                        PointsNumber.Text = "3";
                        for (int index = 0; index < 15; index += 5)
                        {
                            (FirstToSixthPointsGrid.Children[index + 2] as TextBox).Text = "0";
                            (FirstToSixthPointsGrid.Children[index + 4] as TextBox).Text = "0";
                            FirstToSixthPointsGrid.Children[index + 2].IsEnabled = true;
                            FirstToSixthPointsGrid.Children[index + 4].IsEnabled = true;
                        }
                        return;
                    case "Quadrilateral":
                    case "Rectangle":
                        num = 1;
                        break;
                    default:
                        num = str == "Trapeze" ? 1 : 0;
                        break;
                }
                if (num == 0)
                    return;
                numberOfPoints = 4;
                PointsNumber.Text = "4";
                for (int index = 0; index < 20; index += 5)
                {
                    (FirstToSixthPointsGrid.Children[index + 2] as TextBox).Text = "0";
                    (FirstToSixthPointsGrid.Children[index + 4] as TextBox).Text = "0";
                    FirstToSixthPointsGrid.Children[index + 2].IsEnabled = true;
                    FirstToSixthPointsGrid.Children[index + 4].IsEnabled = true;
                }
            }
        }

        private void PointsUp_Click(object sender, RoutedEventArgs e)
        {
            if (numberOfPoints < 18)
            {
                PointsNumber.Text = (++numberOfPoints).ToString();
                if (numberOfPoints < 7)
                {
                    (FirstToSixthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 2] as TextBox).Text = "0";
                    (FirstToSixthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 4] as TextBox).Text = "0";
                    FirstToSixthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 2].IsEnabled = true;
                    FirstToSixthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 4].IsEnabled = true;
                }
                else if (numberOfPoints < 13)
                {
                    (SeventhToTwlelvthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 2] as TextBox).Text = "0";
                    (SeventhToTwlelvthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 4] as TextBox).Text = "0";
                    SeventhToTwlelvthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 2].IsEnabled = true;
                    SeventhToTwlelvthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 4].IsEnabled = true;
                }
                else
                {
                    (ThirteenthToEighteenthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 2] as TextBox).Text = "0";
                    (ThirteenthToEighteenthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 4] as TextBox).Text = "0";
                    ThirteenthToEighteenthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 2].IsEnabled = true;
                    ThirteenthToEighteenthPointsGrid.Children[(numberOfPoints - 1) % 6 * 5 + 4].IsEnabled = true;
                }
            }
            else
            {
                int num = (int)MessageBox.Show("Error: Can't go higher", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void PointsDown_Click(object sender, RoutedEventArgs e)
        {
            if (numberOfPoints > 1)
            {
                PointsNumber.Text = (--numberOfPoints).ToString();
                for (int index = 25; index > -1 && index > 5 * numberOfPoints - 65; index -= 5)
                {
                    (ThirteenthToEighteenthPointsGrid.Children[index + 2] as TextBox).Text = "";
                    (ThirteenthToEighteenthPointsGrid.Children[index + 4] as TextBox).Text = "";
                    ThirteenthToEighteenthPointsGrid.Children[index + 2].IsEnabled = false;
                    ThirteenthToEighteenthPointsGrid.Children[index + 4].IsEnabled = false;
                }
                for (int index = 25; index > -1 && index > 5 * numberOfPoints - 35; index -= 5)
                {
                    (SeventhToTwlelvthPointsGrid.Children[index + 2] as TextBox).Text = "";
                    (SeventhToTwlelvthPointsGrid.Children[index + 4] as TextBox).Text = "";
                    SeventhToTwlelvthPointsGrid.Children[index + 2].IsEnabled = false;
                    SeventhToTwlelvthPointsGrid.Children[index + 4].IsEnabled = false;
                }
                for (int index = 25; index > -1 && index > 5 * numberOfPoints - 5; index -= 5)
                {
                    (FirstToSixthPointsGrid.Children[index + 2] as TextBox).Text = "";
                    (FirstToSixthPointsGrid.Children[index + 4] as TextBox).Text = "";
                    FirstToSixthPointsGrid.Children[index + 2].IsEnabled = false;
                    FirstToSixthPointsGrid.Children[index + 4].IsEnabled = false;
                }
            }
            else
            {
                int num = (int)MessageBox.Show("Error: Can't go lower", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ShapeRadius_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ShapeRadius.IsEnabled)
                return;
            if (double.TryParse(ShapeRadius.Text, out radiusValue) & radiusValue >= 0.0)
                ShapeRadius.Text = radiusValue.ToString();
            else if (ShapeRadius.Text != "")
            {
                int num = (int)MessageBox.Show("Error: Inappropriate radius value", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                radiusValue = 0.0;
                ShapeRadius.Text = radiusValue.ToString();
            }
        
        }
    }
}
