using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectFinSession.Models;
using ProjectFinSession.ViewModels;

namespace ProjectFinSession.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        
         private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> UserManager, SignInManager<AppUser> SignInManager, RoleManager<IdentityRole> roleManager) 
        {

            _signInManager= SignInManager;
            _userManager =UserManager;

            _roleManager=roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Admin"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "User",
                Text = "User"
            });

            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.RoleList = listItems;
            loginViewModel.ReturnUrl = returnUrl ?? Url.Content("~/");
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel,string? returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/");


            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, isPersistent: false, lockoutOnFailure: false);


                    if (result.Succeeded)
                    {
                        if (loginViewModel.RoleSelected != null && loginViewModel.RoleSelected.Length > 0 && loginViewModel.RoleSelected == "Admin")
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        {
                            if (User.IsInRole("Admin"))
                            {
                                var remove = await _userManager.RemoveFromRoleAsync(user, "Admin");

                            }
                            await _userManager.AddToRoleAsync(user, "User");
                        }
                       

                        return LocalRedirect(returnUrl);
                    }
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Account is locked out.");
                        return View("Lockout");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }


            }
           



            return View(loginViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LogOut()
        {

         await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Inscription(string? returnUrl = null)
        {

            InscriptionViewModel inscriptionViewModel = new InscriptionViewModel();
            inscriptionViewModel.ReturnUrl = returnUrl;
            return View(inscriptionViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Inscription(InscriptionViewModel inscriptionViewModel,string? returnUrl=null)
        {
            inscriptionViewModel.ReturnUrl= returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");


            if (ModelState.IsValid)
            
            {

                var user = new AppUser
                {
                    Prenom = inscriptionViewModel.Prenom,
                    Nom = inscriptionViewModel.Nom,
                    Email = inscriptionViewModel.Email,
                    Adresse = inscriptionViewModel.Adresse,
                    UserName = inscriptionViewModel.Email

                };

                var result = await _userManager.CreateAsync(user,inscriptionViewModel.Password);
                
                if(result.Succeeded){

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            } 
            
            

            return View(inscriptionViewModel);
        }


    }
}
