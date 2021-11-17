using Overlay_Client_DSTU.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    class VU_Story : Panel
    {
        private string storyAuthor = "@virtual_dstu";
        [DisplayName("Story Author")]
        [DefaultValue("@virtual_dstu")]
        [Description("Автор сторис")]
        public string StoryAuthor
        {
            get => storyAuthor;
            set
            {
                storyAuthor = value;
                UpdateText();
                Refresh();
            }
        }

        private Color textColor = Color.FromArgb(178, 183, 195);
        [DisplayName("Text Color")]
        [Description("Цвет текста автора сторис")]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                UpdateText();
                Refresh();
            }
        }

        PictureBox image = new PictureBox();
        private StringFormat SF = new StringFormat();

        public VU_Story()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(169, 300);
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
            image.BackgroundImage = BackgroundImage;
            BackgroundImage = null;
            image.BackgroundImageLayout = ImageLayout.Stretch;
            image.Dock = DockStyle.Fill;
            image.SizeMode = PictureBoxSizeMode.StretchImage;
            UpdateText();
            Controls.Add(image);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            image.BackColor = BackColor;
            image.BackgroundImageLayout = ImageLayout.Stretch;
            image.Dock = DockStyle.Fill;
            image.SizeMode = PictureBoxSizeMode.StretchImage;
            UpdateText();
        }


        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            base.OnBackgroundImageChanged(e);
            image.BackgroundImage = BackgroundImage;
        }

        private void UpdateText()
        {
            Image newIm = Resources.Story;
            Graphics graph = Graphics.FromImage(newIm);
            graph.DrawString(StoryAuthor, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Light, Font, 55), new SolidBrush(TextColor), new Rectangle(75, 1830, 930, 55), SF);
            graph.Dispose();
            image.Image = newIm;
        }
    }

}
