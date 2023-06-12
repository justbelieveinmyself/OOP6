using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice.ContosoDevices
{
    public class MassMeasuringDevice : IControllableDevice
    {
        Random random;
        public MassMeasuringDevice()
        {
            random = new Random();
        }
        public int GetLatestMeasure()
        {
            Thread.Sleep(random.Next(5000));
            return random.Next(1390);
        }

        public void StartDevice()
        {

        }

        public void StopDevice()
        {

        }
    }
}
