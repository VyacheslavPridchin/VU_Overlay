using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    class VU_Lesson : PictureBox
    {
        #region ---Свойства---
        private bool roundingEnable = true;
        [DefaultValue(true)]
        [Description("Вкл / выкл закругление")]
        [Category("Закругление")]
        public bool RoundingEnable
        {
            get => roundingEnable;
            set
            {
                roundingEnable = value;
                Refresh();
            }
        }

        private int rounding = 20;
        [DisplayName("Rounding")]
        [DefaultValue(20)]
        [Description("Радиус закругления")]
        [Category("Закругление")]
        public int Rounding
        {
            get => rounding;
            set
            {
                if (value >= 0)
                {
                    rounding = value;
                }
                else rounding = 0;
                Refresh();
            }
        }

        private string nameLesson = "Мы не знаем, что это за пара";
        [DefaultValue(true)]
        [Description("Название пары")]
        [Category("Инфа о паре")]
        public string NameLesson
        {
            get => nameLesson;
            set
            {
                nameLesson = value;
                Refresh();
            }
        }

        private string deskLesson = "Информация отсутствует :(";
        [DefaultValue(true)]
        [Description("Описание пары")]
        [Category("Инфа о паре")]
        public string DescLesson
        {
            get => deskLesson;
            set
            {
                deskLesson = value;
                Refresh();
            }
        }
        #endregion

        #region ---Переменные---
        private StringFormat SF = new StringFormat();
        private StringFormat SF1 = new StringFormat();
        public string idLesson = "";
        public string idArr = "";
        public string typeLesson = "";
        public string uniqueKey = "";
        public string password = "";

        #endregion

        public VU_Button button = new VU_Button();

        public VU_Lesson()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(212, 332);
            BackColor = Color.FromArgb(58, 58, 58);
            ForeColor = Color.FromArgb(178, 183, 195);
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
            SF1.Alignment = StringAlignment.Center;
            SF1.LineAlignment = StringAlignment.Near;
            Font = new Font("Arial", 18f, FontStyle.Regular, GraphicsUnit.Pixel);
            button.Text = "Присоединиться";
            button.Top = Height - button.Height - button.ShadowOY;
            button.Left = (Width - button.Width) / 2;
            button.Width = 158;
            button.Height = 43; 
            button.Font = Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto, button.Font, 18);
            button.Click += Button_Click;
            Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            SQL.Lesson.id = idLesson;
            SQL.Lesson.name = nameLesson;
            SQL.Lesson.UniqueKey = uniqueKey;
            SQL.Lesson.password = password;
            //FindForm().WindowState = FormWindowState.Minimized;
            if (password == "")
            {
                Lesson lesson = new Lesson();
                lesson.Show();
                lesson.WindowState = FormWindowState.Normal;
                FindForm().Hide();
            } else
            {
                EnterPassword entpass = new EnterPassword();
                entpass.ShowDialog();
                if(SQL.Lesson.password == "Correct")
                {
                    Lesson lesson = new Lesson();
                    lesson.Show();
                    lesson.WindowState = FormWindowState.Normal;
                    FindForm().Hide();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            float roundingValue = 0.1f;
            if (RoundingEnable && rounding > 0)
            {
                if (Height < Width)
                    roundingValue = Height;
                else roundingValue = Width;
                if (roundingValue > rounding)
                    roundingValue = rounding;
            }

            graph.Clear(Parent.BackColor);

            //рисование самой кнопки
            GraphicsPath rectPath = Painter.RoundedRectangle(rect, roundingValue);
            //graph.DrawPath(new Pen(Color.FromArgb(150,BackColor)), rectPath);
            graph.FillPath(new SolidBrush(BackColor), rectPath);
            graph.DrawString(nameLesson, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto, Font, 20), new SolidBrush(ForeColor), new Rectangle(0, rounding, Width - 1, (Height - button.Height - rounding) / 5 * 2), SF1);

            graph.DrawString(deskLesson, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Light, Font, 18), new SolidBrush(ForeColor), new Rectangle(0, rounding + (Height - button.Height - rounding) / 5 * 2, Width - 1, (Height - button.Height - rounding) / 5 * 3 - (Height - button.Top - button.Height)), SF);


        }
    }
}
