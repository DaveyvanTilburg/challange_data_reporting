using System.Collections.Generic;

namespace Domain.PointGrouping
{
    public interface IPointGrouping<T> where T : IDateGroupable
    {
        IEnumerable<LinePointsGroup<T>> LinePointGroups();
    }
}