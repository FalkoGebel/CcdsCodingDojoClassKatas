using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _03_BoxPlot
{
    /// <summary>
    /// Interaktionslogik für BoxPlot.xaml
    /// </summary>
    public partial class BoxPlot : UserControl
    {
        private List<int> _values = [];

        public BoxPlot()
        {
            InitializeComponent();
            SetInitValues();
        }

        private void SetInitValues()
        {
            // Add a Line Element
            Line myLine = new()
            {
                Stroke = System.Windows.Media.Brushes.LightSteelBlue,
                X1 = 1,
                X2 = 50,
                Y1 = 1,
                Y2 = 50,
                StrokeThickness = 2
            };
            OutputCanvas.Children.Add(myLine);
        }

        public void UpdateWithValues(List<int> values)
        {
            _values = values;
            UpdateWithoutValues();
        }

        private void UserControl_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            UpdateWithoutValues();
        }

        private void UpdateWithoutValues()
        {
            OutputCanvas.Children.Clear();

            if (_values.Count < 2)
                return;

            int min = _values[0],
                max = _values[^1],
                topMargin = 20,
                leftMargin = 10,
                rightMargin = 10,
                rangeLineLength = 20;

            double width = OutputCanvas.ActualWidth - leftMargin - rightMargin,
                   sideMargin = ((int)OutputCanvas.ActualWidth - width) / 2;

            // Add number line
            Line numberLine = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin,
                Y1 = topMargin,
                X2 = sideMargin + width,
                Y2 = topMargin,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(numberLine);

            // TODO - scale the percentile lines to the width of the number line

            // Add minimum line and number
            Line rangeLine = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin,
                Y1 = topMargin - rangeLineLength / 2,
                X2 = sideMargin,
                Y2 = topMargin + rangeLineLength / 2,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(rangeLine);

            FormattedText text = new(min.ToString(), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Consolas"), 10, Brushes.Black);
            TextBlock textBlock = new()
            {
                Text = text.Text,
                Foreground = Brushes.Black
            };
            double x = rangeLine.X2 - (text.Width / 2);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, rangeLine.Y2 + 5);
            OutputCanvas.Children.Add(textBlock);

            // Add lower quartile line
            double lowerQuartile = Percentile([.. _values], 25);
            rangeLine = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin + lowerQuartile,
                Y1 = topMargin - rangeLineLength / 2,
                X2 = sideMargin + lowerQuartile,
                Y2 = topMargin + rangeLineLength / 2,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(rangeLine);

            // Add median line
            double Median = Percentile([.. _values], 50);
            rangeLine = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin + Median,
                Y1 = topMargin - rangeLineLength / 2,
                X2 = sideMargin + Median,
                Y2 = topMargin + rangeLineLength / 2,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(rangeLine);

            // Add upper quartile line
            double upperQuartile = Percentile([.. _values], 75);
            rangeLine = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin + upperQuartile,
                Y1 = topMargin - rangeLineLength / 2,
                X2 = sideMargin + upperQuartile,
                Y2 = topMargin + rangeLineLength / 2,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(rangeLine);

            // Add maximum line and number
            rangeLine = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin + width,
                Y1 = topMargin - rangeLineLength / 2,
                X2 = sideMargin + width,
                Y2 = topMargin + rangeLineLength / 2,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(rangeLine);

            text = new(max.ToString(), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Consolas"), 10, Brushes.Black);
            textBlock = new()
            {
                Text = text.Text,
                Foreground = Brushes.Black
            };
            x = rangeLine.X2 - (text.Width / 2);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, rangeLine.Y2 + 5);
            OutputCanvas.Children.Add(textBlock);
        }

        /// <summary>
        /// Calculate percentile of a sorted data set.
        /// </summary>
        /// <param name="sortedData">The sorted integer array.</param>
        /// <param name="p">The percentile to calculate.</param>
        /// <returns>Percentile of the sorted data.</returns>
        private static double Percentile(int[] sortedData, double p)
        {
            // algo derived from Aczel pg 15 bottom
            if (p >= 100.0d)
                return sortedData[sortedData.Length - 1];

            double position = (sortedData.Length + 1) * p / 100.0,
                   leftNumber = 0.0d, rightNumber = 0.0d,
                   n = p / 100.0d * (sortedData.Length - 1) + 1.0d;

            if (position >= 1)
            {
                leftNumber = sortedData[(int)Math.Floor(n) - 1];
                rightNumber = sortedData[(int)Math.Floor(n)];
            }
            else
            {
                leftNumber = sortedData[0]; // first data
                rightNumber = sortedData[1]; // first data
            }

            if (Equals(leftNumber, rightNumber))
                return leftNumber;

            double part = n - Math.Floor(n);

            return leftNumber + part * (rightNumber - leftNumber);
        }
    }
}
