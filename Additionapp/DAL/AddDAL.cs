using Additionapp.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additionapp.DAL
{/// <summary>
/// Data layer for all database access functions.
/// </summary>
    public class AddDAL
    {
        private string _connectionstring;
        public AddDAL(IConfiguration iconfig)
        {
            _connectionstring = iconfig.GetConnectionString("Default");
        }
        /// <summary>
        /// To get the list of saved calculations from database.
        /// </summary>

        public List<Dbaddcalcmodel> Getlistofcalc()
        {
            List<Dbaddcalcmodel> addcalclist = new List<Dbaddcalcmodel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("Getaddcalclist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        addcalclist.Add(new Dbaddcalcmodel
                        {
                            Id = Convert.ToInt32(reader[0]),
                            Arg1 = Convert.ToDouble(reader[1]),
                            Arg2 = Convert.ToDouble(reader[2]),
                            Result = Convert.ToDouble(reader[3])


                        });
                    }
                }
            }
            catch (Exception e)
            {
                Log.Information("Getlistofcalc log: " + e.Message);

            }
            return addcalclist;
        }
        /// <summary>
        /// To save the numbers and their result in to the database.
        /// </summary>

        public string Adddatacalc(AddModel am)
        {
            string success = "Data Saved Successfully";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("Checkdataexist", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = cmd.Parameters.Add("@arg1", SqlDbType.Float);
                    param.Value = am.Arg1;
                    param = cmd.Parameters.Add("@arg2", SqlDbType.Float);
                    param.Value = am.Arg2;
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    con.Close();
                    if (count == 0)
                    {
                        SqlCommand ncmd = new SqlCommand("Insertaddcalc", con);
                        ncmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter nparam = ncmd.Parameters.Add("@arg1", SqlDbType.Float);
                        nparam.Value = am.Arg1;
                        nparam = ncmd.Parameters.Add("@arg2", SqlDbType.Float);
                        nparam.Value = am.Arg2;
                        nparam = ncmd.Parameters.Add("@result", SqlDbType.Float);
                        nparam.Value = am.Result;
                        nparam = ncmd.Parameters.Add("@date", SqlDbType.DateTime);
                        nparam.Value = DateTime.UtcNow;
                        con.Open();
                        int successresult = ncmd.ExecuteNonQuery();
                        con.Close();
                        if (successresult == 0)
                        { success = "There is an issue in data saving. Please try again later."; }

                    }

                }

            }
            catch (Exception e)
            {
                Log.Information("Adddatacalc log: " + e.Message);
                success = "There is an issue in data saving. Please check the database connection and try again.";
            }
            return success;

        }
    }
}
