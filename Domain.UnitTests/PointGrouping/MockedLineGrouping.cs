using System.Collections.Generic;
using Domain.LineGrouping;
using Domain.ValueGrouping;

namespace Domain.UnitTests.PointGrouping
{
    internal class MockedLineGrouping : ILineGrouping<Sale>
    {
        public IEnumerable<LineGroup<Sale>> LineGroups()
        {
            yield return
                new LineGroup<Sale>(
                    new List<Sale>
                    {
                        new Sale("\"Glasses\";\"1\";1;\"2018-01-01\""),
                        new Sale("\"Glasses\";\"1\";1;\"2018-01-08\""),
                        new Sale("\"Glasses\";\"1\";1;\"2019-01-01\"")
                    },
                    "Glasses"
                );
        }
    }
}