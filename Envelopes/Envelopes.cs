using cAlgo.API;
using cAlgo.API.Indicators;

namespace cAlgo
{


    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class Envelopes : Indicator
    {


        [Parameter(DefaultValue = 14)]
        public int Period { get; set; }

        [Parameter(DefaultValue = 0.1)]
        public double Deviation { get; set; }

        [Parameter("MA Type", DefaultValue = MovingAverageType.Simple)]
        public MovingAverageType MAType { get; set; }

        [Output("Upper Band", LineColor = "#B268BCFF")]
        public IndicatorDataSeries UpperBand { get; set; }

        [Output("Main", LineColor = "#AA3F3F3F")]
        public IndicatorDataSeries Main { get; set; }

        [Output("Lower Band", LineColor = "#B2FF5861")]
        public IndicatorDataSeries LowerBand { get; set; }

        private MovingAverage _movingAverage;

        protected override void Initialize()
        {

            _movingAverage = Indicators.MovingAverage(Bars.ClosePrices, Period, MAType);

        }

        public override void Calculate(int index)
        {
            var maValue = _movingAverage.Result[index];

            Main[index] = maValue;
            UpperBand[index] = maValue * (1 + Deviation / 100);
            LowerBand[index] = maValue * (1 - Deviation / 100);

        }

    }

}
