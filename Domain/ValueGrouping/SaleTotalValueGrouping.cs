using System;
using System.Linq;
using Domain.PointGrouping;

namespace Domain.ValueGrouping
{
    public class SaleTotalValueGrouping : SaveValueGroupingBase
    {
        public SaleTotalValueGrouping(IPointGrouping<Sale> pointGrouping) : base(pointGrouping) { }

        protected override string Title()
            => "Total sales value";
        protected override int Value(PointGroup<Sale> pointData)
            => Convert.ToInt32(pointData.Data().Sum(item => item.TotalValue()));
    }
}