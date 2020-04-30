using System.Threading.Tasks;
using TheosHero.Helper;
using TheosHero.Model;
using TheosHero.Views;

namespace TheosHero.Service
{
    class NavigationService : INavigationService
    {
        public async Task NavigateToHeroDetail(Hero hero)
        {
            await NavigationHelper.PushAsync(new HeroDetailPage(hero));
        }
    }
}
