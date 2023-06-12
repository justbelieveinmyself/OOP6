using System;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace MeasuringDevice
{
    //TODO - Modify the class definition to implement the extended interface.
    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice, IDisposable
    {
        protected Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController controller;
        protected DeviceType measurementType;

        protected string loggingFileName;
        private TextWriter loggingFileWriter;

        protected int heartBeatIntervalTime;
        public event HeartBeatEventHandler? HeartBeat;
        public int HeartBeatInterval { get; set; }

        /// <summary>
        /// Преобразует необработанные данные, собранные измерительным устройством, в метрическое значение.
        /// </summary>
        /// <returns>Последнее измерение с устройства, преобразованное в метрические единицы.</returns>
        public abstract decimal MetricValue();

        /// <summary>
        /// Преобразует необработанные данные, собранные измерительным устройством, в импералистическое значение.
        /// </summary>
        /// <returns>Последнее измерение с устройства, преобразованное в импералистические единицы.</returns>
        public abstract decimal ImperialValue();

        protected virtual void OnHeartBeat()
        {
            HeartBeatEventHandler handler = HeartBeat;
            if (handler != null)
            {
                handler(this, new HeartBeatEventArgs());
            }
        }

        private BackgroundWorker heartBeatTimer;

        private void StartHeartBeat()
        {
            heartBeatTimer = new BackgroundWorker();
            heartBeatTimer.WorkerSupportsCancellation = true;
            heartBeatTimer.WorkerReportsProgress = true;
            heartBeatTimer.DoWork += (o, args) =>
            {
                while (!heartBeatTimer.CancellationPending)
                {
                    Thread.Sleep(HeartBeatInterval);
                    if (disposed)
                    {
                        break;
                    }
                    heartBeatTimer.ReportProgress(0);
                }
            };
            heartBeatTimer.ProgressChanged += (o, args) => OnHeartBeat();
            heartBeatTimer.RunWorkerAsync();
        }

        private BackgroundWorker dataCollector;

        /// <summary>
        /// Запуск измерительного устройства
        /// </summary>
        public void StartCollecting()
        {
            if (disposed == true) return;

            if (controller == null)
                controller = DeviceController.StartDevice(measurementType);
            StartHeartBeat();
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

            // TODO - Cancel the data collector.
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

            if (heartBeatTimer != null)
            {
                heartBeatTimer.Dispose();
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