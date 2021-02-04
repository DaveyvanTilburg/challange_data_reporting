using System.Collections.Generic;

namespace Domain.PointGrouping
{
    public class PointGroup<T>
    {
        private readonly List<T> _data;
        private readonly string _pointCategory;

        public PointGroup(string pointCategory)
        {
            _data = new List<T>();
            _pointCategory = pointCategory;
        }

        public void Add(T item)
            => _data.Add(item);
        
        public IEnumerable<T> Data()
            => new List<T>(_data);

        public string PointCategory()
            => _pointCategory;
    }
}