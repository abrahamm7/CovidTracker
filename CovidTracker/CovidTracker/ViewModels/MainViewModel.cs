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
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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
        public bool State { get; set; }
        public bool GridState { get; set; }
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
        }

        async Task GetCases()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                GetCountries();
                State = false;
                GridState = true;
                Links Links = new Links();
                try
                {
                    RestService.For<IApiService>(Links.Url);
                    var response1 = await apiServices.GetAllCases();
                    AllCases = response1;
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{e.Message}");
                } 
            }
            else
            {
                State = true;
                GridState = false;
                await service.DisplayAlertAsync("", "Not connection to internet", "ok");
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
                Debug.WriteLine($"{e.Message}");
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
                Debug.WriteLine($"{e.Message}");
            }
        }
    }
}
