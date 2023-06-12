namespace Cars.Web.Controllers
{
    using Cars.Core.Contracts;
    using Cars.Core.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        public async Task<IActionResult> All()
        {
            var cars = await this.carService.GetCarsAsync();

            return View(cars);
        }

        public IActionResult Add()
        {
            var model = new AddCarViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCarViewModel car)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Add));
            }

            try
            {
                await this.carService.AddCarAsync(car);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Add));
            }
        }

        public async Task<IActionResult> Edit(int carId)
        {
            var carToEdit = await this.carService.GetCarByIdAsync(carId);

            if (carToEdit == null)
            {
                return NotFound();
            }

            var model = new EditCarViewModel() 
            {
                Id = carId,
                Make = carToEdit.Make,
                Model = carToEdit.Model,
                Year = carToEdit.Year,
                Type = carToEdit.Type
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int carId, EditCarViewModel car)
        {
            car.Id = carId;

            try
            {
                await this.carService.EditCarAsync(car);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        public async Task<IActionResult> Delete(int carId)
        {
            await this.carService.DeleteCarAsync(carId);

            return RedirectToAction(nameof(All));
        }
    }
}