using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace CovidTracker.Helpers
{
    public class Links
    {
        public string Url { get; set; }
        public string LinkCountries { get; set; }
        public string LinkCountry { get; set; }
      
        
        public Links(string param)
        {
            Url = "https://corona.lmao.ninja/";
            LinkCountries = "https://corona.lmao.ninja/v2/countries?sort=country";
            LinkCountry = $"https://corona.lmao.ninja/v2/countries/{param}";
        }
        public Links()
        {
            Url = "https://corona.lmao.ninja/";
            LinkCountries = "https://corona.lmao.ninja/v2/countries?sort=country";       
           
        }



    }
}
