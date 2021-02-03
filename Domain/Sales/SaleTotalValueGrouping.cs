using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Charts;
using Domain.PointGrouping;

namespace Domain.Sales
{
    public class SaleTotalValueGrouping
    {
        private readonly IPointGrouping<Sale> _pointGrouping;
        
        public SaleTotalValueGrouping(IPointGrouping<Sale> pointGrouping)
        {
            _pointGrouping = pointGrouping;
        }

        public Chart Chart()
        {
            IEnumerable<LinePointsGroup<Sale>> input = _pointGrouping.Groups();
            var chart = new Chart(ChartLines(input), PointCategories(input));
            
            return chart;
        }
        
        private IEnumerable<string> PointCategories(IEnumerable<LinePointsGroup<Sale>> input)
        {
            var pointCategories = new List<string>();

            foreach (LinePointsGroup<Sale> typePeriodGroup in input)
                foreach (PointGroup<Sale> periodGroup in typePeriodGroup.Data())
                pointCategories.Add(periodGroup.Period());

            pointCategories = pointCategories.Distinct().ToList();
            
            return pointCategories;
        }
        
        private IEnumerable<ChartLine> ChartLines(IEnumerable<LinePointsGroup<Sale>> input)
        {
            var chartLines = new List<ChartLine>();
            
            foreach (LinePointsGroup<Sale> typePeriodGroup in input)
            {
                var linePoints = new List<int>();
                foreach (PointGroup<Sale> periodGroup in typePeriodGroup.Data())
                    linePoints.Add(
                        Convert.ToInt32(periodGroup.Data().Sum(item => item.TotalValue()))
                    );

                chartLines.Add(new ChartLine(typePeriodGroup.Type(), linePoints.ToArray()));
            }

            return chartLines;
        }
    }
}