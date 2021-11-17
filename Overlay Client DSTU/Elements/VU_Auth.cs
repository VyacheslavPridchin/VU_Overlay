using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    class VU_Auth : Panel
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

        private bool shadowEnable = true;
        [DefaultValue(true)]
        [Description("Вкл / выкл тени")]
        [Category("Тень")]
        public bool ShadowEnable
        {
            get => shadowEnable;
            set
            {
                shadowEnable = value;
                Refresh();
            }
        }

        private int intensity = 64;
        [DisplayName("Intensity")]
        [DefaultValue(64)]
        [Description("Интенсивность тени")]
        [Category("Тень")]
        public int Intensity
        {
            get => intensity;
            set
            {
                if (value <= 0)
                    intensity = 1;
                else if (value > 255) intensity = 255;
                else intensity = value;
                Refresh();
            }
        }

        private int countIter = 8;
        [DisplayName("Count of Iteration")]
        [DefaultValue(8)]
        [Description("Количество итераций прорисовки")]
        [Category("Тень")]
        public int CountIter
        {
            get => countIter;
            set
            {
                if (value <= 0)
                    countIter = 1;
                else if (value > 50) intensity = 50;
                else countIter = value;
                Refresh();
            }
        }

        private int shadowOX = 0;
        [DisplayName("Shadow OX")]
        [DefaultValue(0)]
        [Description("Сдвиг тени вдоль оси X")]
        [Category("Тень")]
        public int ShadowOX
        {
            get => shadowOX;
            set
            {
                shadowOX = value;
                Refresh();
            }
        }

        private Color shadowColor = Color.Black;
        [DisplayName("Shadow Color")]
        [Description("Цвет тени")]
        [Category("Тень")]
        public Color ShadowColor
        {
            get => shadowColor;
            set
            {
                shadowColor = value;
                Refresh();
            }
        }

        private int shadowOY = 8;
        [DisplayName("Shadow OY")]
        [DefaultValue(8)]
        [Description("Сдвиг тени вдоль оси Y")]
        [Category("Тень")]
        public int ShadowOY
        {
            get => shadowOY;
            set
            {
                shadowOY = value;
                Refresh();
            }
        }

        private string nameService = "Service";
        [DefaultValue(true)]
        [Description("Название подключаемого сервиса")]
        [Category("Инфа о сервисе")]
        public string NameService
        {
            get => nameService;
            set
            {
                nameService = value;
                Refresh();
            }
        }

        private string deskService = "Подключение стороннего API";
        [DefaultValue(true)]
        [Description("Описание подключаемого сервиса")]
        [Category("Инфа о сервисе")]
        public string DescService
        {
            get => deskService;
            set
            {
                deskService = value;
                Refresh();
            }
        }

        [Description("Призрак для логина")]
        [Category("Настройки призраков и др.")]
        public string GhostLogin
        {
            get => login.GhostText;
            set
            {
                login.GhostText = value;
                Refresh();
            }
        }
        [Description("Призрак для пароля")]
        [Category("Настройки призраков и др.")]
        public string GhostPassword
        {
            get => password.GhostText;
            set
            {
                password.GhostText = value;
                Refresh();
            }
        }

        [DefaultValue('◌')]
        [Description("Символ сокрытия для пароля")]
        [Category("Настройки призраков и др.")]
        public char PassChar
        {
            get => password.PasswordChar;
            set
            {
                password.PasswordChar = value;
                Refresh();
            }
        }

        [Description("Призрак для капчи")]
        [Category("Настройки призраков и др.")]
        public string GhostCaptcha
        {
            get => captcha.GhostText;
            set
            {
                captcha.GhostText = value;
                Refresh();
            }
        }

        #endregion

        #region ---Переменные---
        private StringFormat SF = new StringFormat();
        private StringFormat SF1 = new StringFormat();
        private bool OnlyOnce = false;
        #endregion

        public VU_TextBox login = new VU_TextBox();
        public VU_TextBox password = new VU_TextBox();
        public VU_TextBox captcha = new VU_TextBox();
        public PictureBox captchaBox = new PictureBox();
        public VU_Button enter = new VU_Button();

        public VU_Auth()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(340, 324);
            login.Location = new Point(10, 113);
            password.Location = new Point(10, 152);
            captcha.Location = new Point(10, 191);
            captchaBox.Location = new Point(10, 191 + captcha.Height + 10);
            login.Width = Width - 20;
            password.Width = Width - 20;
            captcha.Width = Width - 20;
            enter.Location = new Point((Width - enter.Width) / 2, Height - enter.Height - enter.ShadowOY);
            captchaBox.SizeMode = PictureBoxSizeMode.Zoom;
            captcha.Visible = false;
            captchaBox.Visible = false;
            Controls.Add(login);
            Controls.Add(password);
            Controls.Add(captcha);
            Controls.Add(enter);
            Controls.Add(captchaBox);
            captchaBox.Size = new Size(Width - 20, Height - enter.Height - enter.ShadowOY - (191 + captcha.Height) - 20);
            PassChar = '◌';
            enter.Text = "Войти";
            BackColor = Color.FromArgb(58, 58, 58);
            ForeColor = Color.FromArgb(178, 183, 195);
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
            SF1.Alignment = StringAlignment.Near;
            SF1.LineAlignment = StringAlignment.Center;
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            Graphics parentGraph = Parent.CreateGraphics();
            graph.Clear(Parent.BackColor);

            graph.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            login.Width = Width - 20;
            password.Width = Width - 20;
            captcha.Width = Width - 20;
            enter.Location = new Point((Width - enter.Width) / 2, Height - enter.Height - enter.ShadowOY);
            captchaBox.Size = new Size(Width - 20, Height - enter.Height - enter.ShadowOY - (191 + captcha.Height) - 20);

            float roundingValue = 0.1f;
            if (RoundingEnable && rounding > 0)
            {
                if (Height < Width)
                    roundingValue = Height;
                else roundingValue = Width;
                if (roundingValue > rounding)
                    roundingValue = rounding;
            }

            if (!OnlyOnce) //Переприсваиваем текст полей 1 раз, чтобы появились призраки
            {
                login.Text = GhostLogin;
                captcha.Text = GhostCaptcha;
                password.Text = GhostPassword;
            }

            //Очищаем место под тень
            Rectangle rectFP = new Rectangle(Left, Top, Width - 1 + ShadowOX, Height - 1 + ShadowOY);
            parentGraph.FillRectangle(new SolidBrush(Parent.BackColor), rectFP);

            //Тень
            for (int i = 0; i < countIter; i++)
            {
                //Рисование на родителе

                Rectangle rectForParent = new Rectangle(Left + i * (int)((float)shadowOX / (float)countIter), Top + i * (int)((float)shadowOY / (float)countIter), Width - 1, Height - 1);
                GraphicsPath rectPathForParent = Painter.RoundedRectangle(rectForParent, roundingValue);
                parentGraph.FillPath(new SolidBrush(Color.FromArgb((int)((float)intensity / (float)countIter), shadowColor)), rectPathForParent);

                //Рисование тени на кнопке
                Rectangle rectOffset = new Rectangle(i * (int)((float)shadowOX / (float)countIter), i * (int)((float)shadowOY / (float)countIter), Width - 1, Height - 1);
                GraphicsPath rectPathOffset = Painter.RoundedRectangle(rectOffset, roundingValue);
                graph.FillPath(new SolidBrush(Color.FromArgb((int)((float)intensity / (float)countIter), shadowColor)), rectPathOffset);
            }

            //рисование самой панели
            GraphicsPath rectPath = Painter.RoundedRectangle(rect, roundingValue);
            graph.FillPath(new SolidBrush(BackColor), rectPath);
            graph.DrawString(nameService, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Thin, Font, 40), new SolidBrush(ForeColor), new Rectangle(0, 0, Width - 1, 72), SF1);
            graph.DrawString(deskService, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Light, Font, 12), new SolidBrush(ForeColor), new Rectangle(7, 65, Width - 14, 41), SF1);

            OnlyOnce = true;

        }
    }
}
