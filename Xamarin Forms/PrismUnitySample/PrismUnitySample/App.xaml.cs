using System;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using PrismUnitySample.ViewModels;
using PrismUnitySample.Views;
using Xamarin.Forms;

namespace PrismUnitySample
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();


			//Uncomment this list to Test Prism Tabbed Page 
			NavigationService.NavigateAsync(new System.Uri("/NavigationPage/CustomTabbedPage/Test1Page", System.UriKind.Absolute));

			//Uncomment this list to Test Prism Master DetailPage 
			// NavigationService.NavigateAsync(new System.Uri("/CustomMasterDetailPage/NavigationPage/Test1Page", System.UriKind.Absolute));

			//Uncomment this list to Test Prism general concepts as Custom NavigationPage/Modules/DelegateCommands/Services 
			//NavigationService.NavigateAsync(new System.Uri("http://www.MyTestApp/CustomNavigationPage/LoginPage", System.UriKind.Absolute));

		}

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<CustomTabbedPage>();
            Container.RegisterTypeForNavigation<NavigationPage>();

            Container.RegisterTypeForNavigation<CustomMasterDetailPage, CustomMasterDetailPageViewModel>();

            Container.RegisterTypeForNavigation<CustomNavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>();
            Container.RegisterTypeForNavigation<HomePage, HomePageViewModel>();

            Container.RegisterTypeForNavigation<Test1Page, Test1PageViewModel>();
            Container.RegisterTypeForNavigation<Test2Page>();
        }

		protected override void ConfigureModuleCatalog()
		{
			Type authenticationModuleType = typeof(AuthenticationModule.AuthenticationModule);
			ModuleCatalog.AddModule(
			  new ModuleInfo()
			  {
				  ModuleName = authenticationModuleType.Name,
				  ModuleType = authenticationModuleType,
                InitializationMode = InitializationMode.OnDemand
			  });
		}

	}
}