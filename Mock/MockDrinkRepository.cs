using Dermo.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dermo.Models;

namespace Dermo.Mock
{
    public class MockDrinkRepository : IDrinkRepository
    {
         private readonly ICategoryRepository _categoryRepository;

        public MockDrinkRepository(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Drink> Drinks
        {
            get
            {
                return new List<Drink>
                {
                    new Drink{

                        Name = "Beer",
                        Price = 7.95M,
                        ShortDescription = "The most widely consumed alcohol",
                        Category = _categoryRepository.Categories.First(),
                        ImageUrl = "~/images/img/beer/Beer1.jpg",
                        InStock = true,
                        IsPreferredDrink = true,
                        ImageThumbnailUrl ="~/images/img/beer/Beer1.jpg"

                    },
                     new Drink {
                        Name = "Rum & Coke",
                        Price = 12.95M, ShortDescription = "Cocktail made of cola, lime and rum.",
                        Category =  _categoryRepository.Categories.First(),
                        ImageUrl ="~/images/img/rum and coke/rum.jpg",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl ="~/images/img/rum and coke/rum.jpg"
                    },

                    new Drink {
                        Name = "Tequila ",
                        Price = 12.95M, ShortDescription = "Beverage made from the blue agave plant.",

                        Category =  _categoryRepository.Categories.First(),
                        ImageUrl = "~/images/img/tequila/tequila.jpg",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl = "~/images/img/tequila/tequila.jpg"
                    },

                    new Drink
                    {
                        Name = "Juice ",
                        Price = 12.95M,
                        ShortDescription = "Naturally contained in fruit or vegetable tissue.",

                        Category = _categoryRepository.Categories.Last(),
                        ImageUrl = "~/images/img/juice/juice1.jpg",
                        InStock = true,
                        IsPreferredDrink = false,
                        ImageThumbnailUrl ="~/images/img/juice/juice1.jpg"
                    }

                };
            }
        }

        public IEnumerable<Drink> PreferredDrinks => throw new NotImplementedException();

        public Drink GetDrinkById(int drinkId)
        {
            throw new NotImplementedException();
        }
    }
}
