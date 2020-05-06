using Acr.UserDialogs;
using System;
using System.Linq;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as HeroListViewModel;

            if (vm.Heroes == null)
                vm.InicializarHerois.Execute(null);
        }

        private void CollectionPaginacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = BindingContext as HeroListViewModel;
            vm.SelectedPaginacao = (e.CurrentSelection.FirstOrDefault() as Paginacao);

            vm.PaginacaoCommand.Execute(null);
        }
    }
}