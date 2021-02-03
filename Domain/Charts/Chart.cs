using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Charts
{
    public class Chart
    {
        internal IEnumerable<ChartLine> ChartLines { get; }
        internal IEnumerable<string> PointCategories { get; }
        
        public Chart(IEnumerable<ChartLine> chartLines, IEnumerable<string> pointCategories)
        {
            ChartLines = chartLines;
            PointCategories = pointCategories;
        }
        
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}