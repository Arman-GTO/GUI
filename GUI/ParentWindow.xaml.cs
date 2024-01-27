using System.Diagnostics;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ParentWindow.xaml
    /// </summary>
    public partial class ParentWindow : Window
    {
        #region Properties
        public int ThreadNum { get; set; } = 0;
        List<(ProcessThread thread, string name, string color)> ThreadList = [];
        List<(Process p, string c)> ProcessList = [];
        #endregion

        public ParentWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadNum++;
            ThreadNumTextBox.Text = $"total threads: {ThreadNum}";
            foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
                if (thread.ThreadState.ToString() == Thread.CurrentThread.ThreadState.ToString())
                {
                    ThreadList.Add((thread, "Parent Thread", "b"));
                    var th = ThreadList[^1];
                    ThreadsPanel.Children.Add(new ThreadWindow(th.thread.Id.ToString(), th.name, th.color));
                    break;
                }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var p in ProcessList)
            {
                if (p.c == "g")
                {
                    var q = Process.GetProcessesByName("Snake");
                    foreach (var q2 in q)
                        q2.Kill();
                }
                p.p.Kill();
            }
        }

        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            foreach (var p in ProcessList)
                if (p.p.HasExited)
                {
                    Exited(p.c);
                    ProcessList.Remove(p);
                    break;
                }
        }

        public void Exited(string color)
        {
            ThreadList.Remove(ThreadList.Find(x => x.color == color));
            ThreadsPanel.Children.Clear();
            foreach (var th in ThreadList)
                ThreadsPanel.Children.Add(new ThreadWindow(th.name, th.thread.Id.ToString(), th.color));
            ThreadNum--;
            ThreadNumTextBox.Text = $"total threads: {ThreadNum}";
            switch (color)
            {
                case "g":
                    {
                        EndGame.Visibility = Visibility.Hidden;
                        EndGame.IsEnabled = false;
                        StartGame.IsEnabled = true;
                        StartGame.Visibility = Visibility.Visible;
                        break;
                    }
                case "r":
                    {
                        EndVideo.Visibility = Visibility.Hidden;
                        EndVideo.IsEnabled = false;
                        StartVideo.IsEnabled = true;
                        StartVideo.Visibility = Visibility.Visible;
                        break;
                    }
                case "black":
                    {
                        EndClock.Visibility = Visibility.Hidden;
                        EndClock.IsEnabled = false;
                        StartClock.IsEnabled = true;
                        StartClock.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            var p = Process.Start(Environment.CurrentDirectory + @"..\..\..\..\..\Game\Snake.exe");
            ProcessList.Add((p, "g"));
            foreach (ProcessThread thread in p.Threads)
                if (thread.ThreadState.ToString() == "Running")
                {
                    ThreadList.Add((thread, "Game Thread", "g"));
                    var th = ThreadList[^1];
                    ThreadsPanel.Children.Add(new ThreadWindow(th.thread.Id.ToString(), th.name, th.color));
                    break;
                }
            ThreadNum++;
            ThreadNumTextBox.Text = $"total threads: {ThreadNum}";
            StartGame.Visibility = Visibility.Hidden;
            StartGame.IsEnabled = false;
            EndGame.IsEnabled = true;
            EndGame.Visibility = Visibility.Visible;
        }

        private void EndGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (var p in ProcessList)
                if (p.c == "g")
                {
                    var q = Process.GetProcessesByName("Snake");
                    foreach (var q2 in q)
                        q2.Kill();
                    p.p.Kill();
                    ProcessList.Remove(p);
                    break;
                }
            Exited("g");
        }

        private void StartVideo_Click(object sender, RoutedEventArgs e)
        {
            var p = Process.Start(Environment.CurrentDirectory + @"..\..\..\..\..\Video\bin\Debug\net8.0-windows\Video.exe");
            ProcessList.Add((p, "r"));
            foreach (ProcessThread thread in p.Threads)
                if (thread.ThreadState.ToString() == "Running")
                {
                    ThreadList.Add((thread, "Video Thread", "r"));
                    var th = ThreadList[^1];
                    ThreadsPanel.Children.Add(new ThreadWindow(th.thread.Id.ToString(), th.name, th.color));
                    break;
                }
            ThreadNum++;
            ThreadNumTextBox.Text = $"total threads: {ThreadNum}";
            StartVideo.Visibility = Visibility.Hidden;
            StartVideo.IsEnabled = false;
            EndVideo.IsEnabled = true;
            EndVideo.Visibility = Visibility.Visible;
        }

        private void EndVideo_Click(object sender, RoutedEventArgs e)
        {
            foreach (var p in ProcessList)
                if (p.c == "r")
                {
                    p.p.Kill();
                    ProcessList.Remove(p);
                    break;
                }
            Exited("r");
        }

        private void StartClock_Click(object sender, RoutedEventArgs e)
        {
            var p = Process.Start(Environment.CurrentDirectory + @"..\..\..\..\..\Clock\bin\Debug\net8.0-windows\Clock.exe");
            ProcessList.Add((p, "black"));
            foreach (ProcessThread thread in p.Threads)
                if (thread.ThreadState.ToString() == "Running")
                {
                    ThreadList.Add((thread, "Clock Thread", "black"));
                    var th = ThreadList[^1];
                    ThreadsPanel.Children.Add(new ThreadWindow(th.thread.Id.ToString(), th.name, th.color));
                    break;
                }
            ThreadNum++;
            ThreadNumTextBox.Text = $"total threads: {ThreadNum}";
            StartClock.Visibility = Visibility.Hidden;
            StartClock.IsEnabled = false;
            EndClock.IsEnabled = true;
            EndClock.Visibility = Visibility.Visible;
        }

        private void EndClock_Click(object sender, RoutedEventArgs e)
        {
            foreach (var p in ProcessList)
                if (p.c == "black")
                {
                    p.p.Kill();
                    ProcessList.Remove(p);
                    break;
                }
            Exited("black");
        }
    }
}