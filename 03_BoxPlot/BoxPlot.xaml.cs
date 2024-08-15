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
        public string? Title { get; set; }

        private List<int> _values = [];

        public BoxPlot()
        {
            InitializeComponent();
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

        private void DrawLine(SolidColorBrush color, double x1, double y1, double x2, double y2, double strokeThickness)
        {
            Line line = new()
            {
                Stroke = color,
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                StrokeThickness = strokeThickness
            };
            OutputCanvas.Children.Add(line);
        }

        private void UpdateWithoutValues()
        {
            OutputCanvas.Children.Clear();

            if (_values.Count < 2)
                return;

            int min = _values[0],
                max = _values[^1];

            if (min == max)
                return;

            int topMarginScaleLine = 20,
                topMarginBox = 100,
                leftMargin = 20,
                rightMargin = 20,
                scaleRangeLineLength = 20,
                scaleRangeMinWidth = 20,
                boxHeight = 40,
                range = max - min;

            double width = OutputCanvas.ActualWidth - leftMargin - rightMargin,
                   sideMargin = ((int)OutputCanvas.ActualWidth - width) / 2;
            (double lowerQuartile, double median, double upperQuartile) = GetMedianAndQuartiles([.. _values]);
            double lowerQuartileWidth = (lowerQuartile - min) / range * width,
                   medianWidth = (median - min) / range * width,
                   upperQuartileWidth = (upperQuartile - min) / range * width;

            // Add number line
            DrawLine(Brushes.LightSteelBlue, sideMargin, topMarginScaleLine, sideMargin + width, topMarginScaleLine, 1);

            // Add range lines
            double scaleRangeValue = GetScaleRangeValue(width, scaleRangeMinWidth, min, max);

            List<double> rangeValues = [min];

            if (scaleRangeValue <= 1)
            {
                for (double i = min + scaleRangeValue; i < max; i += scaleRangeValue)
                    rangeValues.Add(i);
            }
            else
            {
                int start = (min + (int)scaleRangeValue) / (int)scaleRangeValue * (int)scaleRangeValue;

                for (double i = start; i <= max - scaleRangeValue; i += scaleRangeValue)
                    rangeValues.Add(i);
            }

            rangeValues.Add(max);

            foreach (double i in rangeValues)
            {
                double scaleRangeWidth = (i - min) / range * width;
                DrawLine(Brushes.LightSteelBlue, sideMargin + scaleRangeWidth, topMarginScaleLine - scaleRangeLineLength / 2, sideMargin + scaleRangeWidth, topMarginScaleLine + scaleRangeLineLength / 2, 1);

                if (i % 1 == 0)
                {
                    int v = (int)i;
                    FormattedText loopText = new(v.ToString(), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Consolas"), 10, Brushes.Black);
                    TextBlock loopTextBlock = new()
                    {
                        Text = loopText.Text,
                        Foreground = Brushes.Black
                    };
                    double loopX = sideMargin + scaleRangeWidth - (loopText.Width / 2);
                    Canvas.SetLeft(loopTextBlock, loopX);
                    Canvas.SetTop(loopTextBlock, topMarginScaleLine + scaleRangeLineLength / 2 + 5);
                    OutputCanvas.Children.Add(loopTextBlock);
                }
            }

            // Add box line left end
            DrawLine(Brushes.Black, sideMargin, topMarginBox - boxHeight / 2, sideMargin, topMarginBox + boxHeight / 2, 2);

            // Add horizontial line left from box
            double addToX2 = lowerQuartile > double.MinValue ? lowerQuartileWidth : medianWidth;
            DrawLine(Brushes.Black, sideMargin, topMarginBox, sideMargin + addToX2, topMarginBox, 2);

            // Add horizontial line right from box
            double addToX1 = upperQuartile > double.MinValue ? upperQuartileWidth : medianWidth;
            DrawLine(Brushes.Black, sideMargin + addToX1, topMarginBox, sideMargin + width, topMarginBox, 2);

            // Add box
            Rectangle rectangle = new()
            {
                Stroke = Brushes.Green,
                Width = addToX1 - addToX2,
                Height = boxHeight,
                StrokeThickness = 2
            };
            Canvas.SetLeft(rectangle, sideMargin + addToX2);
            Canvas.SetTop(rectangle, topMarginBox - boxHeight / 2);
            OutputCanvas.Children.Add(rectangle);

            // Add box line right end
            DrawLine(Brushes.Black, sideMargin + width, topMarginBox - boxHeight / 2, sideMargin + width, topMarginBox + boxHeight / 2, 2);

            // Add box line median
            DrawLine(Brushes.Blue, sideMargin + medianWidth, topMarginBox - boxHeight / 2, sideMargin + medianWidth, topMarginBox + boxHeight / 2, 2);
        }
        private static (double, double, double) GetMedianAndQuartiles(int[] sortedData)
        {
            double lowerQuartile = double.MinValue,
                   median = double.MinValue,
                   upperQuartile = double.MinValue;

            if (sortedData.Length > 0)
            {
                median = GetMedian(sortedData);

                if (sortedData.Length > 2)
                {
                    lowerQuartile = GetMedian(sortedData[0..(sortedData.Length / 2 - (sortedData.Length % 2 == 0 ? 1 : 0))]);
                    upperQuartile = GetMedian(sortedData[(sortedData.Length / 2 + 1)..]);
                }
            }

            return (lowerQuartile, median, upperQuartile);
        }

        private static double GetScaleRangeValue(double width, int minRangeWidth, int min, int max)
        {
            double scaleCandidate = 0.5;
            int[] factors = [2, 5];
            int factorIndex = -1,
                maxNumberOfRanges = (int)width / minRangeWidth,
                range = max - min;

            while (true)
            {
                double numberOfRanges = range / scaleCandidate;

                if (numberOfRanges <= maxNumberOfRanges)
                    return scaleCandidate;

                factorIndex = factorIndex + 1 == factors.Length ? 0 : factorIndex + 1;
                scaleCandidate *= factors[factorIndex];
            }
        }
        private static double GetMedian(int[] sortedData)
        {
            if (sortedData.Length == 0)
                throw new ArgumentException("Invalid collection - sorted data must not be empty");

            int idx = sortedData.Length / 2;
            double output = sortedData[idx];

            if (sortedData.Length % 2 == 0)
            {
                output += sortedData[idx - 1];
                output /= 2;
            }

            return output;
        }
    }
}