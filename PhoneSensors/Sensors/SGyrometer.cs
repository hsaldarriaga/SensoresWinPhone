using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Sensors;

namespace PhoneSensors.Sensors
{
    public class SGyrometer : Sensor
    {
        public static SGyrometer getInstance()
        {
            Gyrometer gr = Gyrometer.GetDefault();
            if (gr == null)
                return null;
            return new SGyrometer(gr);

        }
        private SGyrometer(Gyrometer gr)
        {
            this.gr = gr;
            gr.ReadingChanged += gr_ReadingChanged;
            _name = "Gyrometer";
            _ValuesDimension = 3;
            _values = new System.Collections.ObjectModel.ObservableCollection<double>();
            for (int i = 0; i < _ValuesDimension; i++)
            {
                _values.Add(0.0f);
            }
        }

        private void gr_ReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {
            GyrometerReading r = args.Reading;
            _values.Insert(0, r.AngularVelocityX);
            _values.Insert(1, r.AngularVelocityY);
            _values.Insert(2, r.AngularVelocityZ);
        }

        public override float getMinimumValue()
        {
            return gr.MinimumReportInterval;
        }

        public override float getMaximumValue()
        {
            return gr.ReportInterval;
        }

        ~SGyrometer ()
        {
            gr.ReadingChanged -= gr_ReadingChanged;
        }
        private Gyrometer gr;
    }
}
