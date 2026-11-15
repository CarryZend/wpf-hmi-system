using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfHmiSystem.Models
{
    /// <summary>
    /// 数据点模型类
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 数据点名称
        /// </summary>
        public string DataPointName { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataPointName">数据点名称</param>
        /// <param name="value">数值</param>
        /// <param name="unit">单位</param>
        public DataPoint(string dataPointName, double value, string unit)
        {
            Time = DateTime.Now;
            DataPointName = dataPointName;
            Value = value;
            Unit = unit;
        }
    }
}