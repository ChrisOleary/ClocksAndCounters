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

        private Stopwatch stopWatch = new Stopwatch();
        private string currentTime = string.Empty;
  
        private string counter;

        public string Counter
        {
            get { return counter; }
            set 
            { 
                counter = value;
                OnPropertyChanged("Counter");
            }
        }

        public int ClockArm
        {
            get { return (int)GetValue(ClockArmProperty); }
            set { SetValue(ClockArmProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClockArm.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClockArmProperty =
            DependencyProperty.Register("ClockArm", typeof(int), typeof(MainViewModel), new PropertyMetadata(0));



        public MainViewModel()
        {
            // start timer
            DispatcherTimer GameTimer = new DispatcherTimer();
            GameTimer.Interval = new TimeSpan(0, 0, 0, 0); // sets time to 0
            stopWatch.Start();
            GameTimer.Tick += GameTimer_Tick;
            GameTimer.Start();

            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal,
                 delegate
                 {
                     int newValue = 0;

                     if (ClockArm == int.MaxValue)
                     {
                         newValue = 0;
                     }
                     else
                     {
                         newValue = ClockArm + 6;
                     }
                    SetValue(ClockArmProperty, newValue);
                
                 }, Dispatcher);
            
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan timeSpan = stopWatch.Elapsed;
                Counter = String.Format($"{timeSpan.Minutes}, {timeSpan.Seconds}");
               
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            // this triggers the event property above
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
