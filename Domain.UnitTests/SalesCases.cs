using System.Linq;
using Domain.Charts;
using Domain.LineGrouping;
using Domain.PointGrouping;
using Domain.Sales;
using FluentAssertions;
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
                        new DataSource()
                    )
                )
            );

            Chart chart = subject.Chart();
            
            chart.PointCategories.Count().Should().Be(6);
            
            foreach (ChartLine chartLine in chart.ChartLines)
                chartLine.Values().Length.Should().Be(6);
        }
    }
}