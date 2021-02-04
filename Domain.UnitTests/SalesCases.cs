using System.Diagnostics;
using System.Linq;
using Domain.Charts;
using Domain.LineGrouping;
using Domain.PointGrouping;
using Domain.Sales;
using Domain.ValueGrouping;
using FluentAssertions;
using Infrastructure;
using Xunit;

namespace Domain.UnitTests
{
    public class SalesCases
    {
        [Fact]
        public void SalesByTypePerWeekTotalValue()
        {
            var subject = new SaleTotalValueGrouping(
                new GroupPerWeek<Sale>(
                    new GroupByType<Sale>(
                        new MockedDataSource()
                    )
                )
            );

            Chart chart = subject.Chart();
            
            chart.PointCategories.Count().Should().Be(9);
            
            foreach (ChartLine chartLine in chart.ChartLines)
                chartLine.Data.Length.Should().Be(9);

            chart.PointCategories.First().Should().Be("5");
            chart.PointCategories.Last().Should().Be("13");

            chart.ChartLines.ElementAt(0).Data.Should().BeEquivalentTo(new[] { 0, 0, 0, 0, 0, 0, 0, 2655, 2655 });
            chart.ChartLines.ElementAt(1).Data.Should().BeEquivalentTo(new[] { 0, 0, 0, 0, 0, 0, 2687, 0, 0 });
            chart.ChartLines.ElementAt(2).Data.Should().BeEquivalentTo(new[] { 11729, 0, 0, 0, 0, 0, 0, 0, 0 });
            chart.ChartLines.ElementAt(3).Data.Should().BeEquivalentTo(new[] { 15157, 3789, 11368, 7579, 0, 0, 0, 0, 0 });
        }

        [Fact]
        public void LoadTest()
        {
            var subject = new SaleTotalValueGrouping(
                new GroupPerWeek<Sale>(
                    new GroupByType<Sale>(
                        new SaleDataSource()
                    )
                )
            );

            var stopwatch = Stopwatch.StartNew();
            subject.Chart();
            stopwatch.Stop();

            stopwatch.ElapsedMilliseconds.Should().BeLessOrEqualTo(200);
        }
    }
}