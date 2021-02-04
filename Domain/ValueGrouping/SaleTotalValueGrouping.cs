using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Charts;
using Domain.PointGrouping;
using Domain.Sales;

namespace Domain.ValueGrouping
{
    public class SaleTotalValueGrouping : IValueGrouping
    {
        private readonly IPointGrouping<Sale> _pointGrouping;

        private const string Title = "Total sales value";
        
        public SaleTotalValueGrouping(IPointGrouping<Sale> pointGrouping)
        {
            _pointGrouping = pointGrouping;
        }

        public Chart Chart()
        {
            IEnumerable<LinePointsGroup<Sale>> input = _pointGrouping.Groups();
            var chart = new Chart(ChartLines(input), PointCategories(input), Title);
            
            return chart;
        }
        
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