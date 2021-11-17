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
    public partial class CreateLesson : Form
    {
        #region --- Для перетаскивания формы ---
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        const uint WM_SYSCOMMAND = 0x0112;
        const uint DOMOVE = 0xF012;
        const uint DOSIZE = 0xF008;
        #endregion

        public CreateLesson()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Alpha_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void vU_Button1_Click(object sender, EventArgs e)
        {
            string str = "";
            if (LessonType.Text != LessonType.GhostText && LessonOwner.Text != LessonOwner.GhostText && LessonName.Text != LessonName.GhostText)
            {
                if (LessonNum.Text != LessonNum.GhostText)
                    str = LessonType.Text + "\n\n" + LessonNum.Text + "\n\n" + LessonOwner.Text + "\n\n#онлайн #очно";
                else
                    str = LessonType.Text + "\n\nВиртуальность\n\n" + LessonOwner.Text + "\n\n#онлайн";

                (string, string) temp;

                if (checkBox1.Checked) {
                    if (LessonPass.Text != LessonPass.GhostText)
                        temp = SQL.CreateNewLesson(LessonName.Text, str, LessonPass.Text, "private");
                    else temp = SQL.CreateNewLesson(LessonName.Text, str, "", "private");
                } else
                {
                    if (LessonPass.Text != LessonPass.GhostText)
                        temp = SQL.CreateNewLesson(LessonName.Text, str, LessonPass.Text, "public");
                    else temp = SQL.CreateNewLesson(LessonName.Text, str, "", "public");
                }

                SQL.Lesson.id = temp.Item1;
                SQL.Lesson.UniqueKey = temp.Item2;
                SQL.Lesson.name = LessonName.Text;
                SQL.ChangeIDLesson(SQL.Lesson.id);
                MajorForm.MainForm.Hide();
                Lesson lesson = new Lesson();
                lesson.Show();
                this.Close();
            }
            else MajorForm.MsgBox("Ошибка создания","Вы должны заполнять все обязательные поля");

        }
    }
}
