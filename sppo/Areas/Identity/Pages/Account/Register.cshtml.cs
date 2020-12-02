using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using sppo.Areas.Identity.Data;
using sppo.Data;
using SPPO.EntityModels;

namespace sppo.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private MyContext _db;

        public RegisterModel(
            UserManager<Profile> userManager,
            SignInManager<Profile> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            MyContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string CompanyName { get; set; }
            public City City { get; set; }
            public int CityId { get; set; }

        }
        public List<SelectListItem> cities { get; set; }

        List<SelectListItem> LoadCities()
        {
            List<SelectListItem> g = _db.cities.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return g;
        }

        private User AddUser()
        {
            User u;
            if (Input.CompanyName == null)
            {
                u = new User
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    CityId = Input.CityId
                };
                _db.Add(u);
                _db.SaveChanges();
                return u;
            }
            else
            {
                u = new User();
                u.Id = 0;
                return u;
            }
        }

        private Company AddCompany()
        {
            Company c;
            if (Input.CompanyName != null)
            {
                c = new Company
                {
                    CompanyRepresenterFirstName = Input.FirstName,
                    CompanyRepresenterLastName = Input.LastName,
                    CityId = Input.CityId,
                    Name = Input.CompanyName
                };
                _db.Add(c);
                _db.SaveChanges();
                return c;
            }
            else
            {
                c = new Company();
                c.Id = 0;
                return c;
            }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            cities = LoadCities();
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                cities = LoadCities();
                User u = AddUser();
                Company c = AddCompany();

                var user = new Profile { UserName = Input.Email, Email = Input.Email/*, PhoneNumber = Input.PhoneNumber*/ };
                if (c.Id == 0)
                {
                    user.CompanyID = null;
                    user.UserID = u.Id;
                }
                else
                {
                    user.CompanyID = c.Id;
                    user.UserID = null;
                }

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
