using System.Collections.Generic;
using System.Linq;
using Domain.Charts;
using Domain.PointGrouping;

namespace Domain.ValueGrouping
{
    public abstract class SaveValueGroupingBase : IValueGrouping
    {
        private readonly IPointGrouping<Sale> _pointGrouping;
        
        protected SaveValueGroupingBase(IPointGrouping<Sale> pointGrouping)
        {
            _pointGrouping = pointGrouping;
        }

        public Chart Chart()
        {
            IEnumerable<LinePointsGroup<Sale>> input = _pointGrouping.PointGroups();
            var chart = new Chart(ChartLines(input), PointCategories(input), Title());
            
            return chart;
        }

        protected abstract string Title();
        
        private IEnumerable<string> PointCategories(IEnumerable<LinePointsGroup<Sale>> input)
        {
            var pointCategories = new List<string>();

            foreach (PointGroup<Sale> periodGroup in input.First().Data())
                pointCategories.Add(periodGroup.PointCategory());
            
            return pointCategories;
        }
        
        private IEnumerable<ChartLine> ChartLines(IEnumerable<LinePointsGroup<Sale>> input)
        {
            var chartLines = new List<ChartLine>();
            
            foreach (LinePointsGroup<Sale> typePeriodGroup in input.OrderBy(i => i.Type()))
            {
                var linePoints = new List<int>();
                foreach (PointGroup<Sale> pointGroup in typePeriodGroup.Data())
                    linePoints.Add(Value(pointGroup));

                chartLines.Add(new ChartLine(typePeriodGroup.Type(), linePoints.ToArray()));
            }

            return chartLines;
        }

        protected abstract int Value(PointGroup<Sale> pointData);
    }
}