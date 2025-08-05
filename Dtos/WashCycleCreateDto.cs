namespace CarwashMonitor.Dtos
{
    public class WashCycleCreateDto
    {
        public float WaterConsumption { get; set; }
        public float DetergentConsumption { get; set; }
        public float WaxConsumption { get; set; }
        public int CoinAmount { get; set; }
    }
}
