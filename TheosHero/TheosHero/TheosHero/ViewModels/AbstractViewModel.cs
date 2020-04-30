using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheosHero.Helper;
using Xamarin.Forms;

namespace TheosHero.ViewModels
{
    public class AbstractViewModel : ObservableObject
    {
        private bool _IsBusy;

        public virtual bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        protected IUserDialogs Dialogs { get; }

        protected AbstractViewModel(IUserDialogs dialogs)
        {
            IsBusy = false;
            this.Dialogs = dialogs;
        }
    }
}