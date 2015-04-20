using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using PhoneSensors.Sensors;
// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=391641

namespace PhoneSensors
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InitializeTheRest();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void InitializeTheRest()
        {
            recs = new Rectangle[3];
            recs[0] = r1;
            recs[1] = r2;
            recs[2] = r3;
            comboSelector.SelectionChanged += Selector_SelectionChanged;
            sensor = SAccelerometer.getInstance();
            if (sensor != null)
            {
                sensor.ValuesChangedEventArgs = NotifyCollectionChangedEventHandler;
            }
            else
            {
                comboSelector.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Se invoca cuando esta página se va a mostrar en un objeto Frame.
        /// </summary>
        /// <param name="e">Datos de evento que describen cómo se llegó a esta página.
        /// Este parámetro se usa normalmente para configurar la página.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Preparar la página que se va a mostrar aquí.

            // TODO: Si la aplicación contiene varias páginas, asegúrese de
            // controlar el botón para retroceder del hardware registrándose en el
            // evento Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Si usa NavigationHelper, que se proporciona en algunas plantillas,
            // el evento se controla automáticamente.
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0: sensor = SAccelerometer.getInstance();
                    break;
                case 1: sensor = SInclinometer.getInstance();
                    break;
                case 2: sensor = SGyrometer.getInstance();
                    break;
                case 3: sensor = SCompass.getInstance();
                    break;
                case 4: sensor = SLight.getInstance();
                    break;
            }
            if (sensor != null)
            {
                sensor.ValuesChangedEventArgs = NotifyCollectionChangedEventHandler;
            }
        }
        public void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged();
        }

        public async void CollectionChanged()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (sensor != null)
                {
                    for (int i = 0; i < sensor.Dimension; i++)
                    {
                        double w = gridParent.ActualWidth;
                        double min = sensor.getMinimumValue();
                        double max = sensor.getMaximumValue();
                        double value = sensor.getValue(i);
                        Rectangle rc = recs[i];
                        rc.Width = (Math.Abs(value) / (max - min)) * w;
                    }
                }
            });
        }
        private Sensor sensor;
        private Rectangle[] recs;
    }
}
