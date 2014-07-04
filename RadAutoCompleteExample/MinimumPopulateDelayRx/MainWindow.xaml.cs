using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace MinimumPopulateDelay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private bool isHandled = true;
        private bool isDeleting;

        public MainWindow()
        {
            InitializeComponent();
            this.timer.Interval = TimeSpan.FromSeconds((this.DataContext as ViewModel).SelectedDelay);
        }


        private void SetStatusBusyIndicator(bool isBusy)
        {
            if (this.StatusRadBusyIndicator != null)
            {
                this.StatusRadBusyIndicator.IsBusy = isBusy;
            }
        }

        private void OnDelaysComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RefreshDelayTimer(int.Parse((sender as RadComboBox).SelectedItem.ToString()));
        }

        private void RefreshDelayTimer(int delay)
        {
            this.timer.Interval = TimeSpan.FromSeconds(delay);
        }
    }
}
