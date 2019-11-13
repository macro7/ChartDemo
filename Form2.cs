using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartDemo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            chart1.Series.Clear();
            ChartHelper.AddSeries(chart1, "柱状图", SeriesChartType.Column, Color.Lime, Color.Red, true);
            ChartHelper.AddSeries(chart1, "曲线图", SeriesChartType.Spline, Color.Red, Color.Red);
            ChartHelper.SetTitle(chart1, "柱状图与曲线图", new Font("微软雅黑", 12), Docking.Bottom, Color.White);
            ChartHelper.SetStyle(chart1, Color.Transparent, Color.White);
            ChartHelper.SetLegend(chart1, Docking.Top, StringAlignment.Center, Color.Transparent, Color.White);
            ChartHelper.SetXY(chart1, "序号", "数值", StringAlignment.Far, Color.White, Color.White, AxisArrowStyle.SharpTriangle, 1, 2, 0, 1000);
            ChartHelper.SetMajorGrid(chart1, Color.Gray, 20, 2);

            RefreshData();
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            timer1.Start();
        }

        public void RefreshData()
        {
            List<int> x1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            List<int> y1 = new List<int>();
            Random ra = new Random();
            y1 = new List<int>() {
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100),
                ra.Next(1, 100)
            };
            RefreshChart(x1, y1, "chart1");
        }

        public delegate void RefreshChartDelegate(List<int> x, List<int> y, string type);
        public void RefreshChart(List<int> x, List<int> y, string type)
        {
            if (chart1.InvokeRequired)
            {
                RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
                Invoke(stcb, new object[] { x, y, type });
            }
            else
            {
                chart1.Series[0].Points.DataBindXY(x, y);
                chart1.Series[1].Points.DataBindXY(x, y);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(RefreshData)).Start();
        }
    }
}
