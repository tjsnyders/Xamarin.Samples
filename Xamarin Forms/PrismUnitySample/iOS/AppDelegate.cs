using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Practices.Unity;
using Prism.Unity;
using PrismUnitySample.iOS.Services;
using PrismUnitySample.Services;
using UIKit;

namespace PrismUnitySample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        static BatteryService batteryService = new BatteryService();
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
        public class iOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IUnityContainer container)
            {
				container.RegisterInstance<IBatteryService>(batteryService, new ExternallyControlledLifetimeManager());
			}
        }
    }
}



