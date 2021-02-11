using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace ClocksAndCounters.ViewModels
{
    public class MainViewModel : DependencyObject, INotifyPropertyChanged
    {
        private int sliderValue;

        public int SliderValue
        {
            get { return sliderValue; }
            set 
            {
                if (value > 0 && value < 170)
                {
                    sliderValue = value;
                    Angle = value - 85;
                    OnPropertyChanged("Angle");
                    OnPropertyChanged("SliderValue");
                }
            }
        }


        private int angle;

        public int Angle
        {
            get { return angle; }
            private set 
            {
                angle = value;
                OnPropertyChanged("Angle");
            }
        }


        private int timeAngle;

        public int TimeAngle
        {
            get { return timeAngle; }
            set 
            { 
                timeAngle = value;
                OnPropertyChanged("TimeAngle");
            }
        }

      
        public int Counter
        {
            get { return (int)GetValue(CounterProperty); }
            set { SetValue(CounterProperty, value); }
        }

        public MainViewModel()
        {
            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal,
                 delegate
                 {
                     int newValue = 0;

                     if (Counter == int.MaxValue)
                     {
                         newValue = 0;
                     }
                     else
                     {
                         newValue = Counter + 1;
                     }
                     SetValue(CounterProperty, newValue);
                 }, Dispatcher);
        }

    

        // Using a DependencyProperty as the backing store for Counter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CounterProperty =
            DependencyProperty.Register("Counter", typeof(int), typeof(MainViewModel), new PropertyMetadata(0));



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            // this triggers the event property above
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        
    }
}
