using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using CurrencyConsole.Model;
using DbAccessLibrary;
using System.Threading.Tasks;

namespace CurrencyConsole.BAL
{
    public class ApplicationLogic
    {
        // User inputs are accepted and send to db to get final converted amount and showed it to console

        private readonly IConfiguration _configuration;

        public ApplicationLogic (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // User inputs are received and send to database for the result
        public void  CurrencyConversion()
        {

            //TODO - LOG -------

            ConversionInput objInput = new ConversionInput();

            Console.WriteLine("Enter Conversion Currency!");
            objInput.ExchangeCur = Console.ReadLine().ToString();

            Console.WriteLine("Enter Base Currency!");
            objInput.BaseCur = Console.ReadLine();

            Console.WriteLine("Enter the  Amount!");
            objInput.Amount = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter the History date in (DD-MM-YYYY)");
            objInput.Date = Console.ReadLine();

            // Access to db with input paramater with user inputs and expects the out as conversion amount
            DatabaseAccess objDB = new DatabaseAccess(_configuration);
            double amt =  objDB.CurrencyConversion(objInput.BaseCur, objInput.ExchangeCur, objInput.Amount,objInput.Date);

            Console.WriteLine("Convert amt -", amt);
            Console.ReadLine();
        }


    }
}
