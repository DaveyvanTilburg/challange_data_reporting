using System.Collections.Generic;

namespace Domain
{
    public interface IDataSource<out T>
    {
        IEnumerable<T> Data();
    }
}