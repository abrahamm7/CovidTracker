using CovidTracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CovidTracker.Services
{
    public class ApiServices : IApiService
    {
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
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    HttpClient httpClient = new HttpClient();
                    var text = await httpClient.GetStringAsync("https://corona.lmao.ninja/v2/countries?sort=country");
                    return JsonConvert.DeserializeObject<List<Country>>(text);
                     
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
    }
}
