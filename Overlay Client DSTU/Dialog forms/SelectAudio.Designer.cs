
namespace Overlay_Client_DSTU
{
    partial class SelectAudio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectAudio));
            this.vU_Button1 = new Overlay_Client_DSTU.VU_Button();
            this.vU_Label1 = new Overlay_Client_DSTU.VU_Label();
            this.audioDeviceList = new System.Windows.Forms.ComboBox();
            this.Alpha = new Overlay_Client_DSTU.VU_Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.vU_Label2 = new Overlay_Client_DSTU.VU_Label();
            this.outputList = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vU_Button1
            // 
            this.vU_Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button1.Location = new System.Drawing.Point(236, 199);
            this.vU_Button1.Name = "vU_Button1";
            this.vU_Button1.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button1.Size = new System.Drawing.Size(158, 43);
            this.vU_Button1.TabIndex = 21;
            this.vU_Button1.Text = "Запустить";
            this.vU_Button1.UseVisualStyleBackColor = false;
            this.vU_Button1.Click += new System.EventHandler(this.vU_Button1_Click);
            // 
            // vU_Label1
            // 
            this.vU_Label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Label1.Location = new System.Drawing.Point(0, 51);
            this.vU_Label1.Name = "vU_Label1";
            this.vU_Label1.Size = new System.Drawing.Size(631, 23);
            this.vU_Label1.TabIndex = 20;
            this.vU_Label1.Text = "Выбор микрофона";
            this.vU_Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // audioDeviceList
            // 
            this.audioDeviceList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.audioDeviceList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.audioDeviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.audioDeviceList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.audioDeviceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.audioDeviceList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.audioDeviceList.FormattingEnabled = true;
            this.audioDeviceList.Location = new System.Drawing.Point(27, 82);
            this.audioDeviceList.Name = "audioDeviceList";
            this.audioDeviceList.Size = new System.Drawing.Size(577, 32);
            this.audioDeviceList.TabIndex = 19;
            // 
            // Alpha
            // 
            this.Alpha.CustomFont = Overlay_Client_DSTU.Painter.Fonts.FontsName.Roboto_Light;
            this.Alpha.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Alpha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.Alpha.Location = new System.Drawing.Point(12, 0);
            this.Alpha.Name = "Alpha";
            this.Alpha.Size = new System.Drawing.Size(329, 20);
            this.Alpha.TabIndex = 3;
            this.Alpha.Text = "Выбор аудиоустройств";
            this.Alpha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Alpha.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Alpha_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.panel1.Controls.Add(this.Alpha);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 20);
            this.panel1.TabIndex = 18;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(581, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 20);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // vU_Label2
            // 
            this.vU_Label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Label2.Location = new System.Drawing.Point(0, 123);
            this.vU_Label2.Name = "vU_Label2";
            this.vU_Label2.Size = new System.Drawing.Size(631, 23);
            this.vU_Label2.TabIndex = 23;
            this.vU_Label2.Text = "Выбор аудиовыхода";
            this.vU_Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outputList
            // 
            this.outputList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.outputList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.outputList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.outputList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outputList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.outputList.FormattingEnabled = true;
            this.outputList.Location = new System.Drawing.Point(27, 154);
            this.outputList.Name = "outputList";
            this.outputList.Size = new System.Drawing.Size(577, 32);
            this.outputList.TabIndex = 22;
            // 
            // SelectAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(631, 255);
            this.Controls.Add(this.vU_Label2);
            this.Controls.Add(this.outputList);
            this.Controls.Add(this.vU_Button1);
            this.Controls.Add(this.vU_Label1);
            this.Controls.Add(this.audioDeviceList);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectAudio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор аудиоустройств";
            this.Load += new System.EventHandler(this.SelectAudio_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private VU_Button vU_Button1;
        private VU_Label vU_Label1;
        private System.Windows.Forms.ComboBox audioDeviceList;
        private VU_Label Alpha;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private VU_Label vU_Label2;
        private System.Windows.Forms.ComboBox outputList;
    }
}