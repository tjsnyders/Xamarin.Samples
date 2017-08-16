using System;
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


			//Descomment this list to Test Prism Tabbed Page 
			NavigationService.NavigateAsync(new System.Uri("/CustomTabbedPage/NavigationPage/Test1Page", System.UriKind.Absolute));

			//Descomment this list to Test Prism Master DetailPage 

			//Descomment this list to Test Prism general concepts as Custom NavigationPage/Modules/DelegateCommands/Services 
			//NavigationService.NavigateAsync(new System.Uri("http://www.MyTestApp/CustomNavigationPage/LoginPage", System.UriKind.Absolute));

		}

        protected override void RegisterTypes()
        {
			Container.RegisterTypeForNavigation<CustomTabbedPage>();
            Container.RegisterTypeForNavigation<CustomNavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>();
            Container.RegisterTypeForNavigation<HomePage, HomePageViewModel>();

            Container.RegisterTypeForNavigation<Test1Page>();
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