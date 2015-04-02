using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPricer
{
    public class InstrumentDescription
    {
        private readonly double _spot;
        private readonly double _correlation;

        public InstrumentDescription(double spot, double correlation)
        {
            _spot = spot;
            _correlation = correlation;
        }

        public double Correlation
        {
            get { return _correlation; }
        }

        public double Spot
        {
            get { return _spot; }
        }
    }
}
