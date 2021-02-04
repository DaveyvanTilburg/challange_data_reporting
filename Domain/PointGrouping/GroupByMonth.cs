using System;
using System.Collections.Generic;
using System.Linq;
using Domain.LineGrouping;

namespace Domain.PointGrouping
{
    public class GroupByMonth<T> : PointGroupingBase<T> where T : class, IDateGroupable, ITypeGroupable
    {
        public GroupByMonth(ILineGrouping<T> lineGrouping) : base(lineGrouping) { }
        
        protected override IEnumerable<string> LinePoints(IEnumerable<LineGroup<T>> input)
        {
            var allItems = input.SelectMany(l => l.Data());

            DateTime min = allItems.Min(i => i.Date());
            DateTime max = allItems.Max(i => i.Date());

            var months = (max.Year - min.Year) * 12 + max.Month - min.Month;

            var linePoints = new List<string>();
            for (int i = 0; i <= months; i++)
            {
                DateTime cursor = min.AddMonths(i);
                linePoints.Add(Key(cursor));
            }

            return linePoints;
        }

        protected override string ItemKey(T item)
            => Key(item.Date());

        private string Key(DateTime date)
            => $"{date.Year}-{date.Month}";
    }
}