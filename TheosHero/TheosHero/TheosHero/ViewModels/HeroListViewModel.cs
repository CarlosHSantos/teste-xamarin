using Acr.UserDialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Schema;
using TheosHero.Model;
using TheosHero.Service;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheosHero.ViewModels
{
    public class HeroListViewModel : AbstractViewModel
    {
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

        private ObservableCollection<Paginacao> paginacao;
        public ObservableCollection<Paginacao> Paginacao
        {
            get
            {
                return paginacao;
            }
            set
            {
                paginacao = value;
                OnPropertyChanged();
            }
        }

        Paginacao selectedPaginacao;
        public Paginacao SelectedPaginacao
        {
            get
            {
                return selectedPaginacao;
            }
            set
            {
                if (selectedPaginacao != value)
                {
                    selectedPaginacao = value;
                    OnPropertyChanged();
                }
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

        public Command PaginacaoCommand { get; set; }
        public Command PrimeiroCommand { get; set; }
        public Command UltimoCommand { get; set; }
        public Command ProximoCommand { get; set; }
        public Command AnteriorCommand { get; set; }

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
                    Heroes = await FiltrarHero("", 6, 0, true);

                    SelectedPaginacao = Paginacao.FirstOrDefault();
                }
            });

            PaginacaoCommand = new Command(async () =>
            {
                using (UserDialogs.Instance.Loading("Carregando"))
                {
                    if (string.IsNullOrEmpty(SearchText))
                        Heroes = await FiltrarHero("", 6, SelectedPaginacao.OffSet);
                    else
                        Heroes = await FiltrarHero(SearchText, 6, SelectedPaginacao.OffSet);
                }
            });

            PrimeiroCommand = new Command(async () =>
            {
                using (UserDialogs.Instance.Loading("Carregando"))
                {
                    SelectedPaginacao = Paginacao.FirstOrDefault();
                    if (string.IsNullOrEmpty(SearchText))
                        Heroes = await FiltrarHero("", 6, SelectedPaginacao.OffSet);
                    else
                        Heroes = await FiltrarHero(SearchText, 6, SelectedPaginacao.OffSet);
                }
            });

            UltimoCommand = new Command(async () =>
            {
                using (UserDialogs.Instance.Loading("Carregando"))
                {
                    SelectedPaginacao = Paginacao.LastOrDefault();
                    if (string.IsNullOrEmpty(SearchText))
                        Heroes = await FiltrarHero("", 6, SelectedPaginacao.OffSet);
                    else
                        Heroes = await FiltrarHero(SearchText, 6, SelectedPaginacao.OffSet);
                }
            });

            ProximoCommand = new Command(async () =>
            {
                if (SelectedPaginacao.Pagina < Paginacao.LastOrDefault().Pagina)
                    using (UserDialogs.Instance.Loading("Carregando"))
                    {
                        SelectedPaginacao = Paginacao.FirstOrDefault(x => x.Pagina == SelectedPaginacao.Pagina + 1);
                        if (string.IsNullOrEmpty(SearchText))
                            Heroes = await FiltrarHero("", 6, SelectedPaginacao.OffSet);
                        else
                            Heroes = await FiltrarHero(SearchText, 6, SelectedPaginacao.OffSet);
                    }
            });

            AnteriorCommand = new Command(async () =>
            {
                if (selectedPaginacao.Pagina > Paginacao.FirstOrDefault().Pagina)
                    using (UserDialogs.Instance.Loading("Carregando"))
                    {
                        SelectedPaginacao = Paginacao.FirstOrDefault(x => x.Pagina == SelectedPaginacao.Pagina - 1);
                        if (string.IsNullOrEmpty(SearchText))
                            Heroes = await FiltrarHero("", 6, SelectedPaginacao.OffSet);
                        else
                            Heroes = await FiltrarHero(SearchText, 6, SelectedPaginacao.OffSet);
                    }
            });

            SearchCommand = new Command(async () =>
            {
                using (UserDialogs.Instance.Loading("Carregando"))
                {
                    Heroes = await FiltrarHero(SearchText, 6, 0, true);
                }
            });
        }

        private async Task<ObservableCollection<Hero>> FiltrarHero(String filtro = null, int limit = 6, int offset = 0, bool criaPaginacao = false)
        {
            var heroes = new ObservableCollection<Hero>();
            var result = await dataService.GetHeros(filtro, limit, offset);

            if (result.Total > 0)
            {
                if (criaPaginacao)
                    CriaPaginacao(limit, result);

                ThereIsResultList = true;
                heroes = new ObservableCollection<Hero>(result.Results);
            }
            else
            {
                ThereIsResultList = false;
            }

            return heroes;
        }

        private void CriaPaginacao(int limit, MarvelApiData<Hero> result)
        {
            var pag = 1;
            Paginacao = new ObservableCollection<Paginacao>();
            for (int i = 0; i < result.Total; i++)
            {
                if (IsDivididoPor(i, limit) || Paginacao.Count() == 0)
                {
                    Paginacao.Add(new Paginacao()
                    {
                        OffSet = i,
                        Pagina = pag
                    });
                    pag += 1;
                }
            }
        }

        private Boolean IsDivididoPor(long numero, long divNumero)
        {
            if (divNumero != 0)
            {
                return (numero % divNumero) == 0;
            }
            else
                return false;
        }
    }
}