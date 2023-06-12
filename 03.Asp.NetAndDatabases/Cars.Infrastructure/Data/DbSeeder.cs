namespace Cars.Infrastructure.Data
{
    using Cars.Infrastructure.Enums;
    using Cars.Infrastructure.Models;

    public class DbSeeder
    {
        public static void SeedData(CarsDbContext dbContext)
        {
            SeedEngines(dbContext);
            SeedCars(dbContext);
        }

        private static void SeedEngines(CarsDbContext dbContext)
        {
            if (dbContext.Engines.Any())
            {
                return;
            }

            var enginesToSeed = new List<Engine>()
            {
                new Engine()
                {
                    Model = "M57",
                    Volume = 3000,
                    Horsepower = 235,
                    Fuel = eFuel.Diesel
                },
                new Engine()
                {
                    Volume = 3000,
                    Horsepower = 306,
                    Fuel = eFuel.Petrol
                }
            };

            dbContext.Engines.AddRange(enginesToSeed);
            dbContext.SaveChanges();
        }

        private static void SeedCars(CarsDbContext dbContext)
        {
            if (dbContext.Cars.Any())
            {
                return;
            }

            var carToSeed = new List<Car>()
            {
                new Car()
                {
                    Make = "BMW",
                    Model = "E60 530d",
                    ProductionYear = 2009,
                    Type = eCarType.Sedan,
                    EngineId = 1
                },
                new Car()
                {
                    Make = "BMW",
                    Model = "F30 335xi",
                    ProductionYear = 2014,
                    Type = eCarType.Sedan,
                    EngineId = 2
                }
            };

            dbContext.Cars.AddRange(carToSeed);
            dbContext.SaveChanges();
        }
    }
}