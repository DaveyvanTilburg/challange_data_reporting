namespace Domain.Charts
{
    public class ChartLine
    {
        public string Name { get; }
        public int[] Data { get; }

        public ChartLine(string name, int[] data)
        {
            Name = name;
            Data = data;
        }
    }
}