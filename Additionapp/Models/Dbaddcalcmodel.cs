using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additionapp.Models
{
    public class Dbaddcalcmodel
    {
        public int Id { get; set; }
        public double Arg1 { get; set; }
        public double Arg2 { get; set; }
        public double Result { get; set; }
        public DateTime Createddate { get; set; }
    }
}
