using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace PhoneSensors.Sensors
{
    public class SInclinometer : Sensor
    {
        public static SInclinometer getInstance()
        {
            Inclinometer ic = Inclinometer.GetDefault();
            if (ic == null)
                return null;
            return new SInclinometer(ic);

        }
        private SInclinometer(Inclinometer ic)
        {
            this.ic = ic;
            ic.ReadingChanged += ic_ReadingChanged;
            _name = "Inclinometer";
            _ValuesDimension = 2;
            _values = new System.Collections.ObjectModel.ObservableCollection<double>();
            for (int i = 0; i < _ValuesDimension; i++)
            {
                _values.Add(0.0f);
            }
        }

        private void ic_ReadingChanged(Inclinometer sender, InclinometerReadingChangedEventArgs args)
        {
            InclinometerReading r = args.Reading;
            _values.Insert(0, r.PitchDegrees);
            _values.Insert(1, r.RollDegrees);
        }

        public override float getMinimumValue()
        {
            return ic.MinimumReportInterval;
        }

        public override float getMaximumValue()
        {
            return ic.ReportInterval;
        }

        ~SInclinometer ()
        {
            ic.ReadingChanged -= ic_ReadingChanged;
        }
        private Inclinometer ic;
    }
}
