﻿using CovidTracker.Helpers;
using CovidTracker.Models;
using CovidTracker.Services;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
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

        public MainViewModel(IApiService api, INavigationService navigationService)
        {
            apiServices = api;
            GetCases();
        }

        async Task GetCases()
        {
            try
            {
                RestService.For<IApiService>(Nav.LinkUrl);
                var response1 = await apiServices.GetAllCases();
                AllCases = response1;
                await App.Current.MainPage.DisplayAlert("", $"{AllCases.cases}", "ok");
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("", $"{e.Message}", "ok");
            }
        }
    }
}
