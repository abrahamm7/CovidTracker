using CovidTracker.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Services
{
    public interface IApiService
    {
        [Get("/corona.lmao.ninja/v2/all")]
        Task<AllCases> GetAllCases();

        [Get("/corona.lmao.ninja/v2/countries?sort=country")]
        Task<List<Country>> GetCountries();

        [Get("/corona.lmao.ninja/v2/countries/:country")]
        Task<CountrySel> GetCountriesSelected(string param);
    }
}
