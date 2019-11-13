using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartDemo
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var students = GetStudents();

            chart1.Series.Clear();

            ChartHelper.AddSeries(chart1, "柱状图", SeriesChartType.Column, Color.Lime, Color.Red, true);
            ChartHelper.SetTitle(chart1, "三班学生年龄柱状图", new Font("微软雅黑", 12), Docking.Bottom, Color.DarkBlue);
            ChartHelper.SetStyle(chart1, Color.Transparent, Color.White);
            ChartHelper.SetLegend(chart1, Docking.Top, StringAlignment.Center, Color.Transparent, Color.DarkBlue);

            var min = students.Min(a => a.Age);
            var max = students.Max(a => a.Age) + 2;
            ChartHelper.SetXY(chart1, "序号", "数值", StringAlignment.Far, Color.DarkBlue, Color.Black, AxisArrowStyle.SharpTriangle, 1, 2, min, max);
            ChartHelper.SetMajorGrid(chart1, Color.Gray, 20, 2);

            chart1.Series[0].XValueMember = "Name";
            chart1.Series[0].YValueMembers = "Age";
            chart1.DataSource = students;
        }

        /// <summary>
        /// 假造一些数据-------用List集合返回
        /// </summary>
        /// <returns></returns>
        private List<Student> GetStudents()
        {
            var students = new List<Student>();
            Random ran = new Random();
            for (var i = 0; i < 20; i++)
            {
                students.Add(new Student()
                {
                    Name = "张三" + i,
                    Age = ran.Next(6, 18),
                    Sex = i % 2 == 0 ? "女" : "男"
                });
            }
            return students;
        }
    }

    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
    }
}
