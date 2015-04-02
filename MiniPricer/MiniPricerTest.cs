using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using System;

namespace MiniPricer
{
    [TestFixture]
    class MiniPricerTest
    {
        private MiniPricer _pricer;
        

        [SetUp]
        public void SetUp()
        {
            _pricer = new MiniPricer();
        }

        private static readonly DateTime Monday = new DateTime(2015, 03, 16);
        private static readonly DateTime Tuesday = new DateTime(2015, 03, 17);
        private static readonly DateTime Wenedestday = new DateTime(2015, 03, 18);
        
        
        private static readonly DateTime Saturday = new DateTime(2015, 03, 21);
        private static readonly DateTime Sunday = new DateTime(2015, 03, 22);

        private static readonly DateTime MayThe1Rst = new DateTime(2015, 05, 01);
        private static readonly DateTime MayThe2Nd = new DateTime(2015, 05, 02);
        private double _otherSpot;
        private double _otherCorrelation;


        private static IEnumerable TestCases {
            get
            {
                yield return new TestCaseData(Monday, Tuesday, 100, 10).Returns(110).SetName("One Day");
                yield return new TestCaseData(Monday, Wenedestday, 100, 10).Returns(121).SetName("Two Day");
                yield return new TestCaseData(Monday, Tuesday, 100, 0).Returns(100).SetName("No volatility");
                yield return new TestCaseData(Saturday, Sunday, 100, 10).Returns(100).SetName("No price change on week-end");
                yield return new TestCaseData(MayThe1Rst, MayThe2Nd, 100, 10).Returns(100).SetName("No price change on May the 1rst");
            }
        }

        [Test]
        [TestCaseSource("TestCases")]
        public double generic_test(DateTime startDate, DateTime endDate, double spotPrice, double volatility)
        {
            var moqGenerator = new Mock<IVolatilityGenerator>();
            moqGenerator.Setup(x => x.GetVolatility()).Returns(1*volatility);

            var price = _pricer.ComputeFuturePrice(startDate, endDate, spotPrice, moqGenerator.Object);

            return price;
        }

        [Test]
        public void call_volatility_generator_many_times_for_every_each_open_day()
        {
            var moqGenerator = new Mock<IVolatilityGenerator>();
            moqGenerator.Setup(x => x.GetVolatility()).Returns(1);

            _pricer.ComputeFuturePrice(Monday, Wenedestday, 100, moqGenerator.Object);

            moqGenerator.Verify( f => f.GetVolatility(), Times.AtLeast(3));
        }

        [Test]
        public void Basket_price_one_instrument_one_day()
        {
            //Actors
            var volatility = 10.0d;
            _otherSpot = 100.0d;
            _otherCorrelation = 0.80;
            var otherInstrument = new List<InstrumentDescription> {new InstrumentDescription(_otherSpot, _otherCorrelation)};
            double pivotSpot = 100.0d;
            var basket = new Basket(pivotSpot, volatility, otherInstrument);

            var moqGenerator = new Mock<IVolatilityGenerator>();
            moqGenerator.Setup(x => x.GetVolatility()).Returns(1 * basket.VolatilityPivot);
            
            //Act
            var prices = _pricer.ComputeBasketPrice(Monday, Tuesday, basket, moqGenerator.Object);
            
            //Assert
            Assert.AreEqual(2, prices.Count,"Result should contains pivot instrument price + another price for the underlying in basket");
            Assert.AreEqual(pivotSpot + volatility, prices[0], "The pivot instrument price is wrong");
            Assert.AreEqual(_otherSpot + _otherCorrelation * volatility, prices[1], "the basket instrument price is wrong");
        }
    }
}
