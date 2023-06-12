namespace Cars.Core.Contracts
{
    using Cars.Core.ViewModels;

    public interface ICarService
    {
        Task<List<CarViewModel>> GetCarsAsync();

        Task<CarViewModel> GetCarByIdAsync(int carId);

        Task AddCarAsync(AddCarViewModel car);

        Task EditCarAsync(EditCarViewModel car);

        Task DeleteCarAsync(int carId);

        Task<List<EngineViewModel>> GetEnginesAsync();

        Task<EngineViewModel?> GetEngineByIdAsync(int id);
    }
}