using ArcadeAppNick.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeAppNick.Models; 

internal partial class WeatherInfo : ObservableObject
{
    private readonly WeatherApiService weatherApiService;

    public WeatherInfo()
    {
        weatherApiService = new WeatherApiService();
    }

    [ObservableProperty]
    private string city;

    [ObservableProperty]
    private string state_country;

    [ObservableProperty]
    private string temperature;

    [ObservableProperty]
    private string weather_icon;

    [ObservableProperty]
    private string wind_speed;

    [ObservableProperty]
    private string wind_degree;

    [ObservableProperty]
    private string wind_direction;

    [ObservableProperty]
    private string precipitation;

    [ObservableProperty]
    private string humidity;

    [ObservableProperty]
    private string cloudcover;

    [ObservableProperty]
    private string feelslike;

    [ObservableProperty]
    private string visibilty;

    [ObservableProperty]
    private string location; 

    [RelayCommand]
    private async Task FetchWeatherInformation()
    {
        var weatherApiResponse = await weatherApiService.GetWeatherInformation(City); 
        if(weatherApiResponse.Current != null)
        {
            Weather_icon = weatherApiResponse.Current.weather_icons[0];
            Temperature = $"{weatherApiResponse.Current.temperature}°C";
            Location = weatherApiResponse.Location.name + " Weather";
            Feelslike = $"Feels like {weatherApiResponse.Current.feelslike}°C";
            Cloudcover = $"Cloud cover at {weatherApiResponse.Current.cloudcover}%";
            Wind_speed = $"{weatherApiResponse.Current.wind_speed} MPH";
            Wind_degree = $"{weatherApiResponse.Current.wind_degree}° {weatherApiResponse.Current.wind_dir}";
            Precipitation = $"{weatherApiResponse.Current.precip}% chance of rain";
            Humidity = $"{weatherApiResponse.Current.humidity}% humidity";
            Visibilty = $"Visibility is {weatherApiResponse.Current.visibility} mi"; 
        }
    }
}