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
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Overlay_Client_DSTU
{
    public partial class SelectWebcam : Form
    {
        #region --- Подключение веб-камеры ---
        private FilterInfoCollection CaptureDevices;
        private VideoCaptureDevice videoSource;
        #endregion

        public SelectWebcam()
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
        private void SelectWebcam_Load(object sender, EventArgs e)
        {
            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (CaptureDevices.Count != 0)
            {
                foreach (FilterInfo Device in CaptureDevices)
                {
                    comboBox1.Items.Add(Device.Name);
                }
                comboBox1.SelectedIndex = 0;
                videoSource = new VideoCaptureDevice(CaptureDevices[comboBox1.SelectedIndex].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();
            } else
            {
                vU_Label1.Text = "Видеоустройства не найдены!";
                comboBox1.Enabled = false;
                vU_Button1.Visible = false;
                CommunicationCenter.Information.selectedWebcam = "null";
            }
    }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            System.GC.Collect();
        }

        private void vU_Button1_Click(object sender, EventArgs e)
        {
            CommunicationCenter.Information.selectedWebcam = CaptureDevices[comboBox1.SelectedIndex].MonikerString;
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (videoSource != null)
            {
                if (videoSource.IsRunning)
                    videoSource.Stop();

                //pictureBox1.Image = null;
                //pictureBox1.Invalidate();

                videoSource = new VideoCaptureDevice(CaptureDevices[comboBox1.SelectedIndex].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommunicationCenter.Information.selectedWebcam = "null";
            CommunicationCenter.Information.webcamIsWorking = false;

            Close();
        }

        private void SelectWebcam_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(videoSource.IsRunning)
                videoSource.Stop();
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
