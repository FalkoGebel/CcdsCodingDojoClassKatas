using _03_BoxPlot;
using FluentAssertions;

namespace Tests
{
    [TestClass]
    public class BoxPlotTests
    {
        [TestMethod]
        public void Test_GetMedianAndQuartiles_Method_1()
        {
            int[] values = [17, 18, 18, 19, 19, 20, 24, 24, 24, 25];
            double expectedLowerQuartile = 18,
                   expectedMedian = 19.5,
                   exprectedUpperQuartile = 24,
                   lowerQuartile,
                   median,
                   upperQuartile;

            (lowerQuartile, median, upperQuartile) = BoxPlotHelpers.GetMedianAndQuartiles(values);

            median.Should().Be(expectedMedian);
            lowerQuartile.Should().Be(expectedLowerQuartile);
            upperQuartile.Should().Be(exprectedUpperQuartile);
        }

        [TestMethod]
        public void Test_GetMedianAndQuartiles_Method_2()
        {
            int[] values = [1, 2, 3];
            double expectedLowerQuartile = 1,
                   expectedMedian = 2,
                   exprectedUpperQuartile = 3,
                   lowerQuartile,
                   median,
                   upperQuartile;

            (lowerQuartile, median, upperQuartile) = BoxPlotHelpers.GetMedianAndQuartiles(values);

            median.Should().Be(expectedMedian);
            lowerQuartile.Should().Be(expectedLowerQuartile);
            upperQuartile.Should().Be(exprectedUpperQuartile);
        }

        [TestMethod]
        public void Test_GetMedianAndQuartiles_Method_3()
        {
            int[] values = [-5, -1, 0, 0, 0, 2, 4];
            double expectedLowerQuartile = -1,
                   expectedMedian = 0,
                   exprectedUpperQuartile = 2,
                   lowerQuartile,
                   median,
                   upperQuartile;

            (lowerQuartile, median, upperQuartile) = BoxPlotHelpers.GetMedianAndQuartiles(values);

            median.Should().Be(expectedMedian);
            lowerQuartile.Should().Be(expectedLowerQuartile);
            upperQuartile.Should().Be(exprectedUpperQuartile);
        }
    }
}
