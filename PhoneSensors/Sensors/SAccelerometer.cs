using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Sensors;
namespace PhoneSensors.Sensors
{
    public class SAccelerometer : Sensor
    {
        public static SAccelerometer getInstance()
        {
            Accelerometer ac = Accelerometer.GetDefault();
            if (ac == null)
                return null;
            return new SAccelerometer(ac);

        }
        private SAccelerometer(Accelerometer ac)
        {
            this.ac = ac;
            ac.ReadingChanged += ac_ReadingChanged;
            _name = "Accelerometer";
            _ValuesDimension = 3;
            _values = new System.Collections.ObjectModel.ObservableCollection<double>();
            for (int i = 0; i < _ValuesDimension; i++)
            {
                _values.Add(0.0f);
            }
        }

        private void ac_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            AccelerometerReading r = args.Reading;
            _values.Insert(0, r.AccelerationX);
            _values.Insert(1, r.AccelerationY);
            _values.Insert(2, r.AccelerationZ);
        }

        public override float getMinimumValue()
        {
            return ac.MinimumReportInterval;
        }

        public override float getMaximumValue()
        {
            return ac.ReportInterval;
        }

        ~SAccelerometer ()
        {
            ac.ReadingChanged -= ac_ReadingChanged;
        }
        private Accelerometer ac;
    }
}
