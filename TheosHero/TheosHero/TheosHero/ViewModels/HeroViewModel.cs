using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TheosHero.Model;
using Xamarin.Forms;

namespace TheosHero.ViewModels
{
    public class HeroViewModel : AbstractViewModel
    {
        private Hero hero;
        public Hero Hero
        {
            get
            {
                return hero;
            }
            set
            {
                hero = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MarvelItem> comicsItens;
        public ObservableCollection<MarvelItem> ComicsItens
        {
            get
            {
                return comicsItens;
            }
            set
            {
                comicsItens = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MarvelItem> seriesItens;
        public ObservableCollection<MarvelItem> SeriesItens
        {
            get
            {
                return seriesItens;
            }
            set
            {
                seriesItens = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MarvelItem> storiesItens;
        public ObservableCollection<MarvelItem> StoriesItens
        {
            get
            {
                return storiesItens;
            }
            set
            {
                storiesItens = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MarvelItem> eventsItens;
        public ObservableCollection<MarvelItem> EventsItens
        {
            get
            {
                return eventsItens;
            }
            set
            {
                eventsItens = value;
                OnPropertyChanged();
            }
        }
        public HeroViewModel(IUserDialogs dialogs, Hero hero) : base(dialogs)
        {
            Hero = hero;
            ComicsItens = new ObservableCollection<MarvelItem>(hero.Comics.Items);
            SeriesItens = new ObservableCollection<MarvelItem>(hero.Series.Items);
            StoriesItens = new ObservableCollection<MarvelItem>(hero.Stories.Items);
            EventsItens = new ObservableCollection<MarvelItem>(hero.Events.Items);
        }
    }
}