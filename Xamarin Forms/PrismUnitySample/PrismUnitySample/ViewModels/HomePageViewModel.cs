using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace PrismUnitySample.ViewModels
{
    public class HomePageViewModel : BindableBase,INavigatedAware
    {
		private string _userName;
		public string UserName
		{
			get { return _userName; }
			set { SetProperty(ref _userName, value); }
		}

        INavigationService _navigationService;
        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        void INavigatedAware.OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        void INavigatedAware.OnNavigatedTo(NavigationParameters parameters)
        {
            if(parameters.ContainsKey("UserName")){
                UserName = parameters.GetValue<string>("UserName");
            }
        }
    }
}
