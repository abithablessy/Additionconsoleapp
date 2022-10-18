using Additionapp.Businesslogic;
using Additionapp.DAL;
using Additionapp.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additionapp
{
    class Program
    {
        private static IConfiguration _iconfig;
        /// <summary>
        /// This method helps to read the database information from the file to setup the configuration.
        /// </summary>
        static void Getsettingfile()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfig = builder.Build();
        }
        /// <summary>
        /// This method helps to create a log file to register all logs for easy error tracking.
        /// </summary>
        static void Loggingtofile()
        {
            Log.Logger = new LoggerConfiguration()
   .WriteTo.File("Additionapp.log")
   .CreateLogger();
        }

        static void Main(string[] Args)
        {
            Loggingtofile();
            Inputmodel i = new Inputmodel();
            AddModel a = new AddModel();
            try
            {

                Console.WriteLine("This application helps to add two number");
                Console.WriteLine("Enter a number1");
                string val1 = Console.ReadLine();

                /* Get user input 1 and validate.*/

                a.Arg1 = i.GetuserInput(val1);
                Console.WriteLine("Enter a number2");
                string val2 = Console.ReadLine();

                /* Get user input 2 and validate. */

                a.Arg2 = i.GetuserInput(val2);

                /* Pass two args in to Addition method to perform Addition. */

                a.Result = a.Addition(a.Arg1, a.Arg2);
                Console.WriteLine("Result is " + a.Result);

                /* Get the db config files. */

                Getsettingfile();
                AddDAL ad = new AddDAL(_iconfig);

                /* Save the values in to database. */

                string saveresult = ad.Adddatacalc(a);
                Console.WriteLine(saveresult);

                /* Get users choice to show the list of saved values from database.*/

                Console.WriteLine("Do you want to see the list of saved Additions? y/n");
                string getyorn = Console.ReadLine();

                /* Validate the user's choice to show the list of saved values. */

                getyorn = i.Imexemethod(getyorn);
                if (getyorn == "y")
                {
                    List<Dbaddcalcmodel> Getlistofcalc = ad.Getlistofcalc();
                    if (Getlistofcalc.Count() == 0)
                    { Console.WriteLine("There may be some issue. Please check the database connection and try again."); }
                    else
                    { /* Display list of saved values from database. */

                        Getlistofcalc.ForEach(item =>
                        {
                            Console.WriteLine("Id:" + item.Id + "\n" + "Number 1: " + item.Arg1 + "\n" + "Number 2: " + item.Arg2 + "\n" +
                                "Result: " + item.Result + "\n");
                        });
                    }
                }
                Console.WriteLine("Thank you. Have a great day!");
                Console.WriteLine("Press any key to stop.");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Log.Information("Main Method log: " + e.Message);
            }
        }
    }
}
