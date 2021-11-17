using System;
using System.Linq;
using System.Windows.Forms;
using VkNet.Model;
using VkNet;
using VkNet.Enums.Filters;
using System.Net;
using VkNet.Utils.AntiCaptcha;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using VkNet.Model.RequestParams;
using System.Collections.Generic;
using VkNet.Enums.SafetyEnums;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.Mail;

namespace Overlay_Client_DSTU
{
    public partial class MajorForm : Form
    {
        #region ◌ ◌ ◌ Идеи для создания библиотеки элементов ◌ ◌ ◌
        //В Painter засунуть список частоиспользуемых цветов
        //В каждый элемент добавит customFont, для возможности использования встроенного Font
        //В каждый элемент добавить возможность создать тень
        //Для тени сделать расширение, будто элемент приподнят над плоскостью
        #endregion

        #region ◌ ◌ ◌ TODO ◌ ◌ ◌
        //Двухфакторная авторизация ВК
        //Сообщения ВК
        //Авторизация в системах ДГТУ
        #endregion

        #region --- Для перетаскивания формы ---
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        const uint WM_SYSCOMMAND = 0x0112;
        const uint DOMOVE = 0xF012;
        const uint DOSIZE = 0xF008;

        //Переход в UNITY
        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        #endregion

        #region ---Переменные---
        public static Captcha captcha = new Captcha();
        public static Logger log = new Logger();
        public int CountOfLoads = 0;
        public static string captchaKey = "The cake is a lie";
        public VkApi VK = new VkApi(log, captcha);
        public static dynamic MainForm, VU_Auths;
        public static bool isCaptcha, isTwoFactor;
        public static string nameOfVU_Auth;
        public string CodeAuth = "The cake is a lie";
        List<VU_Post> postsObj = new List<VU_Post>();

        //private long?[] groups = new long?[] { -9346281, -201442170, -64994669, -76738339, -73153561, -137269494, -182868130, -163858577, -52706929, -142768882, -66871469, -126983867, -59109699, -66101359, -122406894, -19798488, -8014947, -142151516, -1081610, -30618587 };
        private long?[] groups = new long?[] { -9346281, -201442170, -64994669, -76738339, -73153561, -137269494, -182868130, -163858577 };
        #endregion

        #region ---Внутренние интерфейсы C#---

        //Интерфейс логгера, не используется, нужен для объявления ВК
        public class Logger : ILogger<VkApi>
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                throw new NotImplementedException();
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                //throw new NotImplementedException();
            }
        }

        //Интерфейс капчи
        public class Captcha : ICaptchaSolver
        {
            void ICaptchaSolver.CaptchaIsFalse()
            {
                //throw new NotImplementedException();
            }

            string ICaptchaSolver.Solve(string url)
            {

                Solve(url);
                //throw new NotImplementedException();
                return captchaKey;
            }
        }
        #endregion

        #region ---Первичные настройки приложения---
        public MajorForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Создание ссылок для статиков
            MainForm = this;
            VU_Auths = this.Controls.OfType<VU_Auth>();

            //Привязка действий к динамическим объектам
            vU_Auth1.enter.Click += AuthVK;
            vU_Auth2.enter.Click += Enter_Click;
            vU_Auth3.enter.Click += Enter_Click1;
            vU_Auth4.enter.Click += Enter_Click2;
            //WindowState = FormWindowState.Maximized;

            //Заполнение полей данными
            Login_Settings.Text = SQL.User.login;
            Nickname_Settings.Text = SQL.User.nickname;

            //Открытие панели пар
            ChangePanel(Lessons);

            LoadLessons();
            if (SQL.User.type == "student" | SQL.User.type == "guest")
            {

                CreateLesson_button.Visible = false;
                items.Top = items.Top - CreateLesson_button.Height - CreateLesson_button.Margin.Top - CreateLesson_button.Margin.Bottom;
                items.Height = items.Height + CreateLesson_button.Height + CreateLesson_button.Margin.Top + CreateLesson_button.Margin.Bottom;

                if (SQL.User.type == "guest")
                {
                    vU_Label12.Visible = false;
                    vU_Label13.Visible = false;
                    vU_Label14.Visible = false;
                    vU_Label15.Visible = false;

                    OldPass_Settings.Visible = false;
                    NewPass_Settings.Visible = false;
                    dbl_NewPass_Settings.Visible = false;
                    vU_Button3.Visible = false;
                }
            }
            vU_ButtonText5.StickyPressed = true;

            //Центрирование
            Menu.Left = (Width - Menu.Width) / 2;
        }
        #endregion

        #region --- Функции ---
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            VU_Label label = new VU_Label();
            VU_TextBox textBox = new VU_TextBox();
            VU_Button buttonOk = new VU_Button();

            //System.Windows.Forms.Button buttonCancel = new System.Windows.Forms.Button();
            form.BackColor = Color.FromArgb(36, 35, 37);
            label.ForeColor = Color.FromArgb(178, 183, 195);
            label.CustomFont = Painter.Fonts.FontsName.Roboto_Light;
            label.Font = new Font("Arial", 12);
            label.TextAlign = ContentAlignment.MiddleCenter;
            form.Text = title;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Font = Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Light, label.Font, 10);
            label.Text = promptText;
            textBox.Text = value;
            form.ClientSize = new Size(350, 135);
            textBox.textBox.TextAlign = HorizontalAlignment.Center;
            buttonOk.Text = "Ок";
            //buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            //buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(10, 1, form.ClientSize.Width - 20, 40); ;
            textBox.Location = new Point((form.ClientSize.Width - textBox.Width) / 2, label.Height + label.Top + 5);
            buttonOk.Location = new Point((form.ClientSize.Width - buttonOk.Width) / 2, textBox.Height + textBox.Top + 5);
            //buttonCancel.SetBounds(309, 72, 75, 23);
            //buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk });

            form.AcceptButton = buttonOk;
            //form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public static void MsgBox(string title, string promptText)
        {
            Form form = new Form();
            VU_Label label = new VU_Label();
            VU_Button buttonOk = new VU_Button();

            //System.Windows.Forms.Button buttonCancel = new System.Windows.Forms.Button();
            form.BackColor = Color.FromArgb(36, 35, 37);
            label.ForeColor = Color.FromArgb(178, 183, 195);
            label.CustomFont = Painter.Fonts.FontsName.Roboto_Light;
            label.Font = new Font("Arial", 12);
            label.TextAlign = ContentAlignment.MiddleCenter;
            form.Text = title;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Font = Painter.Fonts.GetFont(Painter.Fonts.FontsName.Roboto_Light, label.Font, 10);
            label.Text = promptText;
            form.ClientSize = new Size(350, 100);
            buttonOk.Text = "Окей";
            //buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            //buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(10, 1, form.ClientSize.Width - 20, 40); ;
            buttonOk.Location = new Point((form.ClientSize.Width - buttonOk.Width) / 2, label.Top + label.Height + 5);
            //buttonCancel.SetBounds(309, 72, 75, 23);
            //buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            form.Controls.AddRange(new Control[] { label, buttonOk });

            form.AcceptButton = buttonOk;
            //form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
        }

        public static void ChangePanel(Control ctrl)
        {
            for (int i = 0; i < MainForm.Controls.Count; i++)
                if ((MainForm.Controls[i].Tag as string) == "Tabs")
                    MainForm.Controls[i].Visible = false;
            ctrl.Left = (MainForm.Width - ctrl.Width) / 2;
            ctrl.Top = 77;
            ctrl.Visible = true;
        }

        public void LoadLessons()
        {
            var result = SQL.FullRead($"SELECT id, name, description, type, UniqueKey, password FROM Lessons WHERE type LIKE 'public'");

            if (result.Count != 0) items.Controls.Clear();
            if (result.Count == 0)
            {
                if (items.Controls.Count == 1)
                {
                    if (items.Controls[0].GetType().ToString() != "Overlay_Client_DSTU.VU_Label")
                    {
                        items.Controls.Clear();
                    }
                }
                else
                {
                    items.Controls.Clear();
                }
            }
            VU_Lesson less;
            for (int i = 0; i < result.Count; i++)
            {
                less = new VU_Lesson();
                less.password = result[i][5];
                less.uniqueKey = result[i][4];
                less.typeLesson = result[i][3];
                less.DescLesson = result[i][2];
                less.NameLesson = result[i][1];
                less.idLesson = result[i][0];
                less.Margin = new Padding(9, 18, 9, 0);
                //await MainForm.Invoke(new Action(() => { MainForm.Lessons.Controls.Add(less); }));
                items.Controls.Add(less);
            }
        }

        #endregion

        #region ---Функции ВК---
        public static void Solve(string url) //Решение капчи, привязка к интерфейсу капчи и авторизации ВК
        {

            MainForm.Invoke(new Action(() => MainForm.Controls["settingsPanel"].Controls[nameOfVU_Auth].captchaBox.Image = Painter.GetImage(url)));
            MainForm.Invoke(new Action(() => MainForm.Controls["settingsPanel"].Controls[nameOfVU_Auth].captcha.Visible = true));
            MainForm.Invoke(new Action(() => MainForm.Controls["settingsPanel"].Controls[nameOfVU_Auth].captchaBox.Visible = true));
            MainForm.Invoke(new Action(() => MainForm.Controls["settingsPanel"].Controls[nameOfVU_Auth].Height = 350));
            MainForm.Invoke(new Action(() => ChangePanel(MainForm.Controls["settingsPanel"])));

            while (!isCaptcha) { Thread.Sleep(1000); } //Искусственное торможение потока while'ом, до ввода капчи. Костыль. Sleep для ослабления нагрузки на процессор

            isCaptcha = false;

            MainForm.Invoke(new Action(() => MainForm.Controls["settingsPanel"].Controls[nameOfVU_Auth].captcha.Visible = false));
            MainForm.Invoke(new Action(() => MainForm.Controls["settingsPanel"].Controls[nameOfVU_Auth].captchaBox.Visible = false));
            MainForm.Invoke(new Action(() => MainForm.Controls["settingsPanel"].Controls[nameOfVU_Auth].Height = 250));
            MainForm.Invoke(new Action(() => MainForm.Controls["settingsPanel"].Controls[nameOfVU_Auth].captcha.Text = MainForm.Controls["settingsPanel"].Controls[nameOfVU_Auth].captcha.GhostText));
        }

        public async void AuthVK(object sender, EventArgs e) //Функция авторизации в ВК. Крепко связано с компонентом VU_Auth
        {
            if (VK.IsAuthorized)
            {
                await VK.LogOutAsync();
                if (File.Exists("Account")) File.Delete("Account");
                MsgBox("Приложение будет закрыто", "Приложение будет закрыто после выхода из ВК. Запустите приложение заново.");
                System.Windows.Forms.Application.Exit();
            }

            dynamic AuthForm = vU_Auth1; //Привязка объекта формы авторизации
            nameOfVU_Auth = vU_Auth1.Name;
            if (AuthForm.captcha.Text != AuthForm.captcha.GhostText)
            {
                captchaKey = AuthForm.captcha.Text;
                isCaptcha = true;
            }

            try
            {
                if (!isCaptcha)
                {

                    await VK.AuthorizeAsync(new ApiAuthParams()
                    {
                        Login = AuthForm.login.Text,
                        Password = AuthForm.password.Text,
                        ApplicationId = 7786065,
                        Settings = Settings.All,
                        TwoFactorAuthorization = () =>
                        {
                            string value = "";
                            InputBox("Двухфакторная авторизация", "Введите код подтверждения для авторизации в аккаунте: ", ref value);
                            return value;
                        }
                    });


                    if (VK.IsAuthorized == true)
                    {

                        using (StreamWriter sw = new StreamWriter("Account", false, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(AuthForm.login.Text);
                            sw.WriteLine(AuthForm.password.Text);
                            sw.Close();
                        }

                        AuthForm.enter.Text = "Выйти";
                        var user = VK.Users.Get(new long[] { VK.UserId.GetValueOrDefault() });
                        AuthForm.DescService = user[0].LastName + " " + user[0].FirstName + ", авторизация прошла успешно!";
                        AuthForm.login.Visible = false;
                        AuthForm.password.Visible = false;
                        AuthForm.Height = 165;
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter("Logger.txt", false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(ex.Message + "\n\n" + ex.ToString());
                }

                var str = AuthForm.DescService.Substring(0); //Переприсваивание переменной, ибо при создании потока, он не успевает забрать старое значение DescService
                var myThread = new Thread(() => ShowMessage_Thread(str, AuthForm));
                myThread.Start();

                AuthForm.DescService = ex.Message;
            }

        }

        public async void AuthVKOnLoad()
        {
            try
            {
                if (File.Exists("Account"))
                    using (StreamReader sr = new StreamReader("Account"))
                    {
                        await VK.AuthorizeAsync(new ApiAuthParams
                        {
                            Login = sr.ReadLine(),
                            Password = sr.ReadLine(),
                            ApplicationId = 7788276,
                            Settings = Settings.All,
                            TwoFactorAuthorization = () =>
                            {
                                string value = "";
                                InputBox("Двухфакторная авторизация", "Введите код подтверждения для авторизации в аккаунте: ", ref value);
                                return value;
                            }
                        });

                        sr.Close();
                    }

                if (VK.IsAuthorized)
                {
                    vU_Auth1.enter.Text = "Выйти";
                    var user = VK.Users.Get(new long[] { VK.UserId.GetValueOrDefault() });
                    vU_Auth1.DescService = user[0].LastName + " " + user[0].FirstName + ", авторизация прошла успешно!";
                    vU_Auth1.login.Visible = false;
                    vU_Auth1.password.Visible = false;
                    vU_Auth1.Height = 165;
                }
            }
            catch (Exception e)
            {
                if (File.Exists("Account")) File.Delete("Account");
                MainForm.Invoke(new Action(() => ChangePanel(settingsPanel_Outer)));
                MainForm.Invoke(new Action(() => MainForm.vU_ButtonText4.StickyPressed = true));
            }
        }

        public void ShowMessage_Thread(string str, dynamic obj)
        {
            try
            {
                Thread.Sleep(3000);
                MainForm.Invoke(new Action(() => obj.DescService = str));
            }
            catch (Exception e)
            {

            }
        }

        private async void LoadNews()
        {
            try
            {
                if (VK.IsAuthorized)
                {
                    for (int j = 0; j < groups.Length; j++)
                    {
                        var posts = await VK.Wall.GetAsync(new WallGetParams
                        {
                            //OwnerId = -9346281
                            OwnerId = groups[j],
                            Count = 1,
                            Offset = (ulong)(CountOfLoads)
                        });

                        //dynamic Group;
                        //if(groups[j] < 0)
                        var Group = VK.Groups.GetById(null, (-1 * groups[j]).ToString(), null).FirstOrDefault();
                        //else Group = VK.Users.Get(null, groups[j].ToString(), null).FirstOrDefault();
                        System.Drawing.Image imGr;
                        imGr = Painter.GetImage(Group.Photo50.AbsoluteUri);

                        for (int i = 0; i < posts.WallPosts.Count; i++)
                        {
                            Thread.Sleep(400);

                            if (posts.WallPosts[i].Text != string.Empty)
                            {
                                postsObj.Add(new VU_Post());

                                if (posts.WallPosts[i].Attachment != null)
                                    if (posts.WallPosts[i].Attachment.Instance != null)
                                        if (posts.WallPosts[i].Attachment.Type.ToString() == "VkNet.Model.Attachments.Photo")
                                        {
                                            //VK.Call("photos.get", new VkNet.Utils.VkParameters)
                                            var get = await VK.Photo.GetAsync(new PhotoGetParams()
                                            {
                                                OwnerId = groups[j],
                                                AlbumId = PhotoAlbumType.Wall,
                                                PhotoIds = new[] { posts.WallPosts[i].Attachment.Instance.Id.ToString() }
                                            });

                                            if (get.LastOrDefault().Sizes.LastOrDefault().Url != null)
                                                postsObj.LastOrDefault().Image2 = Painter.GetImage(get.LastOrDefault().Sizes.LastOrDefault().Url.ToString());
                                        }
                                if (postsObj.LastOrDefault().Image2 == null)
                                {
                                    postsObj.LastOrDefault().picBox2.Visible = false;
                                    postsObj.LastOrDefault().Height = postsObj[i].picBox2.Top + 100;
                                }
                                else
                                {
                                    postsObj.LastOrDefault().Height = postsObj[i].picBox2.Top + 389 + 100;
                                }

                                postsObj.LastOrDefault().NameAuthor = Group.Name + " | ВК";
                                postsObj.LastOrDefault().Url = "https://vk.com/wall" + groups[j].ToString() + "_" + posts.WallPosts[i].Id;
                                postsObj.LastOrDefault().PostText = posts.WallPosts[i].Text;
                                postsObj.LastOrDefault().Image = imGr;
                                NewsPanel_Inner.Controls.Add(postsObj.LastOrDefault());
                            }
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ---События по нажатию на объекты---
        private void Enter_Click2(object sender, EventArgs e)
        {
            MsgBox("Facebook", "На данный момент синхронизация с сервисом Facebook недоступна");
            //throw new NotImplementedException();
        }

        private void Enter_Click1(object sender, EventArgs e)
        {
            MsgBox("Edu DonSTU", "На данный момент синхронизация с сервисом Edu DonSTU недоступна");
            //throw new NotImplementedException();        }
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            MsgBox("Мой ДГТУ", "На данный момент синхронизация с сервисом Мой ДГТУ недоступна");
            //throw new NotImplementedException();
        }

        private void vU_ButtonText3_Click(object sender, EventArgs e) => ChangePanel(Library);

        private void button1_Click(object sender, EventArgs e) => System.Windows.Forms.Application.Exit();

        private void Form1_Shown(object sender, EventArgs e)
        {
            AuthVKOnLoad();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //ReleaseCapture();
            //PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void vU_ButtonText5_Click(object sender, EventArgs e)
        {
            ChangePanel(Lessons);
            LoadLessons();
        }

        private void vU_ButtonText4_Click(object sender, EventArgs e) => ChangePanel(settingsPanel_Outer);

        private void vU_Button2_Click(object sender, EventArgs e)
        {

            if (SQL.User.type == "guest")
            {
                SQL.DeleteAccount();
            }
            else
            {
                SQL.DeleteMac(SQL.GetMAC());
            }
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void vU_Button1_Click(object sender, EventArgs e)
        {
            if (Login_Settings.Text != Login_Settings.GhostText)
            {

                if (Login_Settings.Text != SQL.User.login)
                {
                    if (SQL.User.type != "guest")
                    {
                        if (!SQL.ChangeLogin(Login_Settings.Text))
                        {
                            MsgBox("Изменение личных данных", "Ошибка изменения логина. Такой логин уже есть");
                        }
                        else MsgBox("Изменение личных данных", "Логин успешно изменён");
                    }
                    else
                    {
                        MsgBox("Изменение личных данных", "Измение логина в аккаунте гостя невозможно");
                    }
                }

            }
            else
                MsgBox("Изменение личных данных", "Заполните поле логина");

            if (Nickname_Settings.Text != Nickname_Settings.GhostText)
            {
                if (Nickname_Settings.Text != SQL.User.nickname)
                {
                    SQL.ChangeNickname(Nickname_Settings.Text);
                    MsgBox("Никнейм личных данных", "Никнейм успешно изменён");
                }
            }
            else MsgBox("Изменение личных данных", "Заполните поле никнейма");

            Login_Settings.Text = SQL.User.login;
            Nickname_Settings.Text = SQL.User.nickname;
        }

        private void vU_Button3_Click(object sender, EventArgs e)
        {
            if (OldPass_Settings.Text != OldPass_Settings.GhostText && NewPass_Settings.Text != NewPass_Settings.GhostText && dbl_NewPass_Settings.Text != dbl_NewPass_Settings.GhostText)
            {
                if (SQL.Authorize(SQL.User.login, OldPass_Settings.Text))
                {
                    if (NewPass_Settings.Text == dbl_NewPass_Settings.Text)
                    {
                        SQL.ChangePassword(NewPass_Settings.Text);
                        MsgBox("Смена пароля", "Смена пароля прошла успешно");
                        NewPass_Settings.Text = NewPass_Settings.GhostText;
                        dbl_NewPass_Settings.Text = dbl_NewPass_Settings.GhostText;
                        OldPass_Settings.Text = OldPass_Settings.GhostText;
                    }
                    else
                    {
                        MsgBox("Смена пароля", "Новые пароли не совпадают");
                    }
                }
                else
                {
                    MsgBox("Смена пароля", "Неверный старый пароль");
                }
            }
            else MsgBox("Смена пароля", "Заполните все поля");

        }

        private void vU_Button4_Click(object sender, EventArgs e)
        {
            SQL.DeleteAccount();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void vU_Button6_Click(object sender, EventArgs e)
        {
            CreateLesson createLesson = new CreateLesson();
            createLesson.ShowDialog();
            LoadLessons();
        }

        private void button2_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        private void vU_PanelWithScroll1_MouseMove(object sender, MouseEventArgs e)
        {
            //panel5.AutoScroll = true;
            //panel5.VerticalScroll.Enabled = true;
            //panel5.VerticalScroll.Visible = false;
            //vU_PanelWithScroll1.Maximum = settingsPanel.VerticalScroll.Maximum;
            //panel5.VerticalScroll.Value = vU_PanelWithScroll1.Value;
        }

        private void vU_PanelWithScroll1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void vU_Button5_Click(object sender, EventArgs e)
        {
            bool gotoVirtual = false;

            string ProcName = "VDSTU";
            var runningProcs = from proc in Process.GetProcesses(".") orderby proc.Id select proc;
            if (runningProcs.Count(p => p.ProcessName.Contains(ProcName)) != 0)
            {
                Process[] ps = Process.GetProcessesByName(ProcName);
                IntPtr hwnd = IntPtr.Zero;
                foreach (var p in ps)
                {
                    if (p.MainWindowHandle != IntPtr.Zero)
                    {
                        hwnd = p.MainWindowHandle;
                        break;
                    }
                }
                ShowWindow(hwnd, SW_MAXIMIZE);
                this.FormBorderStyle = FormBorderStyle.None;

                WindowState = FormWindowState.Minimized;


            }
            else
            {

                DialogResult exit = MessageBox.Show("Перейти в виртуальный университет?", "Переход в виртуальную среду", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (exit == DialogResult.Yes)
                    gotoVirtual = true;


                if (exit == DialogResult.No)
                    gotoVirtual = false;


                try
                {
                    if (gotoVirtual)
                    {
                        Process.Start(Directory.GetCurrentDirectory() + "\\" + ProcName + ".exe");
                        this.FormBorderStyle = FormBorderStyle.None;

                        WindowState = FormWindowState.Minimized;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибочка ¯\\_(ツ)_/¯");
                }
            }
        }

        private void vU_Button6_MouseDown(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                vU_Panel2.RoundingEnable = true;
                WindowState = FormWindowState.Normal;
            }

            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOSIZE, 0);
        }

        private void Alpha_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
            Refresh();
        }

        private void MajorForm_ResizeEnd(object sender, EventArgs e)
        {
            Refresh();
        }

        private void vU_Button7_Click(object sender, EventArgs e)
        {
            if (IDLesson_TextBox.Text.Length == 6)
            {
                if (SQL.SQLCounter("SELECT id FROM Lessons WHERE UniqueKey = " + IDLesson_TextBox.Text) != 0)
                {
                    //var result = SQL.FullRead($"SELECT id, name, description, type, UniqueKey FROM Lessons WHERE type LIKE 'public'");
                    SQL.Lesson.UniqueKey = IDLesson_TextBox.Text;
                    SQL.Lesson.name = SQL.Read("SELECT name FROM Lessons WHERE UniqueKey = " + IDLesson_TextBox.Text);
                    SQL.Lesson.id = SQL.Read("SELECT id FROM Lessons WHERE UniqueKey = " + IDLesson_TextBox.Text);
                    SQL.Lesson.password = SQL.Read("SELECT password FROM Lessons WHERE UniqueKey = " + IDLesson_TextBox.Text);
                    //FindForm().WindowState = FormWindowState.Minimized;

                    if (SQL.Lesson.password == "")
                    {
                        Lesson lesson = new Lesson();
                        lesson.Show();
                        lesson.WindowState = FormWindowState.Normal;
                        Hide();
                    }
                    else
                    {
                        EnterPassword entpass = new EnterPassword();
                        entpass.ShowDialog();
                        if (SQL.Lesson.password == "Correct")
                        {
                            Lesson lesson = new Lesson();
                            lesson.Show();
                            lesson.WindowState = FormWindowState.Normal;
                            Hide();
                        }
                    }


                    IDLesson_TextBox.Text = IDLesson_TextBox.GhostText;
                    //IDLesson_TextBox.Text;
                }
                else
                {
                    MsgBox("Ошибка", "Пара с ID " + IDLesson_TextBox.Text + " не найдена");
                }
            }
            else
            {
                MsgBox("Ошибка", "ID должен состоять из шести цифр");
            }
        }

        private void CreateLesson_button_Click(object sender, EventArgs e)
        {
            CreateLesson createLesson = new CreateLesson();
            createLesson.ShowDialog();
            LoadLessons();
        }

        private void MajorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void InformationPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void items_Paint(object sender, PaintEventArgs e)
        {

        }

        bool OnlyOnce = false;
        private void vU_ButtonText1_Click(object sender, EventArgs e)
        {
            ChangePanel(NewsPanel_Outer);

            if (!OnlyOnce && VK.IsAuthorized)
            {
                NewsPanel_Inner.Controls.Clear();
                LoadNews();
                OnlyOnce = true;
            }
            //NewsPanel.Controls.Clear();
            //LoadNews();
        }
    }
    #endregion

}

