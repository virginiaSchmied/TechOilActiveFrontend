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
    public class ServicioModel : PageModel
    {
        public List<Servicio> Servicio { get; set; }
        public async Task OnGetAsync()
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.GetAsync("http://localhost:5225/api/Servicio");

                if (response.IsSuccessStatusCode)
                {
                    Servicio = await response.Content.ReadFromJsonAsync<List<Servicio>>();
                }
                else
                {
                    Servicio = new List<Servicio>();
                }
            }

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.DeleteAsync($"http://localhost:5225/api/Servicio/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Servicio");
                }
                else
                {
                    return Page();
                }
            }

        }

        public async Task<IActionResult> OnPostCrearServicio()
        {
            string descr = Request.Form["descr"];
            string estado = Request.Form["estado"];
            string valorHora = Request.Form["valorHora"];

            Console.WriteLine(descr + "" + estado + " " + valorHora);

            var data = new
            {
                descr = descr,
                estado = estado,
                valorHora = valorHora
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("http://localhost:5225/api/Servicio", content);
                if (response.IsSuccessStatusCode)
                {
                    //response = await httpClient.GetAsync("http://localhost:5225/api/Servicio");

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

    public class Servicio
    {
        [Key]
        public int codServicio { get; set; }

        public string? descr { get; set; }

        public int? estado { get; set; }

        public int? valorHora { get; set; }
    }
}
