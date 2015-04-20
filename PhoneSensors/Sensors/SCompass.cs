using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Sensors;

namespace PhoneSensors.Sensors
{
    public class SCompass : Sensor
    {
        public static SCompass getInstance()
        {
            Compass cp = Compass.GetDefault();
            if (cp == null)
                return null;
            return new SCompass(cp);

        }
        private SCompass(Compass cp)
        {
            this.cp = cp;
            cp.ReadingChanged += cp_ReadingChanged;
            _name = "Compass";
            _ValuesDimension = 2;
            _values = new System.Collections.ObjectModel.ObservableCollection<double>();
            for (int i = 0; i < _ValuesDimension; i++)
            {
                _values.Add(0.0f);
            }
        }

        private void cp_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            CompassReading r = args.Reading;
            _values.Insert(0, r.HeadingTrueNorth.HasValue ? r.HeadingTrueNorth.Value : 0d);
            _values.Insert(1, r.HeadingMagneticNorth);
        }

        public override float getMinimumValue()
        {
            return cp.MinimumReportInterval;
        }

        public override float getMaximumValue()
        {
            return cp.ReportInterval;
        }

        ~SCompass ()
        {
            cp.ReadingChanged -= cp_ReadingChanged;
        }
        private Compass cp;
    }
}
