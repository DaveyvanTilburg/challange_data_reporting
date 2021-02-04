using System;
using System.Collections.Generic;
using System.Linq;
using Domain.LineGrouping;

namespace Domain.PointGrouping
{
    public class GroupByMonth<T> : PointGroupingBase<T> where T : class, IDateGroupable, ITypeGroupable
    {
        public GroupByMonth(ILineGrouping<T> lineGrouping) : base(lineGrouping) { }
        
        protected override IEnumerable<int> LinePoints(IEnumerable<LineGroup<T>> input)
        {
            var allItems = input.SelectMany(l => l.Data());

            DateTime min = allItems.Min(i => i.Date());
            DateTime max = allItems.Max(i => i.Date());

            int minMonthNumber = min.Month;
            int maxMonthNumber = max.Month;

            int months = maxMonthNumber - minMonthNumber;

            IEnumerable<int> monthNumberRange = Enumerable.Range(minMonthNumber, months + 1);

            return monthNumberRange;
        }

        protected override int ItemKey(T item)
            => item.Date().Month;
    }
}