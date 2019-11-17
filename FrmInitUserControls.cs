using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartDemo
{
    public partial class FrmInitUserControls : Form
    {
        public FrmInitUserControls()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            var tempDate = DateTime.Today;
            var date = Convert.ToDateTime(tempDate.ToString("yyyy-MM-dd"));


            var date1 = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day);
        }

        public static void SetDouble(Control cc)
        {
            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);

        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
                return;
            base.WndProc(ref m);
        }

        delegate void DAddUCSentDrugInfo(int x, int y, PrescriptionRecords p);
        void AddUCSentDrugInfo(int x, int y, PrescriptionRecords t)
        {
            var uc = new UCSentDrugInfo();
            uc.Height = 94;
            uc.Location = new System.Drawing.Point(x, y);
            uc.Init(t);
            uc.InitEvents();
            this.pnlUCContainer.Controls.Add(uc);
        }

        /// <summary>
        /// 造数据
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private List<PrescriptionRecords> GetPrescriptionRecords(int count)
        {
            List<PrescriptionRecords> prescriptions = new List<PrescriptionRecords>();
            ChineseName chineseName = new ChineseName();
            for (var i = 0; i < count; i++)
            {
                Random ran = new Random();
                var isBill = ran.Next(0, 2);
                var isPay = ran.Next(0, 2);
                var prescriptionType = ran.Next(0, 2);
                var prescriptionNumber = ran.Next(0, 1000).ToString();
                var item = new PrescriptionRecords()
                {
                    Id = i,
                    PatientName = chineseName.RandomChineseName(),
                    DispensingDate = DateTime.Today,
                    DispensingUserDataName = "",
                    Doctor = chineseName.RandomChineseName(),
                    IsBill = isBill,
                    IsPay = isPay,
                    PrescriptionNumber = prescriptionNumber,
                    PrescriptionType = prescriptionType,
                    VisitTime = DateTime.Now
                };
                prescriptions.Add(item);
            }
            return prescriptions;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 开启秒表
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            this.Cursor = Cursors.WaitCursor;

            for (var i = pnlUCContainer.Controls.Count - 1; i >= 0; i--)
            {
                pnlUCContainer.Controls[i].Visible = false;
                pnlUCContainer.Controls.RemoveAt(i);
            }
            // 造数据 100条
            List<PrescriptionRecords> source = GetPrescriptionRecords(100);

            var x = 2;
            var y = 2;
            // 循环数据集加载自定义组件
            for (var i = 0; i < source.Count; i++)
            {
                var t = source[i];


                AddUCSentDrugInfo(x, y, t);

                // 如果用下面这句话去加载的话，最后的读秒不准
                //this.pnlUCContainer.BeginInvoke(new DAddUCSentDrugInfo(AddUCSentDrugInfo), x, y, c, t);
                y += 104;
            }

            this.Cursor = Cursors.Default;
            // 显示耗时时间
            MessageBox.Show("加载100条数据 耗时：" + stopWatch.ElapsedMilliseconds + "毫秒");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage2;
            // 开启秒表
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            this.Cursor = Cursors.WaitCursor;

            for (var i = pnlUCContainer.Controls.Count - 1; i >= 0; i--)
            {
                pnlUCContainer.Controls[i].Visible = false;
                pnlUCContainer.Controls.RemoveAt(i);
            }
            // 造数据 100条
            List<PrescriptionRecords> source = GetPrescriptionRecords(100);

            var x = 2;
            var y = 2;
            // 循环数据集加载自定义组件
            for (var i = 0; i < source.Count; i++)
            {
                var t = source[i];

                UCSentDrugItem ucControlBase1 = new UCSentDrugItem
                {
                    ExContentActiveTime = "2019-11-16 12:00",
                    ExContentAge = "60",
                    ExContentCardNo = "123456789",
                    ExContentDiagnose = "二型糖尿病",
                    ExContentGender = "男",
                    ExContentPatName = "测试患者",
                    ExContentPresNo = "1234567890", //
                    ExShowBorder = true, // 是否显示边框
                    ExBorderWidth = 1, // 边框粗细  
                    ExBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220))))),// 边框颜色
                    ExBackColor = System.Drawing.Color.Transparent,// 背景色
                    ExIsMouseHoverFocused = true, // 开启鼠标移动进入即获取焦点
                    ExIsFocusedBackColor = true,  // 开启获取焦点背景色
                    ExFocusedBackColor = System.Drawing.Color.DeepSkyBlue, // 获取焦点背景色
                    ExIsRadius = true, // 是否圆角
                    ExConerRadius = 24,  // 圆角弧度
                    Font = this.Font,
                    Location = new System.Drawing.Point(x, y),
                    Name = "ucControlBase" + i.ToString(),
                    Size = new System.Drawing.Size(310, 88),
                    TabIndex = i
                };

                this.panel1.Controls.Add(ucControlBase1);
                // 如果用下面这句话去加载的话，最后的读秒不准
                //this.pnlUCContainer.BeginInvoke(new DAddUCSentDrugInfo(AddUCSentDrugInfo), x, y, c, t);
                y += 90;
            }


            SetDouble(this.panel1);
            this.Cursor = Cursors.Default;
            // 显示耗时时间
            MessageBox.Show("加载100条数据 耗时：" + stopWatch.ElapsedMilliseconds + "毫秒");
        }
    }

    public class PrescriptionRecords
    {
        internal string PatientName { get; set; }
        internal string PrescriptionNumber { get; set; }
        internal string DispensingUserDataName { get; set; }
        internal string Doctor { get; set; }
        internal int IsPay { get; set; }
        internal int IsBill { get; set; }
        internal object DispensingDate { get; set; }
        internal int PrescriptionType { get; set; }
        public DateTime VisitTime { get; set; }
        internal int Id { get; set; }
    }
}
