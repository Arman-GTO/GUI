using System.Windows;

namespace Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();

        public ClockWindow()
        {
            InitializeComponent();
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();
        }

        private void Timer_Click(object? sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            var deciSec = Convert.ToDecimal(d.Millisecond / 10);
            ClockLabel.Content = $"{d.Hour}:{d.Minute}:{d.Second + deciSec / 100}";
            if (deciSec == 0)
                ClockLabel.Content += ".00";
            else if ((deciSec) % 10 == 0)
                ClockLabel.Content += "0";
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }
    }
}