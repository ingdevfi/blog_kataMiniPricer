using NUnit.Framework;
using System.Linq;

namespace MiniPricer
{
    [TestFixture]
    public class VolatilityGeneratorTest
    {
        [Test]
        public void Test_Only_One_Two_Three()
        {
            var generator = new VolatilityGenerator(1);

            var generatedList = Enumerable.Range(1, 10).Select(x => generator.GetVolatility()).ToList();

            Assert.False(generatedList.Any(x => x != 1 && x != 0 && x != -1));
        }
    }
}