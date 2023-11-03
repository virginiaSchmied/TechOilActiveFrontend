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
    public class TrabajoModel : PageModel
    {
        public List<Trabajo> Trabajo { get; set; }
        public async Task OnGetAsync()
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.GetAsync("http://localhost:5225/api/Trabajo");

                if (response.IsSuccessStatusCode)
                { 
                    Trabajo = await response.Content.ReadFromJsonAsync<List<Trabajo>>();
                }
                else
                {
                    Trabajo = new List<Trabajo>();
                }
            }

        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.DeleteAsync($"http://localhost:5225/api/Trabajo/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Trabajos");
                }
                else
                {
                    return Page();
                }
            }
        }

        public async Task<IActionResult> OnPostCrearTrabajo()
        {

            string fecha = Request.Form["fecha"];
            string codProyecto = Request.Form["codProyecto"];
            string codServicio = Request.Form["codServicio"];
            string cantHoras = Request.Form["cantHoras"];
            string valorHora = Request.Form["valorHora"];
            //string costo = Request.Form["costo"];

            Console.WriteLine(fecha + " " + codProyecto + " " + codServicio + " " + cantHoras + "" + valorHora);

            var data = new
            {
                fecha = fecha,
                codProyecto = codProyecto,
                codServicio = codServicio,
                cantHoras = cantHoras,
                valorHora = valorHora,
                //costo = costo
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("http://localhost:5225/api/Trabajo", content);
                if (response.IsSuccessStatusCode)
                {
                    //response = await httpClient.GetAsync("http://localhost:5225/api/Trabajo");

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    Proyecto = await response.Content.ReadFromJsonAsync<List<Servicio>>();
                    //}
                    await OnGetAsync();
                    foreach (var trabajo in Trabajo)
                    {
                        // Realiza la operación matemática y asigna el resultado a la propiedad Resultado
                        trabajo.costo = trabajo.valorHora * trabajo.cantHoras;
                    }


                    return Page();
                }
                else
                {
                    return Page();
                }
            }
        }
    }

    public class Trabajo
    {
        [Key]
        public int codTrabajo { get; set; }

        public int fecha { get; set; }

        public int codProyecto { get; set; }

        public int codServicio { get; set; }

        public int? cantHoras { get; set; }

        public int? valorHora { get; set; }

        public int? costo { get; set; }

    }
}
