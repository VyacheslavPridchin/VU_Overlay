using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    public partial class Loading : Form
    {
        public Loading()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            InitializeComponent();

        }

        #region --- Для перетаскивания формы ---
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        const uint WM_SYSCOMMAND = 0x0112;
        const uint DOMOVE = 0xF012;
        const uint DOSIZE = 0xF008;
        #endregion

        private void middle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void bottom_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void vU_Label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        bool onlyOnce = false, ShowForm = false, ProcedureIsEnd = false, ProcedureIsStart = false;
        double opacity = -1;
        Form LoadedForm = null;
        bool Connected = false, Auth = false;

        private void Loading_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        int top_height, top_top, middle_height, middle_top, middle_left, middle_width, bottom_width, bottom_left, counter = 0, wait = 0;


        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            top.Top = top_top + (int)(Math.Cos((counter + 5) / 8f) * 80) / 2 * -1;
            top.Height = (int)(top_height + Math.Cos((counter + 5) / 8f) * 80);

            middle.Top = middle_top + (int)(Math.Cos(counter / 8f) * 60) / 2 * -1;
            middle.Height = (int)(middle_height + Math.Cos(counter / 8f) * 60);

            middle.Left = middle_left + (int)(Math.Cos(counter / 8f) * 60) / 2 * -1;
            middle.Width = (int)(middle_width + Math.Cos(counter / 8f) * 60);

            bottom.Left = bottom_left + (int)(Math.Cos((counter - 5) / 8f) * 40) / 2 * -1;
            bottom.Width = (int)(bottom_width + Math.Cos((counter - 5) / 8f) * 40);

            if (counter < 20)
                Opacity = Opacity + 0.2;

            if (counter > 10)
            {

                if (!ProcedureIsEnd && !ProcedureIsStart)
                    Task.Run(() => ConnectAndAuth());

                if (Connected && ProcedureIsEnd && !ShowForm)
                {
                    ShowForm = true;
                    if (Auth)
                    {
                        //с 0 до 6 часов — ночь
                        //с 6 до 12 часов — утро
                        //с 12 до 18 часов — день
                        //с 18 до 24 часов — вечер
                        var hour = DateTime.Now.Hour;
                        var str = "Приветствуем, repl!";
                        if (hour >= 0 && hour < 6)
                        {
                            str = "Доброй ночи, repl🌙";
                        }
                        if (hour >= 6 && hour < 12)
                        {
                            str = "Доброе утро, repl!";
                        }
                        if (hour >= 12 && hour < 18)
                        {
                            str = "Хорошего дня, repl!";
                        }
                        if (hour >= 18 && hour < 24)
                        {
                            str = "Приятного вечера, repl🌙";
                        }

                        vU_Label1.Text = str.Replace("repl", SQL.User.nickname);

                        LoadedForm = new MajorForm();
                    }
                    else
                    {
                        var hour = DateTime.Now.Hour;
                        var str = "repl, приветствуем Вас!";
                        if (hour >= 0 && hour < 6)
                        {
                            str = "Доброй ночи, пользователь🌙";
                        }
                        if (hour >= 6 && hour < 12)
                        {
                            str = "Доброе утро, друг!";
                        }
                        if (hour >= 12 && hour < 18)
                        {
                            str = "Хорошего дня, юзер :)";
                        }
                        if (hour >= 18 && hour < 24)
                        {
                            str = "Приятного вечера, пользователь🌙";
                        }

                        vU_Label1.Text = str.Replace("repl", "Гость");

                        LoadedForm = new LoginForm();
                    }
                    LoadedForm.Opacity = -1;

                    LoadedForm.Show();
                }

                if (!Connected && ProcedureIsEnd && !onlyOnce)
                {
                    onlyOnce = true;
                    MajorForm.MsgBox("Ошибочка", "Нет подключения к серверам :(\n" + "Проверьте интернет " + CommunicationCenter.GetRandomText());
                    Close();
                }


            }


            if (ProcedureIsEnd && Connected)
            {
                if (wait > 30)
                {
                    if (opacity < 1)
                    {
                        Opacity = Opacity - 0.1;
                        opacity = opacity + 0.2;
                        LoadedForm.Opacity = opacity;
                    }
                    else if (opacity >= 1)
                    {
                        Hide();
                        timer1.Stop();
                    }
                }
                else
                {
                    wait++;
                }
            }

        }

        void ConnectAndAuth()
        {
            try
            {
                ProcedureIsStart = true;
                if (!SQL.ConnectDB())
                {
                    Connected = false;
                    ProcedureIsEnd = true;

                }
                else
                {
                    Connected = true;
                    if (SQL.Authorize(SQL.GetMAC()))
                    {
                        Auth = true;
                        ProcedureIsEnd = true;

                    }
                    else
                    {
                        Auth = false;
                        ProcedureIsEnd = true;

                    }
                }
            }
            catch (Exception ex)
            {
                Connected = false;
            }
        }

        private void Loading_Shown(object sender, EventArgs e)
        {
            top_height = top.Height;
            top_top = top.Top;
            middle_height = middle.Height;
            middle_left = middle.Left;
            middle_top = middle.Top;
            middle_width = middle.Width;
            bottom_left = bottom.Left;
            bottom_width = bottom.Width;

            top.Top = top_top + (int)(Math.Cos((counter + 5) / 8f) * 80) / 2 * -1;
            top.Height = (int)(top_height + Math.Cos((counter + 5) / 8f) * 80);

            middle.Top = middle_top + (int)(Math.Cos(counter / 8f) * 60) / 2 * -1;
            middle.Height = (int)(middle_height + Math.Cos(counter / 8f) * 60);

            middle.Left = middle_left + (int)(Math.Cos(counter / 8f) * 60) / 2 * -1;
            middle.Width = (int)(middle_width + Math.Cos(counter / 8f) * 60);

            bottom.Left = bottom_left + (int)(Math.Cos((counter - 5) / 8f) * 40) / 2 * -1;
            bottom.Width = (int)(bottom_width + Math.Cos((counter - 5) / 8f) * 40);
        }
    }
}
