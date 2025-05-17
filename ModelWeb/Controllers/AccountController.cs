
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Text;
using Services.Data.Models;
using Services.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace ModelWeb.Controllers
{
       
    public class AccountController : Controller
    {
		private readonly ILogger<AccountController> _logger;
		private const string V = "UserID";
		private readonly IRepository<UserInfo> _repository;
		private readonly IRepository<UserRole> _UserRole;
		public AccountController(ILogger<AccountController> logger, IRepository<UserInfo> repository, IRepository<UserRole> UserRole)
        {
            _logger = logger;

            _repository = repository;
			_UserRole = UserRole;
		}
        
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
  
        [HttpPost]
        //[AuditActionFilter]
        public async Task<IActionResult> Login(DTOLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Invalid User Or Password!";
                return View(model);
            }

            bool isAuthenticated = false;
            var user =  _repository.GetAll(x=>x.UserName==model.UserName && x.PassWord==model.Password && x.Isactive==true,m=>m.UserRoles).FirstOrDefault()   ;
            if (user != null)
            {
              

			///ffdf
				var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
				 new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Email, user.UserFullName),
			    new Claim("UserId", user.UserId.ToString()),
			 

			};
				foreach (var roleName in _UserRole.GetAll(x=>x.UserId==user.UserId ,x=>x.Role).Select(x => x.Role.RoleName))
				{
					claims.Add(new Claim(ClaimTypes.Role, roleName));
				}
				//var userId = User.FindFirst("UserId")?.Value;
				var claimsIdentity = new ClaimsIdentity(
				 claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var authProperties = new AuthenticationProperties
				{
					IsPersistent = true,
					ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
				};
				//CookieAuthenticationDefaults.AuthenticationScheme
				await HttpContext.SignInAsync(
					IdentityConstants.ApplicationScheme
					,
					new ClaimsPrincipal(claimsIdentity),
					authProperties);

				string returnUrl = TempData["ReturnUrl"]?.ToString();
				if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					return Redirect(returnUrl);
				else
					return RedirectToAction("Index", "Home");
			}
			ViewData["ErrorMessage"] = "Invalid User Or Password!";
			//ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			return View(model);
		}
		public class DTOLoginModel
		{
			public string UserName { get; set; }
			public string Password { get; set; }
		}
		public async Task<IActionResult> Logout()
		{
			// Clear the user's authentication cookie
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
         //   HttpContext.Session.Clear();
            // Redirect to the login page or home page after sign-out
            return RedirectToAction("Login", "Account");
		}
		//private bool VerifyPassword(string password, string storedHash)
		//{
		//    using var sha256 = SHA256.Create();
		//    var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
		//    var hash = Convert.ToBase64String(hashBytes);

		//    return hash == storedHash;
		//}
	}
}
