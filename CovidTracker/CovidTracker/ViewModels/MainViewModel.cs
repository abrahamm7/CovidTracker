using CovidTracker.Helpers;
using CovidTracker.Models;
using CovidTracker.Services;
using Prism.Navigation;
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
        public MainViewModel(IApiService api, INavigationService navigationService)
        {
            apiServices = api;
            GetCases();
            GetCountries();
        }

        async Task GetCases()
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
        async Task GetCountries()
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
        async Task GetCountries(string param)
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
    }
}
