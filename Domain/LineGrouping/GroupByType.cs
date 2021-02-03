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
            IEnumerable<T> input = _dataSource.Data();

            IEnumerable<LineGroup<T>> itemsByType =
                from entry in input
                group entry by entry.Type()
                into groupedSource
                orderby groupedSource.Key
                select new LineGroup<T>(groupedSource, groupedSource.Key);

            return itemsByType;
        }
    }
}