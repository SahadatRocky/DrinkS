using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dermo.interfaces;
using Dermo.ViewModels;
using Dermo.Models;

namespace Dermo.Controllers
{
    public class DrinkController : Controller
    {
       

        private readonly ICategoryRepository _categoryRepository;
        private readonly IDrinkRepository _drinkRepository;

        public DrinkController(ICategoryRepository categoryRepository, IDrinkRepository drinkRepository)
        {
            _categoryRepository = categoryRepository;
            _drinkRepository = drinkRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult List(String category)
        {
            string _category = category;
            IEnumerable<Drink> drinks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                drinks = _drinkRepository.Drinks.OrderBy(p=>p.DrinkId);
                currentCategory = "All Drinks";
            }
            else
            {
                if (string.Equals("Alcoholic",_category,StringComparison.OrdinalIgnoreCase))
                {
                    drinks = _drinkRepository.Drinks.Where(p=>p.Category.CategoryName.Equals("Alcoholic")).OrderBy(p=>p.Name);
                }
                else
                {
                    drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Non-alcoholic")).OrderBy(p => p.Name);
                }
                currentCategory = _category;
            }


            return View(new DrinkListViewModel {
                 Drinks=drinks,
                  CurrentCategory=currentCategory
            });
        }

    }
}