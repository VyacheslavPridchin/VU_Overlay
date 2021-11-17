using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    class VU_ButtonText : Control
    {
        #region ---Свойства---
        private string familyButton = "defaultFamily";
        [DefaultValue("defaultFamily")]
        [Description("Семья объекта. При нажатии кнопка может отключить другие кнопки только из своей семьи")]
        public string FamilyButton
        {
            get => familyButton;
            set
            {
                familyButton = value;
                Refresh();
            }
        }

        private Painter.Fonts.FontsName customFont = Painter.Fonts.FontsName.Roboto;
        [DisplayName("Custom Font")]
        [DefaultValue(Painter.Fonts.FontsName.Roboto)]
        [Description("Кастомный шрифт текста")]
        public Painter.Fonts.FontsName CustomFont
        {
            get => customFont;
            set
            {
                customFont = value;
                Refresh();
            }
        }

        private bool stickyButton = false;
        [DefaultValue(false)]
        [Description("Вкл / выкл залипания кнопки")]
        public bool StickyButton
        {
            get => stickyButton;
            set
            {
                stickyButton = value;
                Refresh();
            }
        }

        private bool stickyPressed = false;
        [DefaultValue(false)]
        [Description("Кнопка нажата")]
        [ReadOnly(true)]
        public bool StickyPressed
        {
            get => stickyPressed;
            set
            {
                stickyPressed = value;
                Refresh();
            }
        }
        #endregion

        #region ---Переменные---
        public StringFormat SF = new StringFormat();
        private bool MouseEntered = false, MousePressed = false, MouseDown = false;
        #endregion

        public VU_ButtonText()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(188, 56);
            ForeColor = Color.FromArgb(178, 183, 195);
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
            Font = new Font("Arial", 18f, FontStyle.Regular, GraphicsUnit.Pixel);
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            graph.Clear(Parent.BackColor);

            graph.DrawString(Text, Painter.Fonts.GetFont(customFont, Font), new SolidBrush(Color.FromArgb(178, 183, 195)), rect, SF);

            if (stickyPressed) 
                graph.DrawString(Text, Painter.Fonts.GetFont(customFont, Font), new SolidBrush(Color.FromArgb(113, 129, 143)), rect, SF);

            if (MouseEntered)
                graph.DrawString(Text, Painter.Fonts.GetFont(customFont, Font), new SolidBrush(Color.FromArgb(40, Color.Black)), rect, SF);

            if(MouseDown)
                graph.DrawString(Text, Painter.Fonts.GetFont(customFont, Font), new SolidBrush(Color.FromArgb(60, Color.Black)), rect, SF);

            if (MousePressed)
                graph.DrawString(Text, Painter.Fonts.GetFont(customFont, Font), new SolidBrush(Color.FromArgb(113, 129, 143)), rect, SF);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            Invalidate();

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MouseDown = true;
            
            //this.FindForm().Update();

            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Focus();
            //MessageBox.Show(Parent.Controls.OfType<VU_ButtonText>().Count().ToString());
            if(Parent != null)
            foreach (var pb in Parent.Controls.OfType<VU_ButtonText>())
            {
                if (pb.familyButton == familyButton)
                {
                    pb.stickyPressed = false;
                    pb.Invalidate();
                }
            }

            if (!stickyButton)
                MousePressed = true;

            if (stickyButton)
                stickyPressed = true;


        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MouseDown = false;
            if (!stickyButton)
                MousePressed = false;
            Invalidate();
        }
    }
}
