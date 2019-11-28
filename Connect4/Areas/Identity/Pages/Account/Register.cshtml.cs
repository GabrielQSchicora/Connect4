using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Connect4.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Data de nascimento")]
            [DataType(DataType.Date)]
            public DateTime Nascimento { get; set; }

            [Required]
            [RegularExpression(@"^[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}$", ErrorMessage = "Seu CPF está fora do padrão.")]
            [Display(Name = "CPF")]
            [MaxLength(14)]
            public string CPF { get; set; }

            [Required]
            [RegularExpression(@"^[0-9]{5}-[0-9]{3}$", ErrorMessage = "Seu CEP está fora do padrão.")]
            [Display(Name = "CEP")]
            [MaxLength(9)]
            public string CEP { get; set; }

            [Required]
            [Display(Name = "Endereço")]
            public string Endereco { get; set; }

            [Required]
            [Display(Name = "Número")]
            public string NumeroCasa { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, 
                                                 Nome = Input.Nome, 
                                                 Email = Input.Email,
                                                 Nascimento = Input.Nascimento, 
                                                 CPF = Input.CPF,
                                                 CEP = Input.CEP,
                                                 Endereco = Input.Endereco,
                                                 NumeroCasa = Input.NumeroCasa};

                Boolean cpfValido = user.ValidaCPF(Input.CPF);

                if (!cpfValido)
                {
                    ModelState.AddModelError("CPF", "Insira um CPF válido.");
                    return Page();
                }

                var cpfAlreadyRegistered = _context.ApplicationUser.Where(u => u.CPF == Input.CPF).FirstOrDefault();

                if (cpfAlreadyRegistered != null)
                {
                    ModelState.AddModelError("CPF", "Já existe alguem cadastrado com esse CPF.");
                    return Page();
                }

                var idade = (DateTime.Now - Input.Nascimento).TotalDays / 365;

                if (idade < 18 || idade > 120)
                {
                    ModelState.AddModelError("Nascimento", "Sua idade deverá ser entre 18 e 120 anos");
                    return Page();
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
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
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
