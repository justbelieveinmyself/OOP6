using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice
{
    public class MeasureMassDevice : MeasureDataDevice, IMeasuringDevice
    {

        public MeasureMassDevice(Units unitsToUse, string filePath)
        {
            this.unitsToUse = unitsToUse;
            measurementType = DeviceType.MASS;
            loggingFileName = filePath;
        }


        public override decimal ImperialValue()
        {
            if (unitsToUse == Units.Imperial)
            {
                return mostRecentMeasure;
            }
            else
            {
                return (decimal)(mostRecentMeasure * 2.2046);
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
                return (decimal)(mostRecentMeasure * 0.4536);
            }
        }
    }
}
