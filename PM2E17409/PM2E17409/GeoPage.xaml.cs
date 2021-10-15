using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using PM2E17409.Modals;

namespace PM2E17409
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeoPage : ContentPage
    {
        Double lactitud;
        Double Longitud;

        public GeoPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();



            Longitud = Convert.ToDouble(txtLongitudMapa.Text);
            lactitud = Convert.ToDouble(txtLactitudMapa.Text);

            Pin ubicacion = new Pin();
            {
                ubicacion.Label = txtCortaDescipcionMapa.Text;
                ubicacion.Address = txtLargaDescripcionMapa.Text;
                ubicacion.Type = PinType.Place;
                ubicacion.Position = new Position(lactitud, Longitud);

            }
            mpMap.Pins.Add(ubicacion);


            var localizacion = await Geolocation.GetLastKnownLocationAsync();

            if (localizacion == null)
            {

                localizacion = await Geolocation.GetLocationAsync();
            }
            mpMap.MoveToRegion(MapSpan.FromCenterAndRadius(ubicacion.Position, Distance.FromKilometers(1)));
        }
    }
}
