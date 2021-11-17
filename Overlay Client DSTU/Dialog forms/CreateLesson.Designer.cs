
namespace Overlay_Client_DSTU
{
    partial class CreateLesson
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateLesson));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Alpha = new Overlay_Client_DSTU.VU_Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.vU_Label4 = new Overlay_Client_DSTU.VU_Label();
            this.LessonPass = new Overlay_Client_DSTU.VU_TextBox();
            this.vU_Label3 = new Overlay_Client_DSTU.VU_Label();
            this.LessonOwner = new Overlay_Client_DSTU.VU_TextBox();
            this.vU_Label2 = new Overlay_Client_DSTU.VU_Label();
            this.LessonType = new Overlay_Client_DSTU.VU_TextBox();
            this.vU_Button1 = new Overlay_Client_DSTU.VU_Button();
            this.vU_Label1 = new Overlay_Client_DSTU.VU_Label();
            this.LessonNum = new Overlay_Client_DSTU.VU_TextBox();
            this.vU_Label10 = new Overlay_Client_DSTU.VU_Label();
            this.LessonName = new Overlay_Client_DSTU.VU_TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(33)))));
            this.panel1.Controls.Add(this.Alpha);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(452, 20);
            this.panel1.TabIndex = 2;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // Alpha
            // 
            this.Alpha.CustomFont = Overlay_Client_DSTU.Painter.Fonts.FontsName.Roboto_Light;
            this.Alpha.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Alpha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.Alpha.Location = new System.Drawing.Point(12, 0);
            this.Alpha.Name = "Alpha";
            this.Alpha.Size = new System.Drawing.Size(224, 20);
            this.Alpha.TabIndex = 3;
            this.Alpha.Text = "Alpha | Создание виртуальной пары";
            this.Alpha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Alpha.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Alpha_MouseDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(402, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 20);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.checkBox1.Location = new System.Drawing.Point(154, 401);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(144, 26);
            this.checkBox1.TabIndex = 35;
            this.checkBox1.Text = "Частная пара";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // vU_Label4
            // 
            this.vU_Label4.BackColor = System.Drawing.Color.Transparent;
            this.vU_Label4.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Label4.Location = new System.Drawing.Point(0, 333);
            this.vU_Label4.Margin = new System.Windows.Forms.Padding(0);
            this.vU_Label4.Name = "vU_Label4";
            this.vU_Label4.Size = new System.Drawing.Size(452, 24);
            this.vU_Label4.TabIndex = 34;
            this.vU_Label4.Text = "Пароль, если необходим";
            this.vU_Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LessonPass
            // 
            this.LessonPass.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LessonPass.GhostText = "Пароль";
            this.LessonPass.Location = new System.Drawing.Point(67, 363);
            this.LessonPass.Margin = new System.Windows.Forms.Padding(10);
            this.LessonPass.Name = "LessonPass";
            this.LessonPass.PasswordChar = '◌';
            this.LessonPass.Size = new System.Drawing.Size(318, 30);
            this.LessonPass.TabIndex = 33;
            this.LessonPass.Text = "Пароль";
            // 
            // vU_Label3
            // 
            this.vU_Label3.BackColor = System.Drawing.Color.Transparent;
            this.vU_Label3.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Label3.Location = new System.Drawing.Point(0, 262);
            this.vU_Label3.Margin = new System.Windows.Forms.Padding(0);
            this.vU_Label3.Name = "vU_Label3";
            this.vU_Label3.Size = new System.Drawing.Size(452, 24);
            this.vU_Label3.TabIndex = 32;
            this.vU_Label3.Text = "Преподаватели";
            this.vU_Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LessonOwner
            // 
            this.LessonOwner.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LessonOwner.GhostText = "Преподаватели";
            this.LessonOwner.Location = new System.Drawing.Point(67, 292);
            this.LessonOwner.Margin = new System.Windows.Forms.Padding(10);
            this.LessonOwner.Name = "LessonOwner";
            this.LessonOwner.PasswordChar = '\0';
            this.LessonOwner.Size = new System.Drawing.Size(318, 30);
            this.LessonOwner.TabIndex = 31;
            this.LessonOwner.Text = "Преподаватели";
            // 
            // vU_Label2
            // 
            this.vU_Label2.BackColor = System.Drawing.Color.Transparent;
            this.vU_Label2.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Label2.Location = new System.Drawing.Point(0, 97);
            this.vU_Label2.Margin = new System.Windows.Forms.Padding(0);
            this.vU_Label2.Name = "vU_Label2";
            this.vU_Label2.Size = new System.Drawing.Size(452, 24);
            this.vU_Label2.TabIndex = 30;
            this.vU_Label2.Text = "Тип пары";
            this.vU_Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LessonType
            // 
            this.LessonType.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LessonType.GhostText = "Тип";
            this.LessonType.Location = new System.Drawing.Point(67, 127);
            this.LessonType.Margin = new System.Windows.Forms.Padding(10);
            this.LessonType.Name = "LessonType";
            this.LessonType.PasswordChar = '\0';
            this.LessonType.Size = new System.Drawing.Size(318, 30);
            this.LessonType.TabIndex = 29;
            this.LessonType.Text = "Тип";
            // 
            // vU_Button1
            // 
            this.vU_Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.vU_Button1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Button1.Location = new System.Drawing.Point(147, 440);
            this.vU_Button1.Margin = new System.Windows.Forms.Padding(10);
            this.vU_Button1.Name = "vU_Button1";
            this.vU_Button1.ShadowColor = System.Drawing.Color.Black;
            this.vU_Button1.Size = new System.Drawing.Size(158, 43);
            this.vU_Button1.TabIndex = 27;
            this.vU_Button1.Text = "Создать";
            this.vU_Button1.UseVisualStyleBackColor = true;
            this.vU_Button1.Click += new System.EventHandler(this.vU_Button1_Click);
            // 
            // vU_Label1
            // 
            this.vU_Label1.BackColor = System.Drawing.Color.Transparent;
            this.vU_Label1.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Label1.Location = new System.Drawing.Point(0, 167);
            this.vU_Label1.Margin = new System.Windows.Forms.Padding(0);
            this.vU_Label1.Name = "vU_Label1";
            this.vU_Label1.Size = new System.Drawing.Size(452, 48);
            this.vU_Label1.TabIndex = 25;
            this.vU_Label1.Text = "Если пара проходит очно, \r\nто укажите кабинет";
            this.vU_Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LessonNum
            // 
            this.LessonNum.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LessonNum.GhostText = "Кабинет";
            this.LessonNum.Location = new System.Drawing.Point(67, 222);
            this.LessonNum.Margin = new System.Windows.Forms.Padding(10);
            this.LessonNum.Name = "LessonNum";
            this.LessonNum.PasswordChar = '\0';
            this.LessonNum.Size = new System.Drawing.Size(318, 30);
            this.LessonNum.TabIndex = 26;
            this.LessonNum.Text = "Кабинет";
            // 
            // vU_Label10
            // 
            this.vU_Label10.BackColor = System.Drawing.Color.Transparent;
            this.vU_Label10.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.vU_Label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            this.vU_Label10.Location = new System.Drawing.Point(0, 28);
            this.vU_Label10.Margin = new System.Windows.Forms.Padding(0);
            this.vU_Label10.Name = "vU_Label10";
            this.vU_Label10.Size = new System.Drawing.Size(452, 24);
            this.vU_Label10.TabIndex = 23;
            this.vU_Label10.Text = "Название пары";
            this.vU_Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LessonName
            // 
            this.LessonName.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LessonName.GhostText = "Название";
            this.LessonName.Location = new System.Drawing.Point(67, 57);
            this.LessonName.Margin = new System.Windows.Forms.Padding(10);
            this.LessonName.Name = "LessonName";
            this.LessonName.PasswordChar = '\0';
            this.LessonName.Size = new System.Drawing.Size(318, 30);
            this.LessonName.TabIndex = 24;
            this.LessonName.Text = "Название";
            // 
            // CreateLesson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(37)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(452, 494);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.vU_Label4);
            this.Controls.Add(this.LessonPass);
            this.Controls.Add(this.vU_Label3);
            this.Controls.Add(this.LessonOwner);
            this.Controls.Add(this.vU_Label2);
            this.Controls.Add(this.LessonType);
            this.Controls.Add(this.vU_Button1);
            this.Controls.Add(this.vU_Label1);
            this.Controls.Add(this.LessonNum);
            this.Controls.Add(this.vU_Label10);
            this.Controls.Add(this.LessonName);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateLesson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание пары";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private VU_Label Alpha;
        private VU_Label vU_Label10;
        private VU_TextBox LessonName;
        private VU_Button vU_Button1;
        private VU_TextBox LessonNum;
        private VU_Label vU_Label1;
        private VU_TextBox LessonType;
        private VU_Label vU_Label2;
        private VU_Label vU_Label3;
        private VU_TextBox LessonOwner;
        private VU_Label vU_Label4;
        private VU_TextBox LessonPass;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}