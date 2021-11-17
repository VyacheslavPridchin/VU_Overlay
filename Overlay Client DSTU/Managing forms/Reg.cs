using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    public partial class Reg : Form
    {
        #region --- Для перетаскивания формы ---
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        const uint WM_SYSCOMMAND = 0x0112;
        const uint DOMOVE = 0xF012;
        const uint DOSIZE = 0xF008;
        #endregion

        public Reg()
        {
            InitializeComponent();
        }

        private void vU_Button1_Click(object sender, EventArgs e)
        {
            if (login.Text != login.GhostText)
                if (nickname.Text != nickname.GhostText)
                    if (dbl_password.Text == password.Text)
                    {
                        Opacity = 0.5;

                        var result = SQL.CreateNewAccount(login.Text, password.Text, nickname.Text, "student");
                        Opacity = 1;

                        if (result == 0)
                        {
                            log.Text = "Регистрация прошла успешно!";
                            MajorForm main = new MajorForm();
                            main.Show();
                            this.Hide();

                        }
                        else if (result == 1)
                        {
                            var myThread = new Thread(() => ShowMessage_Thread(""));
                            myThread.Start();
                            log.Text = "Аккаунт c таким логином уже есть";
                        }
                        else if (result == 2)
                        {
                            var myThread = new Thread(() => ShowMessage_Thread(""));
                            myThread.Start();
                            log.Text = "Вы не подтвердили почту";
                        }
                    }
                    else
                    {
                        var myThread = new Thread(() => ShowMessage_Thread(""));
                        myThread.Start();
                        log.Text = "Пароли не совпадают";
                    }
        }

        public void ShowMessage_Thread(string str)
        {
            try
            {
                Thread.Sleep(5000);
                Invoke(new Action(() => log.Text = "Регистрация в Виртуальном ДГТУ"));
            }
            catch (Exception e) { }
        }

        private void vU_ButtonText1_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            Dispose();
            GC.Collect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void Reg_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void log_Click(object sender, EventArgs e)
        {

        }
    }
}
