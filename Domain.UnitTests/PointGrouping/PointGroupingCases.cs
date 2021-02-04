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

            IEnumerable<LinePointsGroup<Sale>> pointGroups = subject.PointGroups();

            pointGroups.Count().Should().Be(1);
            
            pointGroups.ElementAt(0).Type().Should().Be("Glasses");
            pointGroups.ElementAt(0).Data().Count().Should().Be(13);
        }

        [Fact]
        public void GroupByWeek()
        {
            var subject = new GroupByWeek<Sale>(
                new MockedLineGrouping()
            );

            IEnumerable<LinePointsGroup<Sale>> pointGroups = subject.PointGroups();

            pointGroups.Count().Should().Be(1);

            pointGroups.ElementAt(0).Type().Should().Be("Glasses");
            pointGroups.ElementAt(0).Data().Count().Should().Be(53);

            pointGroups.ElementAt(0).Data().ElementAt(0).Data().Count().Should().Be(1);
            pointGroups.ElementAt(0).Data().ElementAt(1).Data().Count().Should().Be(1);
            pointGroups.ElementAt(0).Data().ElementAt(52).Data().Count().Should().Be(1);
        }
    }
}