using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceDetection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Capture myCapture = new Capture();

        private void GetVideo(object sender, EventArgs e)
        {
            CascadeClassifier Cascade = new CascadeClassifier("haarcascade_frontalface_default.xml") ;
            
            Image<Bgr, Byte> image = myCapture.RetrieveBgrFrame().Flip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);
            
            Image<Gray, Byte> grayImage = image.Convert<Gray, Byte>();
            //Ищем признаки лица
            var Faces = Cascade.DetectMultiScale(grayImage, 1.1, 10, Size.Empty, Size.Empty);
           
            foreach (var face in Faces)
            {
                image.Draw(new Rectangle(face.X, face.Y, face.Width, face.Height), new Bgr(255, 255, 255), 10);
            }

            //Выводим обработаное приложение
            pictureBox1.Image = image.ToBitmap();
        }

          private void button1_Click_1(object sender, EventArgs e)
          {
              Application.Idle += GetVideo;
          }
    }
}