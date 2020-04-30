using System;
using TheosHero.Service;
using TheosHero.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheosHero
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<INavigationService, NavigationService>();
            MainPage = new NavigationPage(new HeroListPage());
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
