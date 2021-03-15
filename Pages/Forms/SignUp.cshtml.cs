using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cored.Web;
using System.Net.Http;
using Models.ApiModel.Client;

namespace AyudaWebApp.Pages.Forms
{
    public class SignUpModel : PageModel
    {

        [BindProperty]
        [Required]
        public string FullName { get; set; }
        [BindProperty]
        [Required]
        [EmailAddress(ErrorMessage = "Email cannot be empty")]
        public string EmailAddress { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Password must contain at least 8 characters, 1 lowercase, uppercase and symbol")]
        public string Password { get; set; }
        [Required]
        [BindProperty]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Phone]
        [BindProperty]
        [RegularExpression(@"^[0-9](10)$", ErrorMessage = "Only numbers are allowed")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        [BindProperty]
        [Required]
        public string NationalIDNumber { get; set; }


        public void OnGet()
        {
        }

        private HttpClient _client;
        public SignUpModel(HttpClient client)
        {
            _client = client;
        }

      


        public async Task<IActionResult> OnPostAsync()
        {
          
                var request = new RegisterRequestApiModel {
                    Email = EmailAddress,
                    FullName = FullName,
                    NationalId = NationalIDNumber,
                    Password = Password,
                    PhoneNumber = PhoneNumber



                };

                var response = await _client.PostAsync<UserProfileDetailsResponseApiModel>("https://localhost:5001/register",request);

                if (response.Successful)
                {
                    return RedirectToPage("/Index");
                }

            return Page();
         
           

        }
    }
}
