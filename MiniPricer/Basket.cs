using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniPricer
{
    public class Basket
    {
        private readonly double _spotPivot;
        private readonly double _volatilityPivot;

        private readonly List<InstrumentDescription> _otherIntrument;

        public Basket(double spotPivot, double volatilityPivot, List<InstrumentDescription> otherIntrument)
        {
            _spotPivot = spotPivot;
            _volatilityPivot = volatilityPivot;
            _otherIntrument = otherIntrument;
        }

        public double SpotPivot
        {
            get { return _spotPivot; }
        }

        public double VolatilityPivot
        {
            get { return _volatilityPivot; }
        }

        public List<InstrumentDescription> OtherIntrument
        {
            get { return _otherIntrument; }
        }
    }
}
