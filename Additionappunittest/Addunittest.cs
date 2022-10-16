
using Additionapp.Businesslogic;
using Additionapp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Additionappunittest
{
    [TestClass]
    public class Addunittest
    {
        [TestMethod]
        public void Add_normal_whole_number()
        {
          AddModel am= new AddModel();
          var result=am.Addition(10, 15);
          var Expectedresult = 25;
          Assert.AreEqual(Expectedresult, result);

        }
        [TestMethod]
        public void Add_with_arg1_negative_number()
        {
            AddModel am = new AddModel();
            var result = am.Addition(-30, 15);
            var Expectedresult = -15;
            Assert.AreEqual(Expectedresult, result);

        }
        [TestMethod]
        public void Add_with_both_negative_number()
        {
            AddModel am = new AddModel();
            var result = am.Addition(-28, -10);
            var Expectedresult = -38;
            Assert.AreEqual(Expectedresult, result);

        }
        [TestMethod]
        public void Add_with_arg2_negative_number()
        {
            AddModel am = new AddModel();
            var result = am.Addition(108, -10);
            var Expectedresult = 98;
            Assert.AreEqual(Expectedresult, result);

        }
        [TestMethod]
        public void Add_simple_decimal_number()
        {
            AddModel am = new AddModel();
            var result = am.Addition(5.8999, 8.5677);
            var Expectedresult = 14.468;
            Assert.AreEqual(Expectedresult, result);

        }
        [TestMethod]
        public void Add_large_decimal_number()
        {
            AddModel am = new AddModel();
            var result = am.Addition(100.9999999999, 200.9999999999);
            var Expectedresult = 302;
            Assert.AreEqual(Expectedresult, result);

        }
       
    }
}