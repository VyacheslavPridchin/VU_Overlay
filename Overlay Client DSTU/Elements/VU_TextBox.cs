using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    class VU_TextBox : Control
    {
        #region ---Свойства---
        private string ghostText = "Введите текст";
        [DefaultValue("Введите текст")]
        [Description("Текст-призрак, выполняет роль подсказки. Исчезает при нажатии")]
        public string GhostText
        {
            get => ghostText;
            set
            {
                ghostText = value;
                if (Text == "") Text = ghostText;
                Refresh();
            }
        }

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

        [DisplayName("Multiline")]
        [DefaultValue(false)]
        [Description("Назначает многострочность объекта")]
        public bool Multiline
        {
            get => textBox.Multiline;
            set
            {
                textBox.Multiline = value;
                Refresh();
            }
        }

        public override string Text
        {
            get => textBox.Text;
            set
            {
                textBox.Text = value;
                Refresh();
            }
        }

        public override Color ForeColor
        {
            get => textBox.ForeColor;
            set
            {
                textBox.ForeColor = value;
                textBox.Refresh();
            }
        }


        public override Color BackColor
        {
            get => textBox.BackColor;
            set
            {
                textBox.BackColor = value;
                textBox.Refresh();
            }
        }

        private char passwordChar;
        [DisplayName("PasswordChar")]
        [Description("Символ, который будет скрывать символы пароля")]
        public char PasswordChar
        {
            get => passwordChar;
            set
            {
                passwordChar = value;
                Refresh();
            }
        }
        #endregion

        #region ---Переменные---
        private bool MouseEntered = false;
        #endregion

        public TextBox textBox = new TextBox();

        public VU_TextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(318, 30);
            BackColor = Color.FromArgb(45, 45, 45);
            textBox.BorderStyle = BorderStyle.None;
            textBox.GotFocus += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.MouseEnter += TextBox_MouseEnter;
            textBox.MouseLeave += TextBox_MouseLeave;
            Controls.Add(textBox);

            if (Text == "")
                Text = ghostText;

            if (Text != ghostText)
                ForeColor = Color.FromArgb(178, 183, 195);
            else
                ForeColor = Color.FromArgb(92, 95, 102);

            Font = new Font("Arial", 18f, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        private void TextBox_MouseLeave(object sender, EventArgs e)
        {
            MouseEntered = false;
            Refresh(); 
            //throw new NotImplementedException();
        }

        private void TextBox_MouseEnter(object sender, EventArgs e)
        {
            MouseEntered = true;
            Refresh();

            //throw new NotImplementedException();
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            if (Text == "")
            {
                Text = ghostText;
                ForeColor = Color.FromArgb(92, 95, 102);
            } else
            {
                ForeColor = Color.FromArgb(178, 183, 195);
            }
            //throw new NotImplementedException();
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {

            if (Text == ghostText)
            {
                Text = "";
                ForeColor = Color.FromArgb(178, 183, 195);
            } 

            //throw new NotImplementedException();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            graph.Clear(Parent.BackColor);
            graph.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            textBox.Top = (Height - textBox.Height) / 2;
            textBox.Left = rounding / 2;
            textBox.Size = new Size(Width - rounding, Height);

            if(Multiline)
                Font = new Font("Arial", Font.Size, FontStyle.Regular, GraphicsUnit.Pixel);
            else
                Font = new Font("Arial", Height - 12, FontStyle.Regular, GraphicsUnit.Pixel);
            textBox.Font = Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Light, Font);

            if (Text != ghostText)
            {
                ForeColor = Color.FromArgb(178, 183, 195);
                if (!MouseEntered)
                    textBox.PasswordChar = passwordChar;
                else
                    textBox.PasswordChar = '\0';
            }
            else
            {
                textBox.PasswordChar = '\0';
                ForeColor = Color.FromArgb(92, 95, 102);
            }

            float roundingValue = 0.1f;
            if (RoundingEnable && rounding > 0)
            {
                if (Height < Width)
                    roundingValue = Height;
                else roundingValue = Width;
                if (roundingValue > rounding)
                    roundingValue = rounding;
            }

            //рисование самой панели
            GraphicsPath rectPath = Painter.RoundedRectangle(rect, roundingValue);
            //graph.DrawPath(new Pen(Color.FromArgb(150,BackColor)), rectPath);
            graph.FillPath(new SolidBrush(BackColor), rectPath);
        }
    }
}
