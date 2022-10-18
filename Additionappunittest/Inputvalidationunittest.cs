using Additionapp.Businesslogic;
using Additionapp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Net.Mime.MediaTypeNames;

namespace Additionappunittest
{
    [TestClass]
    public class Inputvalidationunittest
    {
        [TestMethod]
        public void Input_num_string_for_add()
        {
            Inputmodel im = new Inputmodel();
            var stringreader = new StringReader("8");
            Console.SetIn(stringreader);
            var readstring = Console.ReadLine();
            var result = im.GetuserInput(readstring);
            var expectedresult = 8;
            Assert.AreEqual(expectedresult, result);

        }
        [TestMethod]
        public void Input_negnum_string_for_add()
        {
            Inputmodel im = new Inputmodel();
            var stringreader = new StringReader("-5.9");
            Console.SetIn(stringreader);
            var readstring = Console.ReadLine();
            var result = im.GetuserInput(readstring);
            var expectedresult = -5.9;
            Assert.AreEqual(expectedresult, result);

        }

    }
}
