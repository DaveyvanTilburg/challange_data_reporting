using System.Collections.Generic;
using System.Linq;
using Domain.PointGrouping;

namespace Domain.LineGrouping
{
    public class GroupByType<T> : ILineGrouping<T> where T : IDateGroupable, ITypeGroupable
    {
        private readonly IDataSource<T> _dataSource;
        
        public GroupByType(IDataSource<T> dataSource)
        {
            _dataSource = dataSource;
        }
        
        public IEnumerable<LineGroup<T>> LineGroups()
        {
            IEnumerable<LineGroup<T>> itemsByType =
                from entry in _dataSource.Data()
                group entry by entry.Type()
                into groupedSource
                select new LineGroup<T>(groupedSource, groupedSource.Key);

            return itemsByType;
        }
    }
}