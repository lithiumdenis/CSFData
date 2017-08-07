using System;
using System.Drawing;
using System.Windows.Forms;

namespace Проекции
{
    public partial class FormMain : Form
    {
        bool drawing = false; //Запуск Рисования
        Graphics g0; // Элемент Graphics
        Body td; // Тело Для Работы
        
        //Инициализация Формы
        public FormMain()
        {
            InitializeComponent();
        }

        //Загрузка Формы Пользователем
        private void FormMain_Load(object sender, EventArgs e)
        {
            g0 = CreateGraphics();
            td = new Body(ClientRectangle);
            bitmap = new Bitmap(bitmap, Width, Height);
        }

        //Нажать Мышь
        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
        }

        //Движение Мыши
        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                double a = e.X - Width / 2.0;
                double b = e.Y - Height / 2.0;
                switch (Body.flRotate)
                {
                    case 1:
                        Body.Alf = Math.Atan2(b, a);
                        Body.Bet = Math.Sqrt((a / 10) * (a / 10) + (b / 10) * (b / 10));
                        break;
                    case 0:
                        Body.Alf1 = Math.Atan2(b, a);
                        Body.Bet1 = Math.Sqrt((a / 10) * (a / 10) + (b / 10) * (a / 10));
                        break;
                }
                DrawBody();
            }
        }

        //Отпустить Мышь
        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        //Нарисовать Тело
        public void DrawBody()
        {
            try
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    td.Draw(g);
                }
                g0.DrawImage(bitmap, ClientRectangle);
            }
            catch (NullReferenceException)
            {
            }
        }

        //Отрисовка
        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            DrawBody();
        }

        //Изменение Размера
        private void FormMain_Resize(object sender, EventArgs e)
        {
            DrawBody();
        }

        //Выбрать В Меню Тетраэдр
        private void тетраэдрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Body.flBody = 0;
            Program.formMain.DrawBody();
        }

        //Выбрать В Меню Гексаэдр
        private void гексаэдрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Body.flBody = 1;
            Program.formMain.DrawBody();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Body.Zs = -trackBar1.Value / 50.0;
            Program.formMain.DrawBody();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Body.Xs = -trackBar2.Value / 50.0;
            Program.formMain.DrawBody();
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Body.clr = colorDialog1.Color;
            Program.formMain.DrawBody();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Body.drawAxes == false)
            {
                Body.drawAxes = true;
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                Body.drawAxes = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }
            Program.formMain.DrawBody();
        }

        private void октаэдрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Body.flBody = 2;
            Program.formMain.DrawBody();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Body.flRotate = 0;
            Program.formMain.DrawBody();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Body.flRotate = 1;
            Program.formMain.DrawBody();
        }

        private void икосаэдрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Body.flBody = 3;
            Program.formMain.DrawBody();
        }
    }
}