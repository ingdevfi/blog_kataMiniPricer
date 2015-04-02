using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniPricer
{
    public class MiniPricer
    {
        private const int Iterations = 10000;

        public double ComputeFuturePrice(DateTime startDate, DateTime endDate, double spot, IVolatilityGenerator volatilityGenerator)
        {
            double result = 0.0d;
            
            for (int i = 0; i < Iterations; i++)
            {
               result+= ComputeOnePath(startDate, endDate, spot, volatilityGenerator);
            }

            return result /= Iterations;
        }

        private static double ComputeOnePath(DateTime startDate, DateTime endDate, double spot,
            IVolatilityGenerator volatilityGenerator)
        {
            double extrapolatedPrice = spot;

            for (var currentDate = startDate; currentDate < endDate; currentDate = currentDate.AddDays(1))
            {
                if (currentDate.IsOpened())
                {
                    extrapolatedPrice *= ((1 + volatilityGenerator.GetVolatility()/100));
                }
            }

            return Math.Round(extrapolatedPrice, 7);
        }

        public List<double> ComputeBasketPrice(DateTime startDay, DateTime endDay, Basket basket, IVolatilityGenerator volatilityGenerator)
        {
            var prices = new List<double>();
            double pivotPrice = ComputeFuturePrice(startDay, endDay, basket.SpotPivot, volatilityGenerator);
            prices.Add(pivotPrice);

            foreach (var instrument in basket.OtherIntrument)
                prices.Add(instrument.Spot + (pivotPrice - basket.SpotPivot) * instrument.Correlation);

            return prices;
        }
    }
}