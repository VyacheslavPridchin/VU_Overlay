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
    public partial class EnterName : Form
    {
        public EnterName()
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

            Random rnd = new Random();
            var login = "Guest" + rnd.Next(1000000, 9999999).ToString();

            SQL.SQLExecute($"INSERT INTO Accounts (id, login, nickname, type, remoteIP) VALUES (NULL, '{login}', '{Name_TextBox.Text}', '{"guest"}', 'null')");
            SQL.User.login = login;
            SQL.User.nickname = Name_TextBox.Text;
            SQL.User.id = SQL.Read($"SELECT id FROM Accounts WHERE nickname LIKE '{Name_TextBox.Text}' AND login LIKE '{login}'");
            SQL.User.type = "guest";

            //MessageBox.Show(GlobalID);
            SQL.ChangeMac(SQL.GetMAC());

            Close();
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
    }
}
