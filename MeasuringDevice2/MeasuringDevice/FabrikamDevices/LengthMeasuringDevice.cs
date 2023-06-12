using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice.FabrikamDevices
{
    public class LengthMeasuringDevice : IControllableDevice
    {
        Random random;
        public LengthMeasuringDevice()
        {
            random = new Random();
        }
        public int GetLatestMeasure()
        {
            Thread.Sleep(random.Next(6000));
            return random.Next(1000);
        }

        public void StartDevice()
        {

        }

        public void StopDevice()
        {

        }
    }
}
