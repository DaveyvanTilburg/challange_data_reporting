using System;
using Domain.LineGrouping;
using Domain.PointGrouping;

namespace Domain.Sales
{
    public class Sale : IDateGroupable, ITypeGroupable
    {
        private readonly Lazy<string[]> _sections;
        private readonly Lazy<DateTime> _date;

        private const char Separator = ';';
        
        public Sale(string data)
        {
            _sections = new Lazy<string[]>(() => Sections(data));
            _date = new Lazy<DateTime>(Date());
        }

        private string[] Sections(string data)
            => data.Split(Separator);

        private string Section(int index)
            => _sections.Value[index].Trim('"');

        string ITypeGroupable.Type()
            => Section(0);
        
        private decimal Price()
        {
            if (!decimal.TryParse(Section(1), out decimal price))
                throw new Exception($"Invalid price value: {Section(1)}");

            return price;
        }
        
        private int Quantity()
        {
            if (!int.TryParse(Section(2), out int quantity))
                throw new Exception($"Invalid quantity value: {Section(2)}");

            return quantity;
        }

        public decimal TotalValue()
            => Price();

        public decimal AverageValue()
            => Price() / Quantity();
        
        private DateTime Date()
        {
            if (!DateTime.TryParseExact(
                Section(3),
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime date))
                throw new Exception($"Invalid date value: {Section(3)}");

            return date;
        }

        DateTime IDateGroupable.Date()
            => _date.Value;
    }
}