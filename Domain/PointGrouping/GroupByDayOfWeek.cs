using System;
using System.Collections.Generic;
using System.Linq;
using Domain.LineGrouping;

namespace Domain.PointGrouping
{
    public class GroupByDayOfWeek<T> : PointGroupingBase<T> where T : class, IDateGroupable, ITypeGroupable
    {
        public GroupByDayOfWeek(ILineGrouping<T> lineGrouping) : base(lineGrouping) { }
        
        protected override IEnumerable<string> LinePoints(IEnumerable<LineGroup<T>> input)
        {
            IEnumerable<DayOfWeek> daysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

            var linePoints = new List<string>();
            foreach (DayOfWeek dayOfWeek in daysOfWeek)
                linePoints.Add(dayOfWeek.ToString());
            
            return linePoints;
        }

        protected override string ItemKey(T item)
            => item.Date().DayOfWeek.ToString();
    }
}