using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectFinSession.Data;
using ProjectFinSession.Models;
using ProjectFinSession.ViewModels;
namespace ProjectFinSession.Controllers
{

    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, ApplicationDbContext Context, UserManager<AppUser> UserManager, SignInManager<AppUser> SignInManager)
        {
            _roleManager = roleManager;
            _context = Context;
            _userManager = UserManager;
            _signInManager = SignInManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(roleModel?.RoleName);
                if (roleExists)
                {
                    ModelState.AddModelError("", "Role Already Exists");
                }
                else
                {
          
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = roleModel?.RoleName
                    };

                    IdentityResult result = await _roleManager.CreateAsync(identityRole);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(roleModel);
        }



        public IActionResult Employers() {

            EmployersViewModel employersViewModel = new EmployersViewModel
            {
                InscriptionViewModel = new InscriptionViewModel(),
                Employers=_context.Users.ToList()
            };
          
            return View(employersViewModel);
        }
        [HttpPost]

        public async Task<IActionResult> InscriptionAdmin(EmployersViewModel employersViewModel)
        {

            
                var search= await _userManager.FindByEmailAsync(employersViewModel.InscriptionViewModel.Email);
                if (search != null)
                {
                    var user = new AppUser
                    {
                        Nom = employersViewModel.InscriptionViewModel.Nom,
                        Prenom = employersViewModel.InscriptionViewModel.Prenom,
                        Email = employersViewModel.InscriptionViewModel.Email,
                        Adresse=employersViewModel.InscriptionViewModel.Adresse

                    };

                    var result= await _userManager.CreateAsync(user);
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    if (result.Succeeded)
                    {
                        if (employersViewModel.Admin == true)
                        {
                            var role = await _userManager.AddToRoleAsync(user, "Admin");

                        }
                        else
                        {
                            var role = await _userManager.AddToRoleAsync(user, "User");

                        }

                        return RedirectToAction("Products");
                    }

                    ModelState.AddModelError("user", "User not created");
                }

               


            
            return RedirectToAction("ProductsIndex");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployer(AppUser employer, bool isAdmin)
        {
            if (ModelState.IsValid)
            {
                var existingEmployer = await _userManager.FindByIdAsync(employer.Id);
                if (existingEmployer != null)
                {
                    existingEmployer.Nom = employer.Nom;
                    existingEmployer.Prenom = employer.Prenom;
                    existingEmployer.Email = employer.Email;
                    existingEmployer.Adresse = employer.Adresse;

                    var roles = await _userManager.GetRolesAsync(existingEmployer);
                    if (isAdmin && !roles.Contains("Admin"))
                    {
                        await _userManager.AddToRoleAsync(existingEmployer, "Admin");
                    }
                    else if (!isAdmin && roles.Contains("Admin"))
                    {
                        await _userManager.RemoveFromRoleAsync(existingEmployer, "Admin");
                        await _userManager.AddToRoleAsync(existingEmployer, "User");
                    }

                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Employer not found." });
            }

            return Json(new { success = false, message = "Invalid data." });
        }


        [HttpPost]
        public IActionResult DeleteEmployer(int id)
        {
            var employer = _context.Users.Find(id);
            if (employer != null)
            {
                _context.Users.Remove(employer);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Employer not found." });
        }


        public IActionResult ProductsIndex()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        public IActionResult Products()
        {
            var model = new ProductViewModel
            {
                CreateProduct = new CreateProductViewModel(),
                Products = _context.Products.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _context.Products.Find(product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.ImageUrl = product.ImageUrl;
                    existingProduct.Price = product.Price;

                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Product not found." });
            }

            return Json(new { success = false, message = "Invalid data." });
        }





        [HttpPost]
        public IActionResult CreateProduct(ProductViewModel productViewModel)
        {
            if (productViewModel == null)
            {
                throw new ArgumentNullException(nameof(productViewModel), "Model cannot be null.");
            }
            productViewModel.Products= _context.Products.ToList();

            
                var product = new Product
                {
                    Name = productViewModel.CreateProduct.Name,
                    Description = productViewModel.CreateProduct.Description,
                    ImageUrl = productViewModel.CreateProduct.ImageUrl,
                    Price = productViewModel.CreateProduct.Price,
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Products");
            

    ;
        }
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Product not found." });
        }

    }

}
