using ASI.Basecode.Data.Models;
using ASI.Basecode.Data.ViewModels;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.WebApp.Authentication;
using ASI.Basecode.WebApp.Models;
using ASI.Basecode.WebApp.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ASI.Basecode.Resources.Constants.Enums;
using ASI.Basecode.Services.Services;
using static ASI.Basecode.Resources.Constants.Constants;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ASI.Basecode.WebApp.Controllers
{
    [Authorize(Roles = "Superadmin")]
    public class AccountController : Controller
    {

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
        private readonly SessionManager _sessionManager;
        //private readonly SignInManager _signInManager;
        private readonly TokenValidationParametersFactory _tokenValidationParametersFactory;
        private readonly TokenProviderOptionsFactory _tokenProviderOptionsFactory;
        private readonly IConfiguration _appConfiguration;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        protected ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="localizer">The localizer.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="tokenValidationParametersFactory">The token validation parameters factory.</param>
        /// <param name="tokenProviderOptionsFactory">The token provider options factory.</param>
        public AccountController(
                            //SignInManager signInManager,
                            SignInManager<IdentityUser> signInManager,
                            IHttpContextAccessor httpContextAccessor,
                            ILoggerFactory loggerFactory,
                            IConfiguration configuration,
                            IMapper mapper,
                            IUserService userService,
                            TokenValidationParametersFactory tokenValidationParametersFactory,
                            TokenProviderOptionsFactory tokenProviderOptionsFactory,
                            RoleManager<IdentityRole> roleManager,
                            UserManager<IdentityUser> userManager)
        {
            //this._sessionManager = new SessionManager(this._session);
            this._signInManager = signInManager;
            this._tokenProviderOptionsFactory = tokenProviderOptionsFactory;
            this._tokenValidationParametersFactory = tokenValidationParametersFactory;
            this._appConfiguration = configuration;
            this._userService = userService;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._logger = loggerFactory.CreateLogger<AccountController>();
        }


        /*public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToPage(returnUrl);
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToPage(returnUrl);
        }*/

        /// <summary>
        /// Login Method
        /// </summary>
        /// <returns>Created response view</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl = null)
        {
            _logger.LogInformation("hello from account controller");
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;

            /*TempData["returnUrl"] = System.Net.WebUtility.UrlDecode(HttpContext.Request.Query["ReturnUrl"]);
            this._sessionManager.Clear();
            this._session.SetString("SessionId", System.Guid.NewGuid().ToString());*/
            return View();
        }

        /// <summary>
        /// Authenticate user and signs the user in when successful.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns> Created response view </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout    
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserId, model.Password, Input.RememberMe, lockoutOnFailure: false);
				var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        if (roles.Contains("Superadmin"))
                            return RedirectToAction("UserList", "Account");
                        if (roles.Contains("Bookmaster"))
                            return RedirectToAction("BookList", "Book");
                        if (roles.Contains("Genremaster"))
                            return RedirectToAction("GenreList", "Genre");
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        //_logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                        {
                            _logger.LogInformation(error.ErrorMessage);
                        }
                        return RedirectToPage(returnUrl);
                    }
                }

                if (!ModelState.IsValid)
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        _logger.LogInformation(error.ErrorMessage);
                    }
                }
            }
				

            // If we got this far, something failed, redisplay form
            return RedirectToPage(returnUrl);

            /*this._session.SetString("HasSession", "Exist");

            User user = null;
            var loginResult = _userService.AuthenticateUser(model.UserId, model.Password, ref user);
            if (loginResult == LoginResult.Success)
            {
                // 認証OK
               // await this._signInManager.SignInAsync(user);
                this._session.SetString("UserName", user.Name);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // 認証NG
                TempData["ErrorMessage"] = "Incorrect UserId or Password";
                return View();
            }
            return View();*/
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var roles = _userService.GetRoles().Select(r => r.Name).ToList();
            var userViewModel = new UserViewModel
            {
                Roles = roles
            };
            return View(userViewModel);
        }

		[HttpGet]
		public async Task<IActionResult> UserList()
        {
            var roles = _userService.GetRoles().Select(r => r.Name).ToList();
            var users = _userService.GetUsers().ToList();
            var userList = new List<IdentityUserViewModel>();
            foreach(var user in users)
            {
                var identityUser = await _userManager.FindByEmailAsync(user.Email);
                var identityRoles = await _userManager.GetRolesAsync(identityUser);
                var viewModel = new IdentityUserViewModel
                {
                    User = user,
                    UserRoles = identityRoles.ToList(),
                };

                userList.Add(viewModel);
            }
			var userViewModel = new UserViewModel
            {
                Roles = roles,
                Users = users
            };

            var commonViewModel = new UserViewStorageModel
            {
                ViewModel = userViewModel,
                IdentityUsers = userList
            };

            return View("Views/Account/UserList.cshtml", commonViewModel);
		}

        [HttpPost]
        public IActionResult UpdateUser(UserViewModel model)
        {
            _userService.UpdateUser(model);
                
            return RedirectToAction("UserList", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            _logger.LogInformation("register user");
            try
			{
				var identityUser = new IdentityUser
				{
					Email = model.Email,
					UserName = model.UserId
				};

				var result = await _userManager.CreateAsync(identityUser, model.Password);

				if (result.Succeeded)
				{
					_userService.AddUser(model);

					foreach (var selectedRole in model.SelectedRoles)
					{
						var userRole = _roleManager.FindByNameAsync(selectedRole).Result;

						if (userRole != null)
						{
							await _userManager.AddToRoleAsync(identityUser, userRole.Name);
						}
					}
				}

				return RedirectToAction("UserList", "Account");
			}
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (System.Exception ex)
            {
                TempData["ErrorMessage"] = Resources.Messages.Errors.ServerError;
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteUser(UserViewModel model)
        {
            _userService.DeleteUser(model.UserId);
            return RedirectToAction("UserList", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            _logger.LogInformation("register user");
            try
			{
				var identityUser = new IdentityUser
				{
					Email = model.Email,
					UserName = model.UserId
				};

				var result = await _userManager.CreateAsync(identityUser, model.Password);

				if (result.Succeeded)
				{
					_userService.AddUser(model);

					foreach (var selectedRole in model.SelectedRoles)
					{
						var userRole = _roleManager.FindByNameAsync(selectedRole).Result;

						if (userRole != null)
						{
							await _userManager.AddToRoleAsync(identityUser, userRole.Name);
						}
					}
				}

				return RedirectToAction("UserList", "Account");
			}
            catch(InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch(System.Exception ex)
            {
                TempData["ErrorMessage"] = Resources.Messages.Errors.ServerError;
            }
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {

                IdentityResult result = await _userService.CreateRole(createRoleViewModel.RoleName);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserList", "Account");
                }

            return View();
        }

        /// <summary>
        /// Sign Out current account and return login view.
        /// </summary>
        /// <returns>Created response view</returns>
        [AllowAnonymous]
        public async Task<IActionResult> SignOutUser()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        private async Task SendEmailWithToken(string userEmail, string token)
        {
            try
            {
                // Initialize the SendGrid API key
                var apiKey = "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";

                // Create a SendGrid client
                var client = new SendGridClient(apiKey);

                // Compose the email message
                var from = new EmailAddress("aloysiusmatthew1@gmail.com", "Bibiliobibuli");
                var to = new EmailAddress(userEmail);
                var subject = "Password Reset Token";
                var plainTextContent = $"Your password reset token is: {token}";
                var htmlContent = $"<strong>Your password reset token is: {token}</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                // Send the email asynchronously
                var response = await client.SendEmailAsync(msg);

                // Log the success message
                Console.WriteLine($"Verification code successfully sent. Response: {response.StatusCode}");
            }
            catch (System.Exception ex)
            {
                // Handle any exception related to email sending (e.g., log the error)
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordEmail()
        {
            return View("Views/Account/ForgotPasswordEmail.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordCode(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var originalToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                await SendEmailWithToken(user.Email, originalToken);

                // Create a view model to hold email and originalToken
                var viewModel = new ForgotPasswordCodeViewModel
                {
                    Email = email,
                    OriginalToken = originalToken
                };

                return View("Views/Account/ForgotPasswordCode.cshtml", viewModel);
            }
            else
            {
                ViewBag.EmailNotExist = "Email does not exist";
                return View("ForgotPasswordEmail");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult ForgotPasswordNew(string token, string originalToken, string email)
        {
            // Compare the input token with the original token
            if (token == originalToken)
            {
                var viewModel = new ForgotPasswordNewViewModel
                {
                    Email = email, // Assign the email to the view model
                    OriginalToken = originalToken // Assign the original token to the view model
                };

                ViewBag.ResetToken = originalToken;
                // Tokens match, allow password reset
                return View("Views/Account/ForgotPasswordNew.cshtml", viewModel);
            }
            else
            {
                ViewBag.TokenMismatch = "Token is incorrect";
                var viewModel = new ForgotPasswordCodeViewModel
                {
                    Email = email, // Assign the email to the view model
                    OriginalToken = originalToken // Assign the original token to the view model
                };
                return View("Views/Account/ForgotPasswordCode.cshtml", viewModel);
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordDone(string email, string password, string confirmPassword, string originalToken)
        {
            if (password == confirmPassword)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, originalToken, password);
                    if (result.Succeeded)
                    {
                        return View("Views/Account/Login.cshtml");
                    }
                }
            }
            else
            {
                ViewBag.PasswordMismatch = "Passwords do not match";
                var viewModel = new ForgotPasswordNewViewModel
                {
                    Email = email, // Assign the email to the view model
                    OriginalToken = originalToken // Assign the original token to the view model
                };
                return View("Views/Account/ForgotPasswordNew.cshtml", viewModel);
            }

            return View(email);
        }
    }
}
