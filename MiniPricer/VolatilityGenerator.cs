using System;

namespace MiniPricer
{
    internal class VolatilityGenerator : IVolatilityGenerator
    {
        private readonly double _volatility;
        private readonly Random _random;

        public VolatilityGenerator(double volatility)
        {
            _volatility = volatility;
            _random = new Random();
        }

        public double GetVolatility()
        {
            var random = (int) (Math.Round(_random.NextDouble()*2) - 1);

            return random * _volatility;
        }
    }

    public interface IVolatilityGenerator
    {
        double GetVolatility();
    }
}