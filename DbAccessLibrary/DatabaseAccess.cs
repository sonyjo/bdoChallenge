using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace DbAccessLibrary
{
    //Summary
     // This is a class library for database connection
    //
    public class DatabaseAccess
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public  DatabaseAccess(IConfiguration configuration)
        {
            this._configuration = configuration;
            
        }


        // For inserting data from the Fixer API to db
        public void  postDBInsert(DataTable dt)
        {
            try
            {
                var _connectionString = _configuration.GetSection("ConnectionStrings").GetSection("DbConstr").Value;

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.LatestCurrencyRate";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("ApiTimestamp", "ApiTimestamp");
                        sqlBulkCopy.ColumnMappings.Add("BaseCur", "BaseCur");
                        sqlBulkCopy.ColumnMappings.Add("ApiDate", "ApiDate");
                        sqlBulkCopy.ColumnMappings.Add("CurrencyCode", "CurrencyCode");
                        sqlBulkCopy.ColumnMappings.Add("CurrencyRate", "CurrencyRate");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            catch ( Exception ex)
            {
                throw ex;
            }

        }

        // For access the Exchange rate amount from the db
        public double CurrencyConversion(string BaseCur,string ExchCur , double amt,string date)
        {
            
                var _connectionString = _configuration.GetSection("ConnectionStrings").GetSection("DbConstr").Value;

                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                try
                {

                    SqlCommand sql_cmnd = new SqlCommand("Sp_CurrencyCoversion", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;

                   
                    //SqlParameter param  = new SqlParameter("@BaseCur", BaseCur);
                    //param.Direction = ParameterDirection.Input;
                    //param.DbType = DbType.String;
                    //sql_cmnd.Parameters.Add(param);

                 

                    sql_cmnd.Parameters.Add("@BaseCur", SqlDbType.NVarChar,10).Value = BaseCur;
                    sql_cmnd.Parameters.Add("@ExchangeCur", SqlDbType.NVarChar,10).Value = ExchCur;
                    sql_cmnd.Parameters.Add("@Amount", SqlDbType.Float).Value = amt;
                    sql_cmnd.Parameters.Add("@Date", SqlDbType.VarChar,10).Value = date;
                    sql_cmnd.Parameters.Add("@ConvertedAmt", SqlDbType.VarChar).Direction = ParameterDirection.Output;
               


                    sqlCon.Open();
                    //sql_cmnd.ExecuteNonQuery();
                    // sqlCon.Close();

                    var back = sql_cmnd.ExecuteScalar();
                    double result;
                    double.TryParse(back.ToString(), out result);
                    return result;
                    sqlCon.Close();

                }
         
            catch (SqlException ex)
            {
                throw ex;
            }
                catch(Exception ex)
                {
                    throw ex;
                    // LOG INFORMATION
                }
            }

            //using (SqlConnection conn = new SqlConnection(connection))
            //{
            //    DataSet dataset = new DataSet();
            //    SqlDataAdapter adapter = new SqlDataAdapter();
            //    adapter.SelectCommand = new SqlCommand("MyProcedure", conn);
            //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //    adapter.Fill(dataset);
            //    return dataset;
            //}


            //var ret_par = comm.Parameters.Add("@ret", SqlDbType.Int);
            //ret_par.Direction = ParameterDirection.ReturnValue;

            //using (var dr = comm.ExecuteReader())
            //{
            //    while (dr.Read())
            //    {

            //    }
            //}

            //var ret_val = (int)ret_par.Value; // result: 4712
        }
    }
}
