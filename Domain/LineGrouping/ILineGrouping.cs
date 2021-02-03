using System.Collections.Generic;
using Domain.PointGrouping;

namespace Domain.LineGrouping
{
    public interface ILineGrouping<T> where T : IDateGroupable, ITypeGroupable
    {
        IEnumerable<LineGroup<T>> LineGroups();
    }
}