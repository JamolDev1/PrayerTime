using System;
using System.Text.Json;
using System.Threading.Tasks;
using lesson10.Dto.PrayerTime;
using lesson10.Dto.User;
using lesson10.Services;

namespace lesson10
{
    class Program
    {
        private static string usersApi = "https://randomuser.me/api/";
        private static string prayerTimeApi = "http://api.aladhan.com/v1/timingsByCity?city=Tashkent&country=Uzbekistan&method=8";

        private static void edit(string city, string country)
        {
            prayerTimeApi = $"http://api.aladhan.com/v1/timingsByCity?city={city}&country={country}&method=8";
        }
        static async Task Main(string[] args)
        {
            System.Console.Write("City: ");
            var city = Console.ReadLine();

            System.Console.Write("Country: ");
            var country = Console.ReadLine();
            edit(city, country);
            
            var httpService = new HttpClientService();
            var result = await httpService.GetObjectAsync<PrayerTime>(prayerTimeApi);

            if(result.IsSuccess)
            {
                var settings = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };

                var date = JsonSerializer.Serialize(result.Data.Data.Date.Gregorian.Date, settings).Replace("\"", "");
                Console.WriteLine($"\nDate: {date}");
                var json = JsonSerializer.Serialize(result.Data.Data.Timings.Fajr, settings).Replace("\"", "");
                Console.Write($"Fajr: {json} | ");
                json = JsonSerializer.Serialize(result.Data.Data.Timings.Sunrise, settings).Replace("\"", "");
                Console.Write($"Sunrise: {json} | ");
                json = JsonSerializer.Serialize(result.Data.Data.Timings.Dhuhr, settings).Replace("\"", "");
                Console.Write($"Dhuhr: {json} | ");
                json = JsonSerializer.Serialize(result.Data.Data.Timings.Asr, settings).Replace("\"", "");
                Console.Write($"Asr: {json} | ");
                json = JsonSerializer.Serialize(result.Data.Data.Timings.Sunset, settings).Replace("\"", "");
                Console.Write($"Sunset: {json} | ");
                json = JsonSerializer.Serialize(result.Data.Data.Timings.Maghrib, settings).Replace("\"", "");
                Console.Write($"Maghrib: {json} | ");
                json = JsonSerializer.Serialize(result.Data.Data.Timings.Isha, settings).Replace("\"", "");
                Console.WriteLine($"Isha: {json}");
                System.Console.WriteLine();
                
            }
            else
            {
                Console.WriteLine($"{result.ErrorMessage}");
            }
        }
        static async Task Main_user(string[] args)
        {
            var httpService = new HttpClientService();
            var result = await httpService.GetObjectAsync<User>(usersApi);

            if(result.IsSuccess)
            {
                Console.WriteLine($"{result.Data.Results[0].Name.First}");
            }
            else
            {
                Console.WriteLine($"{result.ErrorMessage}");
            }
            
        }
    }
}