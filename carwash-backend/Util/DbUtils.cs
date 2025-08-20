using CarwashMonitor.Enums;
using CarwashMonitor.Models;

namespace CarwashMonitor.Util
{
    public class DbUtils
    {
        public static void SeedInitialData(CarwashDbContext? db)
        {
            if (db != null && !db.Stations.Any())
            {
                var stations = new List<Station>();

                for (int s = 1; s <= 3; s++)
                {
                    var station = new Station
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Station {s}",
                        Boxes = new List<Box>()
                    };

                    for (int b = 1; b <= 3; b++)
                    {
                        var box = new Box
                        {
                            Id = Guid.NewGuid(),
                            Number = b,
                            Status = BoxStatus.AVAILABLE,
                            CoinTrayAmount = 0,
                            CoinTrayLimit = 200,
                            WashCycles = new List<WashCycle>()
                        };

                        for (int w = 1; w <= 5; w++)
                        {
                            var washCycle = new WashCycle
                            {
                                Id = Guid.NewGuid(),
                                WaterConsumption = Random.Shared.Next(50, 200) / 1f,
                                DetergentConsumption = Random.Shared.Next(10, 50) / 1f,
                                WaxConsumption = Random.Shared.Next(5, 25) / 1f,
                                DateCreated = DateTime.Now.AddDays(-w),
                                CoinAmount = Random.Shared.Next(0, 50)
                            };
                            box.WashCycles.Add(washCycle);
                        }

                        station.Boxes.Add(box);
                    }

                    stations.Add(station);
                }

                db.Stations.AddRange(stations);
                db.SaveChanges();
            }
        }
    }
}
