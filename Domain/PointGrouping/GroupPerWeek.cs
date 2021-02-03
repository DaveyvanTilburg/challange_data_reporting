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
        
        private static string WeekNumber(DateTime date)
            =>  CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday).ToString();

        public IEnumerable<LinePointsGroup<T>> Groups()
        {
            IEnumerable<LineGroup<T>> input = _lineGrouping.LineGroups();

            var groupedByWeek = new List<LinePointsGroup<T>>();
            foreach(LineGroup<T> typeGroup in input)
                groupedByWeek.Add(
                    new LinePointsGroup<T>(
                        from entry in typeGroup.Data()
                        group entry by WeekNumber(entry.Date())
                        into groupedSource
                        orderby groupedSource.Key
                        select new PointGroup<T>(groupedSource, groupedSource.Key),
                    
                        typeGroup.Type()
                    )
                );
            
            return groupedByWeek;
        }
    }
}