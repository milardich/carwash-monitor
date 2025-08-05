namespace CarwashMonitor.Models
{
    public class Station
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<Box> Boxes { get; set; } = [];
    }
}
