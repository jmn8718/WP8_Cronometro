using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Cronometro.Resources;
using System.Diagnostics;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace Cronometro
{
    public partial class MainPage : PhoneApplicationPage
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DateTime empezar;
        bool timer = false;
        DateTime pausa;

        DateTime vuelta;
        // Constructor        
        public MainPage()
        {
            InitializeComponent();

            textRelojCrono.Text = "00:00:00.000";
            
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            actualizar();
        }

        private void actualizar()
        {
            TimeSpan interval = DateTime.Now - empezar;
            string msg = interval.ToString(@"hh\:mm\:ss\.fff");
            textRelojCrono.Text = msg;            
        }

        private void botonStopCrono_Click(object sender, RoutedEventArgs e)
        {
            if (timer)
            {
                dispatcherTimer.Stop();
                botonResetCrono.IsEnabled = true;
                botonLapCrono.IsEnabled = false;
                botonStartCrono.IsEnabled = true;
                pausa = DateTime.Now;
            }
        }

        private void botonStartCrono_Click(object sender, RoutedEventArgs e)
        {
            if (!timer)
            {
                empezar = DateTime.Now;
                vuelta = empezar;
                timer = true;
            }
            else 
            {
                TimeSpan ts = DateTime.Now - pausa;
                empezar = empezar.Add(ts);
                vuelta = vuelta.Add(ts);
            }
            
            dispatcherTimer.Start();
            botonResetCrono.IsEnabled = false;
            botonLapCrono.IsEnabled = true;
            botonStartCrono.IsEnabled = false;
        }

        private void botonResetCrono_Click(object sender, RoutedEventArgs e)
        {
            timer = false;
            textRelojCrono.Text = "00:00:00.000";
            textVueltaCrono.Text = "00:00.000";
            
            botonResetCrono.IsEnabled = false;
            botonLapCrono.IsEnabled = false;
            botonStartCrono.IsEnabled = true;
        }

        private void botonLapCrono_Click(object sender, RoutedEventArgs e)
        {
            DateTime ahora = DateTime.Now;
            TimeSpan interval = ahora - vuelta;
            vuelta = ahora;
            string msg = interval.ToString(@"mm\:ss\.fff");
            textVueltaCrono.Text = msg;
        }

        
    }
}