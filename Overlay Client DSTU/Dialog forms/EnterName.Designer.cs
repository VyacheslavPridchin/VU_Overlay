
namespace Overlay_Client_DSTU
{
    partial class EnterName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterName));
            this.Name_TextBox = new Overlay_Client_DSTU.VU_TextBox();
            this.vU_Button1 = new Overlay_Client_DSTU.VU_Button();
            this.vU_Label1 = new Overlay_Client_DSTU.VU_Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Alpha = new Overlay_Client_DSTU.VU_Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Name_TextBox
            // 
            this.Name_TextBox.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name_TextBox.GhostText = "Имя";
            this.Name_TextBox.Location = new System.Drawing.Point(59, 64);
            this.Name_TextBox.Name = "Name_TextBox";
            this.Name_TextBox.PasswordChar = '\0';
            this.Name_TextBox.Size = new System.Drawing.Size(435, 30);
            this.Name_TextBox.TabIndex = 33;
            this.Name_TextBox.Text = "Имя";
            // 
            // vU_Button1
            // 
            this.vU_Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button1.Location = new System.Drawing.Point(197, 107);
            this.vU_Button1.Margin = new System.Windows.Forms.Padding(10);
            this.vU_Button1.Name = "vU_Button1";
            this.vU_Button1.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button1.Size = new System.Drawing.Size(158, 43);
            this.vU_Button1.TabIndex = 32;
            this.vU_Button1.Text = "Войти";
            this.vU_Button1.UseVisualStyleBackColor = true;
            this.vU_Button1.Click += new System.EventHandler(this.vU_Button1_Click);
            // 
            // vU_Label1
            // 
            this.vU_Label1.BackColor = System.Drawing.Color.Transparent;
            this.vU_Label1.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Label1.Location = new System.Drawing.Point(50, 28);
            this.vU_Label1.Margin = new System.Windows.Forms.Padding(0);
            this.vU_Label1.Name = "vU_Label1";
            this.vU_Label1.Size = new System.Drawing.Size(452, 24);
            this.vU_Label1.TabIndex = 31;
            this.vU_Label1.Text = "Введите временное имя";
            this.vU_Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.panel1.Controls.Add(this.Alpha);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 20);
            this.panel1.TabIndex = 30;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
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
            this.Alpha.Text = "Авторизация";
            this.Alpha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Alpha.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Alpha_MouseDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(502, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 20);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EnterName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(552, 162);
            this.Controls.Add(this.Name_TextBox);
            this.Controls.Add(this.vU_Button1);
            this.Controls.Add(this.vU_Label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnterName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ввод имени";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VU_TextBox Name_TextBox;
        private VU_Button vU_Button1;
        private VU_Label vU_Label1;
        private System.Windows.Forms.Panel panel1;
        private VU_Label Alpha;
        private System.Windows.Forms.Button button1;
    }
}