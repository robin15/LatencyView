using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Wpf.CartesianChart.MaterialCards
{
    /// <summary>
    /// Interaction logic for MaterialCards.xaml
    /// </summary>
    public partial class MaterialCards : Window
    {
        public MaterialCards()
        {
            InitializeComponent();
            LastSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0)
                    }
                }
            };
            DataContext = this;
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
                        System.Net.NetworkInformation.PingReply reply = p.Send("yahoo.co.jp");
                        if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                        {
                            LastSeries[0].Values.Add(new ObservableValue(reply.RoundtripTime));
                            LastSeries[0].Values.RemoveAt(0);
                            rtt.Text = reply.RoundtripTime.ToString();
                        }
                    });
                }
            });
            this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
            this.MouseDoubleClick += (sender, e) => { Application.Current.Shutdown(); };
        }
        public SeriesCollection LastSeries { get; set; }
        public string LastLatency { get; set; }
    }
}
