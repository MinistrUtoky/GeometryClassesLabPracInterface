using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
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
        private Shape itemToRemove;
        private Shape itemToTransform;
        private Shape itemToIntersect1;
        private Shape itemToIntersect2;
        private double rotationAngle;
        private double[] positionShiftVector;
        private int numberOfPoints;
        private double radiusValue;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                numberOfPoints = 1;
                shapesMap = new Dictionary<Shape, IShape>();
                currentGrid = MainFormGrid;
                canvasCenter = new System.Windows.Point(Width * 5.0 / 8.0 / 2.0, Height * 51.0 / 116.0);
                rotationAngle = 0; positionShiftVector = new double[2] { 0, 0 };
                CreateAxis();
                CreatePointFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateAxis()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreatePointFields()
        {
            try {
                CreateSixPointFields(FirstToSixthPointsGrid, 1);
                CreateSixPointFields(SeventhToTwlelvthPointsGrid, 7);
                CreateSixPointFields(ThirteenthToEighteenthPointsGrid, 13);            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateSixPointFields(Grid parentalGrid, int start)
        {
            try { 
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenAnotherGrid(Grid anotherGrid)
        {
            try { 
                if (anotherGrid.Visibility != Visibility.Hidden)
                    return;
                currentGrid.Visibility = Visibility.Hidden;
                anotherGrid.Visibility = Visibility.Visible;
                currentGrid = anotherGrid;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MainGrid_Click(object sender, RoutedEventArgs e) => OpenAnotherGrid(MainFormGrid);
        private void AddFigureGrid_Click(object sender, RoutedEventArgs e) => OpenAnotherGrid(AddFigureGrid);
        private void MoveFigureGrid_Click(object sender, RoutedEventArgs e) => OpenAnotherGrid(MoveFigureGrid);
        private void RemoveFigureGrid_Click(object sender, RoutedEventArgs e) => OpenAnotherGrid(RemoveFigureGrid);
        private void IntersectFiguresGrid_Click(object sender, RoutedEventArgs e) => OpenAnotherGrid(IntersectFiguresGrid);

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            try { 
                if (shapesMap.Count != 0)
                {
                    foreach (UIElement key in shapesMap.Keys)
                        MainCanvas.Children.Remove(key);
                    shapesMap.Clear();
                    ShapesListComboBox.Items.Clear();
                    MovableShapesListComboBox.Items.Clear();
                    ShapesToIntersectListComboBox.Items.Clear();
                    ShapesToIntersect2ListComboBox.Items.Clear();
                    MessageBox.Show("Canvas cleared.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    MessageBox.Show("Error: No shapes found.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Perimeter_Click(object sender, RoutedEventArgs e)
        {
            try { 
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Area_Click(object sender, RoutedEventArgs e)
        {
            try { 
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddShape_Click(object sender, RoutedEventArgs e)
        {
            try { 
                if (ShapeType.Text == "")
                {
                    MessageBox.Show("Error: Shape type haven't been chosen", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    string str = ((ContentControl)ShapeType.SelectedItem).Content.ToString();
                    bool num; Shape visualShape= null;
                    switch (str)
                    {
                        case "Polyline":
                            //if (numberOfPoints < 3) MessageBox.Show("Error: Not enough points to form a shape", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            //else 
                            visualShape = SpawnPolyline();
                            AddItemToShapeListComboBoxes((shapesMap[visualShape] as GeometryClasses.Polyline).toString());
                            break;
                        case "Polygon":
                            //if (numberOfPoints < 3) MessageBox.Show("Error: Not enough points to form a shape", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            //else 
                            visualShape = SpawnPolygon();
                            AddItemToShapeListComboBoxes((shapesMap[visualShape] as NGon).toString());
                            break;
                        case "Circle":
                            visualShape = SpawnCircle(new double[2]
                            {
                              double.Parse((FirstToSixthPointsGrid.Children[2] as TextBox).Text),
                              double.Parse((FirstToSixthPointsGrid.Children[4] as TextBox).Text)
                            });
                            AddItemToShapeListComboBoxes((shapesMap[visualShape] as Circle).toString());
                            break;
                        case "Segment":
                            visualShape = SpawnSegment(new double[2]
                            {
                              double.Parse((FirstToSixthPointsGrid.Children[2] as TextBox).Text),
                              double.Parse((FirstToSixthPointsGrid.Children[4] as TextBox).Text)
                            }, new double[2]
                            {
                              double.Parse((FirstToSixthPointsGrid.Children[7] as TextBox).Text),
                              double.Parse((FirstToSixthPointsGrid.Children[9] as TextBox).Text)
                            }, false);
                            AddItemToShapeListComboBoxes((shapesMap[visualShape] as Segment).toString());
                            break;
                        case "Triangle":
                            visualShape = SpawnTriangle();
                            AddItemToShapeListComboBoxes((shapesMap[visualShape] as TGon).toString());
                            break;
                        case "Quadrilateral":
                            visualShape = SpawnQuadrilateral();
                            AddItemToShapeListComboBoxes((shapesMap[visualShape] as QGon).toString());
                            break;
                        case "Rectangle":
                            visualShape = SpawnRectangle();
                            AddItemToShapeListComboBoxes((shapesMap[visualShape] as GeometryClasses.Rectangle).toString());
                            break;
                        case "Trapeze":
                            visualShape = SpawnTrapeze();
                            AddItemToShapeListComboBoxes((shapesMap[visualShape] as Trapeze).toString());
                            break;
                    }
                    MessageBox.Show("Item successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
        private void AddItemToShapeListComboBoxes(string value)
        {
            try { 
                ShapesListComboBox.Items.Add(value);
                ShapesToIntersectListComboBox.Items.Add(value);
                ShapesToIntersect2ListComboBox.Items.Add(value);
                MovableShapesListComboBox.Items.Add(value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InsertItemToShapeListComboBoxes(string value, int index)
        {
            try { 
                ShapesListComboBox.Items.Insert(index, value);
                ShapesToIntersectListComboBox.Items.Insert(index, value);
                ShapesToIntersect2ListComboBox.Items.Insert(index, value);
                MovableShapesListComboBox.Items.Insert(index, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private System.Windows.Shapes.Polyline SpawnPolyline()
        {
            System.Windows.Shapes.Polyline visualPolyline = new System.Windows.Shapes.Polyline();
            try { 
                visualPolyline.StrokeThickness = 2.0;
                visualPolyline.Visibility = Visibility.Visible;
                visualPolyline.Stroke = (Brush)Brushes.Black;
                visualPolyline.Points = FormPointCollection();
                List<Point2D> points = new List<Point2D>();
                foreach (var point in visualPolyline.Points)
                    points.Add(new Point2D(new double[2] { point.X - canvasCenter.X, canvasCenter.Y - point.Y  }));
                GeometryClasses.Polyline polyline = new GeometryClasses.Polyline(points.ToArray());
                shapesMap.Add((Shape)visualPolyline, (IShape)polyline);
                MainCanvas.Children.Add((UIElement)visualPolyline);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return visualPolyline;
        }

        private Polygon SpawnPolygon()
        {
            Polygon visualPolygon = new Polygon();
            try { 
                visualPolygon.StrokeThickness = 2.0;
                visualPolygon.Visibility = Visibility.Visible;
                visualPolygon.Stroke = (Brush)Brushes.Black;
                visualPolygon.Points = FormPointCollection();
                List<Point2D> points = new List<Point2D>();
                foreach (var point in visualPolygon.Points)
                    points.Add(new Point2D(new double[2] { point.X - canvasCenter.X, canvasCenter.Y - point.Y }));
                NGon polygon = new NGon(points.ToArray());
                shapesMap.Add((Shape)visualPolygon, (IShape)polygon);
                MainCanvas.Children.Add((UIElement)visualPolygon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return visualPolygon;
        }

        private Polygon SpawnTriangle()
        {
            Polygon visualPolygon = new Polygon();
            try { 
                visualPolygon = SpawnPolygon(); 
                List<Point2D> points = new List<Point2D>();
                foreach (var point in visualPolygon.Points)
                    points.Add(new Point2D(new double[2] { point.X - canvasCenter.X, canvasCenter.Y - point.Y }));
                shapesMap[(Shape)visualPolygon] = new TGon(points.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return visualPolygon;
        }

        private Polygon SpawnQuadrilateral()
        {
            Polygon visualPolygon = new Polygon();
            try { 
                visualPolygon = SpawnPolygon();
                List<Point2D> points = new List<Point2D>();
                foreach (var point in visualPolygon.Points)
                    points.Add(new Point2D(new double[2] { point.X - canvasCenter.X, canvasCenter.Y - point.Y }));
                shapesMap[(Shape)visualPolygon] = new QGon(points.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return visualPolygon;
        }

        private Polygon SpawnRectangle()
        {
            Polygon visualPolygon = new Polygon();
            try { 
                visualPolygon = SpawnPolygon();
                List<Point2D> points = new List<Point2D>();
                foreach (var point in visualPolygon.Points)
                    points.Add(new Point2D(new double[2] { point.X - canvasCenter.X, canvasCenter.Y - point.Y }));
                shapesMap[(Shape)visualPolygon] = new GeometryClasses.Rectangle(points.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return visualPolygon;
        }

        private Polygon SpawnTrapeze()
        {
            Polygon visualPolygon = new Polygon();
            try { 
                visualPolygon = SpawnPolygon();
                List<Point2D> points = new List<Point2D>();
                foreach (var point in visualPolygon.Points)
                    points.Add(new Point2D(new double[2] { point.X - canvasCenter.X, canvasCenter.Y - point.Y }));
                shapesMap[(Shape)visualPolygon] = new Trapeze(points.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return visualPolygon;
        }

        private PointCollection FormPointCollection()
        {
            PointCollection pointCollection = new PointCollection();
            try { 
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return pointCollection;
        }

        private Ellipse SpawnCircle(double[] center)
        {
            Ellipse ellipse = new Ellipse();
            try { 
                Circle circle = new Circle(new Point2D(center), radiusValue);
                ellipse.Height = 2.0 * radiusValue;
                ellipse.Width = 2.0 * radiusValue;
                ellipse.StrokeThickness = 2.0;
                ellipse.Margin = new Thickness(canvasCenter.X + center[0] - radiusValue, canvasCenter.Y - center[1] - radiusValue, 0.0, 0.0);
                ellipse.Visibility = Visibility.Visible;
                ellipse.Stroke = (Brush)Brushes.Black;
                shapesMap.Add((Shape)ellipse, (IShape)circle);
                MainCanvas.Children.Add((UIElement)ellipse);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return ellipse;
        }

        private Line SpawnSegment(double[] start, double[] end, bool asAPart)
        {
            Line line = new Line();
            try { 
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return line;
        }

        private void ShapeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PointsUp_Click(object sender, RoutedEventArgs e)
        {
            try { 
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PointsDown_Click(object sender, RoutedEventArgs e)
        {
            try { 
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
                    MessageBox.Show("Error: Can't go lower", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShapeRadius_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { 
                if (!ShapeRadius.IsEnabled)
                    return;
                if (double.TryParse(ShapeRadius.Text, out radiusValue) & radiusValue >= 0.0)
                    ShapeRadius.Text = radiusValue.ToString();
                else if (ShapeRadius.Text != "")
                {
                    MessageBox.Show("Error: Inappropriate radius value", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    radiusValue = 0.0;
                    ShapeRadius.Text = radiusValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShapesListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int i = ShapesListComboBox.SelectedIndex, j = 0;
                foreach (Shape sh in shapesMap.Keys.ToArray().ToList())
                {
                    if (i == j)
                    {
                        itemToRemove = sh; break;
                    }
                    j++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try { 
                if (ShapesListComboBox.Text == "")
                    MessageBox.Show("Error: Shape to remove haven't been chosen", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else {
                    shapesMap.Remove(itemToRemove);
                    MainCanvas.Children.Remove(itemToRemove);
                    MovableShapesListComboBox.Items.RemoveAt(ShapesListComboBox.SelectedIndex);
                    ShapesToIntersectListComboBox.Items.RemoveAt(ShapesListComboBox.SelectedIndex);
                    ShapesToIntersect2ListComboBox.Items.RemoveAt(ShapesListComboBox.SelectedIndex);
                    ShapesListComboBox.Items.Remove(ShapesListComboBox.SelectedItem);
                    MessageBox.Show("Item successfully removed", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void XPositionShiftValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double posX = 0;
                if (!XPositionShiftValue.IsEnabled)
                    return;
                else if (!Double.TryParse(XPositionShiftValue.Text, out posX) && XPositionShiftValue.Text != "" && XPositionShiftValue.Text != "-")
                {
                    MessageBox.Show("Error: Inappropriate movement vector coordinate value", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    positionShiftVector[1] = 0;
                    XPositionShiftValue.Text = positionShiftVector[1].ToString();
                }
                positionShiftVector[0] = posX;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void YPositionShiftValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { 
                double posY = 0;
                if (!YPositionShiftValue.IsEnabled)
                    return;
                else if (!Double.TryParse(YPositionShiftValue.Text, out posY) && YPositionShiftValue.Text != "" && YPositionShiftValue.Text != "-")
                {
                    MessageBox.Show("Error: Inappropriate movement vector coordinate value", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    positionShiftVector[0] = 0;
                    YPositionShiftValue.Text = positionShiftVector[0].ToString();
                }
                positionShiftVector[1] = posY;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShapeRotationValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!ShapeRotationValue.IsEnabled)
                    return;
                else if (!Double.TryParse(ShapeRotationValue.Text, out rotationAngle) && ShapeRotationValue.Text != "" && ShapeRotationValue.Text != "-")
                {
                    MessageBox.Show("Error: Inappropriate rotation angle value", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    rotationAngle = 0.0;
                    ShapeRotationValue.Text = rotationAngle.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SwitchAxisButtonUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AxisToReflectShapeOver.Text = AxisToReflectShapeOver.Text == "x" ? "y" : "x";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SwitchAxisButtonDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AxisToReflectShapeOver.Text = AxisToReflectShapeOver.Text == "x" ? "y" : "x";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void MovementTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 
                string str = ((ContentControl)MovementTypeComboBox.SelectedItem).Content.ToString();
                if (str == "") return;
                XPositionShiftValue.IsEnabled = false;
                YPositionShiftValue.IsEnabled = false;
                ShapeRotationValue.IsEnabled = false;
                SwitchAxisButtonDown.IsEnabled = false;
                SwitchAxisButtonUp.IsEnabled = false;
                rotationAngle = 0; positionShiftVector[0] = 0; positionShiftVector[1] = 0;
                XPositionShiftValue.Text = "0"; YPositionShiftValue.Text = "0";
                ShapeRotationValue.Text = "0"; AxisToReflectShapeOver.Text = "x";
                if (str == "Position")
                {
                    XPositionShiftValue.IsEnabled = true;
                    YPositionShiftValue.IsEnabled = true;
                }
                else if (str == "Rotation")
                    ShapeRotationValue.IsEnabled = true;
                else if (str == "Axis Reflection")
                {
                    SwitchAxisButtonDown.IsEnabled = true;
                    SwitchAxisButtonUp.IsEnabled = true;
                }
                if (string.IsNullOrEmpty(MovableShapesListComboBox.Text)) return;
                MovementValuesGrid.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MovableShapesListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 
                int i = MovableShapesListComboBox.SelectedIndex, j = 0;
                foreach (Shape sh in shapesMap.Keys.ToArray().ToList())
                {
                    if (i == j)
                    {
                        itemToTransform = sh;
                        if (itemToTransform.GetType() == typeof(Segment)) throw new Exception("!!!");
                        
                        break;
                    }
                    j++;
                }
                if (string.IsNullOrEmpty(MovementTypeComboBox.Text)) return;
                MovementValuesGrid.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            try { 
                if (string.IsNullOrEmpty(MovementTypeComboBox.Text) || string.IsNullOrEmpty(MovableShapesListComboBox.Text))
                {
                    MessageBox.Show("Error: Shape to move have not been chosen", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                IShape ishape = shapesMap[itemToTransform];
                Shape shape = itemToTransform;
                int index = MovableShapesListComboBox.SelectedIndex;
                shapesMap.Remove(itemToTransform);
                MainCanvas.Children.Remove(itemToTransform);
                ShapesListComboBox.Items.RemoveAt(index);
                ShapesToIntersectListComboBox.Items.RemoveAt(index);
                ShapesToIntersect2ListComboBox.Items.RemoveAt(index);
                MovableShapesListComboBox.Items.Remove(MovableShapesListComboBox.SelectedItem);

                if (MovementTypeComboBox.Text == "Position")
                {
                    if (XPositionShiftValue.Text == "-" || YPositionShiftValue.Text == "-")
                    {
                        MessageBox.Show("Error: Inappropriate coordinate value", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    ishape.shift(new Point2D(positionShiftVector));

                    if (shape.GetType() == typeof(Line))
                    {
                        Line l = (shape as Line);
                        l.X1 = l.X1 + positionShiftVector[0];
                        l.X2 = l.X2 + positionShiftVector[0];
                        l.Y1 = l.Y1 - positionShiftVector[1];
                        l.Y2 = l.Y2 - positionShiftVector[1];
                    }
                    else if (shape.GetType() == typeof(Ellipse))
                    {
                        Ellipse el = (shape as Ellipse);
                        el.Margin = new Thickness(el.Margin.Left + positionShiftVector[0], el.Margin.Top - positionShiftVector[1], 0, 0);
                    }
                    else if (shape.GetType() == typeof(System.Windows.Shapes.Polyline))
                    {
                        System.Windows.Shapes.Polyline pl = (shape as System.Windows.Shapes.Polyline);
                        PointCollection pc = new PointCollection();
                        for (int i = 0; i < pl.Points.Count; i++)
                        {
                            pc.Add(pl.Points[i]); pc[i] = new System.Windows.Point(pc[i].X + positionShiftVector[0], pc[i].Y - positionShiftVector[1]);
                        }
                        pl.Points = pc;
                    }
                    else if (shape.GetType() == typeof(Polygon))
                    {
                        Polygon pg = (shape as Polygon);
                        PointCollection pc = new PointCollection();
                        for (int i = 0; i < pg.Points.Count; i++)
                        {
                            pc.Add(pg.Points[i]); pc[i] = new System.Windows.Point(pc[i].X + positionShiftVector[0], pc[i].Y - positionShiftVector[1]);
                        }
                        pg.Points = pc;
                    }
                }
                else if (MovementTypeComboBox.Text == "Rotation")
                {
                    if (ShapeRotationValue.Text == "-")
                    {
                        MessageBox.Show("Error: Inappropriate coordinate value", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    ishape = ishape.rot(rotationAngle);

                    if (shape.GetType() == typeof(Line))
                    {
                        Line l = (shape as Line);
                        Point2D p1 = new Point2D();
                        Point2D p2 = new Point2D();
                        p1.setX(new double[]{ l.X1 - canvasCenter.X, l.Y1 - canvasCenter.Y });
                        p2.setX(new double[] { l.X2 - canvasCenter.X, l.Y2 - canvasCenter.Y });
                        p1.rot(-rotationAngle); p2.rot(-rotationAngle);
                        l.X1 = canvasCenter.X + p1.getX()[0];
                        l.X2 = canvasCenter.X + p2.getX()[0];
                        l.Y1 = canvasCenter.Y + p1.getX()[1];
                        l.Y2 = canvasCenter.X + p2.getX()[1];
                    }
                    else if (shape.GetType() == typeof(System.Windows.Shapes.Polyline))
                    {
                        System.Windows.Shapes.Polyline pl = (shape as System.Windows.Shapes.Polyline);
                        PointCollection pc = new PointCollection();
                        for (int i = 0; i < pl.Points.Count; i++)
                        {
                            Point2D p1 = new Point2D(new double[] { pl.Points[i].X - canvasCenter.X, pl.Points[i].Y - canvasCenter.Y });
                            p1.rot(-rotationAngle);
                            pc.Add(pl.Points[i]); pc[i] = new System.Windows.Point(p1.x[0] + canvasCenter.X, p1.x[1] + canvasCenter.Y);
                        }
                        pl.Points = pc;
                    }
                    else if (shape.GetType() == typeof(Polygon))
                    {
                        System.Windows.Shapes.Polygon pg = (shape as System.Windows.Shapes.Polygon);
                        PointCollection pc = new PointCollection();
                        for (int i = 0; i < pg.Points.Count; i++)
                        {
                            Point2D p1 = new Point2D(new double[] { pg.Points[i].X - canvasCenter.X, pg.Points[i].Y - canvasCenter.Y });
                            p1.rot(-rotationAngle);
                            pc.Add(pg.Points[i]); pc[i] = new System.Windows.Point(p1.x[0] + canvasCenter.X, p1.x[1] + canvasCenter.Y);
                        }
                        pg.Points = pc;
                    }
                    else if (shape.GetType() == typeof(Ellipse))
                    {
                        Ellipse el = (shape as Ellipse);
                        Point2D center = new Point2D(new double[] { el.Margin.Left + el.Width/2 - canvasCenter.X,
                                                                     canvasCenter.Y - el.Margin.Top - el.Height/2 });
                        center.rot(rotationAngle);
                        el.Margin = new Thickness(canvasCenter.X + center.x[0] - el.Width/2, canvasCenter.Y - center.x[1] - el.Height/2, 0.0, 0.0);
                    }
                }
                else if (MovementTypeComboBox.Text == "Axis Reflection")
                {
                    int axis = AxisToReflectShapeOver.Text == "x" ? 0 : 1;
                    ishape.symAxis(axis); 
                    if (shape.GetType() == typeof(Line))
                    {
                        Line l = (shape as Line);
                        l.X1 = axis == 0 ? l.X1 : 2 * canvasCenter.X - l.X1;
                        l.X2 = axis == 0 ? l.X2 : 2 * canvasCenter.X - l.X2;
                        l.Y1 = axis == 0 ? 2 * canvasCenter.Y - l.Y1 : l.Y1;
                        l.Y2 = axis == 0 ? 2 * canvasCenter.Y - l.Y2 : l.Y2;
                    }
                    else if (shape.GetType() == typeof(Ellipse))
                    {
                        Ellipse el = (shape as Ellipse);
                        if (axis==0)
                            el.Margin = new Thickness(el.Margin.Left, 2 * canvasCenter.Y - el.Margin.Top - el.Height, 0.0, 0.0);
                        else if (axis==1)
                            el.Margin = new Thickness(2 * canvasCenter.X - el.Margin.Left - el.Width, el.Margin.Top , 0.0, 0.0);
                    }
                    else if (shape.GetType() == typeof(System.Windows.Shapes.Polyline))
                    {
                        System.Windows.Shapes.Polyline pl = (shape as System.Windows.Shapes.Polyline);
                        PointCollection pc = new PointCollection();
                        for (int i = 0; i < pl.Points.Count; i++)
                        {
                            pc.Add(pl.Points[i]);
                            if (axis == 0)
                                pc[i] = new System.Windows.Point(pc[i].X, 2 * canvasCenter.Y - pc[i].Y);
                            else if (axis == 1)
                                pc[i] = new System.Windows.Point(2 * canvasCenter.X - pc[i].X, pc[i].Y);
                        }
                        pl.Points = pc;
                    }
                    else if (shape.GetType() == typeof(Polygon))
                    {
                        Polygon pg = (shape as Polygon);
                        PointCollection pc = new PointCollection();
                        for (int i = 0; i < pg.Points.Count; i++)
                        {
                            pc.Add(pg.Points[i]);
                            if (axis == 0)
                                pc[i] = new System.Windows.Point(pc[i].X, 2 * canvasCenter.Y - pc[i].Y);
                            else if (axis == 1)
                                pc[i] = new System.Windows.Point(2 * canvasCenter.X - pc[i].X, pc[i].Y);
                        }
                        pg.Points = pc;
                    }
                }
                shapesMap.Add(shape, ishape);
                MainCanvas.Children.Insert(index, shape);
                if (ishape.GetType() == typeof(Circle)) InsertItemToShapeListComboBoxes((ishape as Circle).toString(), index);
                if (ishape.GetType() == typeof(Segment)) InsertItemToShapeListComboBoxes((ishape as Segment).toString(), index);
                if (ishape.GetType() == typeof(NGon)) InsertItemToShapeListComboBoxes((ishape as NGon).toString(), index);
                if (ishape.GetType() == typeof(GeometryClasses.Polyline)) InsertItemToShapeListComboBoxes((ishape as GeometryClasses.Polyline).toString(), index);
                if (ishape.GetType() == typeof(GeometryClasses.Rectangle)) InsertItemToShapeListComboBoxes((ishape as GeometryClasses.Rectangle).toString(), index);
                if (ishape.GetType() == typeof(QGon)) InsertItemToShapeListComboBoxes((ishape as QGon).toString(), index);
                if (ishape.GetType() == typeof(TGon)) InsertItemToShapeListComboBoxes((ishape as TGon).toString(), index);
                if (ishape.GetType() == typeof(Trapeze)) InsertItemToShapeListComboBoxes((ishape as Trapeze).toString(), index);
                MessageBox.Show("Item successfully transformed", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShapesToIntersectListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 
                int i = ShapesToIntersectListComboBox.SelectedIndex, j = 0;
                if (i == ShapesToIntersect2ListComboBox.SelectedIndex)
                {
                    ShapesToIntersectListComboBox.SelectedItem = null;
                    return;
                }
                if (itemToIntersect1!=null) itemToIntersect1.Stroke = Brushes.Black;
                foreach (Shape sh in shapesMap.Keys.ToArray().ToList())
                {
                    if (i == j)
                    {
                        sh.Stroke = Brushes.Red;
                        itemToIntersect1 = sh; break;
                    }
                    else if (sh != itemToIntersect2) sh.Stroke = Brushes.Black;
                    j++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShapesToIntersect2ListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int i = ShapesToIntersect2ListComboBox.SelectedIndex, j = 0;
                if (i == ShapesToIntersectListComboBox.SelectedIndex)
                {
                    ShapesToIntersect2ListComboBox.SelectedItem = null;
                    return;
                }
                if (itemToIntersect2 != null) itemToIntersect2.Stroke = Brushes.Black;
                foreach (Shape sh in shapesMap.Keys.ToArray().ToList())
                {
                    if (i == j)
                    {
                        sh.Stroke = Brushes.Red;
                        itemToIntersect2 = sh; break;
                    }
                    else if (sh != itemToIntersect1) sh.Stroke = Brushes.Black;
                    j++;
                }
            }            
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Intersect_Click(object sender, RoutedEventArgs e)
        {
            try { 
                if (string.IsNullOrEmpty(ShapesToIntersectListComboBox.Text) || string.IsNullOrEmpty(ShapesToIntersect2ListComboBox.Text))
                {
                    MessageBox.Show("Error: Choice of shapes for intersection has not been complete.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                PerimeterOrArea.Text = "Intersection:";
                if (shapesMap[itemToIntersect1].cross(shapesMap[itemToIntersect2]))
                    PerimeterOrAreaField.Text = "Intersect.";
                else
                    PerimeterOrAreaField.Text = "Don't intersect.";
                MessageBox.Show("Items successfully checked on intersection", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                OpenAnotherGrid(MainFormGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
