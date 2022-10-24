using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CurrencyDailyUpdate.Model;
using System.Data;
using System.Reflection;
using DbAccessLibrary;


namespace CurrencyDailyUpdate.BAL
{
    public class ApplicationLogic
    {
        private readonly IConfiguration _configuration;
        public ApplicationLogic(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task Run()
        {
           

            // TODO : log information

            //   Log.Information();

            //return response;

            // Fetch data from the Fixer API
            FixerApiResponse objFixerApiResponse = new FixerApiResponse();
            FetchApiData objApiData = new FetchApiData(_configuration);
            objFixerApiResponse = await objApiData.MakeApiCall();

            DataTable dt = new DataTable(); 
            dt.Columns.Add("ApiTimestamp");
            dt.Columns.Add("BaseCur");
            dt.Columns.Add("ApiDate");
            dt.Columns.Add("CurrencyCode");
            dt.Columns.Add("CurrencyRate");            


            foreach (KeyValuePair<string, double> entry in objFixerApiResponse.rates)
            {

               DataRow r = dt.NewRow();

                r["ApiTimestamp"] = objFixerApiResponse.timestamp;
                r["BaseCur"] = objFixerApiResponse.Base;
                r["ApiDate"] = objFixerApiResponse.date;
                r["CurrencyCode"] = entry.Key;
                r["CurrencyRate"] = entry.Value;
                dt.Rows.Add(r);


            }

            // Then the output from the API is inserted to a database using BULK INSERT. Database class is a class library
            DatabaseAccess objDB = new DatabaseAccess(_configuration);
            objDB.postDBInsert(dt);

           

        }

       


    }
}
