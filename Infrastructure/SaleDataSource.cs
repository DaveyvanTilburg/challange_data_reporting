using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using Domain.Sales;

namespace Infrastructure
{
    public class SaleDataSource : IDataSource<Sale>
    {
        public IEnumerable<Sale> Data()
        {
            List<string> lines = File.ReadAllLines(".\\MockedData\\sales.csv").ToList();

            foreach (string line in lines.Skip(1))
                yield return new Sale(line);
        }
    }
}