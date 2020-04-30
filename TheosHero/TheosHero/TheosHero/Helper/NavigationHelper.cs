using System.Threading.Tasks;
using Xamarin.Forms;

namespace TheosHero.Helper
{
    public static class NavigationHelper
    {
        public static Page CurrentPage => Application.Current.MainPage;

        private static INavigation Navigation => CurrentPage.Navigation;

        public static async Task PopAsync()
        {
            if (CurrentPage != null)
                await CurrentPage.Navigation.PopAsync();
            else
                await Navigation.PopAsync();
        }

        public static async Task PopModalAsync()
        {
            if (CurrentPage != null)
                await CurrentPage.Navigation.PopModalAsync();
            else
                await Navigation.PopModalAsync();
        }

        public static async Task PushModalAsync(Page page)
        {
            if (CurrentPage != null)
                await CurrentPage.Navigation.PushModalAsync(page);
            else
                await Navigation.PushModalAsync(page);
        }

        public static async Task PushAsync(Page page)
        {
            if (CurrentPage != null)
                await CurrentPage.Navigation.PushAsync(page);
            else
                await Navigation.PushAsync(page);
        }
    }
}
