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
    public partial class EnterPassword : Form
    {
        public EnterPassword()
        {
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

        private void vU_Button1_Click(object sender, EventArgs e)
        {
            if(SQL.Lesson.password == Password_TextBox.Text)
            {
                SQL.Lesson.password = "Correct";
                Close();
            } else
            {
                vU_Label1.Text = "Неверный пароль";
                Task.Run(() => ShowText());
            }
        }

        void ShowText()
        {
            Thread.Sleep(3000);
            FindForm().Invoke(new Action(() =>
            {
                vU_Label1.Text = "Введите пароль для входа на пару";
            }));
        }

        private void EnterPassword_Shown(object sender, EventArgs e)
        {
            Alpha.Text = "Вход на пару | Уникальный идентификатор: " + SQL.Lesson.UniqueKey;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void Alpha_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void EnterPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
