namespace Cars.Core.Services
{
    using Cars.Core.Contracts;
    using Cars.Core.ViewModels;
    using Cars.Infrastructure.Data;
    using Cars.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;

    public class CarService : ICarService
    {
        private readonly CarsDbContext dbContext;

        public CarService(CarsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CarViewModel>> GetCarsAsync()
        {
            var cars = await this.dbContext.Cars
                .Select(c => new CarViewModel()
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.ProductionYear,
                    Type = c.Type
                })
                .ToListAsync();

            return cars;
        }

        public async Task<CarViewModel?> GetCarByIdAsync(int carId)
        {
            var car = await this.dbContext.Cars
                .FirstOrDefaultAsync(c => c.Id == carId);

            return car == null ? null : 
                new CarViewModel()
                {
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.ProductionYear,
                    Type = car.Type
                };
        }

        public async Task AddCarAsync(AddCarViewModel car)
        {
            var carToAdd = new Car()
            {
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.Year,
                Type = car.Type,
                EngineId = car.EngineId
            };

            await this.dbContext.Cars.AddAsync(carToAdd);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditCarAsync(EditCarViewModel car)
        {
            var carToEdit = await this.dbContext.Cars
                .FirstAsync(c => c.Id == car.Id);

            carToEdit.Make = car.Make;
            carToEdit.Model = car.Model;
            carToEdit.ProductionYear = car.Year;
            carToEdit.Type = car.Type;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int carId)
        {
            var carToDelete = await this.dbContext.Cars
                .FirstAsync(c => c.Id == carId);

            this.dbContext.Cars.Remove(carToDelete);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<List<EngineViewModel>> GetEnginesAsync()
        {
            var engines = await this.dbContext.Engines
                .Select(e => new EngineViewModel()
                {
                    Volume = e.Volume,
                    Horsepower = e.Horsepower,
                    Fuel = e.Fuel
                })
                .ToListAsync();

            return engines;
        }

        public async Task<EngineViewModel?> GetEngineByIdAsync(int id)
        {
            var engine = await this.dbContext.Engines
                .FirstOrDefaultAsync(e => e.Id == id);

            if (engine == null)
            {
                return null;
            }

            return new EngineViewModel()
            {
                Id = engine.Id,
                Volume = engine.Volume,
                Horsepower = engine.Horsepower,
                Fuel = engine.Fuel
            };
        }
    }
}