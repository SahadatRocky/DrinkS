using Dermo.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dermo.Models;
using Dermo.db_context;
using Microsoft.EntityFrameworkCore;

namespace Dermo.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly AppDbContext _context;

        public DrinkRepository(AppDbContext context)
        {
            _context = context;
        } 

        public IEnumerable<Drink> Drinks => _context.Drinks.Include(c=>c.Category);

        public IEnumerable<Drink> PreferredDrinks => _context.Drinks.Where(p=>p.IsPreferredDrink).Include(c=>c.Category);

        public Drink GetDrinkById(int drinkId)=> _context.Drinks.FirstOrDefault(p=>p.DrinkId==drinkId);
        
    }
}
