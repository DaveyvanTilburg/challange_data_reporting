using System.Collections.Generic;

namespace Domain.PointGrouping
{
    public class PointGroup<T>
    {
        private readonly IEnumerable<T> _data;
        private readonly string _period;

        public PointGroup(IEnumerable<T> data, string period)
        {
            _data = data;
            _period = period;
        }

        public IEnumerable<T> Data()
            => new List<T>(_data);

        public string Period()
            => _period;
    }
}