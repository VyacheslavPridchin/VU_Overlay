using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    public class VU_ImFromUser : Control
    {
        #region ---Свойства---
        //private int shadowOY = 8;
        //[DisplayName("Image")]
        //[DefaultValue(8)]
        //[Description("Сдвиг тени вдоль оси Y")]
        //[Category("Тень")]
        //public int ShadowOY
        //{
        //    get => shadowOY;
        //    set
        //    {
        //        shadowOY = value;
        //        Refresh();
        //    }
        //}
        #endregion

        #region ---Переменные---
        public string OwnerID = "-1";
        public CustomClasses.Package.Types OwnerType = CustomClasses.Package.Types.unknown;
        public bool isStream = false;
        private bool MouseEntered = false, MousePressed = false;
        private StringFormat SF = new StringFormat();
        #endregion

        public VU_ImFromUser()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(179, 102);
            BackColor = Color.FromArgb(58, 58, 58);
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
            Margin = new Padding(3, 0, 0, 5);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            graph.Clear(BackColor);
            graph.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            int newWidth = (int)(Height * (float)BackgroundImage.Width / (float)BackgroundImage.Height);

            graph.DrawImage(BackgroundImage, new Rectangle((Width - newWidth) / 2, 1, newWidth - 1, Height - 1), new Rectangle(0, 0, BackgroundImage.Width - 1, BackgroundImage.Height - 1), GraphicsUnit.Pixel);
            if (OwnerID != "-1")
            {
                if (isStream)
                {
                    if (MouseEntered)
                    {
                        graph.FillRectangle(new SolidBrush(Color.FromArgb(200, 178, 183, 195)), rect);
                        graph.DrawString(Text, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font, 11), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);
                    }
                    if (MousePressed)
                    {
                        graph.FillRectangle(new SolidBrush(Color.FromArgb(220, 178, 183, 195)), rect);
                        graph.DrawString(Text, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font, 11), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);
                    }
                }
                else
                {


                    if (MouseEntered)
                    {
                        if (OwnerID != SQL.User.id) {
                            graph.FillRectangle(new SolidBrush(Color.FromArgb(200, 178, 183, 195)), rect);
                            graph.DrawString("Заглушить пользователя " + Text, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font, 11), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);

                            if (MousePressed)
                            {
                                graph.FillRectangle(new SolidBrush(Color.FromArgb(220, 178, 183, 195)), rect);
                                graph.DrawString("Заглушить пользователя " + Text, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font, 11), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);
                            }
                        } else
                        {
                            

                            graph.FillRectangle(new SolidBrush(Color.FromArgb(200, 178, 183, 195)), rect);
                            graph.DrawString(CommunicationCenter.GetRandomText(), Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font, 11), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);

                            if (MousePressed)
                            {
                                graph.FillRectangle(new SolidBrush(Color.FromArgb(220, 178, 183, 195)), rect);
                                graph.DrawString(CommunicationCenter.GetRandomText(), Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font, 11), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);
                            }
                        }
                    }
                    else
                    {
                        graph.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), rect);
                        graph.DrawString(Text, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font, 11), new SolidBrush(Color.FromArgb(178, 183, 195)), new Rectangle(0, 0, Width, Height), SF);

                    }
                }
            }
            else
            {
                if (MouseEntered)
                {
                    graph.FillRectangle(new SolidBrush(Color.FromArgb(200, 178, 183, 195)), rect);
                    graph.DrawString(Text, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font, 11), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);
                }
                if (MousePressed)
                {
                    graph.FillRectangle(new SolidBrush(Color.FromArgb(220, 178, 183, 195)), rect);
                    graph.DrawString(Text, Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Medium, Font, 11), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), SF);
                }
            }
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

            MousePressed = true;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            Invalidate();
        }

        private bool isPlaying = true;
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (OwnerID != "-1")
            {
                if (isStream)
                {
                    CommunicationCenter.currentStream = (OwnerID, OwnerType);
                    CommunicationCenter.SendPackage(new CustomClasses.Package(SQL.User.id, SQL.Lesson.id, CustomClasses.Package.Types.SetTypePack, CommunicationCenter.ObjToArr(new CustomClasses.setType(OwnerType, int.Parse(OwnerID)))));
                }
                else
                {
                    if (CommunicationCenter.Audios.ContainsKey(int.Parse(OwnerID)))
                        if (isPlaying)
                        {

                            CommunicationCenter.output.Stop();
                            isPlaying = false;
                        }
                        else
                        {
                            CommunicationCenter.output.Play();
                            isPlaying = true;
                        }
                }
            }
            Focus();
        }


    }
}
