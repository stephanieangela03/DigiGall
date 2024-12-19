using Microsoft.AspNetCore.Mvc;
using DigiGall.Data;
using DigiGall.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using DigiGall.Dtos.User;
using DigiGall.Mappers;

namespace DigiGall.Controllers
{
    // [Route("Account")]
    // [ApiController]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        // Injecting ILogger<AccountController> into the constructor
        public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        // [HttpGet("register")]
        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            var user = new User(); 
            return View(user);
        }
        
        // [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.NamaLengkap),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.NamaLengkap);
            HttpContext.Session.SetString("UserSaldo", user.SaldoDigigall.ToString());

            if (user.Role == "User")
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Quest");
        }


        // POST method for Register
        [HttpPost]
        public IActionResult Register(CreateUserDto createUserDto)
        {
            // Logging the data received in the request
            _logger.LogInformation("Received Register request with data: {NamaLengkap}, {Email}, {Asrama}", 
                               createUserDto.NamaLengkap, createUserDto.Email, createUserDto.Asrama);

            if (ModelState.IsValid)
            {
                // Check if the email already exists in the database
                if (_context.Users.Any(u => u.Email == createUserDto.Email))
                {
                    ViewBag.ErrorMessage = "Email already exists.";
                    return View(createUserDto);
                }

                // Creating a new user from the CreateUserDto
                var user = new User
                {
                    NamaLengkap = createUserDto.NamaLengkap,
                    Email = createUserDto.Email,
                    Password = HashPassword(createUserDto.Password), // Hash the password before saving
                    Asrama = createUserDto.Asrama,
                    Role = "User" // Default role for a new user
                };

                // Adding the user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            // If there are validation errors, return to the form with validation messages
            return View(createUserDto);
        }

        // Method to hash the password using SHA256
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
