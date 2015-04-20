using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSensors.Sensors
{
    public abstract class Sensor
    {
        protected String _name;
        protected int _ValuesDimension;
        protected ObservableCollection<double> _values;
        protected NotifyCollectionChangedEventHandler _ChangedEvent;

        public String Name
        {
            get { return _name; }
        }

        public int Dimension
        {
            get { return _ValuesDimension; }
        }

        public double getValue(int index)
        {
            return _values[index];
        }

        public NotifyCollectionChangedEventHandler ValuesChangedEventArgs
        {
            set { _ChangedEvent = value; _values.CollectionChanged += value; }
        }

        public abstract float getMinimumValue();

        public abstract float getMaximumValue();
    }
}
