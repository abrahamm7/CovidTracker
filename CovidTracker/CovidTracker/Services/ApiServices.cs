using CovidTracker.Helpers;
using CovidTracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CovidTracker.Services
{
    public class ApiServices : IApiService
    {
        Links Links;
        public async Task<AllCases> GetAllCases()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {                   
                    HttpClient httpClient = new HttpClient();                    
                    var text = await httpClient.GetStringAsync("https://corona.lmao.ninja/v2/all");
                    return JsonConvert.DeserializeObject<AllCases>(text);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Advice", "Not connection to internet", "Ok");
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task <List<Country>> GetCountries()
        {
            Links = new Links();
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    HttpClient httpClient = new HttpClient();                    
                    var text = await httpClient.GetStringAsync(Links.LinkCountries);
                    return JsonConvert.DeserializeObject<List<Country>>(text);
                     
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Not connection to internet", "Ok");
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<CountrySel> GetCountriesSelected(string param)
        {
            Links = new Links(param);
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    HttpClient httpClient = new HttpClient();                         
                    var text = await httpClient.GetStringAsync(Links.LinkCountry);
                    return JsonConvert.DeserializeObject<CountrySel>(text);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Not connection to internet", "Ok");
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
