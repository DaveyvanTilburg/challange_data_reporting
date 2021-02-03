using System.Collections.Generic;

namespace Domain.PointGrouping
{
    public class LinePointsGroup<T>
    {
        private readonly IEnumerable<PointGroup<T>> _data;
        private readonly string _type;

        public LinePointsGroup(IEnumerable<PointGroup<T>> data, string type)
        {
            _data = data;
            _type = type;
        }

        public IEnumerable<PointGroup<T>> Data()
            => _data;

        public string Type()
            => _type;
    }
}