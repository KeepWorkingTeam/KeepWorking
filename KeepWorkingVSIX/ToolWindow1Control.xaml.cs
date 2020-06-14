using System;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Threading;
using BusinessLogic;


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
        
        public ToolWindow1Control()
        {
            this.InitializeComponent();

            TimerManager tm = new TimerManager();
            tm.CreateTimer("Timer1");
            tm.CreateTimer("Timer2");
            tm.CreateTimer("Timer3");

            TimerList.ItemsSource = tm.GetAllTimers();
          
            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //DateTime.Now.ToString();


            //TimerList.Items.Add(new Button());
        }

        private void newTimer (object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Создание таймера");
        }

        private void deleteTimer(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Удаление таймера");
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
    }
}