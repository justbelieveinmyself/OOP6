namespace MeasuringDevice
{
    public interface IControllableDevice
    {
    
        void StartDevice();

        void StopDevice();

        int GetLatestMeasure();
    }
}