using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SecondOrderCurve
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawCurve(object sender, RoutedEventArgs e)
        {
            // Getting user's input
            decimal a;
            decimal b;
            decimal c;
            bool aCheck = Decimal.TryParse(ValueA.Text, out a);
            bool bCheck = Decimal.TryParse(ValueB.Text, out b);
            bool cCheck = Decimal.TryParse(ValueC.Text, out c);

            // Getting Canvas' height and width
            decimal canvasHeight = (decimal)DrawingCanvas.Height;
            decimal canvasWidth = (decimal)DrawingCanvas.Width;

            // Checking if there are any values
            if (a == 0 || b==0 || c==0) 
            {
                MessageBox.Show("Please type a number in all boxes other than zero.");
                return;
            }
            else if(aCheck != bCheck != cCheck != true)
            {
                MessageBox.Show("Please type a number in all boxes other than zero.");
                return;
            }
            
            // Clearing and rebuilding canvas
            ClearAndBuildCanvas();

            // Creating a polyline and changing the design
            Polyline polyline = new Polyline();
            polyline.Stroke = Brushes.Tomato;
            polyline.StrokeThickness = 2;
            polyline.Name = "Curve";

            decimal yScale = Scaling(a, b, c);

            Polyline testPolyline = new Polyline();

            // Drawing the polyline
            for (decimal x = -4;  x <= 4; x += 0.1m)
            {
                decimal y = a*x*x + b*x +c;
                // Scaling according to fixed 300px x 300px Canvas and for arbitrary values of X (from -4 to + 3)
                // As well as for maximum and minimum Y values using Scaling method, defined below
                polyline.Points.Add(new Point((double)(((x + 4) / 8) * canvasWidth), (double)(canvasHeight/2 - (y * yScale))));
                testPolyline.Points.Add(new Point((double)x,(double)y));
            }
            // Adding polyline to canvas
            DrawingCanvas.Children.Add(polyline);
        }

        private decimal Scaling(decimal a, decimal b, decimal c)
        {
            // Creating a list to find max and min Y values
            List<decimal> yValues = new List<decimal>();
            for (decimal x = -4; x <= 4; x += 0.1m)
            {
                decimal y = a * x * x + b * x + c;
                yValues.Add(y);
            }

            // Assigning variables
            decimal max = yValues.Max();
            decimal min = yValues.Min();
            List<decimal> decimals = new List<decimal> { Math.Abs(min), Math.Abs(max) };
            decimal scale = 1m;

            // Creating labels for Canvas Y values Scale
            Label yMaxValue = new Label();
            Label yMinValue = new Label();
            Canvas.SetLeft(yMaxValue, 110);
            Canvas.SetTop(yMaxValue, 0);
            Canvas.SetLeft(yMinValue, 110);
            Canvas.SetTop(yMinValue, 300);

            // For each case calculating the proper scale to be applied according to
            // maximum and minimum values of Y. In addition, max width of the canvas (300)
            // is taken into consideration. Offset of 0 is 150 px below the top canvas border
            if (min > 0 && max > 0)
            {
                scale = 150 / max;
                yMaxValue.Content = (int)max;
                yMinValue.Content = (int)-max;
            }
            else if(min < 0 && max < 0) 
            {
                scale = 150 / (Math.Abs(min));
                yMaxValue.Content = (int)-min;
                yMinValue.Content = (int)min;
            }
            else if(max > 0 && min < 0)
            {
                scale = 300 / (decimals.Max()*2);

                yMaxValue.Content = (int)decimals.Max();
                yMinValue.Content = (int)-decimals.Max();
            }

            // Adding labels to scale on Canvas
            DrawingCanvas.Children.Add(yMinValue);
            DrawingCanvas.Children.Add(yMaxValue);

            // Setting the values of respective labels
            MinValue.Content = $" Minimum value is {min:F2}";
            MaxValue.Content = $"Maximum value is {max:F2}";
            return scale;
        }

        private void ClearAndBuildCanvas()
        {
            // Clearing and setting the Coordinate System
            DrawingCanvas.Children.Clear();

            // X Axis
            Polyline xAxis = new Polyline();
            xAxis.Stroke = Brushes.Black;
            xAxis.StrokeThickness = 2;
            xAxis.Points.Add(new Point(0, 150));
            xAxis.Points.Add(new Point(300, 150));
            
            // Y axis
            Polyline yAxis = new Polyline();
            yAxis.Stroke = Brushes.Black;
            yAxis.StrokeThickness = 2;
            yAxis.Points.Add(new Point(150, 0));
            yAxis.Points.Add(new Point(150, 300));

            // Y Axis label
            Label yLabel = new Label();
            yLabel.Content = "Y";
            Canvas.SetLeft(yLabel, 150);
            Canvas.SetTop(yLabel, 0);

            // X Scale - Minimum value
            Label xMinLabel = new Label();
            xMinLabel.Content = "-4";
            Canvas.SetLeft(xMinLabel, 0);
            Canvas.SetTop(xMinLabel, 150);

            // X Scale - Max value
            Label xMaxLabel = new Label();
            xMaxLabel.Content = "4";
            Canvas.SetLeft(xMaxLabel, 300);
            Canvas.SetTop(xMaxLabel, 150);

            // Zero label
            Label zeroLabel = new Label();
            zeroLabel.Content = "0";
            Canvas.SetLeft(zeroLabel, 150);
            Canvas.SetTop(zeroLabel, 150);

            // Adding to Canvas
            DrawingCanvas.Children.Add(xAxis);
            DrawingCanvas.Children.Add(yAxis);
            DrawingCanvas.Children.Add(yLabel);
            DrawingCanvas.Children.Add(xMinLabel);
            DrawingCanvas.Children.Add(xMaxLabel);
            DrawingCanvas.Children.Add(zeroLabel);
        }
    }
}
