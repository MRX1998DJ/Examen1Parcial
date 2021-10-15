using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E17409
{
   
    public partial class App : Application
    { 
        public static string SQliteUbicacion = string.Empty;
        public App(String DBLocal)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            SQliteUbicacion = DBLocal;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
