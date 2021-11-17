
namespace Overlay_Client_DSTU
{
    partial class UnusedTestForm
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
            this.vU_Story1 = new Overlay_Client_DSTU.VU_Story();
            this.SuspendLayout();
            // 
            // vU_Story1
            // 
            this.vU_Story1.BackgroundImage = global::Overlay_Client_DSTU.Properties.Resources.ShyAway;
            this.vU_Story1.Location = new System.Drawing.Point(586, 46);
            this.vU_Story1.Name = "vU_Story1";
            this.vU_Story1.Size = new System.Drawing.Size(225, 400);
            this.vU_Story1.TabIndex = 0;
            this.vU_Story1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(183)))), ((int)(((byte)(195)))));
            // 
            // UnusedTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(35)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(1465, 735);
            this.Controls.Add(this.vU_Story1);
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Name = "UnusedTestForm";
            this.Text = "UnusedTestForm";
            this.Load += new System.EventHandler(this.UnusedTestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VU_Story vU_Story1;
    }
}