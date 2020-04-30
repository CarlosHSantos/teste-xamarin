using Acr.UserDialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TheosHero.Model;
using TheosHero.Service;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheosHero.ViewModels
{
    public class HeroListViewModel : AbstractViewModel
    {
        public Command LoadMoreCommand { get; set; }
        public Command SearchCommand { get; set; }
        public Command InicializarHerois { get; set; }

        private ObservableCollection<Hero> heroes;
        public ObservableCollection<Hero> Heroes
        {
            get
            {
                return heroes;
            }
            set
            {
                heroes = value;
                OnPropertyChanged();
            }
        }

        private Hero selectedHero;
        public Hero SelectedHero
        {
            get
            {
                return selectedHero;
            }
            set
            {
                selectedHero = value;
                OnPropertyChanged();
            }
        }

        private String searchText;
        public String SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                OnPropertyChanged();

                if (String.IsNullOrEmpty(searchText))
                {
                    InicializarHerois?.Execute(null);
                }
            }
        }

        private Boolean thereIsResultList;
        public Boolean ThereIsResultList
        {
            get
            {
                return thereIsResultList;
            }
            set
            {
                thereIsResultList = value;
                OnPropertyChanged();
            }
        }

        private DataService dataService;

        private readonly INavigationService navigationService;

        public HeroListViewModel(IUserDialogs dialogs) : base(dialogs)
        {
            dataService = new DataService();
            navigationService = DependencyService.Get<INavigationService>();

            InicializarHerois = new Command(async () =>
            {
                using (UserDialogs.Instance.Loading("Carregando"))
                {
                    Heroes = await FiltrarHero();
                }
            });

            SearchCommand = new Command(async () =>
            {
                using (UserDialogs.Instance.Loading("Carregando"))
                {
                    Heroes = await FiltrarHero(SearchText);
                }
            });

            int offSetSearch = 6;
            LoadMoreCommand = new Command(async () =>
            {
                IsBusy = true;

                var result = await FiltrarHero(SearchText, 6, offSetSearch);
                offSetSearch += 30;

                if (result.Any())
                {
                    ThereIsResultList = true;
                    result.ForEach(hero => Heroes.Add(hero));
                }
                else
                {
                    ThereIsResultList = false;
                }

                IsBusy = false;

            }, CanExecute());
        }

        private async Task<ObservableCollection<Hero>> FiltrarHero(String filtro = null, int limit = 6, int offset = 0)
        {
            var heroes = new ObservableCollection<Hero>();
            var result = await dataService.GetHeros(filtro, limit, offset);

            if (result.Any())
            {
                ThereIsResultList = true;
                heroes = result;
            }
            else
            {
                ThereIsResultList = false;
            }

            return heroes;
        }

        private Func<bool> CanExecute()
        {
            return new Func<bool>(() => ThereIsResultList);
        }
    }
}