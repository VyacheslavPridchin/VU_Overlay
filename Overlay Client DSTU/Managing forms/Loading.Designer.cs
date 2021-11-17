
namespace Overlay_Client_DSTU
{
    partial class Loading
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loading));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.vU_Label1 = new Overlay_Client_DSTU.VU_Label();
            this.top = new Overlay_Client_DSTU.VU_Panel();
            this.middle = new Overlay_Client_DSTU.VU_Panel();
            this.bottom = new Overlay_Client_DSTU.VU_Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(37)))));
            this.pictureBox1.BackgroundImage = global::Overlay_Client_DSTU.Properties.Resources.Лого_VU;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(148, 165);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(376, 255);
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // vU_Label1
            // 
            this.vU_Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(37)))));
            this.vU_Label1.CustomFont = Overlay_Client_DSTU.Painter.Fonts.FontsName.Roboto_Light;
            this.vU_Label1.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Label1.Location = new System.Drawing.Point(154, 423);
            this.vU_Label1.Margin = new System.Windows.Forms.Padding(0);
            this.vU_Label1.Name = "vU_Label1";
            this.vU_Label1.Size = new System.Drawing.Size(364, 78);
            this.vU_Label1.TabIndex = 34;
            this.vU_Label1.Text = "Загрузка Виртуального универа...";
            this.vU_Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.vU_Label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vU_Label1_MouseDown);
            // 
            // top
            // 
            this.top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(37)))));
            this.top.Location = new System.Drawing.Point(145, 49);
            this.top.Name = "top";
            this.top.ShadowColor = System.Drawing.Color.Black;
            this.top.ShadowEnable = false;
            this.top.Size = new System.Drawing.Size(383, 574);
            this.top.TabIndex = 36;
            // 
            // middle
            // 
            this.middle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.middle.Location = new System.Drawing.Point(81, 122);
            this.middle.Name = "middle";
            this.middle.ShadowColor = System.Drawing.Color.Black;
            this.middle.ShadowEnable = false;
            this.middle.Size = new System.Drawing.Size(511, 429);
            this.middle.TabIndex = 37;
            // 
            // bottom
            // 
            this.bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.bottom.Location = new System.Drawing.Point(29, 195);
            this.bottom.Name = "bottom";
            this.bottom.ShadowColor = System.Drawing.Color.Black;
            this.bottom.ShadowEnable = false;
            this.bottom.Size = new System.Drawing.Size(614, 282);
            this.bottom.TabIndex = 38;
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(677, 670);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.vU_Label1);
            this.Controls.Add(this.top);
            this.Controls.Add(this.middle);
            this.Controls.Add(this.bottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Loading";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Загрузка";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Loading_FormClosed);
            this.Shown += new System.EventHandler(this.Loading_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private VU_Label vU_Label1;
        private VU_Panel top;
        private VU_Panel middle;
        private VU_Panel bottom;
    }
}