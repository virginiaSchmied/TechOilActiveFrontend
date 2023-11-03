using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace TechOilActiveRazorPage.Pages
{
    public class UsuarioModel : PageModel
    {
        public List<Usuario> Usuario { get; set; }
        public async Task OnGetAsync()
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.GetAsync("http://localhost:5225/api/Usuario");

                if (response.IsSuccessStatusCode)
                {
                    Usuario = await response.Content.ReadFromJsonAsync<List<Usuario>>();
                }
                else
                {
                    Usuario = new List<Usuario>();
                }
            }

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.DeleteAsync($"http://localhost:5225/api/Usuario/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Usuario");
                }
                else
                {
                    return Page();
                }
            }

        }

        public async Task<IActionResult> OnPostCrearUsuario()
        {
            string nombre = Request.Form["nombre"];
            string dni = Request.Form["dni"];
            string tipo = Request.Form["tipo"];
            string contraseña = Request.Form["contraseña"];

            var data = new
            {
                nombre = nombre,
                dni = dni,
                tipo = tipo,
                contraseña = contraseña
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("http://localhost:5225/api/Usuario", content);
                if (response.IsSuccessStatusCode)
                {
                    //response = await httpClient.GetAsync("http://localhost:5225/api/Usuario");

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    Proyecto = await response.Content.ReadFromJsonAsync<List<Servicio>>();
                    //}
                    await OnGetAsync();
                    return Page();
                }
                else
                {
                    return Page();
                }
            }
        }
    }

    public class Usuario
    {
        [Key]
        public int codUsuario { get; set; }

        public string? nombre { get; set; }

        public int? dni { get; set; }

        public int? tipo { get; set; }

        public int? contraseña { get; set; }
    }
}
