﻿using System.Diagnostics;
using System.Linq;
using Domain.Charts;
using Domain.LineGrouping;
using Domain.PointGrouping;
using Domain.UnitTests.LineGrouping;
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
                new GroupByWeek<Sale>(
                    new GroupByType<Sale>(
                        new MockedDataSource()
                    )
                )
            );

            Chart chart = subject.Chart();
            
            chart.PointCategories.Count().Should().Be(9);
            
            foreach (ChartLine chartLine in chart.ChartLines)
                chartLine.Data.Length.Should().Be(9);

            chart.PointCategories.First().Should().Be("2018-5");
            chart.PointCategories.Last().Should().Be("2018-13");

            chart.ChartLines.ElementAt(0).Data.Should().BeEquivalentTo(new[] { 0, 0, 0, 0, 0, 0, 0, 2655, 2655 });
            chart.ChartLines.ElementAt(1).Data.Should().BeEquivalentTo(new[] { 0, 0, 0, 0, 0, 0, 2687, 0, 0 });
            chart.ChartLines.ElementAt(2).Data.Should().BeEquivalentTo(new[] { 11729, 0, 0, 0, 0, 0, 0, 0, 0 });
            chart.ChartLines.ElementAt(3).Data.Should().BeEquivalentTo(new[] { 15157, 3789, 11368, 7579, 0, 0, 0, 0, 0 });
        }

        [Fact]
        public void SalesByTypePerMonthTotalValue()
        {
            var subject = new SaleTotalValueGrouping(
                new GroupByMonth<Sale>(
                    new GroupByType<Sale>(
                        new MockedDataSource()
                    )
                )
            );

            Chart chart = subject.Chart();

            chart.PointCategories.Count().Should().Be(2);

            foreach (ChartLine chartLine in chart.ChartLines)
                chartLine.Data.Length.Should().Be(2);

            chart.PointCategories.First().Should().Be("2018-2");
            chart.PointCategories.Last().Should().Be("2018-3");

            chart.ChartLines.ElementAt(0).Data.Should().BeEquivalentTo(new[] { 0, 5309 });
            chart.ChartLines.ElementAt(1).Data.Should().BeEquivalentTo(new[] { 0, 2687 });
            chart.ChartLines.ElementAt(2).Data.Should().BeEquivalentTo(new[] { 0, 11729 });
            chart.ChartLines.ElementAt(3).Data.Should().BeEquivalentTo(new[] { 37893, 0 });
        }

        [Fact]
        public void LoadTest()
        {
            var subject = new SaleTotalValueGrouping(
                new GroupByWeek<Sale>(
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