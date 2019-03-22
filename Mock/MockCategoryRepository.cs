using Dermo.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dermo.Models;

namespace Dermo.Mock
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories
        {

            get
            {
                return new List<Category> {
                    new Category{CategoryName="Alcoholic",Description="All Alcoholic Drinks" },
                    new Category{CategoryName="NonAlcoholic",Description="All NonAlcoholic Drinks" }
                };

            }

        }
    }
}
