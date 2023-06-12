using MeasuringDevice;
using System.Security.Cryptography.X509Certificates;

namespace TestModule
{
    public partial class Form1 : Form
    {
        MeasureMassDevice device;
        EventHandler newMeasurementTaken;
        public Form1()
        {
            InitializeComponent();
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {

            if (device == null)
            {

                device = new MeasureMassDevice(Units.Metric, "Logs.txt");

            }
            newMeasurementTaken = new EventHandler(device_NewMeashurementTaken);
            device.NewMeasurementTaken += newMeasurementTaken;

            device.StartCollecting();


        }
        private void device_NewMeashurementTaken(object sender, EventArgs e)
        {
            if (device != null)
            {
                labelMetric.Text = device.MetricValue().ToString();
                labelImperial.Text = device.ImperialValue().ToString();
                textBoxOutput.Clear();
                int[] rawData = device.GetRawData();
                foreach (var data in rawData)
                {
                    textBoxOutput.Text += data.ToString() + Environment.NewLine;
                    Console.WriteLine(data.ToString());
                }

            }
        }



        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (device != null)
            {
                device.StopCollecting();
                device.NewMeasurementTaken -= newMeasurementTaken;
            }
        }

        private void buttonDispose_Click(object sender, EventArgs e)
        {
            if (device != null)
            {
                device.Dispose();
                device = null;
            }
        }
    }
}