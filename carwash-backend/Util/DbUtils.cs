using System;
using CarwashMonitor.Enums;
using CarwashMonitor.Models;

namespace CarwashMonitor.Util
{
    public class DbUtils
    {
        public static void SeedInitialData(CarwashDbContext? db)
        {
            var random = new Random();

            if (db != null && !db.Stations.Any())
            {
                var stations = new List<Station>();

                var numberOfStations = random.Next(3, 6);
                for (int s = 1; s <= numberOfStations; s++)
                {
                    var station = new Station
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Station {s}",
                        Boxes = new List<Box>()
                    };

                    var numberOfBoxes = random.Next(3, 15);
                    for (int b = 1; b <= numberOfBoxes; b++)
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

                        var numberOfWashCycles = random.Next(1, 100);
                        for (int w = 1; w <= numberOfWashCycles; w++)
                        {
                            var daysAgo = random.Next(0, 10);
                            var hoursAgo = random.Next(0, 24);
                            var minutesAgo = random.Next(0, 60);
                            var secondsAgo = random.Next(0, 60);

                            var randomDateTime = DateTime.UtcNow
                                             .AddDays(-daysAgo)
                                             .AddHours(-hoursAgo)
                                             .AddMinutes(-minutesAgo)
                                             .AddSeconds(-secondsAgo);

                            var washCycle = new WashCycle
                            {
                                Id = Guid.NewGuid(),
                                WaterConsumption = Random.Shared.Next(50, 200) / 1f,
                                DetergentConsumption = Random.Shared.Next(10, 50) / 1f,
                                WaxConsumption = Random.Shared.Next(5, 25) / 1f,
                                DateCreated = randomDateTime,
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
