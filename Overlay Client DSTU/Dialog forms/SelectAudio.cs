using NAudio.Wave;
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
    public partial class SelectAudio : Form
    {
        public SelectAudio()
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

        private void SelectAudio_Load(object sender, EventArgs e)
        {
            audioDeviceList.Items.Clear();
            if (WaveIn.DeviceCount != 0)
            {
                for (int i = 0; i < WaveIn.DeviceCount; i++)
                    audioDeviceList.Items.Add(WaveIn.GetCapabilities(i).ProductName + "...");
                audioDeviceList.SelectedIndex = 0;
            }
            else
            {
                audioDeviceList.Enabled = false;
                vU_Label1.Text = "Микрофон не найден";
            }

            outputList.Items.Clear();
            if (WaveOut.DeviceCount != 0)
            {
                for (int i = 0; i < WaveOut.DeviceCount; i++)
                    outputList.Items.Add(WaveOut.GetCapabilities(i).ProductName + "...");
                outputList.SelectedIndex = 0;
            }
            else
            {
                outputList.Enabled = false;
                vU_Label2.Text = "Аудиовыход не найден";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommunicationCenter.Information.selectedMicro = -1;
            CommunicationCenter.Information.selectedHead = -1;
            CommunicationCenter.Information.headIsWorking = false;
            CommunicationCenter.Information.microIsWorking = false;

            Close();
        }

        private void vU_Button1_Click(object sender, EventArgs e)
        {
            CommunicationCenter.Information.selectedMicro = audioDeviceList.SelectedIndex;
            CommunicationCenter.Information.selectedHead = outputList.SelectedIndex;

            CommunicationCenter.Information.headIsWorking = (outputList.SelectedIndex != -1);
            CommunicationCenter.Information.microIsWorking = (audioDeviceList.SelectedIndex != -1);

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
