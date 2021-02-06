using System;
using System.Linq;
using Domain.PointGrouping;

namespace Domain.ValueGrouping
{
    public class SaleAverageValueGrouping : SaveValueGroupingBase
    {
        public SaleAverageValueGrouping(IPointGrouping<Sale> pointGrouping) : base(pointGrouping) { }

        protected override string Title()
            => "Average sales value";

        protected override int Value(PointGroup<Sale> pointData)
            => pointData.Data().Any() ?
                Convert.ToInt32(pointData.Data().Sum(item => item.TotalValue()) / pointData.Data().Sum(item => item.Quantity())) : 
                0;
    }
}