using System;
using SQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using PM2E17409.Modals;


namespace PM2E17409
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            var localizacion = CrossGeolocator.Current;
            if (localizacion.IsGeolocationEnabled)//Servicio de Geolocalizacion existente
            {

            }
            else
            {
                DisplayAlert("Permisos Geolocalizacion", "Por favor, de Acceso a su ubicacion/geolocalizacion de manera manual en dispositivo", "OK");
            }

        }

        private async void Localizacion()
        {

            var Gps = CrossGeolocator.Current;

            if (Gps.IsGeolocationEnabled)//VALIDA QUE EL GPS ESTA ENCENDIDO
            {
                var localizacion = CrossGeolocator.Current;
                var posicion = await localizacion.GetPositionAsync();
                txtLongitud.Text = posicion.Longitude.ToString();
                txtLatitud.Text = posicion.Latitude.ToString();
            }

            else
            {
                await DisplayAlert("Gps Desactivado", "Por favor, Active su GPS/Ubicacion", "OK");
            }

        }


        private void btnNuevaUbicacion_Clicked(object sender, EventArgs e)
        {
            Localizacion();
            txtDescripcion.Text = "";
            txtDescripcionCorta.Text = "";
        }

        private void Cargar()
        {
            Int32 resultado = 0;
            using (SQLiteConnection conexion = new SQLiteConnection(App.SQliteUbicacion))
            {   //mandamos a que tabla se guarda
                conexion.CreateTable<Mapeo>();
               

                //Primera Ubicacion Predeterminada
                var Location = new Mapeo()
                {
                    Latitud = 13.376050848869307,
                    Longitud = -86.95442141354778,
                    DescripcionLarga = "Ubicada en San Marcos de Colon",
                    DescripcionCorta = "La Peña",
                };

                resultado = conexion.Insert(Location);


                //Segunda Ubicacion Predeterminada
                var Location2 = new Mapeo()
                {
                    Latitud = 13.31415238095598,
                    Longitud = -87.19161111806798,
                    DescripcionLarga = "Salida a Choluteca",
                    DescripcionCorta = "Hotel Gualiqueme",
                };

                resultado = conexion.Insert(Location2);

                //Tercera Ubicacion Predeterminada
                var Location3 = new Mapeo()
                {
                    Latitud = 13.465497133002827,
                    Longitud = -86.7309240180663,
                    DescripcionLarga = "San Marcos de Colon, Cerca de La Frotenra con Nicaragua",
                    DescripcionCorta = "Cañon Caulato",
                };

                resultado = conexion.Insert(Location3);


                if (resultado > 0)
                {

                    DisplayAlert("Aviso", "Datos de prueba ingresados!! ", "Ok");

                }
            }
        }
      
        private async void btnListaUbicacion1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaUbicaciones());
        }



        private async void btnGuardarUbicacion_Clicked(object sender, EventArgs e)
        {
            Int32 resultado = 0;


            //validamos que los campos no esten vacios
            if (String.IsNullOrEmpty(txtLongitud.Text) && String.IsNullOrEmpty(txtLatitud.Text))
            {
                await DisplayAlert("Sin Datos", "Para Obtener la Lactitud y Longitud presionar <<Nueva Ubicacion>> ", "Ok");
            }
            else if (String.IsNullOrEmpty(txtDescripcion.Text))
            {
                await DisplayAlert("Campo Vacio", "Por favor, Ingrese una Descripcion de la Ubicacion ", "Ok");
            }
            else if (String.IsNullOrEmpty(txtDescripcionCorta.Text))
            {
                await DisplayAlert("Campo Vacio", "Por favor, Ingrese una Descripcion Corta, Por favor ", "Ok");
            }
            else
            {

                using (SQLiteConnection conexion = new SQLiteConnection(App.SQliteUbicacion))
                {   
                    conexion.CreateTable<Mapeo>();
                    
                    var Location = new Mapeo()
                    {
                        Longitud = Convert.ToDouble(txtLongitud.Text),
                        Latitud = Convert.ToDouble(txtLatitud.Text),
                        DescripcionLarga = Convert.ToString(txtDescripcion.Text),
                        DescripcionCorta = Convert.ToString(txtDescripcionCorta.Text),
                    };

                    resultado = conexion.Insert(Location);

                    if (resultado > 0)
                    {

                        await DisplayAlert("Aviso", "Se Guardo Exitosamente ", "Ok");

                        txtDescripcion.Text = "";
                        txtDescripcionCorta.Text = "";

                    }
                    else
                    {
                        await DisplayAlert("Aviso", "hubo un error al enviar los datos", "Ok");
                    }
                }
            }
        }

        

        private void btnRegistrarData_Clicked(object sender, EventArgs e)
        {
            Cargar();
        }

        
    }
    }