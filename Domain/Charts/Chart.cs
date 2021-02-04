using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domain.Charts
{
    public class Chart
    {
        public IEnumerable<ChartLine> ChartLines { get; }
        public IEnumerable<string> PointCategories { get; }
        public string Title { get; }
        
        public Chart(IEnumerable<ChartLine> chartLines, IEnumerable<string> pointCategories, string title)
        {
            ChartLines = chartLines;
            PointCategories = pointCategories;
            Title = title;
        }
        
        public override string ToString()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string result = JsonConvert.SerializeObject(this, serializerSettings);
            return result;
        }
    }
}