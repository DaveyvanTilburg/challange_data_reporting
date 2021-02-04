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

        protected override IEnumerable<string> LinePoints(IEnumerable<LineGroup<T>> input)
        {
            List<T> allItems = input.SelectMany(l => l.Data()).ToList();

            DateTime min = StartOfWeek(allItems.Min(i => i.Date()));
            DateTime max = StartOfWeek(allItems.Max(i => i.Date()));

            int weeks = (int)Math.Ceiling((max - min).TotalDays / 7);

            var linePoints = new List<string>();
            for (int i = 0; i <= weeks; i++)
                linePoints.Add(Key(StartOfWeek(min.AddDays(i * 7))));
            
            return linePoints.Distinct().ToList();
        }

        protected override string ItemKey(T item)
            => Key(StartOfWeek(item.Date()));
        
        private static string Key(DateTime date)
            => $"{date.Year}-{WeekNumber(date)}";

        private static int WeekNumber(DateTime date)
            => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);

        public static DateTime StartOfWeek(DateTime date)
        {
            int difference = (7 + (date.DayOfWeek - DayOfWeek.Sunday)) % 7;
            return date.AddDays(-1 * difference).Date;
        }
    }
}