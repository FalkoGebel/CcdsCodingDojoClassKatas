namespace _03_BoxPlot
{
    public static class BoxPlotHelpers
    {
        public static (double, double, double) GetMedianAndQuartiles(int[] sortedData)
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