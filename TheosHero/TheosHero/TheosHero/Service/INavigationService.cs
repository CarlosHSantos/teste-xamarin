using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheosHero.Model;

namespace TheosHero.Service
{
    public interface INavigationService
    {
        Task NavigateToHeroDetail(Hero hero);
    }
}
