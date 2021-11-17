
namespace Overlay_Client_DSTU
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.vU_ButtonText1 = new Overlay_Client_DSTU.VU_ButtonText();
            this.log = new Overlay_Client_DSTU.VU_Label();
            this.login = new Overlay_Client_DSTU.VU_TextBox();
            this.password = new Overlay_Client_DSTU.VU_TextBox();
            this.vU_Button1 = new Overlay_Client_DSTU.VU_Button();
            this.vU_ButtonText2 = new Overlay_Client_DSTU.VU_ButtonText();
            this.vU_ButtonText3 = new Overlay_Client_DSTU.VU_ButtonText();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 20);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(350, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 20);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Overlay_Client_DSTU.Properties.Resources.Лого_VU;
            this.pictureBox1.Location = new System.Drawing.Point(0, 125);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 232);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // vU_ButtonText1
            // 
            this.vU_ButtonText1.CustomFont = Overlay_Client_DSTU.Painter.Fonts.FontsName.Roboto_Light;
            this.vU_ButtonText1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_ButtonText1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_ButtonText1.Location = new System.Drawing.Point(0, 576);
            this.vU_ButtonText1.Name = "vU_ButtonText1";
            this.vU_ButtonText1.Size = new System.Drawing.Size(400, 24);
            this.vU_ButtonText1.TabIndex = 6;
            this.vU_ButtonText1.Text = "Зарегистрироваться";
            this.vU_ButtonText1.Click += new System.EventHandler(this.vU_ButtonText1_Click);
            // 
            // log
            // 
            this.log.CustomFont = Overlay_Client_DSTU.Painter.Fonts.FontsName.Roboto_Light;
            this.log.Dock = System.Windows.Forms.DockStyle.Top;
            this.log.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.log.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.log.Location = new System.Drawing.Point(0, 20);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(400, 104);
            this.log.TabIndex = 5;
            this.log.Text = "Авторизация в Виртуальном ДГТУ";
            this.log.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.log.UseMnemonic = false;
            // 
            // login
            // 
            this.login.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.login.GhostText = "Почта или имя";
            this.login.Location = new System.Drawing.Point(41, 371);
            this.login.Name = "login";
            this.login.PasswordChar = '\0';
            this.login.Size = new System.Drawing.Size(318, 30);
            this.login.TabIndex = 4;
            this.login.Text = "Почта или имя";
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.password.GhostText = "Пароль";
            this.password.Location = new System.Drawing.Point(41, 407);
            this.password.Name = "password";
            this.password.PasswordChar = '◌';
            this.password.Size = new System.Drawing.Size(318, 30);
            this.password.TabIndex = 3;
            this.password.Text = "Пароль";
            // 
            // vU_Button1
            // 
            this.vU_Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button1.Location = new System.Drawing.Point(112, 449);
            this.vU_Button1.Name = "vU_Button1";
            this.vU_Button1.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button1.Size = new System.Drawing.Size(177, 39);
            this.vU_Button1.TabIndex = 2;
            this.vU_Button1.Text = "Авторизация";
            this.vU_Button1.UseVisualStyleBackColor = true;
            this.vU_Button1.Click += new System.EventHandler(this.vU_Button1_Click);
            // 
            // vU_ButtonText2
            // 
            this.vU_ButtonText2.CustomFont = Overlay_Client_DSTU.Painter.Fonts.FontsName.Roboto_Light;
            this.vU_ButtonText2.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_ButtonText2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_ButtonText2.Location = new System.Drawing.Point(0, 555);
            this.vU_ButtonText2.Name = "vU_ButtonText2";
            this.vU_ButtonText2.Size = new System.Drawing.Size(400, 24);
            this.vU_ButtonText2.TabIndex = 8;
            this.vU_ButtonText2.Text = "Войти как гость";
            this.vU_ButtonText2.Click += new System.EventHandler(this.vU_ButtonText2_Click);
            // 
            // vU_ButtonText3
            // 
            this.vU_ButtonText3.CustomFont = Overlay_Client_DSTU.Painter.Fonts.FontsName.Roboto_Light;
            this.vU_ButtonText3.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_ButtonText3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_ButtonText3.Location = new System.Drawing.Point(1, 493);
            this.vU_ButtonText3.Name = "vU_ButtonText3";
            this.vU_ButtonText3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.vU_ButtonText3.Size = new System.Drawing.Size(400, 24);
            this.vU_ButtonText3.TabIndex = 9;
            this.vU_ButtonText3.Text = "Забыли пароль?";
            this.vU_ButtonText3.Click += new System.EventHandler(this.vU_ButtonText3_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(400, 605);
            this.Controls.Add(this.vU_ButtonText3);
            this.Controls.Add(this.vU_ButtonText2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.vU_ButtonText1);
            this.Controls.Add(this.log);
            this.Controls.Add(this.login);
            this.Controls.Add(this.password);
            this.Controls.Add(this.vU_Button1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private VU_Button vU_Button1;
        private VU_TextBox password;
        private VU_TextBox login;
        private VU_Label log;
        private VU_ButtonText vU_ButtonText1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private VU_ButtonText vU_ButtonText2;
        private VU_ButtonText vU_ButtonText3;
    }
}