using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.LineGrouping;

namespace Domain.PointGrouping
{
    public class GroupByWeek<T> : PointGroupingBase<T> where T : class, IDateGroupable, ITypeGroupable
    {
        public GroupByWeek(ILineGrouping<T> lineGrouping) : base(lineGrouping) { }
        
        private static int WeekNumber(DateTime date)
            =>  CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);

        protected override IEnumerable<int> LinePoints(IEnumerable<LineGroup<T>> input)
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

        protected override int ItemKey(T item)
            => WeekNumber(item.Date());
    }
}