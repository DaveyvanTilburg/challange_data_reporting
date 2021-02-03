namespace Domain.Charts
{
    public class ChartLine
    {
        private readonly string _type;
        private readonly int[] _values;

        public ChartLine(string type, int[] values)
        {
            _type = type;
            _values = values;
        }
        
        public string Type()
            => _type;

        public int[] Values()
            => _values;
    }
}