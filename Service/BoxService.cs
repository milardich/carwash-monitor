using CarwashMonitor.Enum;
using CarwashMonitor.Model;

namespace CarwashMonitor.Service
{
    public class BoxService : IBoxService
    {
        private readonly CarwashDbContext _context;

        public BoxService(CarwashDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBoxAsync(Guid stationId)
        {
            var station = await _context.Stations.FindAsync(stationId);
            if (station == null)
                throw new Exception("Station not found.");

            var newBox = new Box()
            { 
                Id = Guid.NewGuid(),
                StationId = stationId,
                Status = BoxStatus.INACTIVE,
            };

            _context.Boxes.Add(newBox);
            return await _context.SaveChangesAsync();
        }

        public async Task<Box?> GetBoxAsync(Guid boxId)
        {
            return await _context.Boxes.FindAsync(boxId);
        }

        public async Task<List<Box?>> GetBoxesAsync(Guid stationId)
        {
            var station = await _context.Stations.FindAsync(stationId);
            if (station == null)
                throw new Exception("Station not found.");

            return station.Boxes;
        }
    }
}
