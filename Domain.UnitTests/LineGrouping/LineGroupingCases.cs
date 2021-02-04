using System.Collections.Generic;
using System.Linq;
using Domain.LineGrouping;
using Domain.ValueGrouping;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.LineGrouping
{
    public class LineGroupingCases
    {
        [Fact]
        public void GroupByType()
        {
            var subject = new GroupByType<Sale>(
                new MockedDataSource()
            );

            IEnumerable<LineGroup<Sale>> lineGroups = subject.LineGroups();

            lineGroups.Count().Should().Be(4);

            lineGroups.ElementAt(0).Type().Should().Be("Teddybear");
            lineGroups.ElementAt(0).Data().Count().Should().Be(10);

            lineGroups.ElementAt(1).Type().Should().Be("Table");
            lineGroups.ElementAt(1).Data().Count().Should().Be(4);

            lineGroups.ElementAt(2).Type().Should().Be("Lamp");
            lineGroups.ElementAt(2).Data().Count().Should().Be(4);

            lineGroups.ElementAt(3).Type().Should().Be("Chair");
            lineGroups.ElementAt(3).Data().Count().Should().Be(2);
        }
    }
}