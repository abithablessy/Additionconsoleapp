using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Additionapp.Models
{/// <summary>
/// This helps to do the user input validation.
/// </summary>
    public class Inputmodel
    {

        public string Inp { get; set; }
        public double GetuserInput(string inp)
        {
            double n = 0;
            try
            {

                bool isNumeric = double.TryParse(inp, out n);
                while (!isNumeric)
                {
                    Console.WriteLine("Please enter some numbers");
                    inp = Console.ReadLine();
                    isNumeric = double.TryParse(inp, out n);
                }

            }
            catch (Exception e)
            {
                Log.Information("GetuserInput log: " + e.Message);

            }
            return n;
        }
    }
    /* Extension method helps to validate yes or no user input. */
    public static class InputModelexte
    {
        public static string Imexemethod(this Inputmodel im, string Inp)
        {
            try
            {
                while (Inp != "y" && Inp != "n")
                {
                    Console.WriteLine("Please enter y/n");
                    Inp = Console.ReadLine();
                }

            }
            catch (Exception e)
            {
                Log.Information("Imexemethod log: " + e.Message);
            }
            return Inp;
        }
    }
}
