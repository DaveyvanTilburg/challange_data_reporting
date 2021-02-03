using System.Collections.Generic;

namespace Domain.LineGrouping
{
    public class LineGroup<T>
    {
        private readonly string _type;
        private readonly IEnumerable<T> _data;
        
        public LineGroup(IEnumerable<T> data, string type)
        {
            _type = type;
            _data = data;
        }

        public string Type()
            => _type;

        public IEnumerable<T> Data()
            => new List<T>(_data);
    }
}