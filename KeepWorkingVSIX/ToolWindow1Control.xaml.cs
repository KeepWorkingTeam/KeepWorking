using System;
using System.Linq;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using BusinessLogic;
using Microsoft.VisualStudio.Shell.Interop;
using Timer = TimerManagement.Timer;


namespace KeepWorkingVSIX
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ToolWindow1Control.
    /// </summary>
    public partial class ToolWindow1Control : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow1Control"/> class.
        /// </summary>
        private TimerManager tm;
        public ToolWindow1Control()
        {
            this.InitializeComponent();

            tm = new TimerManager();

            foreach (var t in tm.GetAllTimers())
            {
                t.Start();
            }

            TimerList.ItemsSource = tm.GetAllTimers();


            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //DateTime.Now.ToString();

            //MessageBox.Show(tm.GetAllTimers()[0].TimeElapsed.ToString());

            TimerList.ItemsSource = null;
            TimerList.ItemsSource = tm.GetAllTimers();



            //TimerList.Items.Add(new Button());
        }



        private void newTimer(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Создание таймера");

            tm.CreateTimer(NameEntering.Text);
            //tm.GetTimersByName(NameEntering.Text).Last().Start();
            NameEntering.Text = "";

        }

        private void timerButton(object sender, RoutedEventArgs e)
        {
            var t = tm.GetTimersByID((Int32)((sender as Button).Tag)).First();
            if (!t.IsStarted) t.Start();
            else t.Stop();
        }

        private void deleteTimer(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Удаление таймера");

            tm.DeleteTimer(tm.GetTimersByName(NameEntering.Text).FirstOrDefault());
            NameEntering.Text = "";
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "KeepWorking");
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromRgb(35,35,35));
        }
    }
}