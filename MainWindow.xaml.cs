using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using WpfHmiSystem.ViewModels;

namespace WpfHmiSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSystem();
        }

        private void InitializeSystem()
        {
            // 初始化ViewModel
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
            
            // 初始化系统组件
            statusBarText.Text = "系统初始化完成";
            
            // 绑定事件处理程序
            btnConnect.Click += BtnConnect_Click;
            btnDisconnect.Click += BtnDisconnect_Click;
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            btnReset.Click += BtnReset_Click;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            // 连接设备逻辑
            viewModel.ConnectDevice();
            tbConnectionStatus.Text = viewModel.ConnectionStatus;
            tbConnectionStatus.Foreground = viewModel.ConnectionStatus == "已连接" ? Brushes.Green : Brushes.Red;
            tbLog.Text = viewModel.LogText;
            statusBarText.Text = viewModel.SystemStatus;
        }

        private void BtnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            // 断开连接逻辑
            viewModel.DisconnectDevice();
            tbConnectionStatus.Text = viewModel.ConnectionStatus;
            tbConnectionStatus.Foreground = viewModel.ConnectionStatus == "已连接" ? Brushes.Green : Brushes.Red;
            tbLog.Text = viewModel.LogText;
            statusBarText.Text = viewModel.SystemStatus;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            // 启动系统逻辑
            viewModel.StartSystem();
            tbLog.Text = viewModel.LogText;
            statusBarText.Text = viewModel.SystemStatus;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            // 停止系统逻辑
            viewModel.StopSystem();
            tbLog.Text = viewModel.LogText;
            statusBarText.Text = viewModel.SystemStatus;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            // 系统复位逻辑
            viewModel.ResetSystem();
            tbLog.Text = viewModel.LogText;
            statusBarText.Text = viewModel.SystemStatus;
        }
    }
}