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

            NavigationService.NavigateAsync(new System.Uri("http://www.MyTestApp/CustomNavigationPage/LoginPage", System.UriKind.Absolute));

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<CustomNavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>();
            Container.RegisterTypeForNavigation<HomePage, HomePageViewModel>();
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