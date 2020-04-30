using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosHero.Model;
using TheosHero.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheosHero.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeroDetailPage : ContentPage
    {
        public HeroDetailPage(Hero hero)
        {
            InitializeComponent();
            BindingContext = new HeroViewModel(UserDialogs.Instance, hero);
        }
    }
}