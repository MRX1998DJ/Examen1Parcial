using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E17409.Modals;
using Plugin.Geolocator;

namespace PM2E17409
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaUbicaciones : ContentPage
    {
        private Mapeo seleccinarId;
        public ListaUbicaciones()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection conexion = new SQLiteConnection(App.SQliteUbicacion))
            {   //mandamos a que tabla se guarda
                conexion.CreateTable<Mapeo>();

                var listamapa = conexion.Table<Mapeo>().ToList();
                ListaUbicacion.ItemsSource = listamapa;

            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(App.SQliteUbicacion))
            {
                if (seleccinarId != null)
                {
                    await DisplayAlert("Aviso", "Se Eliminara el Campo Seleccionado(ID): " + seleccinarId.Id + " Nombre: " + seleccinarId.DescripcionCorta + " De la lista de Ubicaciones guardadas", "Ok");

                    var ListaMapas = conexion.Delete<Mapeo>(seleccinarId.Id);
                    OnAppearing();
                }
                else
                    messagetSelect();
            }

        }

        private async void btnVistaMapa_Clicked(object sender, EventArgs e)
        {
            var Gps = CrossGeolocator.Current;
            if (Gps.IsGeolocationEnabled)//Servicio de Geolocalizacion existente
            {


                if (Gps.IsGeolocationEnabled)//VALIDA QUE EL GPS ESTA ENCENDIDO
                {



                    using (SQLiteConnection conexion = new SQLiteConnection(App.SQliteUbicacion))
                    {
                        if (seleccinarId != null)
                        {
                            OnBackButtonPressed();

                        }

                        else
                            messagetSelect();
                    }
                }

            }
            else
            {
                await DisplayAlert("GPS Apagado", "Para ver la Ubicacion de manera Correcta. Por favor, Active el GPS/ Ubicacion", "OK");
            }

        }

        private void ListaUbicacion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            seleccinarId = e.SelectedItem as Mapeo;


            //Confirmamos que se seleccione
        }
        private async void messagetSelect()
        {
            await DisplayAlert("Sin Seleccion", "Por Favor Seleccione un Dato", "OK");
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Accion", "¿Desea ir a la Ubicacion indicada?", "Yes", "No");
                if (result) Ver();

            });


            return true;
        }

        private async void Ver()
        {

            var mapa = new
            {
                Id = seleccinarId.Id,
                Latitud = seleccinarId.Latitud,
                Longitud = seleccinarId.Longitud,
                DescripcionCorta = seleccinarId.DescripcionCorta,
                DescripcionLarga = seleccinarId.DescripcionLarga
            };

            
            var Page = new GeoPage();
            Page.BindingContext = mapa;
            await Navigation.PushAsync(Page);
            seleccinarId = null;

        }
    }
    }
