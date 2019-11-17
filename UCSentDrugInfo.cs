using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ChartDemo
{
    public partial class UCSentDrugInfo : UserControl
    {
        //public delegate void SetBackColor(Control parentControl, UserControl userControl);
        //public event SetBackColor SetFocusedColor;

        public UCSentDrugInfo(PrescriptionRecords p = null)
        {
            InitializeComponent();
            if (p != null)
            {
                DisplayerControl(p);
            }
            //SetWindowRegion();
        }

        public void InitEvents()
        {
            this.toolStrip1.MouseEnter += toolStrip3_MouseEnter;
            this.toolStrip2.MouseEnter += toolStrip3_MouseEnter;
            this.toolStrip3.MouseEnter += toolStrip3_MouseEnter;
            this.statusStrip1.MouseEnter += toolStrip3_MouseEnter;
            this.toolStrip1.MouseLeave += ToolStrip2_MouseLeave;
            this.toolStrip2.MouseLeave += ToolStrip2_MouseLeave;
            this.toolStrip3.MouseLeave += ToolStrip2_MouseLeave;
            this.statusStrip1.MouseLeave += ToolStrip2_MouseLeave;
            //this.Click += UCSentDrugInfo_Click;
            //this.lblSentName.Click += new System.EventHandler(this.LblName_Click);
            //this.lblSentStatus.Click += new System.EventHandler(this.LblName_Click);
            //this.lblSentName.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);
            //this.lblSentStatus.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.lblPrescriptionNo.Click += new System.EventHandler(this.LblName_Click);
            //this.lblPrescriptionNo.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.lblName.Click += new System.EventHandler(this.LblName_Click);
            //this.lblName.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.lblSenStatus.Click += new System.EventHandler(this.LblName_Click);
            //this.lblSenStatus.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.label1.Click += new System.EventHandler(this.LblName_Click);
            //this.label1.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.label2.Click += new System.EventHandler(this.LblName_Click);
            //this.label2.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.label3.Click += new System.EventHandler(this.LblName_Click);
            //this.label3.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.label5.Click += new System.EventHandler(this.LblName_Click);
            //this.label5.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.label6.Click += new System.EventHandler(this.LblName_Click);
            //this.label6.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.label4.Click += new System.EventHandler(this.LblName_Click);
            //this.label4.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.label7.Click += new System.EventHandler(this.LblName_Click);
            //this.label7.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);

            //this.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);
            //this.MouseLeave += new System.EventHandler(this.UCSentDrugInfo_MouseLeave);

            //this.lblPayCostStatus.Click += new System.EventHandler(this.LblName_Click);
            //this.lblPayCostStatus.MouseEnter += new System.EventHandler(this.Label5_MouseEnter);
        }

        private void ToolStrip2_MouseLeave(object sender, EventArgs e)
        {
            this.panelEx1.BackColor = Color.White;
        }

        private void ToolStrip1_LostFocus(object sender, EventArgs e)
        {
        }

        private void toolStrip3_MouseEnter(object sender, EventArgs e)
        {
            this.panelEx1.BackColor = Color.FromArgb(197, 215, 236);
        }

        internal void Init(PrescriptionRecords prescriptionRecords)
        {
            this.Tag = prescriptionRecords;
            DisplayerControl(prescriptionRecords);
            //if (this.InvokeRequired)
            //{
            //this.BeginInvoke(new DInit(DisplayerControl), prescriptionRecords);
            //}
        }

        private void DisplayerControl(PrescriptionRecords prescriptionRecords)
        {
            lblName.Text = prescriptionRecords.PatientName;
            lblPrescriptionNo.Text = prescriptionRecords.PrescriptionNumber;
            lblSentName.Text = prescriptionRecords.DispensingUserDataName;
            lblSentStatus.Text = prescriptionRecords.Doctor;
            lblPayCostStatus.Text = prescriptionRecords.IsPay == 0 ? "没付款" : "已付款";
            lblSenStatus.Text = prescriptionRecords.IsBill == 0 ? "没发药" : prescriptionRecords.IsBill == 1 ? "已发药" : prescriptionRecords.IsBill == 2 ? "发药中" : "暂时挂单";
            label7.Text = prescriptionRecords.IsBill == 1 ? "发药时间：" : "就诊时间：";
            label4.Text = prescriptionRecords.IsBill == 1 ? prescriptionRecords.DispensingDate.ToString() : prescriptionRecords.VisitTime.ToString();
            if (prescriptionRecords.PrescriptionType == 1)
            {
                label6.Text = "西医";
            }
            else
            {
                label6.Text = "中医";
            }
        }


        delegate void DInit(PrescriptionRecords prescriptionRecords);

        private void UCSentDrugInfo_Click(object sender, EventArgs e)
        {
            LblName_Click(null, null);


        }

        private void LblName_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(197, 215, 236);
        }
        //李戬
        public void SetWindowRegion()//form圆角
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 0);
            this.Region = new Region(FormPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">窗体大小</param>
        /// <param name="radius">圆角大小</param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)//form圆角
        {
            int diameter = 20;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);//左上角

            arcRect.X = rect.Right - diameter;//右上角
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - diameter;// 右下角
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;// 左下角
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }


        public void UCSentDrugInfo_MouseLeave(object sender, System.EventArgs e)
        {
            //if (this.BackColor == Color.FromArgb(197, 215, 236))
            //{
            //    this.BackColor = Color.White;
            //}
        }

        public void Label5_MouseEnter(object sender, System.EventArgs e)
        {
            //if (this.BackColor == Color.White)
            //{
            //    this.BackColor = Color.FromArgb(197, 215, 236);
            //}
            //foreach (UserControl ctr in this.Parent.Controls)
            //{
            //    if (ctr == this)
            //    {
            //        continue;
            //    }
            //    if (ctr.BackColor == Color.FromArgb(197, 215, 236))
            //    {
            //        ctr.BackColor = Color.White;
            //    }
            //}
        }

    }

}
