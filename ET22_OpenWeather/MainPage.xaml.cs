using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ET22_OpenWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // recupera la localización del sistema operativo
            //var permiso = await Geolocator.RequestAccessAsync();
            //if (permiso != GeolocationAccessStatus.Allowed)
            //{
            //    throw new Exception();
            //}

            //var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            //var position = await geolocator.GetGeopositionAsync();

            IpStackProxy misCoordenadas = await IpStackProxy.RecuperaCoordenadas();

            //OpenWeatherProxy miTiempo = await OpenWeatherProxy.RecuperaTiempo(position.Coordinate.Point.Position.Latitude, position.Coordinate.Point.Position.Longitude);

            OpenWeatherProxy miTiempo = await OpenWeatherProxy.RecuperaTiempo(misCoordenadas.Latitude, misCoordenadas.Longitude);

            //OpenWeatherProxy miTiempo = await OpenWeatherProxy.RecuperaTiempo(0.0, 0.0);
            tbInfo.Text = "- " + miTiempo.Name + "\n- " + miTiempo.Main.Temp + "ºC\n- " + miTiempo.Weather[0].Description;

            // string iconoUrl = "http://openweathermap.org/img/w/" + miTiempo.Weather[0].Icon + ".png";
            string iconoUrl = "ms-appx:///Assets/Weather/" + miTiempo.Weather[0].Icon + ".png";
            Icono.Source = new BitmapImage(new Uri(iconoUrl, UriKind.Absolute));
        }

        private async void btRegistrar_Click(object sender, RoutedEventArgs e)
        {
            //var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            //var position = await geolocator.GetGeopositionAsync();

            IpStackProxy misCoordenadas = await IpStackProxy.RecuperaCoordenadas();

            double lat = misCoordenadas.Latitude;
            double lon = misCoordenadas.Longitude;

            string url = "http://servicioopenweather.azurewebsites.net/?lat=" + lat.ToString() + "&lon=" + lon.ToString();
            Uri tileContent = new Uri(url);
            var actualizador = TileUpdateManager.CreateTileUpdaterForApplication();
            actualizador.StartPeriodicUpdate(tileContent, PeriodicUpdateRecurrence.HalfHour);
        }
    }
}
