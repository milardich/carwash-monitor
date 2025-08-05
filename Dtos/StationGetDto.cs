namespace CarwashMonitor.Dtos
{
    public class StationGetDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<BoxInfoDto> BoxInfos { get; set; } = [];
    }
}
