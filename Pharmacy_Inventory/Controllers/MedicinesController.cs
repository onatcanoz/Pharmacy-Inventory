using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Inventory.Data;
using Pharmacy_Inventory.Models;
using Pharmacy_Inventory.Models.Field;

namespace Pharmacy_Inventory.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly PharmacyDbContext pharmacyDbContext;

        public MedicinesController(PharmacyDbContext pharmacyDbContext)
        {
            this.pharmacyDbContext = pharmacyDbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddMedicineViewModel addMedicineRequest)
        {
            var medicines = await pharmacyDbContext.Medicines.FirstOrDefaultAsync(x => x.Name == addMedicineRequest.Name);
            
            if (addMedicineRequest.Name != null && addMedicineRequest.Salary >= 0 && addMedicineRequest.Stock >= 0 && addMedicineRequest.Type != null)
            {
                if (medicines == null)
                {
                    var medicine = new Medicine()
                    {
                        Id = Guid.NewGuid(),
                        Name = addMedicineRequest.Name,
                        Stock = addMedicineRequest.Stock,
                        Salary = addMedicineRequest.Salary,
                        Type = addMedicineRequest.Type,
                        ExpirationDate = addMedicineRequest.ExpirationDate
                    };
                    await pharmacyDbContext.Medicines.AddAsync(medicine);
                    await pharmacyDbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Add");

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var medicines = await pharmacyDbContext.Medicines.ToListAsync();
            return View(medicines);
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var medicine = await pharmacyDbContext.Medicines.FirstOrDefaultAsync(x => x.Id == id);

            if (medicine != null)
            {
                var viewModel = new UpdateMedicineViewModel()
                {
                    Id = medicine.Id,
                    Name = medicine.Name,
                    Stock = medicine.Stock,
                    Salary = medicine.Salary,
                    Type = medicine.Type,
                    ExpirationDate = medicine.ExpirationDate
                };
                return await Task.Run(() => View("View", viewModel));
            }



            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateMedicineViewModel model)
        {
            var medicine = await pharmacyDbContext.Medicines.FindAsync(model.Id);

            if (medicine != null)
            {
                medicine.Name = model.Name;
                medicine.ExpirationDate = model.ExpirationDate;
                medicine.Stock = model.Stock;
                medicine.Salary = model.Salary;
                medicine.Type = model.Type;

                await pharmacyDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateMedicineViewModel model)
        {
            var medicine = await pharmacyDbContext.Medicines.FindAsync(model.Id);

            if (medicine != null)
            {
                pharmacyDbContext.Medicines.Remove(medicine);
                await pharmacyDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
