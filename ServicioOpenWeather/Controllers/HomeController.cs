using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicioOpenWeather.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async System.Threading.Tasks.Task<ActionResult> Index(string lat, string lon)
        {
            // mostrar error si no se pasa lat y/o lon
            double latitud = double.Parse(lat.Substring(0, 5));
            double longitud = double.Parse(lon.Substring(0, 5));

            var elTiempo = await Models.OpenWeatherProxy.RecuperaTiempo(latitud, longitud);

            ViewBag.Name = elTiempo.Name;
            ViewBag.Temp = elTiempo.Main.Temp.ToString() + "ºC";
            ViewBag.Description = elTiempo.Weather[0].Description;
            return View();
        }
    }
}