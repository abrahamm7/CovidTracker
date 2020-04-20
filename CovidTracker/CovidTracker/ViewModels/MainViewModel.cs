using CovidTracker.Helpers;
using CovidTracker.Models;
using CovidTracker.Services;
using Prism.Navigation;
using Prism.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        INavigationService navigation;
        IApiService apiServices;
        IPageDialogService service;
        public AllCases AllCases { get; set; } = new AllCases();
        Country CountrySele;
        public CountrySel CountrySel { get; set; } = new CountrySel();
        public ObservableCollection<CountrySel> listcountrySels { get; set; } = new ObservableCollection<CountrySel>();
        public ObservableCollection<Country> Countries { get; set; } = new ObservableCollection<Country>();
        public Country CountrySelected //Select element in picker//
        {
            get
            {
                return CountrySele;
            }
            set
            {
                CountrySele = value;
                if (CountrySele != null)
                {
                    GetCountries(CountrySele.country);
                }
            }
        }
        public MainViewModel(IApiService api, INavigationService navigationService, IPageDialogService page)
        {
            apiServices = api;
            service = page;
            GetCases();
            GetCountries(); 
        }

        async Task GetCases()
        {
            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                Links Links = new Links();
                try
                {
                    RestService.For<IApiService>(Links.Url);
                    var response1 = await apiServices.GetAllCases();
                    AllCases = response1;
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("", $"{e.Message}", "ok");
                } 
            }
            else
            {
                await service.DisplayAlertAsync("", "Not connection to internet", "Ok");
            }
        }
        async Task GetCountries()
        {
            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                Links Links = new Links();
                try
                {
                    RestService.For<IApiService>(Links.Url);
                    var response1 = await apiServices.GetCountries();
                    foreach (var item in response1)
                    {
                        this.Countries.Add(item);
                    }

                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("", $"{e.Message}", "ok");
                } 
            }
            else
            {
                await service.DisplayAlertAsync("", "Not connection to internet", "Ok");
            }
        }
        async Task GetCountries(string param)
        {
            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                Links Links = new Links(param);
                try
                {
                    RestService.For<IApiService>(Links.Url);
                    var response1 = await apiServices.GetCountriesSelected(param);
                    CountrySel = response1;
                    listcountrySels.Add(CountrySel);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("", $"{e.Message}", "ok");
                } 
            }
            else
            {
                await service.DisplayAlertAsync("", "Not connection to internet", "Ok");
            }
        }

        async void Message()
        {
            await service.DisplayAlertAsync("", "Not connection to internet", "Ok");
        }
    }
}
