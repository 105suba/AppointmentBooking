using Microsoft.AspNetCore.Mvc;
using DAL.Data;
using DAL.Models;
using BCrypt.Net;

namespace MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppointmentDbContext _context;

        public AuthController(AppointmentDbContext context)
        {
            _context = context;
        }

        // ================= SIGNUP =================

        public IActionResult Signup()
        {
            return View();
        }

       [HttpPost]
public IActionResult Signup(Admin user)
{
    if (ModelState.IsValid)
    {
        // Decide role BEFORE hashing
        if (user.Password == "adminsuba110")
            user.Role = "Admin";
        else
            user.Role = "User";

        //  FIX: Hash password
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        _context.Admins.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }

    return View(user);
}

        // ================= LOGIN =================

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
public IActionResult Login(string email, string password)
{
    var user = _context.Admins
        .FirstOrDefault(u => u.Email == email);

    if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
    {
        HttpContext.Session.SetString("Email", user.Email);
        HttpContext.Session.SetString("Role", user.Role);

        if (user.Role == "Admin")
            return RedirectToAction("Index", "Appointment");

        return RedirectToAction("Index", "Appointment");
    }

    ViewBag.Error = "Invalid email or password";
    return View();
}
        

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult ForgotPassword()
{
    return View();
}
[HttpPost]
public IActionResult ForgotPassword(string email, string newPassword, string confirmPassword)
{
    var user = _context.Admins
        .FirstOrDefault(u => u.Email == email);

    if (user == null)
    {
        ViewBag.Error = "User not found";
        return View();
    }

    // 🔐 Confirm password check
    if (newPassword != confirmPassword)
    {
        ViewBag.Error = "Passwords do not match";
        return View();
    }

    // 🔥 Role logic (your existing logic preserved)
    if (newPassword == "adminsuba110")
        user.Role = "Admin";
    else
        user.Role = "User";

    // 🔐 Hash password
    user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

    _context.SaveChanges();

    ViewBag.Message = "Password updated successfully";
    return View();
}
    }
}