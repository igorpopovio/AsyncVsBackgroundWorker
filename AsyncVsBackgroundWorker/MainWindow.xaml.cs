using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncVsBackgroundWorker
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void AsyncTaskButton_Click(object sender, RoutedEventArgs e)
        {
            IProgress<int> progress = new Progress<int>(percentCompleted =>
            {
                AsyncTaskProgressBar.Value = percentCompleted;
            });

            AsyncTaskButton.IsEnabled = false;
            await Task.Run(() =>
            {
                progress.Report(0);
                foreach (var i in Enumerable.Range(1, 4))
                {
                    Thread.Sleep(1000);
                    progress.Report(i * 25);
                }
            });
            AsyncTaskButton.IsEnabled = true;
        }
    }
}
