using Dermo.db_context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dermo.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {

            ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId",cartId);


            return  new ShoppingCart(context) { ShoppingCartId=cartId};
        }

        
        //
        public void AddToCart(Drink drink,int amount)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s=>s.Drink.DrinkId==drink.DrinkId && s.ShoppingCartId==ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {

                    ShoppingCartId=ShoppingCartId,
                    Amount=1,
                    Drink=drink
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);

            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();

        }

        ///
        public int RemoveCart(Drink drink)
        {

            var localAmount = 0;
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s=>s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem!=null)
            {
                if (shoppingCartItem.Amount>1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();

            return localAmount;
        }

        //
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems=_context.ShoppingCartItems.Where(c=>c.ShoppingCartId == ShoppingCartId)
                .Include(s=>s.Drink)
                .ToList());
                   
        }
         //
         public void ClearCart()
        {
            var cartItem = _context.ShoppingCartItems
                .Where(c=>c.ShoppingCartId==ShoppingCartId);

            _context.ShoppingCartItems.RemoveRange(cartItem);
            _context.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems
                .Where(c=>c.ShoppingCartId==ShoppingCartId)
                .Select(c=>c.Drink.Price * c.Amount ).Sum();

            return total;

        }

    }
}
