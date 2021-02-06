using System.Linq;
using Domain.ValueGrouping;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ValueGrouping
{
    public class ValueGroupingCases
    {
        [Fact]
        public void SaleTotalValueGrouping()
        {
            var subject = new SaleTotalValueGrouping(
                new MockedPointGrouping()
            );

            var chart = subject.Chart();
            
            chart.PointCategories.Count().Should().Be(1);
            chart.ChartLines.Count().Should().Be(1);
            chart.ChartLines.ElementAt(0).Data.ElementAt(0).Should().Be(9000);
        }

        [Fact]
        public void SaleAverageValueGrouping()
        {
            var subject = new SaleAverageValueGrouping(
                new MockedPointGrouping()
            );

            var chart = subject.Chart();

            chart.PointCategories.Count().Should().Be(1);
            chart.ChartLines.Count().Should().Be(1);
            chart.ChartLines.ElementAt(0).Data.ElementAt(0).Should().Be(750);
        }
    }
}