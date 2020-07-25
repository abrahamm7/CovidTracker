using CovidTracker.Helpers;
using CovidTracker.Models;
using Newtonsoft.Json;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        IPageDialogService service;
        public ApiServices(IPageDialogService pageDialogService)
        {
            service = pageDialogService;
        }
        public async Task<AllCases> GetAllCases()
        {
            try
            {
                Links = new Links();
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {                   
                    HttpClient httpClient = new HttpClient();                    
                    var text = await httpClient.GetStringAsync(Links.Linkall);
                    return JsonConvert.DeserializeObject<AllCases>(text, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture, FloatParseHandling = FloatParseHandling.Double });
                }
                else
                {
                    await service.DisplayAlertAsync("", "Not connection to internet", "Ok");
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
                Links = new Links();
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    HttpClient httpClient = new HttpClient();                    
                    var text = await httpClient.GetStringAsync(Links.LinkCountries);
                    return JsonConvert.DeserializeObject<List<Country>>(text, new JsonSerializerSettings() { Culture = CultureInfo.InvariantCulture, FloatParseHandling = FloatParseHandling.Double });
                     
                }
                else
                {
                    await service.DisplayAlertAsync("", "Not connection to internet", "Ok");
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
           
            try
            {
                Links = new Links(param);
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    HttpClient httpClient = new HttpClient();                         
                    var text = await httpClient.GetStringAsync(Links.LinkCountry);
                    return JsonConvert.DeserializeObject<CountrySel>(text);
                }
                else
                {
                    await service.DisplayAlertAsync("", "Not connection to internet", "Ok");
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
