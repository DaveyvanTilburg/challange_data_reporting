using System.Collections.Generic;
using System.Linq;
using Domain.PointGrouping;
using Domain.ValueGrouping;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.PointGrouping
{
    public class PointGroupingCases
    {
        [Fact]
        public void GroupByMonth()
        {
            var subject = new GroupByMonth<Sale>(
                    new MockedLineGrouping()
                );

            IEnumerable<LinePointsGroup<Sale>> linePointGroups = subject.LinePointGroups();

            linePointGroups.Count().Should().Be(1);
            
            linePointGroups.ElementAt(0).Type().Should().Be("Glasses");
            linePointGroups.ElementAt(0).Data().Count().Should().Be(13);

            linePointGroups.ElementAt(0).Data().ElementAt(0).Data().Count().Should().Be(2);
            linePointGroups.ElementAt(0).Data().ElementAt(12).Data().Count().Should().Be(1);
        }

        [Fact]
        public void GroupByWeek()
        {
            var subject = new GroupByWeek<Sale>(
                new MockedLineGrouping()
            );

            IEnumerable<LinePointsGroup<Sale>> linePointGroups = subject.LinePointGroups();

            linePointGroups.Count().Should().Be(1);

            linePointGroups.ElementAt(0).Type().Should().Be("Glasses");
            linePointGroups.ElementAt(0).Data().Count().Should().Be(53);

            linePointGroups.ElementAt(0).Data().ElementAt(0).Data().Count().Should().Be(1);
            linePointGroups.ElementAt(0).Data().ElementAt(1).Data().Count().Should().Be(1);
            linePointGroups.ElementAt(0).Data().ElementAt(52).Data().Count().Should().Be(1);
        }

        [Fact]
        public void GroupByDayOfWeek()
        {
            var subject = new GroupByDayOfWeek<Sale>(
                new MockedLineGrouping()
            );

            IEnumerable<LinePointsGroup<Sale>> linePointGroups = subject.LinePointGroups();

            linePointGroups.Count().Should().Be(1);

            linePointGroups.ElementAt(0).Type().Should().Be("Glasses");
            linePointGroups.ElementAt(0).Data().Count().Should().Be(7);

            linePointGroups.ElementAt(0).Data().ElementAt(1).Data().Count().Should().Be(2);
            linePointGroups.ElementAt(0).Data().ElementAt(2).Data().Count().Should().Be(1);
        }
    }
}