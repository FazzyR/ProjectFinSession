using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFinSession.Data;
using ProjectFinSession.Models;
using System.Security.Claims;

namespace ProjectFinSession.Controllers
{
    public class UserController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public UserController(RoleManager<IdentityRole> roleManager, ApplicationDbContext Context, UserManager<AppUser> UserManager, SignInManager<AppUser> SignInManager)
        {
            _roleManager = roleManager;
            _context = Context;
            _userManager = UserManager;
            _signInManager = SignInManager;
        }
        public IActionResult ProductsIndexUser()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public async Task<IActionResult> Cart()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {

                return NotFound();


            }
            else
            {
                if (currentUser.Cart == null)
                {
                    cart cart = new cart();
                    currentUser.Cart = cart;


                }
            }

                return View(currentUser.Cart);
        }

        [HttpPost]

        public async Task<IActionResult> AddToCart(int productId)
        {

            var product =await _context.Products.FindAsync(productId);

            var user=await _userManager.GetUserAsync(User);

            if (user != null && product!=null)
            {
                if (user.Cart != null) {

                    user.Cart.Products.Add(product);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    var cart = new cart { Products = new List<Product>() };
                    user.Cart=cart;
                    user.Cart.Products.Add(product);

                }
                await _context.SaveChangesAsync();

            }
          



            return View("Cart");

        }





    }
}
