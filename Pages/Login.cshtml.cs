using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LoginPageModel : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public void OnGet(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            

            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                return LocalRedirect(ReturnUrl);
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }

        // Si llegamos aqu�, significa que el inicio de sesi�n ha fallado.
        ModelState.AddModelError(string.Empty, "Usuario o contrase�a incorrectos.");
        return Page();
    }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}