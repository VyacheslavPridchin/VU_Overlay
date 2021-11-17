
namespace Overlay_Client_DSTU
{
    partial class Lesson
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lesson));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Alpha = new Overlay_Client_DSTU.VU_Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Updater = new System.Windows.Forms.Timer(this.components);
            this.Checker = new System.Windows.Forms.Timer(this.components);
            this.vU_Panel1 = new Overlay_Client_DSTU.VU_Panel();
            this.vU_Panel2 = new Overlay_Client_DSTU.VU_Panel();
            this.vU_Button4 = new Overlay_Client_DSTU.VU_Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.vU_Button6 = new Overlay_Client_DSTU.VU_Button();
            this.vU_Button5 = new Overlay_Client_DSTU.VU_Button();
            this.vU_Button2 = new Overlay_Client_DSTU.VU_Button();
            this.vU_Button1 = new Overlay_Client_DSTU.VU_Button();
            this.vU_Button3 = new Overlay_Client_DSTU.VU_Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listPanel_Outer = new System.Windows.Forms.Panel();
            this.listPanel_Inner = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonControl = new Overlay_Client_DSTU.VU_ImFromUser();
            this.panel1.SuspendLayout();
            this.vU_Panel1.SuspendLayout();
            this.vU_Panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.listPanel_Outer.SuspendLayout();
            this.listPanel_Inner.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.Alpha);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 20);
            this.panel1.TabIndex = 3;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(814, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 20);
            this.button2.TabIndex = 6;
            this.button2.Text = "_____";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.button3.Dock = System.Windows.Forms.DockStyle.Right;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.Location = new System.Drawing.Point(864, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 20);
            this.button3.TabIndex = 5;
            this.button3.Text = "[       ]";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Alpha
            // 
            this.Alpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Alpha.CustomFont = Overlay_Client_DSTU.Painter.Fonts.FontsName.Roboto_Light;
            this.Alpha.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Alpha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.Alpha.Location = new System.Drawing.Point(12, 0);
            this.Alpha.Name = "Alpha";
            this.Alpha.Size = new System.Drawing.Size(796, 20);
            this.Alpha.TabIndex = 3;
            this.Alpha.Text = "Alpha";
            this.Alpha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Alpha.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Alpha_MouseDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(914, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 20);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Updater
            // 
            this.Updater.Enabled = true;
            this.Updater.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Checker
            // 
            this.Checker.Enabled = true;
            this.Checker.Interval = 1000;
            this.Checker.Tick += new System.EventHandler(this.Checker_Tick);
            // 
            // vU_Panel1
            // 
            this.vU_Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(45)))), ((int)(((byte)(46)))));
            this.vU_Panel1.Controls.Add(this.vU_Panel2);
            this.vU_Panel1.Controls.Add(this.MainPanel);
            this.vU_Panel1.CountIter = 1;
            this.vU_Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vU_Panel1.Intensity = 1;
            this.vU_Panel1.Location = new System.Drawing.Point(0, 20);
            this.vU_Panel1.Margin = new System.Windows.Forms.Padding(0);
            this.vU_Panel1.Name = "vU_Panel1";
            this.vU_Panel1.Rounding = 35;
            this.vU_Panel1.ShadowColor = System.Drawing.Color.Black;
            this.vU_Panel1.ShadowOY = 0;
            this.vU_Panel1.Size = new System.Drawing.Size(964, 674);
            this.vU_Panel1.TabIndex = 4;
            // 
            // vU_Panel2
            // 
            this.vU_Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.vU_Panel2.Controls.Add(this.vU_Button4);
            this.vU_Panel2.Controls.Add(this.panel3);
            this.vU_Panel2.Controls.Add(this.panel2);
            this.vU_Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.vU_Panel2.Location = new System.Drawing.Point(0, 571);
            this.vU_Panel2.Margin = new System.Windows.Forms.Padding(0);
            this.vU_Panel2.Name = "vU_Panel2";
            this.vU_Panel2.Rounding = 40;
            this.vU_Panel2.ShadowColor = System.Drawing.Color.Black;
            this.vU_Panel2.ShadowEnable = false;
            this.vU_Panel2.Size = new System.Drawing.Size(964, 103);
            this.vU_Panel2.TabIndex = 18;
            this.vU_Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.vU_Panel2_Paint);
            // 
            // vU_Button4
            // 
            this.vU_Button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vU_Button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button4.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.vU_Button4.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button4.Location = new System.Drawing.Point(108, 85);
            this.vU_Button4.Name = "vU_Button4";
            this.vU_Button4.Rounding = 20;
            this.vU_Button4.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button4.Size = new System.Drawing.Size(749, 11);
            this.vU_Button4.TabIndex = 20;
            this.vU_Button4.UseVisualStyleBackColor = false;
            this.vU_Button4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vU_Button4_MouseDown);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.vU_Button6);
            this.panel3.Controls.Add(this.vU_Button5);
            this.panel3.Controls.Add(this.vU_Button2);
            this.panel3.Controls.Add(this.vU_Button1);
            this.panel3.Controls.Add(this.vU_Button3);
            this.panel3.Location = new System.Drawing.Point(1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(963, 79);
            this.panel3.TabIndex = 22;
            // 
            // vU_Button6
            // 
            this.vU_Button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.vU_Button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button6.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button6.Image = global::Overlay_Client_DSTU.Properties.Resources.Лого_VU1;
            this.vU_Button6.Location = new System.Drawing.Point(620, 4);
            this.vU_Button6.Name = "vU_Button6";
            this.vU_Button6.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button6.Size = new System.Drawing.Size(70, 70);
            this.vU_Button6.TabIndex = 22;
            this.vU_Button6.Text = "vU_Button6";
            this.vU_Button6.UseVisualStyleBackColor = false;
            this.vU_Button6.Click += new System.EventHandler(this.vU_Button6_Click);
            // 
            // vU_Button5
            // 
            this.vU_Button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.vU_Button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button5.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button5.Image = ((System.Drawing.Image)(resources.GetObject("vU_Button5.Image")));
            this.vU_Button5.Location = new System.Drawing.Point(272, 3);
            this.vU_Button5.Name = "vU_Button5";
            this.vU_Button5.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button5.Size = new System.Drawing.Size(70, 70);
            this.vU_Button5.TabIndex = 21;
            this.vU_Button5.Text = "vU_Button5";
            this.vU_Button5.UseVisualStyleBackColor = false;
            this.vU_Button5.Click += new System.EventHandler(this.vU_Button5_Click);
            // 
            // vU_Button2
            // 
            this.vU_Button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.vU_Button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button2.Image = global::Overlay_Client_DSTU.Properties.Resources.Cam;
            this.vU_Button2.Location = new System.Drawing.Point(446, 3);
            this.vU_Button2.Name = "vU_Button2";
            this.vU_Button2.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button2.Size = new System.Drawing.Size(70, 70);
            this.vU_Button2.TabIndex = 16;
            this.vU_Button2.Text = "vU_Button2";
            this.vU_Button2.UseVisualStyleBackColor = false;
            this.vU_Button2.Click += new System.EventHandler(this.vU_Button2_Click);
            // 
            // vU_Button1
            // 
            this.vU_Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.vU_Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button1.Image = global::Overlay_Client_DSTU.Properties.Resources.Micro;
            this.vU_Button1.Location = new System.Drawing.Point(359, 3);
            this.vU_Button1.Name = "vU_Button1";
            this.vU_Button1.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button1.Size = new System.Drawing.Size(70, 70);
            this.vU_Button1.TabIndex = 15;
            this.vU_Button1.Text = "vU_Button1";
            this.vU_Button1.UseVisualStyleBackColor = false;
            this.vU_Button1.Click += new System.EventHandler(this.vU_Button1_Click);
            // 
            // vU_Button3
            // 
            this.vU_Button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.vU_Button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button3.Image = global::Overlay_Client_DSTU.Properties.Resources.Screen;
            this.vU_Button3.Location = new System.Drawing.Point(533, 3);
            this.vU_Button3.Name = "vU_Button3";
            this.vU_Button3.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button3.Size = new System.Drawing.Size(70, 70);
            this.vU_Button3.TabIndex = 17;
            this.vU_Button3.Text = "vU_Button3";
            this.vU_Button3.UseVisualStyleBackColor = false;
            this.vU_Button3.Click += new System.EventHandler(this.vU_Button3_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(964, 58);
            this.panel2.TabIndex = 18;
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.pictureBox1);
            this.MainPanel.Controls.Add(this.listPanel_Outer);
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(964, 571);
            this.MainPanel.TabIndex = 14;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(782, 571);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // listPanel_Outer
            // 
            this.listPanel_Outer.Controls.Add(this.listPanel_Inner);
            this.listPanel_Outer.Dock = System.Windows.Forms.DockStyle.Right;
            this.listPanel_Outer.Location = new System.Drawing.Point(782, 0);
            this.listPanel_Outer.Name = "listPanel_Outer";
            this.listPanel_Outer.Size = new System.Drawing.Size(182, 571);
            this.listPanel_Outer.TabIndex = 0;
            // 
            // listPanel_Inner
            // 
            this.listPanel_Inner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listPanel_Inner.AutoScroll = true;
            this.listPanel_Inner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.listPanel_Inner.Controls.Add(this.buttonControl);
            this.listPanel_Inner.Location = new System.Drawing.Point(0, 0);
            this.listPanel_Inner.Name = "listPanel_Inner";
            this.listPanel_Inner.Size = new System.Drawing.Size(200, 571);
            this.listPanel_Inner.TabIndex = 0;
            // 
            // buttonControl
            // 
            this.buttonControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.buttonControl.BackgroundImage = global::Overlay_Client_DSTU.Properties.Resources.Лого_VU;
            this.buttonControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.buttonControl.Location = new System.Drawing.Point(3, 0);
            this.buttonControl.Margin = new System.Windows.Forms.Padding(3, 0, 0, 5);
            this.buttonControl.Name = "buttonControl";
            this.buttonControl.Size = new System.Drawing.Size(179, 102);
            this.buttonControl.TabIndex = 1;
            this.buttonControl.Text = "Посмотреть трансляции";
            this.buttonControl.Click += new System.EventHandler(this.vU_ImFromUser1_Click);
            // 
            // Lesson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(45)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(964, 694);
            this.Controls.Add(this.vU_Panel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(450, 213);
            this.Name = "Lesson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пара";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(45)))), ((int)(((byte)(46)))));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Lesson_FormClosing);
            this.Load += new System.EventHandler(this.Lesson_Load);
            this.Shown += new System.EventHandler(this.Lesson_Shown);
            this.ResizeEnd += new System.EventHandler(this.Lesson_ResizeEnd);
            this.panel1.ResumeLayout(false);
            this.vU_Panel1.ResumeLayout(false);
            this.vU_Panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.listPanel_Outer.ResumeLayout(false);
            this.listPanel_Inner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private VU_Label Alpha;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel MainPanel;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel listPanel_Outer;
        public System.Windows.Forms.FlowLayoutPanel listPanel_Inner;
        private VU_Panel vU_Panel2;
        private VU_Button vU_Button1;
        private VU_Button vU_Button3;
        private VU_Button vU_Button2;
        private VU_Panel vU_Panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private VU_Button vU_Button4;
        private VU_Button vU_Button5;
        private VU_ImFromUser buttonControl;
        private System.Windows.Forms.Panel panel3;
        private VU_Button vU_Button6;
        private System.Windows.Forms.Timer Updater;
        private System.Windows.Forms.Timer Checker;
    }
}