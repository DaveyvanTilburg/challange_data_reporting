using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.ValueGrouping;

namespace Domain.UnitTests.LineGrouping
{
    internal class MockedDataSource : IDataSource<Sale>
    {
        public IEnumerable<Sale> Data()
        {
            List<string> lines = File.ReadAllLines(".\\TestData.csv").ToList();

            foreach (string line in lines)
                yield return new Sale(line);
        }
    }
}