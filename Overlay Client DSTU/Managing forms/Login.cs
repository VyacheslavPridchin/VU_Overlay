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
    public partial class LoginForm : Form
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

        public LoginForm()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void vU_Button1_Click(object sender, EventArgs e)
        {
            if (SQL.Authorize(login.Text, password.Text))
            {
                log.Text = "Авторизация прошла успешно!";
                MajorForm main = new MajorForm();
                main.Show();
                this.Hide();

            }
            else
            {
                var myThread = new Thread(() => ShowMessage_Thread(""));
                myThread.Start();
                log.Text = "Неверный логин и/или пароль";
            }
        }

        public void ShowMessage_Thread(string str)
        {
            try
            {
                Thread.Sleep(5000);
                Invoke(new Action(() => log.Text = "Авторизация в Виртуальном ДГТУ"));
            } catch(Exception e) { }
        }

        private void vU_ButtonText1_Click(object sender, EventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            Dispose();
            GC.Collect();
        }

        private void vU_ButtonText2_Click(object sender, EventArgs e)
        {
            Opacity = 0.5;

            EnterName entrnm = new EnterName();
            entrnm.ShowDialog();
            Opacity = 1;

            if (SQL.User.login != "")
            {
                MajorForm main = new MajorForm();
                main.Show();
                Dispose();
                GC.Collect();
            }

        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void vU_ButtonText3_Click(object sender, EventArgs e)
        {
            Opacity = 0.5;
            Recovery rec = new Recovery();
            rec.ShowDialog();
            Opacity = 1;
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            SQL.User.id = "";
            SQL.User.login = "";
            SQL.User.nickname = "";
            SQL.User.type = "";
        }
    }
}
