using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartDemo
{
    public partial class Form1 : Form
    {
        public Form1()
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
            ChartHelper.SetXY(chart1, "序号", "数值", StringAlignment.Far, Color.White, Color.White, AxisArrowStyle.SharpTriangle, 1, 2, 1, 1000);
            ChartHelper.SetMajorGrid(chart1, Color.Gray, 20, 2);

            chart2.Series.Clear();
            ChartHelper.AddSeries(chart2, "饼状图", SeriesChartType.Pie, Color.Lime, Color.Red, true);
            ChartHelper.SetStyle(chart2, Color.Transparent, Color.White);
            ChartHelper.SetLegend(chart2, Docking.Top, StringAlignment.Center, Color.Transparent, Color.White);

            chart3.Series.Clear();
            ChartHelper.AddSeries(chart3, "曲线图", SeriesChartType.SplineRange, Color.FromArgb(100, 46, 199, 201), Color.Red, true);
            ChartHelper.SetTitle(chart3, "曲线图", new Font("微软雅黑", 12), Docking.Bottom, Color.FromArgb(46, 199, 201));
            ChartHelper.SetStyle(chart3, Color.Transparent, Color.White);
            ChartHelper.SetLegend(chart3, Docking.Top, StringAlignment.Center, Color.Transparent, Color.White);
            ChartHelper.SetXY(chart3, "序号", "数值", StringAlignment.Far, Color.White, Color.White, AxisArrowStyle.SharpTriangle, 1, 2, 0, 100);
            ChartHelper.SetMajorGrid(chart3, Color.Gray, 20, 2);

            chart4.Series.Clear();
            ChartHelper.AddSeries(chart4, "饼状图", SeriesChartType.Funnel, Color.Lime, Color.Red, true);
            ChartHelper.SetStyle(chart4, Color.Transparent, Color.White);
            ChartHelper.SetLegend(chart4, Docking.Top, StringAlignment.Center, Color.Transparent, Color.White);

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
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10)
            };
            RefreshChart(x1, y1, "chart1");
            RefreshChart(x1, y1, "chart2");
            RefreshChart(x1, y1, "chart3");
            RefreshChart(x1, y1, "chart4");
        }

        public delegate void RefreshChartDelegate(List<int> x, List<int> y, string type);
        public void RefreshChart(List<int> x, List<int> y, string type)
        {
            if (type == "chart1")
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
            else if (type == "chart2")
            {
                if (chart2.InvokeRequired)
                {
                    RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
                    Invoke(stcb, new object[] { x, y, type });
                }
                else
                {
                    chart2.Series[0].Points.DataBindXY(x, y);
                    List<Color> colors = new List<Color>() {
                        Color.Red,
                        Color.DarkRed,
                        Color.IndianRed,
                        Color.MediumVioletRed,
                        Color.OrangeRed,
                        Color.PaleVioletRed,
                        Color.Purple,
                        Color.DarkOrange,
                        Color.Maroon,
                        Color.LightCoral,
                        Color.LightPink,
                        Color.Magenta
                    };
                    DataPointCollection points = chart2.Series[0].Points;
                    for (int i = 0; i < points.Count; i++)
                    {
                        points[i].Color = colors[i];
                    }
                }
            }
            else if (type == "chart3")
            {
                if (chart3.InvokeRequired)
                {
                    RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
                    Invoke(stcb, new object[] { x, y, type });
                }
                else
                {
                    chart3.Series[0].Points.DataBindXY(x, y);
                }
            }
            else if (type == "chart4")
            {
                if (chart4.InvokeRequired)
                {
                    RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
                    Invoke(stcb, new object[] { x, y, type });
                }
                else
                {
                    chart4.Series[0].Points.DataBindXY(x, y);
                    List<Color> colors = new List<Color>() {
                        Color.Red,
                        Color.DarkRed,
                        Color.IndianRed,
                        Color.MediumVioletRed,
                        Color.OrangeRed,
                        Color.PaleVioletRed,
                        Color.Purple,
                        Color.DarkOrange,
                        Color.Maroon,
                        Color.LightCoral,
                        Color.LightPink,
                        Color.Magenta
                    };
                    DataPointCollection points = chart4.Series[0].Points;
                    for (int i = 0; i < points.Count; i++)
                    {
                        points[i].Color = colors[i];
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(RefreshData)).Start();
        }
    }
}
