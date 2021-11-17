using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using CustomClasses;
using NAudio.Wave;
using Overlay_Client_DSTU.Properties;

namespace Overlay_Client_DSTU
{
    public partial class Lesson : Form
    {
        #region --- Подключение веб-камеры ---
        private VideoCaptureDevice videoSource;

        #endregion

        #region --- Для перетаскивания формы ---
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        const uint WM_SYSCOMMAND = 0x0112;
        const uint DOMOVE = 0xF012;
        const uint DOSIZE = 0xF008;
        #endregion



        public Lesson()
        {
            InitializeComponent();
        }


        //Для переходу в среду
        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private void Alpha_MouseDown(object sender, MouseEventArgs e)
        {
            vU_Panel2.RoundingEnable = true;
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            vU_Panel2.RoundingEnable = true;
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Lesson_Load(object sender, EventArgs e)
        {
            Alpha.Text = "Alpha | " + SQL.Lesson.name + " | Уникальный идентификатор пары: " + SQL.Lesson.UniqueKey;
            CommunicationCenter.form = this;
            CommunicationCenter.client = new UdpClient();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void vU_Button2_Click(object sender, EventArgs e)
        {
            if (!CommunicationCenter.Information.webcamIsWorking)
            {
                SelectWebcam selectWebcam = new SelectWebcam();
                selectWebcam.ShowDialog();
                if (CommunicationCenter.Information.selectedWebcam != "null")
                {
                    videoSource = new VideoCaptureDevice(CommunicationCenter.Information.selectedWebcam);

                    videoSource.NewFrame += VideoSource_NewFrame;
                    videoSource.Start();
                    CommunicationCenter.Information.webcamIsWorking = true;
                    vU_Button2.ShadowColor = Color.White;
                    vU_Button2.BackColor = Color.FromArgb(109, 56, 56);
                }
            }
            else
            {
                videoSource.Stop();
                CommunicationCenter.Information.webcamIsWorking = false;
                vU_Button2.ShadowColor = Color.Black;
                vU_Button2.BackColor = Color.FromArgb(58, 58, 58);
                //Thread.Sleep(500);
                //pictureBox1.Image = null;
                //pictureBox1.Refresh();
            }
        }

        int count = 0;
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            count++;

            //Task.Run(() => CommunicationCenter.SendPackage(new Package(SQL.User.id, SQL.Lesson.id, Package.Types.WebCam, CommunicationCenter.ObjToArr(new partImage(bitmap, bitmap.Width, bitmap.Height, 0, 0, bitmap.Width, bitmap.Height)))));
            if (count % 5 == 0)
            {
                Bitmap minibitmap = (Bitmap)eventArgs.Frame.Clone();
                minibitmap = new Bitmap(minibitmap, (int)(50 * (float)minibitmap.Width / (float)minibitmap.Height), 50);
                var ms1 = new MemoryStream();
                minibitmap.Save(ms1, ImageFormat.Jpeg);
                minibitmap = new Bitmap(ms1);
                Task.Run(() => CommunicationCenter.SendPackage(new Package(SQL.User.id, SQL.Lesson.id, Package.Types.MiniWebcam, CommunicationCenter.ObjToArr(new partImage(CommunicationCenter.ImgToArr(minibitmap), minibitmap.Width, minibitmap.Height, 0, 0, minibitmap.Width, minibitmap.Height)))));
            }

            if (count % 5 == 0)
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                bitmap = new Bitmap(bitmap, (int)(200 * (float)bitmap.Width / (float)bitmap.Height), 200);
                var cc = new CommunicationCenter();
                Task.Run(() => cc.SplitImageAndSend(bitmap, Package.Types.WebCam));
            }

            if (count > 1000) count = 0;
        }

        private void Lesson_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MajorForm.MainForm.Show();

                if (!CommunicationCenter.Disconnect(0, 10))
                    SQL.ChangeIP("null");

                //MajorForm.MainForm.WindowState = FormWindowState.Maximized;

                CommunicationCenter.receiveIsWork = false;
                CommunicationCenter.WebCams.Clear();
                CommunicationCenter.Miniatures.Clear();
                CommunicationCenter.Users.Clear();
                CommunicationCenter.Screens.Clear();

                if (CommunicationCenter.output != null)
                    CommunicationCenter.output.Stop();

                CommunicationCenter.Audios.Clear();
                CommunicationCenter.currentStream = ("", Package.Types.unknown);
                ScreenIsWork = false;

                CommunicationCenter.client.Close();

                CommunicationCenter.client.Dispose();

                if (CommunicationCenter.Information.webcamIsWorking)
                {
                    videoSource.Stop();
                    CommunicationCenter.Information.webcamIsWorking = false;
                }

                if (CommunicationCenter.Information.microIsWorking)
                {
                    CommunicationCenter.input.StopRecording();
                    CommunicationCenter.input.Dispose();
                }

                Updater.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            this.Dispose();
        }

        private void Lesson_Shown(object sender, EventArgs e)
        {


            CommunicationCenter.Information.IPSender = IPAddress.Parse(SQL.Read("SELECT Server_IP FROM Lessons WHERE UniqueKey = " + SQL.Lesson.UniqueKey));

            for (int i = listPanel_Inner.Controls.Count - 1; i >= 0; i--)
            {
                if (listPanel_Inner.Controls[i].Name != "buttonControl")
                {
                    listPanel_Inner.Controls.RemoveAt(i);
                    i--;
                }
            }

            pictureBox1.Image = new Bitmap(1, 1);
            CommunicationCenter.GlobalImage = new Bitmap(1, 1);

            if (!CommunicationCenter.Connect(0, 10, CommunicationCenter.Information.IPSender.ToString(), true))
            {
                Console.WriteLine("Ошибка: " + CommunicationCenter.Information.IPSender.ToString());
                MajorForm.MsgBox("Ошибка", "Ошибка подключения к серверам. Повторите попытку");
                Close();
            }
            else
            {
                //CommunicationCenter.IPs = SQL.GetIps();
                //Task.Run(() => CommunicationCenter.UpdateIp());
                //Task.Run(() => CommunicationCenter.UpdateConnect());

                Task.Run(() => CommunicationCenter.MainReceive());

                SelectAudio audioForm = new SelectAudio();
                audioForm.ShowDialog();

                CommunicationCenter.Information.nowIsStream = false;
                buttonControl.Text = "Посмотреть трансляции";

                if (!CommunicationCenter.Information.headIsWorking && !CommunicationCenter.Information.microIsWorking)
                {
                    MajorForm.MsgBox("Предупреждение", "Аудиоустройства не установлены");
                }
                else
                {
                    if (CommunicationCenter.Information.microIsWorking)
                    {
                        CommunicationCenter.input = new WaveIn();
                        CommunicationCenter.input.WaveFormat = new WaveFormat(8000, 8, 1);
                        CommunicationCenter.input.DeviceNumber = CommunicationCenter.Information.selectedMicro;
                        CommunicationCenter.input.DataAvailable += Input_DataAvailable;
                        CommunicationCenter.input.StartRecording();
                        vU_Button1.ShadowColor = Color.White;
                        vU_Button1.BackColor = Color.FromArgb(109, 56, 56);
                    }
                    else
                    {
                        MajorForm.MsgBox("Предупреждение", "Микрофон не установлен");
                    }

                    if (CommunicationCenter.Information.headIsWorking)
                    {

                        CommunicationCenter.output = new WaveOut();
                        CommunicationCenter.output.DeviceNumber = CommunicationCenter.Information.selectedHead;
                        CommunicationCenter.output.Init(CommunicationCenter.waveMixer);
                        CommunicationCenter.output.Play();
                        vU_Button5.ShadowColor = Color.White;
                        vU_Button5.BackColor = Color.FromArgb(109, 56, 56);
                    }
                    else
                    {
                        MajorForm.MsgBox("Предупреждение", "Аудиовыход не установлен");
                    }
                }
                Task.Run(() => CommunicationCenter.HoldOn());

            }
        }

        private void Input_DataAvailable(object sender, WaveInEventArgs e)
        {
            CommunicationCenter.SendPackage(new Package(SQL.User.id, SQL.Lesson.id, Package.Types.Audio, CommunicationCenter.ObjToArr(new partAudio(true, e.Buffer))));
        }

        private void vU_ImFromUser1_Click(object sender, EventArgs e)
        {

            for (int i = listPanel_Inner.Controls.Count - 1; i >= 0; i--)
            {
                if (listPanel_Inner.Controls[i].Name != "buttonControl")
                {
                    listPanel_Inner.Controls.RemoveAt(i);
                }
            }

            CommunicationCenter.Information.nowIsStream = !CommunicationCenter.Information.nowIsStream;

            if (CommunicationCenter.Information.nowIsStream)
            {
                CommunicationCenter.Miniatures.Clear();
                buttonControl.Text = "Показать участников";
            }
            else
            {
                CommunicationCenter.Users.Clear();
                buttonControl.Text = "Посмотреть трансляции";
            }
            //pictureBox1.Image = null;
            //pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void vU_Panel3_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Lesson_ResizeEnd(object sender, EventArgs e)
        {
            Refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                vU_Panel2.RoundingEnable = false;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                vU_Panel2.RoundingEnable = true;
                WindowState = FormWindowState.Normal;
            }
            Refresh();
        }

        private void vU_Button4_MouseDown(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                vU_Panel2.RoundingEnable = true;
                WindowState = FormWindowState.Normal;
            }

            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOSIZE, 0);
        }

        private void vU_Button3_Click(object sender, EventArgs e)
        {
            if (!ScreenIsWork)
            {
                vU_Button3.ShadowColor = Color.White;
                vU_Button3.BackColor = Color.FromArgb(109, 56, 56);
                ScreenIsWork = true;
                Task.Run(() => SendScreen());
            }
            else
            {
                vU_Button3.ShadowColor = Color.Black;
                vU_Button3.BackColor = Color.FromArgb(58, 58, 58);
                ScreenIsWork = false;
            }

        }

        bool ScreenIsWork = false;
        private void SendScreen()
        {
            var cc = new CommunicationCenter();
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            while (ScreenIsWork)
            {
                //Graphics graphicsScreen = Graphics.FromImage((Image)printscreen);

                try
                {
                    printscreen = CommunicationCenter.ScreenCapturePInvoke.CapturePrimaryScreen(true);

                    //Bitmap TempBitmap = new Bitmap(printscreen, (int)(150 * (float)printscreen.Width / (float)printscreen.Height), 150);

                    //var ms = new MemoryStream();
                    //TempBitmap.Save(ms, ImageFormat.Jpeg);
                    //TempBitmap = new Bitmap(ms);
                    Bitmap minibitmap;

                    if (printscreen.Width > printscreen.Height)
                        minibitmap = new Bitmap(printscreen, 250, (int)(250 * (float)printscreen.Height / (float)printscreen.Width));
                    else
                        minibitmap = new Bitmap(printscreen, (int)(150 * (float)printscreen.Width / (float)printscreen.Height), 150);

                    Bitmap bigbitmap = new Bitmap(printscreen, (int)(500 * (float)printscreen.Width / (float)printscreen.Height), 500);

                    var ms1 = new MemoryStream();
                    minibitmap.Save(ms1, ImageFormat.Jpeg);
                    minibitmap = new Bitmap(ms1);

                    Package pack = new Package(SQL.User.id, SQL.Lesson.id, Package.Types.MiniScreen, CommunicationCenter.ObjToArr(new partImage(CommunicationCenter.ImgToArr(minibitmap), minibitmap.Width, minibitmap.Height, 0, 0, minibitmap.Width, minibitmap.Height)));

                    for (int i = 0; i < 5; i++)
                    {
                        Task.Run(() => CommunicationCenter.SendPackage(pack));
                    }

                    Task.Run(() => cc.SplitImageAndSend(bigbitmap, Package.Types.Screen));

                    Thread.Sleep(200);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void vU_Button1_Click(object sender, EventArgs e)
        {
            if (CommunicationCenter.Information.microIsWorking)
            {
                CommunicationCenter.Information.microIsWorking = false;
                CommunicationCenter.input.StopRecording();
                vU_Button1.ShadowColor = Color.Black;
                vU_Button1.BackColor = Color.FromArgb(58, 58, 58);
            }
            else
            {
                CommunicationCenter.Information.microIsWorking = true;
                CommunicationCenter.input.StartRecording();
                vU_Button1.ShadowColor = Color.White;
                vU_Button1.BackColor = Color.FromArgb(109, 56, 56);
            }
        }

        private void vU_Button5_Click(object sender, EventArgs e)
        {
            if (CommunicationCenter.Information.headIsWorking)
            {
                CommunicationCenter.Information.headIsWorking = false;
                CommunicationCenter.output.Pause();
                vU_Button5.ShadowColor = Color.Black;
                vU_Button5.BackColor = Color.FromArgb(58, 58, 58);
            }
            else
            {
                CommunicationCenter.Information.headIsWorking = true;
                CommunicationCenter.output.Play();
                vU_Button5.ShadowColor = Color.White;
                vU_Button5.BackColor = Color.FromArgb(109, 56, 56);
            }
        }

        private void vU_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void vU_Button6_Click(object sender, EventArgs e)
        {
            try
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

                    if (CommunicationCenter.Information.webcamIsWorking || ScreenIsWork)
                    {
                        DialogResult exit = MessageBox.Show("У Вас запущены трансляции. Оставить их?", "Переход в виртуальную среду", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (exit == DialogResult.Yes)
                            gotoVirtual = true;


                        if (exit == DialogResult.No)
                        {
                            gotoVirtual = true;
                            if (ScreenIsWork)
                            {
                                vU_Button3.ShadowColor = Color.Black;
                                vU_Button3.BackColor = Color.FromArgb(58, 58, 58);
                                ScreenIsWork = false;
                            }

                            if (CommunicationCenter.Information.webcamIsWorking)
                            {
                                videoSource.Stop();
                                CommunicationCenter.Information.webcamIsWorking = false;
                                vU_Button2.ShadowColor = Color.Black;
                                vU_Button2.BackColor = Color.FromArgb(58, 58, 58);
                            }
                        }

                        if (exit == DialogResult.Cancel)
                            gotoVirtual = false;


                    }
                    else
                    {
                        DialogResult exit = MessageBox.Show("Перейти в виртуальный университет?", "Переход в виртуальную среду", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (exit == DialogResult.Yes)
                            gotoVirtual = true;


                        if (exit == DialogResult.No)
                            gotoVirtual = false;
                    }

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
                        MessageBox.Show(ex.Message, "Ошибочка " + CommunicationCenter.GetRandomText());
                    }


                }



            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CommunicationCenter.receiveIsWork)
            {
                try
                {
                    pictureBox1.Image = CommunicationCenter.GlobalImage;

                    foreach (var min in CommunicationCenter.Miniatures)
                    {
                        min.Value.Item1.BackgroundImage = CommunicationCenter.ArrToImg(min.Value.Item2);
                    }
                }
                catch (Exception ex)
                {
                    //Здесь вылетает из-за того что image пытается переприсвоится одновременно с каким-то другим процессом
                    //Объект занят и не может быть использован (или что-то в этом роде)
                    //Поэтому игнорируем ошибку и ставим заглушку
                }
            }


        }

        private void Checker_Tick(object sender, EventArgs e)
        {
            for (int i = listPanel_Inner.Controls.Count - 1; i >= 0; i--)
            {
                if (listPanel_Inner.Controls[i].Name != "buttonControl")
                {
                    if (CommunicationCenter.Information.nowIsStream)
                    {
                        if (CommunicationCenter.Miniatures.ContainsKey((int.Parse((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID), Package.Types.MiniScreen)))
                        {
                            if (DateTime.Now - CommunicationCenter.Miniatures[(int.Parse((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID), Package.Types.MiniScreen)].Item3 > new TimeSpan(0, 0, 10))
                            {
                                if ((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerType == Package.Types.Screen)
                                {
                                    if (((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID, Package.Types.Screen) == CommunicationCenter.currentStream)
                                    {

                                        pictureBox1.Image = new Bitmap(1, 1);
                                        CommunicationCenter.GlobalImage = new Bitmap(1, 1);
                                        CommunicationCenter.Miniatures.Remove((int.Parse((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID), Package.Types.MiniScreen));
                                        listPanel_Inner.Controls.Remove(listPanel_Inner.Controls[i]);

                                        CommunicationCenter.currentStream = ((listPanel_Inner.Controls[listPanel_Inner.Controls.Count - 1] as VU_ImFromUser).OwnerID, (listPanel_Inner.Controls[listPanel_Inner.Controls.Count - 1] as VU_ImFromUser).OwnerType);
                                    }
                                    else
                                    {
                                        CommunicationCenter.Miniatures.Remove((int.Parse((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID), Package.Types.MiniScreen));
                                        listPanel_Inner.Controls.RemoveAt(i);
                                    }

                                    i--;
                                }
                            }
                        }

                        if (CommunicationCenter.Miniatures.ContainsKey((int.Parse((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID), Package.Types.MiniWebcam)))
                        {
                            if (DateTime.Now - CommunicationCenter.Miniatures[(int.Parse((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID), Package.Types.MiniWebcam)].Item3 > new TimeSpan(0, 0, 10))
                            {
                                if ((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerType == Package.Types.WebCam)
                                {
                                    if (((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID, Package.Types.WebCam) == CommunicationCenter.currentStream)
                                    {
                                        pictureBox1.Image = new Bitmap(1, 1);
                                        CommunicationCenter.GlobalImage = new Bitmap(1, 1);
                                        CommunicationCenter.Miniatures.Remove((int.Parse((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID), Package.Types.MiniWebcam));
                                        listPanel_Inner.Controls.RemoveAt(i);
                                        CommunicationCenter.currentStream = ((listPanel_Inner.Controls[listPanel_Inner.Controls.Count - 1] as VU_ImFromUser).OwnerID, (listPanel_Inner.Controls[listPanel_Inner.Controls.Count - 1] as VU_ImFromUser).OwnerType);

                                    }
                                    else
                                    {
                                        CommunicationCenter.Miniatures.Remove((int.Parse((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID), Package.Types.MiniWebcam));
                                        listPanel_Inner.Controls.RemoveAt(i);
                                    }
                                    i--;
                                }
                            }
                        }
                    }
                    else
                    {
                        // MessageBox.Show("Проверка");
                        if (CommunicationCenter.Users.ContainsKey((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID))
                        {
                            if (DateTime.Now - CommunicationCenter.Users[(listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID].Item2 > new TimeSpan(0, 0, 5))
                            {
                                CommunicationCenter.Users.Remove((listPanel_Inner.Controls[i] as VU_ImFromUser).OwnerID);
                                listPanel_Inner.Controls.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
            }
        }
    }
}
