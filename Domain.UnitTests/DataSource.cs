using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Sales;

namespace Domain.UnitTests
{
    internal class DataSource : IDataSource<Sale>
    {
        public IEnumerable<Sale> Data()
        {
            List<string> lines = File.ReadAllLines(".\\TestData.csv").ToList();

            foreach (string line in lines)
                yield return new Sale(line);
        }
    }
}