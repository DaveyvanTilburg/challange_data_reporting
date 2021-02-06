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

        public IEnumerable<LinePointsGroup<T>> LinePointGroups()
        {
            IEnumerable<LineGroup<T>> input = _lineGrouping.LineGroups();
            IEnumerable<string> linePoints = LinePoints(input);

            var result = new List<LinePointsGroup<T>>();
            foreach (LineGroup<T> lineGroup in input)
                result.Add(LinePointsGroup(lineGroup, linePoints));

            return result;
        }

        protected abstract IEnumerable<string> LinePoints(IEnumerable<LineGroup<T>> input);

        private LinePointsGroup<T> LinePointsGroup(LineGroup<T> lineGroup, IEnumerable<string> linePointsWeeks)
        {
            Dictionary<string, PointGroup<T>> linePoints = LinePointsTemplate(linePointsWeeks);

            foreach (T item in lineGroup.Data())
                linePoints[ItemKey(item)].Add(item);

            return new LinePointsGroup<T>(linePoints.Values.AsEnumerable(), lineGroup.Type());
        }

        protected abstract string ItemKey(T item);

        private Dictionary<string, PointGroup<T>> LinePointsTemplate(IEnumerable<string> linePoints)
        {
            var template = new Dictionary<string, PointGroup<T>>();

            foreach (string linePoint in linePoints)
                template.Add(linePoint, new PointGroup<T>(linePoint));

            return template;
        }
    }
}