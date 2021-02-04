using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.LineGrouping;

namespace Domain.PointGrouping
{
    public class GroupPerWeek<T> : IPointGrouping<T> where T : class, IDateGroupable, ITypeGroupable
    {
        private readonly ILineGrouping<T> _lineGrouping;

        public GroupPerWeek(ILineGrouping<T> lineGrouping)
        {
            _lineGrouping = lineGrouping;
        }
        
        private static int WeekNumber(DateTime date)
            =>  CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);

        public IEnumerable<LinePointsGroup<T>> Groups()
        {
            IEnumerable<LineGroup<T>> input = _lineGrouping.LineGroups();
            IEnumerable<int> linePointsWeeks = LinePoints(input);

            var result = new List<LinePointsGroup<T>>();
            foreach (LineGroup<T> lineGroup in input)
                result.Add(LinePointsGroup(lineGroup, linePointsWeeks));

            return result;
        }
        
        private IEnumerable<int> LinePoints(IEnumerable<LineGroup<T>> input)
        {
            var allItems = input.SelectMany(l => l.Data());

            DateTime min = allItems.Min(i => i.Date());
            DateTime max = allItems.Max(i => i.Date());

            int minWeekNumber = WeekNumber(min);
            int maxWeekNumber = WeekNumber(max);

            int weeks = maxWeekNumber - minWeekNumber;

            IEnumerable<int> weekNumberRange = Enumerable.Range(minWeekNumber, weeks + 1);

            return weekNumberRange;
        }
        
        private Dictionary<int, PointGroup<T>> LinePointsTemplate(IEnumerable<int> linePoints)
        {
            var template = new Dictionary<int, PointGroup<T>>();
            
            foreach (int linePoint in linePoints)
                template.Add(linePoint, new PointGroup<T>(linePoint.ToString()));

            return template;
        }
        
        private LinePointsGroup<T> LinePointsGroup(LineGroup<T> lineGroup, IEnumerable<int> linePointsWeeks)
        {
            Dictionary<int, PointGroup<T>> linePoints = LinePointsTemplate(linePointsWeeks);

            foreach (T item in lineGroup.Data())
                linePoints[WeekNumber(item.Date())].Add(item);

            return new LinePointsGroup<T>(linePoints.Values.AsEnumerable(), lineGroup.Type());
        }
    }
}