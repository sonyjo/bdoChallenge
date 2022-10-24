using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CurrencyDailyUpdate.BAL;

namespace CurrencyDailyUpdate
{
     class Program
    {
        /// <summary>
        ///     This application is used for daily update of Fixer Api to db. This can be scheduled in the windows scheduler to run once.
        /// </summary>
        public static IConfiguration Configuration { get; set; }
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()                                       
                                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                                       .Build();
            // Kick off the actual application
             await getLatestCurrency();
            
            
            Console.ReadLine();
        }

        private static async Task  getLatestCurrency()
        {
            //Applciation logic is written in the BAL folder
            ApplicationLogic objApp = new ApplicationLogic(Configuration);

            await objApp.Run();

           


        }
    }
}
