using Acr.UserDialogs;
using System;
using TheosHero.Model;
using TheosHero.Service;
using TheosHero.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheosHero.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeroListPage : ContentPage
    {
        private readonly INavigationService navigationService;
        public HeroListPage()
        {
            InitializeComponent();
            navigationService = DependencyService.Get<INavigationService>();
            BindingContext = new HeroListViewModel(UserDialogs.Instance);
        }

        private async void HeroList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedHero = (Hero)(e.Item);
            await navigationService.NavigateToHeroDetail(selectedHero);
        }
    }
}