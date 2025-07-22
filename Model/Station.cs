using CarwashMonitor.Model;

namespace CarwashMonitor.Models
{
    public class Station
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Box?> Boxes { get; set; } = [];
    }
}
