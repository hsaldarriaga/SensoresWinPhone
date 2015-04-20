using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Sensors;

namespace PhoneSensors.Sensors
{
    public class SLight : Sensor
    {
        public static SLight getInstance()
        {
            LightSensor lg = LightSensor.GetDefault();
            if (lg == null)
                return null;
            return new SLight(lg);

        }
        private SLight(LightSensor lg)
        {
            this.lg = lg;
            lg.ReadingChanged += lg_ReadingChanged;
            _name = "Light Sensor";
            _ValuesDimension = 1;
            _values = new System.Collections.ObjectModel.ObservableCollection<double>();
            for (int i = 0; i < _ValuesDimension; i++)
            {
                _values.Add(0.0f);
            }
        }

        private void lg_ReadingChanged(LightSensor sender, LightSensorReadingChangedEventArgs args)
        {
            LightSensorReading r = args.Reading;
            _values.Insert(0, r.IlluminanceInLux);
        }

        public override float getMinimumValue()
        {
            return lg.MinimumReportInterval;
        }

        public override float getMaximumValue()
        {
            return lg.ReportInterval;
        }

        ~SLight ()
        {
            lg.ReadingChanged -= lg_ReadingChanged;
        }
        private LightSensor lg;
    }
}
