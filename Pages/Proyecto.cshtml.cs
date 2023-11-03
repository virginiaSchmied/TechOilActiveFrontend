using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Threading;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TechOilActiveRazorPage.Pages
{
    public class ProyectoModel : PageModel
    {
        public List<Proyecto> Proyecto { get; set; }
        public async Task OnGetAsync()
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.GetAsync("http://localhost:5225/api/Proyecto");

                if (response.IsSuccessStatusCode)
                {
                    Proyecto = await response.Content.ReadFromJsonAsync<List<Proyecto>>();
                }
                else
                {
                    Proyecto = new List<Proyecto>();
                }
            }

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.DeleteAsync($"http://localhost:5225/api/Proyecto/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Proyecto");
                }
                else
                {
                    return Page();
                }
            }
        }

        public async Task<IActionResult> OnPostCrearProyecto()
        {
            string nombre = Request.Form["nombre"];
            string direccion = Request.Form["direccion"];
            string estado = Request.Form["estado"];

            
            Console.WriteLine(nombre + "" + direccion + " " + estado);


            var data = new
            {
                nombre = nombre,
                direccion = direccion,
                estado = estado
            };

            Console.WriteLine(data);

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("http://localhost:5225/api/Proyecto", content);
                if (response.IsSuccessStatusCode)
                {
                    //response = await httpClient.GetAsync("http://localhost:5225/api/Proyecto");

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    Proyecto = await response.Content.ReadFromJsonAsync<List<Proyecto>>();
                    //}
                    //foreach (var proyecto in Proyecto)
                    //{
                    //    var nuevoEstado = "";
                    //    if (proyecto.estado == 1)
                    //    {
                    //        nuevoEstado = "Pendiente";
                    //    }
                    //    if (estado == "2")
                    //    {
                    //        nuevoEstado = "Confirmado";
                    //    }
                    //    if (estado == "3")
                    //    {
                    //        nuevoEstado = "Terminado";
                    //    }
                    //    estado = nuevoEstado;
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
    public class Proyecto
    {
        [Key]
        public int codProyecto { get; set; }
        [BindProperty]
        public string? nombre { get; set; }
        [BindProperty]
        public string? direccion { get; set; }
        [BindProperty]
        public int estado { get; set; }

    }
}



