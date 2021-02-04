using System.Collections.Generic;
using System.Linq;
using Domain.LineGrouping;

namespace Domain.PointGrouping
{
    public abstract class PointGroupingBase<T> : IPointGrouping<T> where T : class, IDateGroupable, ITypeGroupable
    {
        private readonly ILineGrouping<T> _lineGrouping;

        protected PointGroupingBase(ILineGrouping<T> lineGrouping)
        {
            _lineGrouping = lineGrouping;
        }

        public IEnumerable<LinePointsGroup<T>> Groups()
        {
            IEnumerable<LineGroup<T>> input = _lineGrouping.LineGroups();
            IEnumerable<int> linePoints = LinePoints(input);

            var result = new List<LinePointsGroup<T>>();
            foreach (LineGroup<T> lineGroup in input)
                result.Add(LinePointsGroup(lineGroup, linePoints));

            return result;
        }

        protected abstract IEnumerable<int> LinePoints(IEnumerable<LineGroup<T>> input);

        private LinePointsGroup<T> LinePointsGroup(LineGroup<T> lineGroup, IEnumerable<int> linePointsWeeks)
        {
            Dictionary<int, PointGroup<T>> linePoints = LinePointsTemplate(linePointsWeeks);

            foreach (T item in lineGroup.Data())
                linePoints[ItemKey(item)].Add(item);

            return new LinePointsGroup<T>(linePoints.Values.AsEnumerable(), lineGroup.Type());
        }

        protected abstract int ItemKey(T item);

        private Dictionary<int, PointGroup<T>> LinePointsTemplate(IEnumerable<int> linePoints)
        {
            var template = new Dictionary<int, PointGroup<T>>();

            foreach (int linePoint in linePoints)
                template.Add(linePoint, new PointGroup<T>(linePoint.ToString()));

            return template;
        }
    }
}