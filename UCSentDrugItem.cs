using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ChartDemo
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class UCSentDrugItem : UserControl, IContainerControl
    {
        private Color _rectColor = Color.FromArgb(220, 220, 220);

        /// <summary>
        /// 是否圆角
        /// </summary>
        [Description("是否圆角"), Category("自定义")]
        public bool ExIsRadius { get; set; } = false;
        //圆角角度
        [Description("圆角角度"), Category("自定义")]
        public int ExConerRadius { get; set; } = 24;

        /// <summary>
        /// 是否显示边框
        /// </summary>
        [Description("是否显示边框"), Category("自定义")]
        public bool ExShowBorder { get; set; } = false;
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("自定义")]
        public Color ExBorderColor
        {
            get
            {
                return this._rectColor;
            }
            set
            {
                this._rectColor = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 边框宽度
        /// </summary>
        [Description("边框宽度"), Category("自定义")]
        public int ExBorderWidth { get; set; } = 1;
        /// <summary>
        /// 当使用边框时填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("当使用边框时填充颜色，当值为背景色或透明色或空值则不填充"), Category("自定义")]
        public Color ExBackColor { get; set; } = Color.Transparent;
        [Description("是否开启鼠标进入即获取焦点"), Category("自定义")]
        public bool ExIsMouseHoverFocused { get; set; } = false;
        [Description("是否开启获取焦点改变背景色"), Category("自定义")]
        public bool ExIsFocusedBackColor { get; set; } = false;
        [Description("获取焦点背景色"), Category("自定义")]
        public Color ExFocusedBackColor { get; set; } = Color.Empty;

        [Description("患者姓名"), Category("自定义")]
        public string ExContentPatName { get; set; } = "";
        [Description("性别"), Category("自定义")]
        public string ExContentGender { get; set; } = "";
        [Description("处方号"), Category("自定义")]
        public string ExContentPresNo { get; set; } = "";
        [Description("年龄"), Category("自定义")]
        public string ExContentAge { get; set; } = "";
        [Description("卡号"), Category("自定义")]
        public string ExContentCardNo { get; set; } = "";
        [Description("就诊时间"), Category("自定义")]
        public string ExContentActiveTime { get; set; } = "";
        [Description("就诊时间"), Category("自定义")]
        public string ExContentDiagnose { get; set; } = "";

        public UCSentDrugItem()
        {
            InitializeComponent();
            DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
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
        bool isMouseEnter = false;
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (ExIsMouseHoverFocused)
            {
                isMouseEnter = true;
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isMouseEnter = false;
        }
        //protected override void OnGotFocus(EventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    this.ExBackColor = this.ExFocusedBackColor;
        //}

        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    this.ExBackColor = this.ExBackColor;
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            // 使用双缓冲
            this.DoubleBuffered = true;
            if (this.Visible)
            {
                if (this.ExIsRadius)
                {
                    this.SetWindowRegion();
                }
                if (this.ExShowBorder)
                {
                    Color rectColor = this._rectColor;
                    Pen pen = new Pen(rectColor, (float)this.ExBorderWidth);
                    Rectangle clientRectangle = base.ClientRectangle;
                    GraphicsPath graphicsPath = new GraphicsPath();
                    graphicsPath.AddArc(0, 0, ExConerRadius, ExConerRadius, 180f, 90f);
                    graphicsPath.AddArc(clientRectangle.Width - ExConerRadius - 1, 0, ExConerRadius, ExConerRadius, 270f, 90f);
                    graphicsPath.AddArc(clientRectangle.Width - ExConerRadius - 1, clientRectangle.Height - ExConerRadius - 1, ExConerRadius, ExConerRadius, 0f, 90f);
                    graphicsPath.AddArc(0, clientRectangle.Height - ExConerRadius - 1, ExConerRadius, ExConerRadius, 90f, 90f);
                    graphicsPath.CloseFigure();
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    if ((ExIsFocusedBackColor && this.Focused) || this.isMouseEnter)
                    {
                        if (ExFocusedBackColor != Color.Empty && ExFocusedBackColor != Color.Transparent && ExFocusedBackColor != this.BackColor)
                            e.Graphics.FillPath(new SolidBrush(this.ExFocusedBackColor), graphicsPath);
                    }
                    else
                    {
                        if (ExBackColor != Color.Empty && ExBackColor != Color.Transparent && ExBackColor != this.BackColor)
                            e.Graphics.FillPath(new SolidBrush(this.ExBackColor), graphicsPath);
                    }
                    e.Graphics.DrawPath(pen, graphicsPath);
                }

            }
            base.OnPaint(e);
            int x = 8, y = 8;
            Graphics g = e.Graphics;
            // 姓名
            Font nameFont = new Font(this.Font.FontFamily, this.Font.Size + 4, Font.Style, Font.Unit);
            StringFormat strF = new StringFormat();
            strF.Alignment = StringAlignment.Center;
            strF.LineAlignment = StringAlignment.Center;
            Rectangle rect = new Rectangle(0, 0, 20, 20);
            // 绿色
            Brush brush = new LinearGradientBrush(rect, Color.DarkGreen, Color.DarkGreen, LinearGradientMode.Vertical);
            g.DrawString(ExContentPatName, nameFont, brush, x, y);

            SizeF vSizeF = g.MeasureString(ExContentPatName, nameFont);
            int dStrLength = Convert.ToInt32(Math.Ceiling(vSizeF.Width));
            x += dStrLength + 2;
            y += 4;

            //黑色
            brush = new LinearGradientBrush(rect, Color.Black, Color.Black, LinearGradientMode.Vertical);
            //性别
            string sGender = "性别：";
            if (!string.IsNullOrEmpty(ExContentGender))
            {
                sGender = string.Format("{0}{1}", sGender, ExContentGender);
            }
            g.DrawString(sGender, this.Font, brush, x, y);

            vSizeF = g.MeasureString(sGender, Font);
            dStrLength = Convert.ToInt32(Math.Ceiling(vSizeF.Width));
            x += dStrLength + 2;

            // 处方号
            string sPrescNo = "处方号：";
            if (!string.IsNullOrEmpty(ExContentPresNo))
            {
                sPrescNo = string.Format("{0}{1}", sPrescNo, ExContentPresNo);
            }
            g.DrawString(sPrescNo, this.Font, brush, x, y);
            x = 8;
            y += 26;

            // 年龄
            string sAge = "年龄：";
            if (!string.IsNullOrEmpty(ExContentAge))
            {
                sAge = string.Format("{0}{1}", sAge, ExContentAge);
            }
            g.DrawString(sAge, this.Font, brush, x, y);
            vSizeF = g.MeasureString(sAge, Font);
            dStrLength = Convert.ToInt32(Math.Ceiling(vSizeF.Width));
            x += dStrLength + 2;

            // 卡号
            string sCardNo = "卡号：";
            if (!string.IsNullOrEmpty(ExContentCardNo))
            {
                sCardNo = string.Format("{0}{1}", sCardNo, ExContentCardNo);
            }
            g.DrawString(sCardNo, this.Font, brush, x, y);

            vSizeF = g.MeasureString(sCardNo, Font);
            dStrLength = Convert.ToInt32(Math.Ceiling(vSizeF.Width));
            x += dStrLength + 2;

            // 就诊时间
            string sActiveTime = "就诊时间：";
            if (!string.IsNullOrEmpty(ExContentActiveTime))
            {
                sActiveTime = string.Format("{0}{1}", sActiveTime, ExContentActiveTime);
            }
            g.DrawString(sActiveTime, this.Font, brush, x, y);

            x = 8;
            y += 26;

            // 诊断
            string sDiagnose = "诊断：";
            if (!string.IsNullOrEmpty(ExContentDiagnose))
            {
                sDiagnose = string.Format("{0}{1}", sDiagnose, ExContentDiagnose);
            }
            g.DrawString(sDiagnose, this.Font, brush, x, y);
        }

        private void SetWindowRegion()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height);
            path = this.GetRoundedRectPath(rect, this.ExConerRadius);
            base.Region = new Region(path);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            Rectangle rect2 = new Rectangle(rect.Location, new Size(radius, radius));
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect2, 180f, 90f);//左上角
            rect2.X = rect.Right - radius;
            graphicsPath.AddArc(rect2, 270f, 90f);//右上角
            rect2.Y = rect.Bottom - radius;
            rect2.Width += 1;
            rect2.Height += 1;
            graphicsPath.AddArc(rect2, 360f, 90f);//右下角           
            rect2.X = rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);//左下角
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 20)
            {
                base.WndProc(ref m);
            }
        }
    }
}
