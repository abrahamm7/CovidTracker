using CovidTracker.Helpers;
using CovidTracker.Services;
using CovidTracker.ViewModels;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidTracker
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
      
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(new Uri(Nav.StartPage, UriKind.Relative));
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Pages//

            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<NavigationPage>();

            //Services//
            containerRegistry.Register<IApiService, ApiServices>();
         
        }

    }
}
