using System;
using System.IO;
using System.ComponentModel;

namespace MeasuringDevice
{

    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice, IDisposable
    {
        protected Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController controller;
        protected DeviceType measurementType;

        protected string loggingFileName;
        private TextWriter loggingFileWriter;


        public abstract decimal MetricValue();


        public abstract decimal ImperialValue();

        private BackgroundWorker dataCollector;

        public void StartCollecting()
        {
            if (disposed == true) return;

            if (controller == null)
                controller = DeviceController.StartDevice(measurementType);


            GetMeasurements();
        }


        private void GetMeasurements()
        {
            dataCollector = new BackgroundWorker();
            dataCollector.WorkerSupportsCancellation = true;
            dataCollector.WorkerReportsProgress = true;

            dataCollector.DoWork += new DoWorkEventHandler(dataCollector_DoWork);
            dataCollector.ProgressChanged += new ProgressChangedEventHandler(dataCollector_ProgressChanged);
            dataCollector.RunWorkerAsync();
        }
        void dataCollector_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnNewMeasurementTaken();
        }
        void dataCollector_DoWork(object sender, DoWorkEventArgs e)
        {
            dataCaptured = new int[10];
            int i = 0;
            while (!dataCollector.CancellationPending)
            {
                DataCaptured[i] = controller.TakeMeasurement();
                mostRecentMeasure = dataCaptured[i];

                if (disposed) break;

                if (loggingFileWriter != null)
                {
                    loggingFileWriter.WriteLine($"Measurement-{mostRecentMeasure}");
                }

                dataCollector.ReportProgress(0);
                i++;
                if (i > 9) i = 0;
            }
        }


        public void StopCollecting()
        {
            if (disposed == true) return;

            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }

            if (dataCollector != null)
            {
                dataCollector.CancelAsync();
            }
        }


        public int[] GetRawData()
        {
            return dataCaptured;
        }


        public string GetLoggingFile()
        {
            return loggingFileName;
        }

        private bool disposed = false;

        public void Dispose()
        {

            disposed = true;


            if (loggingFileWriter != null)
            {
                loggingFileWriter.Flush();
                loggingFileWriter.Close();
            }


            if (dataCollector != null)
            {
                dataCollector.Dispose();
            }
        }


        public Units UnitsToUse => unitsToUse;


        public int[] DataCaptured => dataCaptured;

 
        public int MostRecentMeasure => mostRecentMeasure;


        public string LoggingFileName
        {
            get
            {
                return loggingFileName;
            }
            set
            {
                if (loggingFileWriter == null)
                {

                    loggingFileName = value;
                }
                else
                {

                    loggingFileWriter.Close();

                    loggingFileName = value;

                    if (!File.Exists(loggingFileName))
                    {
                        loggingFileWriter = File.CreateText(loggingFileName);
                    }
                    else
                    {
                        loggingFileWriter = new StreamWriter(loggingFileName);
                    }
                }
            }
        }

        public event EventHandler NewMeasurementTaken;
        protected virtual void OnNewMeasurementTaken()
        {
            if (NewMeasurementTaken != null)
            {
                NewMeasurementTaken(this, null);
            }
        }

    }
}