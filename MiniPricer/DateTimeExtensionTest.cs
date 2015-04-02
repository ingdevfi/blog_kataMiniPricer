using System;
using NUnit.Framework;

namespace MiniPricer
{
    [TestFixture]
    class DateTimeExtensionTest
    {
        [Test]
        public void Open_day()
        {
            var usualMonday = new DateTime(2015, 03, 16);

            Assert.True(usualMonday.IsOpened());
        }

        [Test]
        public void Sunday_day()
        {
            var usualSunday = new DateTime(2015, 03, 22);

            Assert.False(usualSunday.IsOpened());
        }

        [Test]
        public void Saturday_day()
        {
            var usualSaturday = new DateTime(2015, 03, 21);

            Assert.False(usualSaturday.IsOpened());
        }

        [Test]
        public void Is1RstMayBankHoliday()
        {
            var mayThe1Rst = new DateTime(2015, 5, 1);

            Assert.False(mayThe1Rst.IsOpened());
        }
    }
}
