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
using DigiGall.Dtos.User;
using DigiGall.Mappers;

namespace DigiGall.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();

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
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Quest");
        }

        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            var user = new User(); 
            return View(user);
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
/*
        public IActionResult Register(CreateUserDto createUserDto)
        {
            if (_context.Users.Any(u => u.Email == createUserDto.Email))
            {
                ViewBag.ErrorMessage = "Email already exists.";
                return View();
            }
*/

            user.Password = HashPassword(user.Password);
            user.Role = "User"; 
/*
            var user = createUserDto.ToUserFromCreateDto();  // Convert CreateUserDto to User
            user.Password = HashPassword(createUserDto.Password);  // Hash the password before saving
*/
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
        
        public IActionResult GetUser(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = user.ToUserDto();  // Convert User to UserDto
            return Ok(userDto);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login");
        }

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
