using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChartDemo
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var students = GetStudents();

            var min = students.Min(a => a.Age);
            var max = students.Max(a => a.Age) + 2;
            chart1.ChartAreas[0].AxisY.Maximum = max;
            chart1.ChartAreas[0].AxisY.Minimum = min;
            chart1.ChartAreas[0].AxisX.Title = "学生姓名";
            chart1.ChartAreas[0].AxisY.Title = "年龄";
            chart1.ChartAreas[0].AxisX.Interval = 1; // 显示刻度
            chart1.ChartAreas[0].AxisY.Interval = 1; // 显示刻度

            //设置网格线   
            //chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Blue;
            //chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 2;//网格间隔
            //chart1.ChartAreas[0].AxisX.MinorGrid.Interval = 2;
            //chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Blue;
            //chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 2;
            //chart1.ChartAreas[0].AxisY.MinorGrid.Interval = 2;
            //设置网格线 颜色与背景色一样的，则可以达到无网格线效果
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            chart1.Series[0].IsValueShownAsLabel = true;// 显示数值

            // 设置 X.Y抽对应的属性fieldname,直接复制 Datasource;
            chart1.Series[0].XValueMember = "Name";
            chart1.Series[0].YValueMembers = "Age";
            chart1.DataSource = students; // 也可以用 datatable

            var dtStudent = GetStudentDt();
            chart1.DataSource = dtStudent; // 也可以用 datatable
        }

        /// <summary>
        /// 假造一些数据-------用DataTable集合返回
        /// </summary>
        /// <returns></returns>
        private DataTable GetStudentDt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Age", typeof(int)));
            dt.Columns.Add(new DataColumn("Sex", typeof(string)));

            Random ran = new Random();
            for (var i = 0; i < 20; i++)
            {
                dt.Rows.Add("张三" + i, ran.Next(6, 18), i % 2 == 0 ? "女" : "男");
            }
            return dt;
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

}
