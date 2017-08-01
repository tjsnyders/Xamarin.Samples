using AuthenticationModule.ViewModels;
using AuthenticationModule.Views;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace AuthenticationModule
{
	public class AuthenticationModule : Prism.Modularity.IModule
    {
		private readonly IUnityContainer _unityContainer;
		public AuthenticationModule(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		public void Initialize()
		{
            _unityContainer.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>();
            _unityContainer.RegisterTypeForNavigation<RegisterPage, RegisterPageViewModel>();

		}
	}
}