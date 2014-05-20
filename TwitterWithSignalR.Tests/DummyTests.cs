using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TwitterWithSignalR.Tests
{
    [TestClass]
    public class DummyTests
    {
        [TestMethod]
        public void Equals()
        {
            Assert.IsTrue(EqualityComparer<string>.Default.Equals("string", "string"));
            Assert.IsTrue(EqualityComparer<int>.Default.Equals(666, 666));
        }
    }
}
