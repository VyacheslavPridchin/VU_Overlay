using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    class VU_Post : Panel
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

        private string postText = "Текста поста";
        [DisplayName("Post Text")]
        [DefaultValue("Текст поста")]
        [Description("Текста поста")]
        public string PostText
        {
            get => postText;
            set
            {
                postText = value;
                Refresh();
            }
        }

        private string url = "";
        [DisplayName("URL")]
        [Description("Ссылка на пост")]
        public string Url
        {
            get => url;
            set
            {
                url = value;
                Refresh();
            }
        }

        private string nameAuthor = "Автор поста";
        [DisplayName("Name Author")]
        [DefaultValue("Автор поста")]
        [Description("Автор поста")]
        public string NameAuthor
        {
            get => nameAuthor;
            set
            {
                nameAuthor = value;
                Refresh();
            }
        }

        private Image image;
        [DisplayName("Image Author")]
        [Description("Изображение автора")]
        public Image Image
        {
            get => image;
            set
            {
                image = value;
                Refresh();
            }
        }

        private Image image2;
        [DisplayName("Image")]
        [Description("Изображение вложения")]
        public Image Image2
        {
            get => image2;
            set
            {
                image2 = value;
                Refresh();
            }
        }
        #endregion

        #region ---Переменные---
        private StringFormat SF = new StringFormat(), SF1 = new StringFormat();
        #endregion

        public PictureBox picBox = new PictureBox();
        public PictureBox picBox2 = new PictureBox();
        //public TextBox textBox = new TextBox();
        public VU_ButtonText label = new VU_ButtonText();

        public VU_Post()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(212, 332);
            Width = 676;
            Height = 752;
            BackColor = Color.FromArgb(58, 58, 58);
            ForeColor = Color.FromArgb(178, 183, 195);

            picBox.Size = new Size(50, 50);
            picBox.Location = new Point(rounding / 2, rounding / 2);
            picBox.SizeMode = PictureBoxSizeMode.Zoom;

            picBox2.SizeMode = PictureBoxSizeMode.Zoom;
            picBox2.Location = new Point(rounding / 2, picBox.Top + picBox.Height + rounding / 2);
            picBox2.Size = new Size(Width - rounding, 365);
            picBox2.Cursor = Cursors.Hand;

            label.Font = new Font("Arial", 18f, FontStyle.Regular, GraphicsUnit.Pixel);
            label.CustomFont = Painter.Fonts.FontsName.Roboto_Light;
            label.Click += Label_Click;
            picBox2.Click += PicBox2_Click;
            label.SF.Alignment = StringAlignment.Near;
            label.SF.LineAlignment = StringAlignment.Near;
            label.Cursor = Cursors.Hand;
            label.AutoSize = false;
            label.BackColor = Color.Transparent;
            label.ForeColor = ForeColor;
            //label.TextAlign = ContentAlignment.MiddleLeft;
            if (postText.Length > 165)
                label.Text = postText.Substring(0, 165).Replace("\n", " ") + "...";
            else
                label.Text = postText.Replace("\n", " ") + "...";

            //textBox.Multiline = true;
            //textBox.Location = new Point(rounding / 2, picBox2.Height + picBox2.Top + rounding / 2);
            //textBox.Size = new Size(Width - rounding, Height - picBox2.Top - picBox2.Height - rounding);
            //textBox.Font = Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Light, Font, 16);
            //textBox.ReadOnly = true;
            //textBox.BackColor = BackColor;
            //textBox.ForeColor = ForeColor;
            //textBox.BorderStyle = BorderStyle.None;
            //textBox.ScrollBars = ScrollBars.Vertical;
            //textBox.Text = postText;

            SF.Alignment = StringAlignment.Near;
            SF.LineAlignment = StringAlignment.Center;
            SF1.Alignment = StringAlignment.Near;
            SF1.LineAlignment = StringAlignment.Near;
            Font = new Font("Arial", 18f, FontStyle.Regular, GraphicsUnit.Pixel);
            Controls.Add(picBox);
            Controls.Add(picBox2);
            //Controls.Add(textBox);
            Controls.Add(label);
        }

        private void PicBox2_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start(Url);
            //throw new System.NotImplementedException();
        }

        private void Label_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start(Url);
            //throw new System.NotImplementedException();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            Graphics parentGraph = Parent.CreateGraphics();
            graph.Clear(Parent.BackColor);
            graph.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            #region --- Рисование фона ---
            float roundingValue = 0.1f;
            if (RoundingEnable && rounding > 0)
            {
                if (Height < Width)
                    roundingValue = Height;
                else roundingValue = Width;
                if (roundingValue > rounding)
                    roundingValue = rounding;
            }

            ////Очищаем место под тень
            //Rectangle rectFP = new Rectangle(Left, Top, Width - 1 + ShadowOX, Height - 1 + ShadowOY);
            //parentGraph.FillRectangle(new SolidBrush(Parent.BackColor), rectFP);

            ////Тень
            //for (int i = 0; i < countIter; i++)
            //{
            //    //Рисование на родителе
            //    Rectangle rectForParent = new Rectangle(Left + i * (int)((float)shadowOX / (float)countIter), Top + i * (int)((float)shadowOY / (float)countIter), Width - 1, Height - 1);
            //    GraphicsPath rectPathForParent = Painter.RoundedRectangle(rectForParent, roundingValue);
            //    parentGraph.FillPath(new SolidBrush(Color.FromArgb((int)((float)intensity / (float)countIter), shadowColor)), rectPathForParent);

            //    //Рисование тени на кнопке
            //    Rectangle rectOffset = new Rectangle(i * (int)((float)shadowOX / (float)countIter), i * (int)((float)shadowOY / (float)countIter), Width - 1, Height - 1);
            //    GraphicsPath rectPathOffset = Painter.RoundedRectangle(rectOffset, roundingValue);
            //    graph.FillPath(new SolidBrush(Color.FromArgb((int)((float)intensity / (float)countIter), shadowColor)), rectPathOffset);
            //}

            //рисование самой панели
            GraphicsPath rectPath = Painter.RoundedRectangle(rect, roundingValue);
            //graph.DrawPath(new Pen(Color.FromArgb(150,BackColor)), rectPath);
            graph.FillPath(new SolidBrush(BackColor), rectPath);
            #endregion

            if (picBox2.Visible == false)
                picBox2.Height = 1;
            else
                picBox2.Size = new Size(Width - rounding, 389);

            graph.DrawString(nameAuthor, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font), new SolidBrush(ForeColor), new Rectangle(picBox.Left + picBox.Width + 5, picBox.Top + (picBox.Height - 20) / 2, Width - (picBox.Left + rounding), 20), SF);

            if (postText.Length > 165)
                label.Text = postText.Substring(0, 165).Replace("\n", " ") + "...";
            else
                label.Text = postText.Replace("\n", " ");
            label.Location = new Point(rounding / 2, picBox2.Height + picBox2.Top + rounding / 2);
            label.Size = new Size(Width - rounding, 100);

            if (image != null)
                picBox.Image = image;

            if (image2 != null)
                picBox2.Image = image2;
        }
    }
}
