using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Domain;
using Domain.Sales;

namespace Infrastructure
{
    public class SaleDataSource : IDataSource<Sale>
    {
        public IEnumerable<Sale> Data()
        {
            string[] lines = ReadResource("Infrastructure.MockedData.sales.csv");

            foreach (string line in lines.Skip(1))
                yield return new Sale(line);
        }

        public string[] ReadResource(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}