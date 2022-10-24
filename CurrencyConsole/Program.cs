using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Threading.Tasks;
using CurrencyConsole.BAL;

namespace CurrencyConsole
{
    public class Program
    {
        /// <summary>
        ///     This console application is used for converted currency amount using user interface
        /// </summary>
        public static IConfiguration Configuration { get; set; }
       
        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                                       .Build();
            // Kick off the actual application
            
            getConverstionAmount();

            
        }

        static void getConverstionAmount()
        {
            // application logic is written inside BAL folder 
            ApplicationLogic objApp = new ApplicationLogic(Configuration);

             objApp.CurrencyConversion();

           


        }
    }
}
