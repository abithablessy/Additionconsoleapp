using Additionapp.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Additionapp.Businesslogic
{
    public static class Addargs
    {
        /// <summary>
        /// This extension method helps to add two numbers
        /// </summary>
        public static double Addition(this AddModel add, double Arg1, double Arg2)
        {
            double Result = 0;
            try
            {
                Result = Arg1 + Arg2;
                Result =Math.Round(Result, 3);
                
            }
            catch (Exception e)
            {
                Log.Information("Addition log: " + e.Message);
            }
            return Result;
        }
    }
}
