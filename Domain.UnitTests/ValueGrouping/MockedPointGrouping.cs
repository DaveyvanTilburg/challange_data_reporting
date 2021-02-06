using System.Collections.Generic;
using Domain.PointGrouping;
using Domain.ValueGrouping;

namespace Domain.UnitTests.ValueGrouping
{
    internal class MockedPointGrouping : IPointGrouping<Sale>
    {
        public IEnumerable<LinePointsGroup<Sale>> LinePointGroups()
        {
            var retrievers = new PointGroup<Sale>("Retrievers");
            retrievers.Add(new Sale("\"Golden\";\"2.000,00\";4;\"2018-01-01\""));
            retrievers.Add(new Sale("\"Golden\";\"5.000,00\";2;\"2018-01-01\""));
            retrievers.Add(new Sale("\"Golden\";\"2.000,00\";6;\"2018-01-01\""));

            yield return new LinePointsGroup<Sale>(
                new List<PointGroup<Sale>>
                {
                    retrievers
                },
                "Puppies"
            );
        }
    }
}