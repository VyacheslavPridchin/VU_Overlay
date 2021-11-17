using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    public partial class Recovery : Form
    {
        public Recovery()
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void vU_Button1_Click(object sender, EventArgs e)
        {
            var MailChecker = new EmailAddressAttribute();
            if (MailChecker.IsValid(EMail.Text))
            {
                if (SQL.SQLCounter($"SELECT id FROM Accounts WHERE email LIKE '{EMail.Text}'") != 0)
                {
                    // отправитель - устанавливаем адрес и отображаемое в письме имя
                    MailAddress from = new MailAddress("virtual_dstu@dstu.online", "Виртуальный ДГТУ");
                    // кому отправляем
                    MailAddress to = new MailAddress(EMail.Text);
                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Напоминание пароля к Виртуальному университету";

                    
                    // текст письма
                    m.Body = GetHTMLMail("recovery_mail.html").Replace("replaсe_this", SQL.Read($"SELECT password FROM Accounts WHERE email LIKE '{EMail.Text}'"));
                    // письмо представляет код html
                    m.IsBodyHtml = true;
                    // адрес smtp-сервера и порт, с которого будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("dstu.online", 587);
                    // логин и пароль
                    smtp.Credentials = new NetworkCredential("virtual_dstu@dstu.online", "E0c5W1o8");
                    smtp.EnableSsl = false;
                    smtp.Send(m);

                    MajorForm.MsgBox("Сделано :)", "Проверьте свою почту и, желательно, смените пароль на новый");
                    Close();
                }
                else
                {
                    MajorForm.MsgBox("Ошибочка " + CommunicationCenter.GetRandomText(), "На эту почту не зарегистрирован аккаунт");
                }
            }
            else
            {
                vU_Label1.Text = "Некорректная почта";
                Task.Run(() => ShowText("Введите почту и мы напомним Вам пароль"));
            }
        }

        private static string GetHTMLMail(string HTML)
        {
            string path = HTML;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
                // асинхронное чтение
                using (StreamReader sr = new StreamReader(path))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        void ShowText(string text)
        {
            Thread.Sleep(3000);
            FindForm().Invoke(new Action(() =>
            {
                vU_Label1.Text = text;
            }));
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

        private void vU_Button2_Click(object sender, EventArgs e)
        {
            var MailChecker = new EmailAddressAttribute();
            if (MailChecker.IsValid(EMail.Text))
            {
                if (SQL.SQLCounter($"SELECT id FROM Accounts WHERE email LIKE '{EMail.Text}'") != 0)
                {
                    // отправитель - устанавливаем адрес и отображаемое в письме имя
                    MailAddress from = new MailAddress("virtual_dstu@dstu.online", "Виртуальный ДГТУ");
                    // кому отправляем
                    MailAddress to = new MailAddress(EMail.Text);
                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Напоминание пароля к Виртуальному университету";

                    var pass = SQL.Read($"SELECT password FROM Accounts WHERE email LIKE '{EMail.Text}'");
                    
                    for(int i = 0; i < pass.Length; i++)
                    {
                        if(i % 2 == 1)
                        {
                            pass = pass.Remove(i, 1).Insert(i, "◌");
                        }
                    }
                    // текст письма
                    m.Body = GetHTMLMail("recovery_mail.html").Replace("replaсe_this", pass);
                    //письмо представляет код
                    m.IsBodyHtml = true;
                    //адрес smtp-сервера и порт, с которого будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("dstu.online", 587);
                    //логин и пароль
                    smtp.Credentials = new NetworkCredential("virtual_dstu@dstu.online", "E0c5W1o8");
                    smtp.EnableSsl = false;
                    smtp.Send(m);

                    MajorForm.MsgBox("Сделано :)", "Проверьте свою почту и, желательно, смените пароль на новый");
                    Close();
                }
                else
                {
                    MajorForm.MsgBox("Ошибочка " + CommunicationCenter.GetRandomText(), "На эту почту не зарегистрирован аккаунт");
                }
            }
            else
            {
                vU_Label1.Text = "Некорректная почта";
                Task.Run(() => ShowText("Введите почту и мы напомним Вам пароль"));
            }
        }
    }
}
