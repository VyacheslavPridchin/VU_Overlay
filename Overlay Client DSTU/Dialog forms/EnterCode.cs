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
    public partial class EnterCode : Form
    {
        public EnterCode()
        {
            InitializeComponent();
        }

        bool mailIsSend = false;
        string email;

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


            if (!mailIsSend)
            {
                var MailChecker = new EmailAddressAttribute();
                if (MailChecker.IsValid(EMailCode.Text))
                {
                    if (SQL.SQLCounter($"SELECT id FROM Accounts WHERE email LIKE '{EMailCode.Text}'") == 0)
                    {
                        // отправитель - устанавливаем адрес и отображаемое в письме имя
                        MailAddress from = new MailAddress("virtual_dstu@dstu.online", "Виртуальный ДГТУ");
                        // кому отправляем
                        MailAddress to = new MailAddress(EMailCode.Text);
                        // создаем объект сообщения
                        MailMessage m = new MailMessage(from, to);
                        // тема письма
                        m.Subject = "Код подтверждения регистрации аккаунта " + SQL.TempNickname;

                        Random rnd = new Random();
                        SQL.RegistrationCode = rnd.Next(100000, 999999).ToString();
                        // текст письма
                        m.Body = GetHTMLMail().Replace("replaсe_this", SQL.RegistrationCode);
                        // письмо представляет код html
                        m.IsBodyHtml = true;
                        // адрес smtp-сервера и порт, с которого будем отправлять письмо
                        SmtpClient smtp = new SmtpClient("dstu.online", 587);
                        // логин и пароль
                        smtp.Credentials = new NetworkCredential("virtual_dstu@dstu.online", "E0c5W1o8");
                        smtp.EnableSsl = false;
                        smtp.Send(m);

                        mailIsSend = true;
                        email = EMailCode.Text;
                        Alpha.Text = "Регистрация | " + email;
                        vU_Button1.Text = "Войти";
                        EMailCode.Text = "Код";
                        EMailCode.GhostText = "Код";
                        vU_Label1.Text = "Введите код, отправленный Вам на почту";
                    }
                    else
                    {
                        MajorForm.MsgBox("Ошибочка " + CommunicationCenter.GetRandomText(), "На эту почту уже зарегестрирован аккаунт :(");
                    }
                }
                else
                {
                    vU_Label1.Text = "Некорректная почта";
                    Task.Run(() => ShowText("Введите почту для подтверждения"));
                }
            }
            else
            {
                if (SQL.RegistrationCode == EMailCode.Text)
                {
                    SQL.RegistrationCode = "Correct";
                    SQL.EMail = email;
                    Close();
                }
                else
                {
                    vU_Label1.Text = "Неверный код подтверждения";
                    Task.Run(() => ShowText("Введите код, отправленный Вам на почту"));
                    //Task.Run(() => ShowText("Введите код, отправленный Вам на почту"));
                }
            }
        }

        private static string GetHTMLMail()
        {
            string path = "mail.html";

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
