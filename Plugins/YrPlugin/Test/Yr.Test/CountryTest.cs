using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using Yr; 

namespace Yr.Test 
{
    [TestClass]
    public class CountryTest 
    {
        public Country country;

        [TestInitialize]
        public void TestInitalize ()
        {
            this.country = new Country(); 
        }
 
        [TestMethod]
        public void TestPreformRequest()
        {
            Assert.IsTrue(this.country.getList().Count > 0); 
        }

        [TestMethod]
        public void TestGetCountryXML()
        {
            XElement element = country.getXml(country.getList());

            String value = element.ToString();

            Assert.IsTrue(!String.IsNullOrEmpty(value));
        }

        [TestMethod]
        public void TestWriteCountryFile()
        {
            country.writeFile(country.getXml(country.getList())); 
        }
    }
}
