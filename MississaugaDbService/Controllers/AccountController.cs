using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MississaugaDbService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly PetLicensingContext _context;

        private readonly UserManager<IdentityUser> _userManager; // used for registering a user

        public AccountController(ILogger<AccountController> logger, PetLicensingContext context, UserManager<IdentityUser> userManager)
        {

            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel model)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Signin", "Account");
            }

            return View();
        }

        [HttpGet]
        [Route("Signin")]
        public IActionResult Signin()
        {
            return View();
        }


    }
}
