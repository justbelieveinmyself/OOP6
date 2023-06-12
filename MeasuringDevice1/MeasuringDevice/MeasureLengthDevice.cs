using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice
{
    public class MeasureLengthDevice : MeasureDataDevice, IMeasuringDevice
    {

        public MeasureLengthDevice(Units unitsToUse)
        {
            this.unitsToUse = unitsToUse;
            measurementType = DeviceType.LENGTH;
        }
        
        public override decimal ImperialValue()
        {
            if(unitsToUse == Units.Imperial)
            {
                return mostRecentMeasure;
            }
            else
            {
                return (decimal)(mostRecentMeasure * 0.03937);
            }
        }

        public override decimal MetricValue()
        {
            if (unitsToUse == Units.Metric)
            {
                return mostRecentMeasure;
            }
            else
            {
                return (decimal) (mostRecentMeasure * 25.4);
            }
        }

    }
}
