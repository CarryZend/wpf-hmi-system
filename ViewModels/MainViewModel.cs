using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfHmiSystem.Models;
using WpfHmiSystem.Services;

namespace WpfHmiSystem.ViewModels
{
    /// <summary>
    /// 主窗口ViewModel类
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private DeviceCommunicationService deviceService;
        private ObservableCollection<DataPoint> realTimeData;
        private string connectionStatus;
        private string systemStatus;
        private string logText;

        public MainViewModel()
        {
            deviceService = new DeviceCommunicationService();
            realTimeData = new ObservableCollection<DataPoint>();
            connectionStatus = "未连接";
            systemStatus = "就绪";
            logText = $"[{DateTime.Now:HH:mm:ss}] 系统启动\r\n";
        }

        /// <summary>
        /// 实时数据集合
        /// </summary>
        public ObservableCollection<DataPoint> RealTimeData
        {
            get { return realTimeData; }
            set
            {
                realTimeData = value;
                OnPropertyChanged(nameof(RealTimeData));
            }
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        public string ConnectionStatus
        {
            get { return connectionStatus; }
            set
            {
                connectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
            }
        }

        /// <summary>
        /// 系统状态
        /// </summary>
        public string SystemStatus
        {
            get { return systemStatus; }
            set
            {
                systemStatus = value;
                OnPropertyChanged(nameof(SystemStatus));
            }
        }

        /// <summary>
        /// 日志文本
        /// </summary>
        public string LogText
        {
            get { return logText; }
            set
            {
                logText = value;
                OnPropertyChanged(nameof(LogText));
            }
        }

        /// <summary>
        /// 连接设备命令
        /// </summary>
        public void ConnectDevice()
        {
            try
            {
                bool result = deviceService.Connect();
                if (result)
                {
                    ConnectionStatus = "已连接";
                    LogText += $"[{DateTime.Now:HH:mm:ss}] 设备已连接\r\n";
                    SystemStatus = "设备已连接";
                }
                else
                {
                    LogText += $"[{DateTime.Now:HH:mm:ss}] 设备连接失败\r\n";
                }
            }
            catch (Exception ex)
            {
                LogText += $"[{DateTime.Now:HH:mm:ss}] 连接设备时发生错误: {ex.Message}\r\n";
            }
        }

        /// <summary>
        /// 断开设备连接命令
        /// </summary>
        public void DisconnectDevice()
        {
            try
            {
                bool result = deviceService.Disconnect();
                if (result)
                {
                    ConnectionStatus = "未连接";
                    LogText += $"[{DateTime.Now:HH:mm:ss}] 设备断开连接\r\n";
                    SystemStatus = "设备已断开";
                }
            }
            catch (Exception ex)
            {
                LogText += $"[{DateTime.Now:HH:mm:ss}] 断开连接时发生错误: {ex.Message}\r\n";
            }
        }

        /// <summary>
        /// 启动系统命令
        /// </summary>
        public void StartSystem()
        {
            if (deviceService.IsConnected)
            {
                LogText += $"[{DateTime.Now:HH:mm:ss}] 系统启动\r\n";
                SystemStatus = "系统运行中";
                // 启动数据采集等操作
            }
            else
            {
                LogText += $"[{DateTime.Now:HH:mm:ss}] 请先连接设备\r\n";
            }
        }

        /// <summary>
        /// 停止系统命令
        /// </summary>
        public void StopSystem()
        {
            LogText += $"[{DateTime.Now:HH:mm:ss}] 系统停止\r\n";
            SystemStatus = "系统已停止";
            // 停止数据采集等操作
        }

        /// <summary>
        /// 系统复位命令
        /// </summary>
        public void ResetSystem()
        {
            LogText += $"[{DateTime.Now:HH:mm:ss}] 系统复位\r\n";
            SystemStatus = "系统已复位";
            // 执行系统复位操作
        }

        /// <summary>
        /// 添加数据点到实时数据显示
        /// </summary>
        /// <param name="dataPoint">数据点</param>
        public void AddDataPoint(DataPoint dataPoint)
        {
            RealTimeData.Add(dataPoint);
            // 限制显示的数据点数量，只保留最新的100条记录
            if (RealTimeData.Count > 100)
            {
                RealTimeData.RemoveAt(0);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}