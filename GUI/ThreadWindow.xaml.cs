using System.Windows.Controls;
using System.Windows.Media;

namespace GUI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ThreadWindow : UserControl
    {
        public string ThreadName { get; set; }
        public string ThreadAddress { get; set; }

        public ThreadWindow(string address, string name, string color)
        {
            InitializeComponent();

            ThreadName = name;
            ThreadAddress = address;
            NameTB.Text = ThreadName;
            NameTB.ToolTip = ThreadName;
            AddressTB.Text = ThreadAddress;
            AddressTB.ToolTip = ThreadAddress;

            switch (color)
            {
                case "r":
                    {
                        MainGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF2222"));
                        break;
                    }
                case "g":
                    {
                        MainGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00C426"));
                        break;
                    }
                case "b":
                    {
                        MainGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF167DFF"));
                        break;
                    }
                default:
                    {
                        MainGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1A1F26"));
                        break;
                    }
            }
        }
    }
}
