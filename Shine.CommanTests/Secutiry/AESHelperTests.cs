using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shine.Comman.Secutiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.Comman.Secutiry.Tests
{
    [TestClass()]
    public class AESHelperTests
    {
        [TestMethod()]
        public void AESEncryptTest()
        {
            string en = "myining";
            string xxx = en.AESEncrypt();
            string enc = xxx.AESDecrypt();
        }
    }
}