using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuadraticSurfaces
{
    public partial class MainForm : Form
    {
        public TDraw3D Ob;
        static Graphics g;
        public static bool flMove = false;
        MouseEventArgs e0;
        bool drawing = false;
        string aboutMessage;
        string aboutCaption;

        public MainForm()
        {
            InitializeComponent();
            эллипсоидToolStripMenuItem.Checked = true;
            выключитьToolStripMenuItem.Checked = true;
            ребраToolStripMenuItem.Checked = true;
            синиеToolStripMenuItem.Checked = true;
            
            синиеToolStripMenuItem.Enabled = false;
            фиолетовыеToolStripMenuItem.Enabled = false;
            жёлтыеToolStripMenuItem.Enabled = false;
            красныеToolStripMenuItem.Enabled = false;
            зелёныеToolStripMenuItem.Enabled = false;
            русскийToolStripMenuItem.Checked = true;

            aboutMessage = "\t Программа «Квадратичные поверхности» \t \n"
            + "\t Выполнил студент 2 курса 6 группы ФКН \n"
                + "\t \t Суходолов Денис \n"
                + "\t \t \t 2014";
            aboutCaption = "О программе";
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            g = Program.formMain.CreateGraphics();
            Ob = new TDraw3D(ClientRectangle.Width, ClientRectangle.Height);
        }

        public void MyDraw() //Функция отрисовки
        {
            try
            {
                Ob.Draw();
                g.DrawImage(TDraw3D.bitmap, Program.formMain.ClientRectangle);
            }
            catch (NullReferenceException)
            { }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e) //Отрисовка
        {
            MyDraw();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e) //Нажать на мышь
        {
            drawing = true;
            e0 = e;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e) //Движение мыши
        {
            if (drawing)
            {
                //Крутить фигуру
                if (e.Button == MouseButtons.Left)
                {
                    double x = e.X - ClientRectangle.Width / 2;
                    double y = e.Y - ClientRectangle.Height / 2;
                    Ob.SetAngle(x, y);
                }
                //Передвигать фигуру
                else
                {
                    Ob.SetDelta(e0, e);
                    e0 = e;
                }
                MyDraw();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e) //Отпустить мышь
        {
            drawing = false;
        }

        private void MainForm_Resize(object sender, System.EventArgs e) //Изменение размера формы
        {
            MyDraw();
        }

        private void эллипсоидToolStripMenuItem_Click(object sender, EventArgs e) //Эллипсоид
        {
            эллипсоидToolStripMenuItem.Checked = true;
            TDraw3D.SurfaceNumber = 0;
            
            однополостныйГиперболоидToolStripMenuItem.Checked = false;
            двуполостныйГиперболоидToolStripMenuItem.Checked = false;
            эллиптическийКонусToolStripMenuItem.Checked = false;
            гиперболическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийЦилиндрToolStripMenuItem.Checked = false;
            параболическийЦилиндрToolStripMenuItem.Checked = false;
            гиперболическийЦилиндрToolStripMenuItem.Checked = false;
            торToolStripMenuItem.Checked = false;

            TDraw3D.fX1 = 0;
            TDraw3D.fX2 = Math.PI;
            TDraw3D.fY1 = -Math.PI;
            TDraw3D.fY2 = Math.PI;

            TDraw3D.a = 1;
            TDraw3D.b = 1;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -3;
            TDraw3D.Xmax = 3;
            TDraw3D.Ymin = -3;
            TDraw3D.Ymax = 3;

            TDraw3D.fn = 36;
            TDraw3D.fm = 36;

            TDraw3D.sg = 0;
            MyDraw();
        }

        private void однополостныйГиперболоидToolStripMenuItem_Click(object sender, EventArgs e) //Однополостный гиперболоид
        {
            однополостныйГиперболоидToolStripMenuItem.Checked = true;
            TDraw3D.SurfaceNumber = 1;
            
            двуполостныйГиперболоидToolStripMenuItem.Checked = false;
            эллиптическийКонусToolStripMenuItem.Checked = false;
            гиперболическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийЦилиндрToolStripMenuItem.Checked = false;
            параболическийЦилиндрToolStripMenuItem.Checked = false;
            гиперболическийЦилиндрToolStripMenuItem.Checked = false;
            торToolStripMenuItem.Checked = false;
            эллипсоидToolStripMenuItem.Checked = false;

            TDraw3D.fX1 = -1;
            TDraw3D.fX2 = 1;
            TDraw3D.fY1 = 0;
            TDraw3D.fY2 = 2 * Math.PI;

            TDraw3D.a = 1;
            TDraw3D.b = 1;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -3;
            TDraw3D.Xmax = 3;
            TDraw3D.Ymin = -3;
            TDraw3D.Ymax = 3;

            TDraw3D.fn = 20;
            TDraw3D.fm = 36;

            TDraw3D.sg = 1;
            MyDraw();
        }

        private void двуполостныйГиперболоидToolStripMenuItem_Click(object sender, EventArgs e) //Двуполостный гиперболоид
        {
            двуполостныйГиперболоидToolStripMenuItem.Checked = true;
            TDraw3D.SurfaceNumber = 2;

            эллиптическийКонусToolStripMenuItem.Checked = false;
            гиперболическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийЦилиндрToolStripMenuItem.Checked = false;
            параболическийЦилиндрToolStripMenuItem.Checked = false;
            гиперболическийЦилиндрToolStripMenuItem.Checked = false;
            торToolStripMenuItem.Checked = false;
            эллипсоидToolStripMenuItem.Checked = false;
            однополостныйГиперболоидToolStripMenuItem.Checked = false;

            TDraw3D.fX1 = 0;
            TDraw3D.fX2 = 3;
            TDraw3D.fY1 = 0;
            TDraw3D.fY2 = 2 * Math.PI;

            TDraw3D.a = 1;
            TDraw3D.b = 1;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -6;
            TDraw3D.Xmax = 6;
            TDraw3D.Ymin = -6;
            TDraw3D.Ymax = 6;

            TDraw3D.fn = 20;
            TDraw3D.fm = 36;

            TDraw3D.sg = 1;
            MyDraw();
        }

        private void эллиптическийКонусToolStripMenuItem_Click(object sender, EventArgs e) //Эллиптический конус
        {
            эллиптическийКонусToolStripMenuItem.Checked = true;
            TDraw3D.SurfaceNumber = 3;

            гиперболическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийЦилиндрToolStripMenuItem.Checked = false;
            параболическийЦилиндрToolStripMenuItem.Checked = false;
            гиперболическийЦилиндрToolStripMenuItem.Checked = false;
            торToolStripMenuItem.Checked = false;
            эллипсоидToolStripMenuItem.Checked = false;
            однополостныйГиперболоидToolStripMenuItem.Checked = false;
            двуполостныйГиперболоидToolStripMenuItem.Checked = false;

            TDraw3D.fX1 = 0;
            TDraw3D.fX2 = 3;
            TDraw3D.fY1 = 0;
            TDraw3D.fY2 = 2 * Math.PI;

            TDraw3D.a = 1;
            TDraw3D.b = 1;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -3;
            TDraw3D.Xmax = 3;
            TDraw3D.Ymin = -3;
            TDraw3D.Ymax = 3;

            TDraw3D.fn = 20;
            TDraw3D.fm = 36;

            TDraw3D.sg = 1;
            MyDraw();
        }

        private void гиперболическийПараболоидToolStripMenuItem_Click(object sender, EventArgs e) //Гиперболический параболоид
        {
            гиперболическийПараболоидToolStripMenuItem.Checked = true;
            TDraw3D.SurfaceNumber = 4;

            эллиптическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийЦилиндрToolStripMenuItem.Checked = false;
            параболическийЦилиндрToolStripMenuItem.Checked = false;
            гиперболическийЦилиндрToolStripMenuItem.Checked = false;
            торToolStripMenuItem.Checked = false;
            эллипсоидToolStripMenuItem.Checked = false;
            однополостныйГиперболоидToolStripMenuItem.Checked = false;
            двуполостныйГиперболоидToolStripMenuItem.Checked = false;
            эллиптическийКонусToolStripMenuItem.Checked = false;
            
            TDraw3D.fX1 = 0;
            TDraw3D.fX2 = 3;
            TDraw3D.fY1 = 0;
            TDraw3D.fY2 = 2 * Math.PI;

            TDraw3D.a = 1;
            TDraw3D.b = 1;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -3;
            TDraw3D.Xmax = 3;
            TDraw3D.Ymin = -3;
            TDraw3D.Ymax = 3;

            TDraw3D.fn = 20;
            TDraw3D.fm = 36;

            TDraw3D.sg = 0;
            MyDraw();
        }

        private void эллиптическийПараболоидToolStripMenuItem_Click(object sender, EventArgs e) //Эллиптический параболоид
        {
            эллиптическийПараболоидToolStripMenuItem.Checked = true;
            TDraw3D.SurfaceNumber = 5;

            эллиптическийЦилиндрToolStripMenuItem.Checked = false;
            параболическийЦилиндрToolStripMenuItem.Checked = false;
            гиперболическийЦилиндрToolStripMenuItem.Checked = false;
            торToolStripMenuItem.Checked = false;
            эллипсоидToolStripMenuItem.Checked = false;
            однополостныйГиперболоидToolStripMenuItem.Checked = false;
            двуполостныйГиперболоидToolStripMenuItem.Checked = false;
            эллиптическийКонусToolStripMenuItem.Checked = false;
            гиперболическийПараболоидToolStripMenuItem.Checked = false;

            TDraw3D.fX1 = 0;
            TDraw3D.fX2 = 3;
            TDraw3D.fY1 = 0;
            TDraw3D.fY2 = 2 * Math.PI;

            TDraw3D.a = 1;
            TDraw3D.b = 1;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -3;
            TDraw3D.Xmax = 3;
            TDraw3D.Ymin = -3;
            TDraw3D.Ymax = 3;

            TDraw3D.fn = 20;
            TDraw3D.fm = 36;

            TDraw3D.sg = 0;
            MyDraw();
        }

        private void эллиптическийЦилиндрToolStripMenuItem_Click(object sender, EventArgs e) //Эллиптический цилиндр
        {
            эллиптическийЦилиндрToolStripMenuItem.Checked = true;
            TDraw3D.SurfaceNumber = 6;

            параболическийЦилиндрToolStripMenuItem.Checked = false;
            гиперболическийЦилиндрToolStripMenuItem.Checked = false;
            торToolStripMenuItem.Checked = false;
            эллипсоидToolStripMenuItem.Checked = false;
            однополостныйГиперболоидToolStripMenuItem.Checked = false;
            двуполостныйГиперболоидToolStripMenuItem.Checked = false;
            эллиптическийКонусToolStripMenuItem.Checked = false;
            гиперболическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийПараболоидToolStripMenuItem.Checked = false;
            
            TDraw3D.fX1 = 0;
            TDraw3D.fX2 = 3;
            TDraw3D.fY1 = 0;
            TDraw3D.fY2 = 2 * Math.PI;

            TDraw3D.a = 1;
            TDraw3D.b = 1;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -3;
            TDraw3D.Xmax = 3;
            TDraw3D.Ymin = -3;
            TDraw3D.Ymax = 3;

            TDraw3D.fn = 20;
            TDraw3D.fm = 36;

            TDraw3D.sg = 0;
            MyDraw();
        }

        private void параболическийЦилиндрToolStripMenuItem_Click(object sender, EventArgs e) //Параболический цилиндр
        {
            параболическийЦилиндрToolStripMenuItem.Checked = true;
            TDraw3D.SurfaceNumber = 7;

            гиперболическийЦилиндрToolStripMenuItem.Checked = false;
            торToolStripMenuItem.Checked = false;
            эллипсоидToolStripMenuItem.Checked = false;
            однополостныйГиперболоидToolStripMenuItem.Checked = false;
            двуполостныйГиперболоидToolStripMenuItem.Checked = false;
            эллиптическийКонусToolStripMenuItem.Checked = false;
            гиперболическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийЦилиндрToolStripMenuItem.Checked = false;
            
            TDraw3D.fX1 = 0;
            TDraw3D.fX2 = 3;
            TDraw3D.fY1 = -1;
            TDraw3D.fY2 = 1;

            TDraw3D.a = 1;
            TDraw3D.b = 1;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -3;
            TDraw3D.Xmax = 3;
            TDraw3D.Ymin = -3;
            TDraw3D.Ymax = 3;

            TDraw3D.fn = 20;
            TDraw3D.fm = 20;

            TDraw3D.sg = 0;
            MyDraw();
        }

        private void гиперболическийЦилиндрToolStripMenuItem_Click(object sender, EventArgs e) //Гиперболический цилиндр
        {
            гиперболическийЦилиндрToolStripMenuItem.Checked = true;
            TDraw3D.SurfaceNumber = 8;

            торToolStripMenuItem.Checked = false;
            эллипсоидToolStripMenuItem.Checked = false;
            однополостныйГиперболоидToolStripMenuItem.Checked = false;
            двуполостныйГиперболоидToolStripMenuItem.Checked = false;
            эллиптическийКонусToolStripMenuItem.Checked = false;
            гиперболическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийЦилиндрToolStripMenuItem.Checked = false;
            параболическийЦилиндрToolStripMenuItem.Checked = false;

            TDraw3D.fX1 = 0;
            TDraw3D.fX2 = 3;
            TDraw3D.fY1 = -1;
            TDraw3D.fY2 = 1;

            TDraw3D.a = 1;
            TDraw3D.b = 1;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -3;
            TDraw3D.Xmax = 3;
            TDraw3D.Ymin = -3;
            TDraw3D.Ymax = 3;

            TDraw3D.fn = 20;
            TDraw3D.fm = 20;

            TDraw3D.sg = 1;
            MyDraw();
        }

        private void торToolStripMenuItem_Click(object sender, EventArgs e) //Тор
        {
            торToolStripMenuItem.Checked  = true;
            TDraw3D.SurfaceNumber = 9;

            эллипсоидToolStripMenuItem.Checked = false;
            однополостныйГиперболоидToolStripMenuItem.Checked = false;
            двуполостныйГиперболоидToolStripMenuItem.Checked = false;
            эллиптическийКонусToolStripMenuItem.Checked = false;
            гиперболическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийПараболоидToolStripMenuItem.Checked = false;
            эллиптическийЦилиндрToolStripMenuItem.Checked = false;
            параболическийЦилиндрToolStripMenuItem.Checked = false;
            гиперболическийЦилиндрToolStripMenuItem.Checked= false;

            TDraw3D.fX1 = 0;
            TDraw3D.fX2 = 2 * Math.PI;
            TDraw3D.fY1 = -Math.PI;
            TDraw3D.fY2 = Math.PI;

            TDraw3D.a = 1;
            TDraw3D.b = 0.55;
            TDraw3D.c = 1;

            TDraw3D.Xmin = -3;
            TDraw3D.Xmax = 3;
            TDraw3D.Ymin = -3;
            TDraw3D.Ymax = 3;

            TDraw3D.fn = 25;
            TDraw3D.fm = 25;

            TDraw3D.sg = 1;
            MyDraw();
        }

        private void включитьToolStripMenuItem_Click(object sender, EventArgs e) //Показывать оси
        {
            включитьToolStripMenuItem.Checked = true;
            выключитьToolStripMenuItem.Checked = false;
            TDraw3D.drawAxis = true;
            MyDraw();
        }

        private void выключитьToolStripMenuItem_Click(object sender, EventArgs e) //Не показывать оси
        {
            включитьToolStripMenuItem.Checked = false;
            выключитьToolStripMenuItem.Checked = true;
            TDraw3D.drawAxis = false;
            MyDraw();
        }

        private void ребраToolStripMenuItem_Click(object sender, EventArgs e) //Показать ребра
        {
            ребраToolStripMenuItem.Checked = true;
            граниToolStripMenuItem.Checked = false;
            TDraw3D.typeFace = false;
            MyDraw();

            синиеToolStripMenuItem.Enabled = false;
            фиолетовыеToolStripMenuItem.Enabled = false;
            жёлтыеToolStripMenuItem.Enabled = false;
            красныеToolStripMenuItem.Enabled = false;
            зелёныеToolStripMenuItem.Enabled = false;
        }

        private void граниToolStripMenuItem_Click(object sender, EventArgs e) //Показать грани
        {
            ребраToolStripMenuItem.Checked = false;
            граниToolStripMenuItem.Checked = true;
            TDraw3D.typeFace = true;
            MyDraw();

            синиеToolStripMenuItem.Enabled = true;
            фиолетовыеToolStripMenuItem.Enabled = true;
            жёлтыеToolStripMenuItem.Enabled = true;
            красныеToolStripMenuItem.Enabled = true;
            зелёныеToolStripMenuItem.Enabled = true;
        }

        private void синиеToolStripMenuItem_Click(object sender, EventArgs e) //Синие грани
        {
            синиеToolStripMenuItem.Checked = true;
            фиолетовыеToolStripMenuItem.Checked = false;
            жёлтыеToolStripMenuItem.Checked = false;
            красныеToolStripMenuItem.Checked = false;
            зелёныеToolStripMenuItem.Checked = false;

            TDraw3D.clrBlue = true;
            TDraw3D.clrPurple = false;
            TDraw3D.clrYellow = false;
            TDraw3D.clrRed = false;
            TDraw3D.clrGreen = false;
            MyDraw();
        }

        private void фиолетовыеToolStripMenuItem_Click(object sender, EventArgs e) //Фиолетовые грани
        {
            фиолетовыеToolStripMenuItem.Checked = true;

            жёлтыеToolStripMenuItem.Checked = false;
            красныеToolStripMenuItem.Checked = false;
            зелёныеToolStripMenuItem.Checked = false;
            синиеToolStripMenuItem.Checked = false;
            
            TDraw3D.clrBlue = false;
            TDraw3D.clrPurple = true;
            TDraw3D.clrYellow = false;
            TDraw3D.clrRed = false;
            TDraw3D.clrGreen = false;
            MyDraw();
        }

        private void жёлтыеToolStripMenuItem_Click(object sender, EventArgs e) //Желтые грани
        {
            жёлтыеToolStripMenuItem.Checked = true;
            красныеToolStripMenuItem.Checked = false;
            зелёныеToolStripMenuItem.Checked = false;
            синиеToolStripMenuItem.Checked = false;
            фиолетовыеToolStripMenuItem.Checked = false;
            
            TDraw3D.clrBlue = false;
            TDraw3D.clrPurple = false;
            TDraw3D.clrYellow = true;
            TDraw3D.clrRed = false;
            TDraw3D.clrGreen = false;
            MyDraw();
        }

        private void красныеToolStripMenuItem_Click(object sender, EventArgs e) //Красные грани
        {
            красныеToolStripMenuItem.Checked = true;
            зелёныеToolStripMenuItem.Checked = false;
            синиеToolStripMenuItem.Checked = false;
            фиолетовыеToolStripMenuItem.Checked = false;
            жёлтыеToolStripMenuItem.Checked = false;
            
            TDraw3D.clrBlue = false;
            TDraw3D.clrPurple = false;
            TDraw3D.clrYellow = false;
            TDraw3D.clrRed = true;
            TDraw3D.clrGreen = false;
            MyDraw();
        }

        private void зелёныеToolStripMenuItem_Click(object sender, EventArgs e) //Зеленые грани
        {
            зелёныеToolStripMenuItem.Checked = true;
            синиеToolStripMenuItem.Checked = false;
            фиолетовыеToolStripMenuItem.Checked = false;
            жёлтыеToolStripMenuItem.Checked = false;
            красныеToolStripMenuItem.Checked = false;
            
            TDraw3D.clrBlue = false;
            TDraw3D.clrPurple = false;
            TDraw3D.clrYellow = false;
            TDraw3D.clrRed = false;
            TDraw3D.clrGreen = true;
            MyDraw();
        }

        private void увеличитьToolStripMenuItem_Click(object sender, EventArgs e) //Масштаб +
        {
            TDraw3D.Xmax = 0.8 * TDraw3D.Xmax;
            TDraw3D.Xmin = 0.8 * TDraw3D.Xmin;
            TDraw3D.Ymax = 0.8 * TDraw3D.Ymax;
            TDraw3D.Ymin = 0.8 * TDraw3D.Ymin;
            MyDraw();
        }

        private void уменьшитьToolStripMenuItem_Click(object sender, EventArgs e) //Масштаб -
        {
            TDraw3D.Xmax = 1.2 * TDraw3D.Xmax;
            TDraw3D.Xmin = 1.2 * TDraw3D.Xmin;
            TDraw3D.Ymax = 1.2 * TDraw3D.Ymax;
            TDraw3D.Ymin = 1.2 * TDraw3D.Ymin;
            MyDraw();
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void русскийToolStripMenuItem_Click(object sender, EventArgs e) //Русская локализация
        {
            русскийToolStripMenuItem.Checked = true;
            englishToolStripMenuItem.Checked = false;
            deutschToolStripMenuItem.Checked = false;

            менюToolStripMenuItem.Text = "Меню";
            поверхностьToolStripMenuItem.Text = "Поверхность";
            эллипсоидToolStripMenuItem.Text = "Эллипсоид";
            однополостныйГиперболоидToolStripMenuItem.Text = "Однополостный гиперболоид";
            двуполостныйГиперболоидToolStripMenuItem.Text = "Двуполостный гиперболоид";
            эллиптическийКонусToolStripMenuItem.Text = "Эллиптический конус";
            гиперболическийПараболоидToolStripMenuItem.Text = "Гиперболический параболоид";
            эллиптическийПараболоидToolStripMenuItem.Text = "Эллиптический параболоид";
            эллиптическийЦилиндрToolStripMenuItem.Text = "Эллиптический цилиндр";
            параболическийЦилиндрToolStripMenuItem.Text = "Параболический цилиндр";
            гиперболическийЦилиндрToolStripMenuItem.Text = "Гиперболический цилиндр";
            торToolStripMenuItem.Text = "Тор";
            осиToolStripMenuItem.Text = "Оси координат";
            включитьToolStripMenuItem.Text = "Показать";
            выключитьToolStripMenuItem.Text = "Скрыть";
            представлениеПоверхностиToolStripMenuItem.Text = "Представление";
            ребраToolStripMenuItem.Text = "Ребра";
            граниToolStripMenuItem.Text = "Грани";
            синиеToolStripMenuItem.Text = "Синие";
            фиолетовыеToolStripMenuItem.Text = "Фиолетовые";
            жёлтыеToolStripMenuItem.Text = "Жёлтые";
            красныеToolStripMenuItem.Text = "Красные";
            зелёныеToolStripMenuItem.Text = "Зелёные";
            масштабToolStripMenuItem.Text = "Масштаб";
            увеличитьToolStripMenuItem.Text = "Увеличить";
            уменьшитьToolStripMenuItem.Text = "Уменьшить";
            языкToolStripMenuItem.Text = "Язык";
            закрытьToolStripMenuItem.Text = "Выход";

            aboutMessage = "\t Программа «Квадратичные поверхности» \t \n"
            + "\t Выполнил студент 2 курса 6 группы ФКН \n"
                + "\t \t Суходолов Денис \n"
                + "\t \t \t 2014";
            aboutCaption = "О программе";
            оПрограммеToolStripMenuItem.Text = "О программе";
            Text = "Квадратичные поверхности";
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e) //Английская локализация
        {
            русскийToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = true;
            deutschToolStripMenuItem.Checked = false;

            менюToolStripMenuItem.Text = "Menu";
            поверхностьToolStripMenuItem.Text = "Surface";
            эллипсоидToolStripMenuItem.Text = "Ellipsoid";
            однополостныйГиперболоидToolStripMenuItem.Text = "Hyperboloid of one sheet";
            двуполостныйГиперболоидToolStripMenuItem.Text = "Hyperboloid of two sheets";
            эллиптическийКонусToolStripMenuItem.Text = "Elliptic cone";
            гиперболическийПараболоидToolStripMenuItem.Text = "Hyperbolic paraboloid";
            эллиптическийПараболоидToolStripMenuItem.Text = "Elliptic paraboloid";
            эллиптическийЦилиндрToolStripMenuItem.Text = "Elliptic cylinder";
            параболическийЦилиндрToolStripMenuItem.Text = "Parabolic cylinder";
            гиперболическийЦилиндрToolStripMenuItem.Text = "Hyperbolic cylinder";
            торToolStripMenuItem.Text = "Torus";
            осиToolStripMenuItem.Text = "Axes";
            включитьToolStripMenuItem.Text = "Visible";
            выключитьToolStripMenuItem.Text = "Invisible";
            представлениеПоверхностиToolStripMenuItem.Text = "Presentation";
            ребраToolStripMenuItem.Text = "Edges";
            граниToolStripMenuItem.Text = "Sides";
            синиеToolStripMenuItem.Text = "Blue";
            фиолетовыеToolStripMenuItem.Text = "Purple";
            жёлтыеToolStripMenuItem.Text = "Yellow";
            красныеToolStripMenuItem.Text = "Red";
            зелёныеToolStripMenuItem.Text = "Green";
            масштабToolStripMenuItem.Text = "Scale";
            увеличитьToolStripMenuItem.Text = "Up";
            уменьшитьToolStripMenuItem.Text = "Down";
            языкToolStripMenuItem.Text = "Language";
            закрытьToolStripMenuItem.Text = "Exit";

            aboutMessage = "\t \t Program «Quadratic Surfaces» \t \n"
            + "\t Created by 2nd year student of 6th group CSF \n"
                + "\t \t Sukhodolov Denis \n"
                + "\t \t \t 2014";
            aboutCaption = "About program";
            оПрограммеToolStripMenuItem.Text = "About program";
            Text = "Quadratic Surfaces";
        }

        private void deutschToolStripMenuItem_Click(object sender, EventArgs e) //Немецкая локализация
        {
            русскийToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = false;
            deutschToolStripMenuItem.Checked = true;

            менюToolStripMenuItem.Text = "Speisekarte";
            поверхностьToolStripMenuItem.Text = "Quadrik";
            эллипсоидToolStripMenuItem.Text = "Ellipsoid";
            однополостныйГиперболоидToolStripMenuItem.Text = "Einschaliges Hyperboloid";
            двуполостныйГиперболоидToolStripMenuItem.Text = "Zweischaliges Hyperboloid";
            эллиптическийКонусToolStripMenuItem.Text = "Zwei schneidende Ebenen";
            гиперболическийПараболоидToolStripMenuItem.Text = "Hyperbolisches Paraboloid";
            эллиптическийПараболоидToolStripMenuItem.Text = "Elliptisches Paraboloid";
            эллиптическийЦилиндрToolStripMenuItem.Text = "Zwei parallele Ebenen";
            параболическийЦилиндрToolStripMenuItem.Text = "Eine Gerade";
            гиперболическийЦилиндрToolStripMenuItem.Text = "Eine Ebene";
            торToolStripMenuItem.Text = "Torus";
            осиToolStripMenuItem.Text = "Achsen";
            включитьToolStripMenuItem.Text = "Sichtbar";
            выключитьToolStripMenuItem.Text = "Unsichtbar";
            представлениеПоверхностиToolStripMenuItem.Text = "Präsentation";
            ребраToolStripMenuItem.Text = "Kanten";
            граниToolStripMenuItem.Text = "Seite";
            синиеToolStripMenuItem.Text = "Blau";
            фиолетовыеToolStripMenuItem.Text = "Lila";
            жёлтыеToolStripMenuItem.Text = "Gelb";
            красныеToolStripMenuItem.Text = "Rot";
            зелёныеToolStripMenuItem.Text = "Grün";
            масштабToolStripMenuItem.Text = "Maßstab";
            увеличитьToolStripMenuItem.Text = "Plus";
            уменьшитьToolStripMenuItem.Text = "Minus";
            языкToolStripMenuItem.Text = "Sprache";
            закрытьToolStripMenuItem.Text = "Schließen";

            aboutMessage = "\t \t Programm «Quadrik» \t \n"
            + "\t Erstellt von zweites studienjahr der sechsten gruppe CSF \n"
                + "\t \t Sukhodolov Denis \n"
                + "\t \t \t 2014";
            aboutCaption = "Über programm";
            оПрограммеToolStripMenuItem.Text = "Über programm";
            Text = "Quadrik";
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(aboutMessage, aboutCaption, buttons);
        }
    }
}