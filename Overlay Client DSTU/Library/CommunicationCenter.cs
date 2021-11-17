using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomClasses;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing.Imaging;
using AForge.Imaging;
using NAudio.Wave;
using System.IO.Compression;
using Overlay_Client_DSTU.Properties;

namespace Overlay_Client_DSTU
{
    public class CommunicationCenter
    {
        #region --- Константы и переменные ---
        public static int FreePort = 27005;
        public static Lesson form;
        public static UdpClient client = new UdpClient();
        public static List<UdpClient> clientsReceiver = new List<UdpClient>();
        public static bool receiveIsWork = true;
        public static (string, Package.Types) currentStream = ("", Package.Types.unknown);

        //поток для нашей речи
        public static WaveIn input;
        //поток для речи собеседника
        public static WaveOut output;
        public static MixingWaveProvider32 waveMixer = new MixingWaveProvider32();

        //буфферный поток для передачи через сеть
        public static BufferedWaveProvider bufferStream;
        #endregion

        #region --- Съемка экрана --- 
        public static class ScreenCapturePInvoke
        {
            [StructLayout(LayoutKind.Sequential)]
            private struct CURSORINFO
            {
                public Int32 cbSize;
                public Int32 flags;
                public IntPtr hCursor;
                public POINTAPI ptScreenPos;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct POINTAPI
            {
                public int x;
                public int y;
            }

            [DllImport("user32.dll")]
            private static extern bool GetCursorInfo(out CURSORINFO pci);

            [DllImport("user32.dll", SetLastError = true)]
            static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

            private const Int32 CURSOR_SHOWING = 0x0001;
            private const Int32 DI_NORMAL = 0x0003;

            public static Bitmap CaptureFullScreen(bool captureMouse)
            {
                var allBounds = Screen.AllScreens.Select(s => s.Bounds).ToArray();
                Rectangle bounds = Rectangle.FromLTRB(allBounds.Min(b => b.Left), allBounds.Min(b => b.Top), allBounds.Max(b => b.Right), allBounds.Max(b => b.Bottom));

                var bitmap = CaptureScreen(bounds, captureMouse);
                return bitmap;
            }

            public static Bitmap CapturePrimaryScreen(bool captureMouse)
            {
                Rectangle bounds = Screen.PrimaryScreen.Bounds;

                var bitmap = CaptureScreen(bounds, captureMouse);
                return bitmap;
            }

            public static Bitmap CaptureScreen(Rectangle bounds, bool captureMouse)
            {
                Bitmap result = new Bitmap(bounds.Width, bounds.Height);

                try
                {
                    using (Graphics g = Graphics.FromImage(result))
                    {
                        g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);

                        if (captureMouse)
                        {
                            CURSORINFO pci;
                            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                            if (GetCursorInfo(out pci))
                            {
                                if (pci.flags == CURSOR_SHOWING)
                                {
                                    var hdc = g.GetHdc();
                                    DrawIconEx(hdc, pci.ptScreenPos.x - bounds.X, pci.ptScreenPos.y - bounds.Y, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                                    g.ReleaseHdc();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    result = null;
                }

                return result;
            }
        }
        #endregion

        #region --- Отдел отправки и приёма ---
        static bool onlyOnce = false;
        public static bool Connect(int startIteration, int Max, string IP, bool isMain) //Рекурсивное подключение к серверу из-за потерь по протоколу UDP. Iteration - счет кол-ва отправок пакетов
        {
            startIteration++;

            //FreePort = 27006;

            //for (int i = 0; i < Information.ServerIPs.Count; i++)
            //{
            //    clientsReceiver[i].SendAsync(temp, temp.Length, new IPEndPoint(Information.ServerIPs[i], FreePort)); //Connect
            //    FreePort++;
            //}

            if (isMain)
            {
                Console.WriteLine("Подключаемся к серверу:");

                byte[] temp = ObjToArr(new Package(true, true, SQL.User.id, SQL.Lesson.id));

                Console.WriteLine(Information.IPSender + ": " + FreePort);

                client.Connect(new IPEndPoint(Information.IPSender, FreePort));

                client.Send(temp, temp.Length);//, new IPEndPoint(Information.IPSender, PortSender)

                Thread.Sleep(100);

                if (!SQL.CheckIP(IP))
                {
                    if (startIteration >= Max)
                        return false;
                    else
                        return Connect(startIteration, Max, IP, true);
                }
                else
                {
                    Information.ServerIPs.Add(Information.IPSender.ToString());
                    //FreePort++;
                    return true;
                }
            }
            else
            {
                byte[] temp = ObjToArr(new Package(true, false, SQL.User.id, SQL.Lesson.id));
                if (clientsReceiver.Count < (FreePort - 27005))
                {
                    Console.WriteLine("Добавили новый UDPClient");
                    clientsReceiver.Add(new UdpClient(FreePort));
                }
                Console.WriteLine("ID Нового: " + (FreePort - 27006));
                clientsReceiver[FreePort - 27006].Connect(new IPEndPoint(IPAddress.Parse(IP), FreePort));
                clientsReceiver[FreePort - 27006].Send(temp, temp.Length);//, new IPEndPoint(Information.IPSender, PortSender)

                Thread.Sleep(100);

                if (!SQL.CheckIP(IP))
                {
                    if (startIteration >= Max)
                        return false;
                    else
                        return Connect(startIteration, Max, IP, false);
                }
                else
                {
                    FreePort++;
                    Console.WriteLine("ПОДКЛЮЧЕН");
                    return true;
                }
            }
        }

        public static bool Disconnect(int startIteration, int Max) //Рекурсивное подключение к серверу из-за потерь по протоколу UDP. Iteration - счет кол-ва отправок пакетов
        {
            startIteration++;
            byte[] temp = ObjToArr(new Package(false, true, SQL.User.id, SQL.Lesson.id));
            client.SendAsync(temp, temp.Length);//, new IPEndPoint(Information.IPSender, PortSender)
            Thread.Sleep(100);

            if (!SQL.CheckIP("null"))
            {
                if (startIteration >= Max)
                    return false;
                else
                    return Disconnect(startIteration, Max);
            }
            else
                return true;
        }

        public static void SendPackage(Package package)
        {
            try
            {
                byte[] temp = ObjToArr(package);

                //foreach (IPEndPoint ip in IPs)
                //{
                //    //Console.WriteLine(ip.ToString());
                //    client.SendAsync(temp, temp.Length, ip);
                //}
                //client.Connect(new IPEndPoint(Information.IPSender, PortSender));

                client.SendAsync(temp, temp.Length);

                GC.Collect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void HoldOn()
        {
            while (CommunicationCenter.receiveIsWork)
            {
                var temp = new Package(Package.Types.HoldOn, SQL.User.id, SQL.Lesson.id);


                CommunicationCenter.SendPackage(temp);

                //foreach (IPEndPoint ip in IPs)
                //{
                //    //Console.WriteLine(ip.ToString());
                //    client.SendAsync(temp, temp.Length, ip);
                //}
                Thread.Sleep(1000);
            }
        }

        public static void UpdateIp()
        {
            //IPs = SQL.GetIps();
            //Thread.Sleep(1000);
            //UpdateIp();
        }


        public static void UpdateConnect()
        {
            //Console.WriteLine("Старт апдейтера");

            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Апдейт");

                var connections = SQL.FullRead("SELECT ServerIP FROM Link_ClientSystem WHERE RoomID = " + SQL.Lesson.id);
                //Console.WriteLine("1");

                for (int i = 0; i < connections.Count; i++)
                {
                    // Console.WriteLine("2");

                    //Console.WriteLine("Кол-во: " + Information.ServerIPs.FindIndex(x => x == connections[i][0]));
                    if (Information.ServerIPs.FindIndex(x => x == connections[i][0]) == -1)
                    {
                        //Console.WriteLine("3");

                        if (Connect(0, 10, connections[i][0], false))
                        {
                            //Console.WriteLine("4");


                            Information.ServerIPs.Add(connections[i][0]);

                            var CommCenter = new CommunicationCenter();
                            Task.Run(() => CommCenter.Receive());
                            //Console.WriteLine("5");

                            Console.WriteLine(Information.ServerIPs.Count);

                            Console.WriteLine(Information.ServerIPs.FindIndex(x => x == connections[i][0]));

                            Console.WriteLine(connections[i][0]);

                        }
                    }
                    //connections[i][0]
                }
                //  Console.WriteLine("6");

            }
        }

        public static Dictionary<int, Bitmap> WebCams = new Dictionary<int, Bitmap>();
        public static Dictionary<int, Bitmap> Screens = new Dictionary<int, Bitmap>();
        public static Dictionary<int, BufferedWaveProvider> Audios = new Dictionary<int, BufferedWaveProvider>();

        public static Dictionary<string, (VU_ImFromUser, DateTime)> Users = new Dictionary<string, (VU_ImFromUser, DateTime)>();
        public static Dictionary<(int, Package.Types), (VU_ImFromUser, byte[], DateTime)> Miniatures = new Dictionary<(int, Package.Types), (VU_ImFromUser, byte[], DateTime)>();

        public static Bitmap GlobalImage = new Bitmap(1, 1);
        public void Receive()
        {

            //CommunicationCenter.receiveIsWork = true;

            //IPEndPoint RemoteIpEndPoint = null;

            //Console.WriteLine("Запуск receive");

            //while (true)
            //{
            //    Console.WriteLine("ID Прослушивания: " + (FreePort - 27007));
            //    var temp = clientsReceiver[FreePort - 27007].Receive(ref RemoteIpEndPoint);

            //    Console.WriteLine("Пришло receive");


            //    try
            //    {
            //        Package pack = ArrToObj(temp) as Package;

            //        if (currentStream.Item1 == pack.ownerID && currentStream.Item2 == pack.type)
            //        {

            //            if (pack.type == Package.Types.WebCam)
            //            {
            //                var bmp = (ArrToObj(pack.data) as partImage);

            //                if (!WebCams.ContainsKey(int.Parse(pack.ownerID)))
            //                    WebCams.Add(int.Parse(pack.ownerID), new Bitmap(bmp.max_x, bmp.max_y));

            //                var graphics = Graphics.FromImage(WebCams[int.Parse(pack.ownerID)]);
            //                graphics.DrawImage(ArrToObj(bmp.bitmap) as Bitmap, new Rectangle(bmp.left * bmp.width, bmp.top * bmp.height, bmp.width, bmp.height), new Rectangle(0, 0, bmp.width, bmp.height), GraphicsUnit.Pixel);

            //                graphics.Dispose();

            //                form.Invoke(new Action(() => { form.pictureBox1.Image = WebCams[int.Parse(pack.ownerID)]; }));
            //            }

            //            if (pack.type == Package.Types.Screen)
            //            {
            //                //Console.WriteLine("ЭКРАН");
            //                var bmp = (ArrToObj(pack.data) as partImage);
            //                //Console.WriteLine("Пришло: " + bmp.left + " | " + bmp.top);
            //                if (!Screens.ContainsKey(int.Parse(pack.ownerID)))
            //                    Screens.Add(int.Parse(pack.ownerID), new Bitmap(bmp.max_x, bmp.max_y));

            //                var graphics = Graphics.FromImage(Screens[int.Parse(pack.ownerID)]);
            //                graphics.DrawImage(ArrToObj(bmp.bitmap) as Bitmap, new Rectangle(bmp.left * bmp.width, bmp.top * bmp.height, bmp.width, bmp.height), new Rectangle(0, 0, bmp.width, bmp.height), GraphicsUnit.Pixel);

            //                graphics.Dispose();



            //                form.Invoke(new Action(() =>
            //                {
            //                    form.pictureBox1.Image = Screens[int.Parse(pack.ownerID)];
            //                }));
            //            }
            //        }

            //        if (pack.type == Package.Types.MiniWebcam)
            //        {

            //            var bmp = (ArrToObj(pack.data) as partImage);

            //            if (!Miniatures.ContainsKey((int.Parse(pack.ownerID), Package.Types.MiniWebcam)))
            //            {
            //                VU_ImFromUser imfromUser = new VU_ImFromUser();
            //                imfromUser.BackgroundImage = ArrToObj(bmp.bitmap) as Bitmap;
            //                imfromUser.Text = SQL.Read("SELECT nickname FROM Accounts WHERE id = " + pack.ownerID);
            //                imfromUser.OwnerID = pack.ownerID;
            //                imfromUser.OwnerType = Package.Types.WebCam;
            //                Miniatures.Add((int.Parse(pack.ownerID), Package.Types.MiniWebcam), imfromUser);
            //                form.Invoke(new Action(() =>
            //                {
            //                    form.listPanel_Inner.Controls.Add(Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniWebcam)]);
            //                }));

            //            }
            //            else
            //            {
            //                Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniWebcam)].BackgroundImage = ArrToObj(bmp.bitmap) as Bitmap;
            //            }

            //        }

            //        if (pack.type == Package.Types.MiniScreen)
            //        {

            //            var bmp = (ArrToObj(pack.data) as partImage);

            //            if (!Miniatures.ContainsKey((int.Parse(pack.ownerID), Package.Types.MiniScreen)))
            //            {
            //                VU_ImFromUser imfromUser = new VU_ImFromUser();
            //                imfromUser.BackgroundImage = ArrToObj(bmp.bitmap) as Bitmap;
            //                imfromUser.Text = SQL.Read("SELECT nickname FROM Accounts WHERE id = " + pack.ownerID);
            //                imfromUser.OwnerID = pack.ownerID;
            //                imfromUser.OwnerType = Package.Types.Screen;
            //                Miniatures.Add((int.Parse(pack.ownerID), Package.Types.MiniScreen), imfromUser);
            //                form.Invoke(new Action(() =>
            //                {
            //                    form.listPanel_Inner.Controls.Add(Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniScreen)]);
            //                }));

            //            }
            //            else
            //            {
            //                Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniScreen)].BackgroundImage = ArrToObj(bmp.bitmap) as Bitmap;
            //            }
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        //MajorForm.MsgBox("Ошибка", ex.Message);
            //    }
            //    //Console.WriteLine("Выполнено");

            //}
        }
        public static void MainReceive()
        {
            CommunicationCenter.receiveIsWork = true;

            //try
            //{
            //    if (client != null)
            //        Console.WriteLine("Слушаем " + client.Client.RemoteEndPoint.ToString());
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);

            IPEndPoint RemoteIpEndPoint = null;

            //}
            while (receiveIsWork)
            {
                //client.Connect(new IPEndPoint(Information.IPSender, PortSender));

                //Thread.Sleep(100);
                //Console.WriteLine("Старт");

                var temp = client.Receive(ref RemoteIpEndPoint);
                //Console.WriteLine("Получено");
                try
                {
                    Package pack = ArrToObj(temp) as Package;

                    if (currentStream.Item1 == pack.ownerID && currentStream.Item2 == pack.type)
                    {

                        if (pack.type == Package.Types.WebCam)
                        {
                            var bmp = (ArrToObj(pack.data) as partImage);

                            if (!WebCams.ContainsKey(int.Parse(pack.ownerID)))
                                WebCams.Add(int.Parse(pack.ownerID), new Bitmap(bmp.max_x, bmp.max_y));

                            var graphics = Graphics.FromImage(WebCams[int.Parse(pack.ownerID)]);
                            graphics.DrawImage(ArrToImg(bmp.bitmap), new Rectangle(bmp.left * bmp.width, bmp.top * bmp.height, bmp.width, bmp.height), new Rectangle(0, 0, bmp.width, bmp.height), GraphicsUnit.Pixel);

                            graphics.Dispose();

                            GlobalImage = WebCams[int.Parse(pack.ownerID)];

                            //form.Invoke(new Action(() => { form.pictureBox1.Image = ; }));
                        }

                        if (pack.type == Package.Types.Screen)
                        {
                            //Console.WriteLine("ЭКРАН");
                            var bmp = (ArrToObj(pack.data) as partImage);
                            //Console.WriteLine("Пришло: " + bmp.left + " | " + bmp.top);
                            if (!Screens.ContainsKey(int.Parse(pack.ownerID)))
                                Screens.Add(int.Parse(pack.ownerID), new Bitmap(bmp.max_x, bmp.max_y));
                            var graphics = Graphics.FromImage(Screens[int.Parse(pack.ownerID)]);
                            graphics.DrawImage(ArrToImg(bmp.bitmap), new Rectangle(bmp.left * bmp.width, bmp.top * bmp.height, bmp.width, bmp.height), new Rectangle(0, 0, bmp.width, bmp.height), GraphicsUnit.Pixel);
                            graphics.Dispose();

                            GlobalImage = Screens[int.Parse(pack.ownerID)];
                            //form.Invoke(new Action(() => { form.pictureBox1.Image = Screens[int.Parse(pack.ownerID)]; }));
                        }
                    }

                    if (pack.type == Package.Types.Audio)
                    {
                        if (pack.ownerID != SQL.User.id)
                        {
                            if (!Audios.ContainsKey(int.Parse(pack.ownerID)))
                            {
                                WaveFormat waveFormat3 = new WaveFormat(8000, 8, 1);
                                Audios.Add(int.Parse(pack.ownerID), new BufferedWaveProvider(waveFormat3) { DiscardOnBufferOverflow = true });
                                WaveFloatTo16Provider WaveFloatTo16Provider = new WaveFloatTo16Provider(Audios[int.Parse(pack.ownerID)]);
                                waveMixer.AddInputStream(Audios[int.Parse(pack.ownerID)]);
                                Console.WriteLine("Добавлен пользователь " + pack.ownerID);
                            }
                            partAudio part = ArrToObj(pack.data) as partAudio;
                            Audios[int.Parse(pack.ownerID)].AddSamples(part.data, 0, part.data.Length);
                            Console.WriteLine("Пришло аудио от " + pack.ownerID + ", размер: " + part.data.Length + "\n");
                        }
                    }

                    if (pack.type == Package.Types.HoldOn)
                    {
                        if (!CommunicationCenter.Information.nowIsStream)
                        {
                            if (!Users.ContainsKey(pack.ownerID))
                            {
                                VU_ImFromUser imfromUser = new VU_ImFromUser();
                                imfromUser.isStream = false;

                                if (pack.ownerID == SQL.User.id)
                                    imfromUser.Text = "Вы: " + SQL.Read("SELECT nickname FROM Accounts WHERE id = " + pack.ownerID);
                                else
                                    imfromUser.Text = SQL.Read("SELECT nickname FROM Accounts WHERE id = " + pack.ownerID);

                                imfromUser.OwnerID = pack.ownerID;
                                imfromUser.OwnerType = Package.Types.unknown;
                                imfromUser.BackgroundImage = Resources.profile;
                                Users.Add(pack.ownerID, (imfromUser, DateTime.Now));

                                form.Invoke(new Action(() =>
                                {
                                    form.listPanel_Inner.Controls.Add(Users[pack.ownerID].Item1);
                                }));
                            } else
                            {
                                Users[pack.ownerID] = (Users[pack.ownerID].Item1, DateTime.Now);
                            }

                            //Console.WriteLine("HoldOn " + pack.ownerID);
                        }
                    }

                    if (CommunicationCenter.Information.nowIsStream)
                    {
                        if (pack.type == Package.Types.MiniWebcam)
                        {
                            var bmp = (ArrToObj(pack.data) as partImage);

                            if (!Miniatures.ContainsKey((int.Parse(pack.ownerID), Package.Types.MiniWebcam)))
                            {
                                VU_ImFromUser imfromUser = new VU_ImFromUser();
                                imfromUser.isStream = true;
                                imfromUser.BackgroundImage = ArrToImg(bmp.bitmap);
                                imfromUser.Text = SQL.Read("SELECT nickname FROM Accounts WHERE id = " + pack.ownerID);
                                imfromUser.OwnerID = pack.ownerID;
                                imfromUser.OwnerType = Package.Types.WebCam;
                                Miniatures.Add((int.Parse(pack.ownerID), Package.Types.MiniWebcam), (imfromUser, new byte[0], DateTime.Now));
                                form.Invoke(new Action(() =>
                                {
                                    form.listPanel_Inner.Controls.Add(Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniWebcam)].Item1);
                                }));
                            }
                            else
                            {
                                Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniWebcam)] = (Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniWebcam)].Item1, bmp.bitmap, DateTime.Now);
                            }
                        }

                        if (pack.type == Package.Types.MiniScreen)
                        {
                            var bmp = (ArrToObj(pack.data) as partImage);

                            if (!Miniatures.ContainsKey((int.Parse(pack.ownerID), Package.Types.MiniScreen)))
                            {
                                VU_ImFromUser imfromUser = new VU_ImFromUser();
                                imfromUser.isStream = true;
                                imfromUser.BackgroundImage = ArrToImg(bmp.bitmap);
                                imfromUser.Text = SQL.Read("SELECT nickname FROM Accounts WHERE id = " + pack.ownerID);
                                imfromUser.OwnerID = pack.ownerID;
                                imfromUser.OwnerType = Package.Types.Screen;
                                Miniatures.Add((int.Parse(pack.ownerID), Package.Types.MiniScreen), (imfromUser, new byte[0], DateTime.Now));
                                form.Invoke(new Action(() =>
                                {
                                    form.listPanel_Inner.Controls.Add(Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniScreen)].Item1);
                                }));

                            }
                            else
                            {
                                Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniScreen)] = (Miniatures[(int.Parse(pack.ownerID), Package.Types.MiniScreen)].Item1, bmp.bitmap, DateTime.Now);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MajorForm.MsgBox("Ошибка", ex.Message);
                }
                //Console.WriteLine("Выполнено");

            }


        }
        #endregion

        #region --- Отдел хранения информации ---
        public class Information
        {
            public static bool webcamIsWorking = false, microIsWorking = false, headIsWorking = false, nowIsStream = false;
            public static string currentAudio = "null";
            public static string selectedWebcam = "null";
            public static int selectedMicro = -1, selectedHead = -1;
            public static IPAddress IPSender = null;
            public static List<string> ServerIPs = new List<string>();
        }
        #endregion

        #region --- Отдел преобразований ---

        public static string GetRandomText()
        {
            var rnd = new Random();

            string[] Smiles = new string[] { "ヽ(・∀・)ﾉ" , "(⌒‿⌒)" , "(´• ω •)" , "(◕‿◕)", "¯\\_(ツ)_/¯", "\\(★ω★)/" , "(╯✧▽✧)╯" , "(─‿‿─)" , 
                "(＾▽＾)", "(´｡• ω •｡)", "o(≧▽≦)o", "(„• ֊ •„)", "( ◡‿◡ *)", "(ง ื▿ ื)ว", "╮(￣ω￣;)╭", "(⊙_⊙)", "(￢‿￢ )" };

            return Smiles[rnd.Next(Smiles.Length)];
        }

        public static string GetRandomText(byte n)
        {
            var rnd = new Random();

            if (n > 16) n = 16;
            
            switch (rnd.Next(1, n))
            {
                case 1:
                    return "ヽ(・∀・)ﾉ";
                case 2:
                    return "(⌒‿⌒)";
                case 3:
                    return "(´• ω •)";
                case 4:
                    return "(◕‿◕)";
                case 5:
                    return "¯\\_(ツ)_/¯";
                case 6:
                    return "\\(★ω★)/";
                case 7:
                    return "(╯✧▽✧)╯";
                case 8:
                    return "(─‿‿─)";
                case 9:
                    return "(＾▽＾)";
                case 10:
                    return "(´｡• ω •｡)";
                case 11:
                    return "o(≧▽≦)o";
                case 12:
                    return "(„• ֊ •„)";
                case 13:
                    return "( ◡‿◡ *)";
                case 14:
                    return "(ง ื▿ ื)ว";
                case 15:
                    return "╮(￣ω￣;)╭";
                case 16:
                    return "(⊙_⊙)";
                case 17:
                    return "(￢‿￢ )";
                default:
                    return "¯\\_(ツ)_/¯";

            }
        }

        public static byte[] ObjToArr(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return Zip.Compress(ms.ToArray());
        }

        public static Object ArrToObj(byte[] arrBytes)
        {
            byte[] finalArrBytes = Zip.Decompress(arrBytes);
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(finalArrBytes, 0, finalArrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = binForm.Deserialize(memStream);
            return obj;
        }

        public static byte[] ImgToArr(Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap clone = new Bitmap(image.Width, image.Height, PixelFormat.Format16bppRgb555);



                using (Graphics gr = Graphics.FromImage(clone))
                {
                    gr.DrawImage(image, new Rectangle(0, 0, clone.Width, clone.Height));
                }

                clone.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public static Bitmap ArrToImg(byte[] arrBytes)
        {
            Bitmap bmp;
            using (var ms = new MemoryStream(arrBytes))
            {
                bmp = new Bitmap(ms);
            }
            return new Bitmap(bmp);
        }

        public static class Zip
        {
            public static byte[] Compress(byte[] src)
            {
                using (var input = new MemoryStream(src))
                {
                    using (var output = new MemoryStream())
                    {
                        using (var compressor = new GZipStream(output, CompressionMode.Compress))
                        {
                            input.CopyTo(compressor);
                        }
                        return output.ToArray();
                    }
                }
            }

            public static byte[] Decompress(byte[] src)
            {
                using (var input = new MemoryStream(src))
                {
                    using (var decompressor = new GZipStream(input, CompressionMode.Decompress))
                    {
                        using (var output = new MemoryStream())
                        {
                            decompressor.CopyTo(output);

                            return output.ToArray();
                        }
                    }
                }
            }
        }

        public void SplitImageAndSend(Bitmap bmp, Package.Types type)
        {
            int size = 200; //150
            int x = bmp.Width / size; //Итерации по ширине
            if (bmp.Width % size != 0) x++;
            int y = bmp.Height / size; //Итерации по высоте
            if (bmp.Height % size != 0) y++;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Bitmap tempBmp = new Bitmap(size, size);
                    var graphics = Graphics.FromImage(tempBmp);

                    graphics.DrawImage(bmp, new Rectangle(0, 0, size, size), new Rectangle(i * size, j * size, size, size), GraphicsUnit.Pixel);

                    var ms1 = new MemoryStream();
                    tempBmp.Save(ms1, ImageFormat.Jpeg);
                    tempBmp = new Bitmap(ms1);
                    //Console.WriteLine(i + "/" + x + " " + j + "/" + y + " | " + tempBmp.Width + " - " + tempBmp.Height);

                    #region --- Отправка ---
                    byte[] tempArr = ObjToArr(new Package(SQL.User.id, SQL.Lesson.id, type, ObjToArr(new partImage(ImgToArr(tempBmp), tempBmp.Width, tempBmp.Height, i, j, bmp.Width, bmp.Height))));
                    //Console.WriteLine("Отправлено: " + i  + " | " + j );

                    //foreach (IPEndPoint ip in IPs)
                    //{
                    //    //Console.WriteLine(ip.ToString());
                    //    client.SendAsync(temp, temp.Length, ip);
                    //}
                    client.Send(tempArr, tempArr.Length);//, new IPEndPoint(Information.IPSender, PortSender)

                    #endregion
                }
            }

            GC.Collect();

        }

        //public static void SplitScreenAndSend()
        //{
        //    int size = 216; //150
        //    float CopyBlock = size * 1.5f;

        //    int x = (int)(Screen.PrimaryScreen.Bounds.Width / (CopyBlock)); //Итерации по ширине
        //    if (Screen.PrimaryScreen.Bounds.Width % (CopyBlock) != 0) x++;
        //    int y = (int)(Screen.PrimaryScreen.Bounds.Height / (CopyBlock)); //Итерации по высоте
        //    if (Screen.PrimaryScreen.Bounds.Height % (CopyBlock) != 0) y++;

        //    for (int i = 0; i < x; i++)
        //    {
        //        for (int j = 0; j < y; j++)
        //        {
        //            Bitmap screen = new Bitmap((int)(CopyBlock), (int)(CopyBlock));
        //            using (Graphics g = Graphics.FromImage(screen))
        //            {
        //                g.CopyFromScreen((int)(i * CopyBlock), (int)(j * CopyBlock), 0, 0, screen.Size);

        //                screen = new Bitmap(screen, size, size);

        //                var ms1 = new MemoryStream();

        //                screen.Save(ms1, ImageFormat.Jpeg);
        //                screen = new Bitmap(ms1);


        //                //Console.WriteLine(i + "/" + x + " " + j + "/" + y + " | " + tempBmp.Width + " - " + tempBmp.Height);

        //                #region --- Отправка ---
        //                byte[] tempArr = ObjToArr(new Package(SQL.User.id, SQL.Lesson.id, Package.Types.Screen, ObjToArr(new partImage(ObjToArr(screen), screen.Width, screen.Height, i, j, (int)(Screen.PrimaryScreen.Bounds.Width / (CopyBlock / size)), (int)(Screen.PrimaryScreen.Bounds.Height / (CopyBlock / size))))));
        //                //Console.WriteLine("Отправлено: " + i  + " | " + j );

        //                //foreach (IPEndPoint ip in IPs)
        //                //{
        //                //    //Console.WriteLine(ip.ToString());
        //                //    client.SendAsync(temp, temp.Length, ip);
        //                //}
        //                client.Send(tempArr, tempArr.Length);//, new IPEndPoint(Information.IPSender, PortSender)


        //                #endregion
        //            }
        //        }
        //        GC.Collect();

        //    }
        //}
        #endregion

        
    }
}
