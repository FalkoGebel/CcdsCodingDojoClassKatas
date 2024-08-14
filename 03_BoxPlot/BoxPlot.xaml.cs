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
                max = _values[^1];

            if (min == max)
                return;

            int topMarginScaleLine = 20,
                topMarginBox = 100,
                leftMargin = 20,
                rightMargin = 20,
                scaleRangeLineLength = 20,
                boxHeight = 40,
                range = max - min;

            double width = OutputCanvas.ActualWidth - leftMargin - rightMargin,
                   sideMargin = ((int)OutputCanvas.ActualWidth - width) / 2;
            (double lowerQuartile, double median, double upperQuartile) = BoxPlotHelpers.GetMedianAndQuartiles([.. _values]);
            double lowerQuartileWidth = (lowerQuartile - min) / range * width,
                   medianWidth = (median - min) / range * width,
                   upperQuartileWidth = (upperQuartile - min) / range * width;

            // Add number line
            Line line = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin,
                Y1 = topMarginScaleLine,
                X2 = sideMargin + width,
                Y2 = topMarginScaleLine,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(line);

            // Add minimum line and number
            line = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin,
                Y1 = topMarginScaleLine - scaleRangeLineLength / 2,
                X2 = sideMargin,
                Y2 = topMarginScaleLine + scaleRangeLineLength / 2,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(line);

            FormattedText text = new(min.ToString(), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Consolas"), 10, Brushes.Black);
            TextBlock textBlock = new()
            {
                Text = text.Text,
                Foreground = Brushes.Black
            };
            double x = line.X2 - (text.Width / 2);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, line.Y2 + 5);
            OutputCanvas.Children.Add(textBlock);

            // Add lower quartile line
            if (lowerQuartile > double.MinValue)
            {
                line = new()
                {
                    Stroke = Brushes.Black,
                    X1 = sideMargin + lowerQuartileWidth,
                    Y1 = topMarginScaleLine - scaleRangeLineLength / 2,
                    X2 = sideMargin + lowerQuartileWidth,
                    Y2 = topMarginScaleLine + scaleRangeLineLength / 2,
                    StrokeThickness = 1
                };
                OutputCanvas.Children.Add(line);

                if (lowerQuartileWidth > 50)
                {
                    text = new(lowerQuartile.ToString("F2"), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Consolas"), 10, Brushes.Black);
                    textBlock = new()
                    {
                        Text = text.Text,
                        Foreground = Brushes.Black
                    };
                    x = line.X2 - (text.Width / 2);
                    Canvas.SetLeft(textBlock, x);
                    Canvas.SetTop(textBlock, line.Y2 + 5);
                    OutputCanvas.Children.Add(textBlock);
                }
            }

            // Add median line
            line = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin + medianWidth,
                Y1 = topMarginScaleLine - scaleRangeLineLength / 2,
                X2 = sideMargin + medianWidth,
                Y2 = topMarginScaleLine + scaleRangeLineLength / 2,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(line);

            text = new(median.ToString("F2"), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Consolas"), 10, Brushes.Black);
            textBlock = new()
            {
                Text = text.Text,
                Foreground = Brushes.Black
            };
            x = line.X2 - (text.Width / 2);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, line.Y2 + 5);
            OutputCanvas.Children.Add(textBlock);

            // Add upper quartile line
            if (upperQuartile > double.MinValue)
            {
                line = new()
                {
                    Stroke = Brushes.Black,
                    X1 = sideMargin + upperQuartileWidth,
                    Y1 = topMarginScaleLine - scaleRangeLineLength / 2,
                    X2 = sideMargin + upperQuartileWidth,
                    Y2 = topMarginScaleLine + scaleRangeLineLength / 2,
                    StrokeThickness = 1
                };
                OutputCanvas.Children.Add(line);

                if (width - upperQuartileWidth > 50)
                {
                    text = new(upperQuartile.ToString("F2"), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Consolas"), 10, Brushes.Black);
                    textBlock = new()
                    {
                        Text = text.Text,
                        Foreground = Brushes.Black
                    };
                    x = line.X2 - (text.Width / 2);
                    Canvas.SetLeft(textBlock, x);
                    Canvas.SetTop(textBlock, line.Y2 + 5);
                    OutputCanvas.Children.Add(textBlock);
                }
            }

            // Add maximum line and number
            line = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin + width,
                Y1 = topMarginScaleLine - scaleRangeLineLength / 2,
                X2 = sideMargin + width,
                Y2 = topMarginScaleLine + scaleRangeLineLength / 2,
                StrokeThickness = 1
            };
            OutputCanvas.Children.Add(line);

            text = new(max.ToString(), System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Consolas"), 10, Brushes.Black);
            textBlock = new()
            {
                Text = text.Text,
                Foreground = Brushes.Black
            };
            x = line.X2 - (text.Width / 2);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, line.Y2 + 5);
            OutputCanvas.Children.Add(textBlock);

            // Add box line left end
            line = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin,
                Y1 = topMarginBox - boxHeight / 2,
                X2 = sideMargin,
                Y2 = topMarginBox + boxHeight / 2,
                StrokeThickness = 2
            };
            OutputCanvas.Children.Add(line);

            // Add horizontial line left from box
            double addToX2 = lowerQuartile > double.MinValue ? lowerQuartileWidth : medianWidth;
            line = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin,
                Y1 = topMarginBox,
                X2 = sideMargin + addToX2,
                Y2 = topMarginBox,
                StrokeThickness = 2
            };
            OutputCanvas.Children.Add(line);

            // Add horizontial line right from box
            double addToX1 = upperQuartile > double.MinValue ? upperQuartileWidth : medianWidth;
            line = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin + addToX1,
                Y1 = topMarginBox,
                X2 = sideMargin + width,
                Y2 = topMarginBox,
                StrokeThickness = 2
            };
            OutputCanvas.Children.Add(line);

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
            line = new()
            {
                Stroke = Brushes.Black,
                X1 = sideMargin + width,
                Y1 = topMarginBox - boxHeight / 2,
                X2 = sideMargin + width,
                Y2 = topMarginBox + boxHeight / 2,
                StrokeThickness = 2
            };
            OutputCanvas.Children.Add(line);

            // Add box line median
            line = new()
            {
                Stroke = Brushes.Blue,
                X1 = sideMargin + medianWidth,
                Y1 = topMarginBox - boxHeight / 2,
                X2 = sideMargin + medianWidth,
                Y2 = topMarginBox + boxHeight / 2,
                StrokeThickness = 2
            };
            OutputCanvas.Children.Add(line);
        }
    }
}