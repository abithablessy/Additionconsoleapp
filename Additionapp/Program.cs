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
        static void Getsettingfile()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfig = builder.Build();
        }
        static void Loggingtofile()
        {
            Log.Logger = new LoggerConfiguration()
   .WriteTo.File("consoleapp.log")
   .CreateLogger();
        }
        static void Main(string[] Args)
        {
            Loggingtofile();
            try
            {
                
                Console.WriteLine("This application helps to add two number");
                Console.WriteLine("Enter a number1");
                string val1 = Console.ReadLine();
                Inputmodel i = new Inputmodel();
                AddModel a = new AddModel();
                a.Arg1=i.GetuserInput(val1);
                Console.WriteLine("Enter a number2");
                string val2 = Console.ReadLine();
                Inputmodel i2 = new Inputmodel();
                a.Arg2 = i.GetuserInput(val2);
                a.Result = a.Addition(a.Arg1, a.Arg2);
                Console.WriteLine("Result is " + a.Result);
                Getsettingfile();
                AddDAL ad = new AddDAL(_iconfig);
                string saveresult=ad.Adddatacalc(a);
                Console.WriteLine(saveresult);
                Console.WriteLine("Do you want to see the list of saved Additions? y/n");
                string Getyorn=Console.ReadLine();
                Inputmodel i3 = new Inputmodel();
                Getyorn=i3.Imexemethod(Getyorn);
                if (Getyorn == "y")
                {
                    List<Dbaddcalcmodel> Getlistofcalc = ad.Getlistofcalc();
                    if (Getlistofcalc.Count() == 0)
                    { Console.WriteLine("There may be some issue. Please check the database connection and try again."); }
                    else
                    {
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
            catch(Exception e)
            {
                Log.Information("Main Method log: " + e.Message);
             }
        }
    }
}
