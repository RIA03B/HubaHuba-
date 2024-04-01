using HBHB.Data;
using HBHB.Models;
using HBPOS.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBHB.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class CareerRegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CareerRegisterModel> _logger;
        private readonly ApplicationDbContext _context; // Add ApplicationDbContext

        public CareerRegisterModel(
             UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<CareerRegisterModel> logger,
            ApplicationDbContext context) // Inject ApplicationDbContext
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context; // Assign ApplicationDbContext
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
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


            [Display(Name = "Affiliated with Fashion Brands or Companies")]
            public string Affiliation { get; set; }

            [Display(Name = "Experience as a Fashion Designer")]
            public string Experience { get; set; }

            [Display(Name = "Specialization in Fashion Design")]
            public string DesignSpecialization { get; set; }

            [Display(Name = "Portfolio or Website")]
            public string Portfolio { get; set; }

            [Display(Name = "Fashion Events or Shows Participation")]
            public string FashionEvents { get; set; }

            [Display(Name = "Design Inspirations")]
            public string DesignInspirations { get; set; }

            [Display(Name = "Special Requirements or Accommodations")]
            public string SpecialRequirements { get; set; }

            [Display(Name = "How did you hear about this opportunity?")]
            public string HeardAbout { get; set; }

            [Display(Name = "Design Aesthetic or Style")]
            public string DesignAesthetic { get; set; }

            [Display(Name = "Target Audience or Customer Base")]
            public string TargetAudience { get; set; }

            [Display(Name = "Materials or Techniques Frequently Used")]
            public string DesignTechniques { get; set; }

            [Display(Name = "Awards or Recognition")]
            public string AwardsRecognition { get; set; }

            [Display(Name = "Challenges or Obstacles Anticipated")]
            public string ChallengesObstacles { get; set; }

            [Display(Name = "Inspiration and Motivation")]
            public string InspirationMotivation { get; set; }

            [Display(Name = "Goals or Milestones")]
            public string GoalsMilestones { get; set; }

            // Add more properties for additional fields
        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Insert user data into CareerProfiles table

                    db dbObj = new db();
                    string con2 = dbObj.getConString();
                    List<Response> responseList = new List<Response>();
                    using (SqlConnection con = new SqlConnection(con2))
                    {
                        SqlCommand cmd = new SqlCommand("CreateCareerProfile", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UID", user.Id);
                        cmd.Parameters.AddWithValue("@Affiliation", Input.Affiliation);
                        cmd.Parameters.AddWithValue("@Experience", Input.Experience);
                        cmd.Parameters.AddWithValue("@DesignSpecialization", Input.DesignSpecialization);
                        cmd.Parameters.AddWithValue("@Portfolio", Input.Portfolio);
                        cmd.Parameters.AddWithValue("@FashionEvents", Input.FashionEvents);
                        cmd.Parameters.AddWithValue("@DesignInspirations", Input.DesignInspirations);
                        cmd.Parameters.AddWithValue("@SpecialRequirements", Input.SpecialRequirements);
                        cmd.Parameters.AddWithValue("@HeardAbout", Input.HeardAbout);
                        cmd.Parameters.AddWithValue("@DesignAesthetic", Input.DesignAesthetic);
                        cmd.Parameters.AddWithValue("@TargetAudience", Input.TargetAudience);
                        cmd.Parameters.AddWithValue("@DesignTechniques", Input.DesignTechniques);
                        cmd.Parameters.AddWithValue("@AwardsRecognition", Input.AwardsRecognition);
                        cmd.Parameters.AddWithValue("@ChallengesObstacles", Input.ChallengesObstacles);
                        cmd.Parameters.AddWithValue("@InspirationMotivation", Input.InspirationMotivation);
                        cmd.Parameters.AddWithValue("@GoalsMilestones", Input.GoalsMilestones);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Redirect user to the career profile route
                    return Redirect("/career-profile");
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