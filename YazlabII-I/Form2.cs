using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazlabII_I
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawIt();
        }
        private void DrawIt()
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
                250, 50, 300, 300);
            string sonucYol = "0-3-4-1-2-5-8";

            //graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
            //graphics.TranslateTransform(100, 50);
            // graphics.PageUnit = GraphicsUnit.Inch;

            //graphics.DrawLine(System.Drawing.Pens.Blue, 0, 0, 160, 80);
            int beginDot = 0;
            int endDot = 8;
            double maTrixBoYut = 9;
            maTrixBoYut = Math.Sqrt(maTrixBoYut);
            graphics.DrawRectangle(System.Drawing.Pens.Black, rectangle);
            double lenght = 300/maTrixBoYut;
            for (int i = 1; i < maTrixBoYut; i++)
            {
                graphics.DrawLine(System.Drawing.Pens.Black, 250, Convert.ToInt32(lenght * i) + 50, 550, Convert.ToInt32(lenght * i) +50);
            }
            for (int i = 1; i < maTrixBoYut; i++)
            {
                graphics.DrawLine(System.Drawing.Pens.Black, Convert.ToInt32(lenght * i) + 250, 50, Convert.ToInt32(lenght * i) + 250, 350);
            }
            double kalan = beginDot / maTrixBoYut;
            if (beginDot%maTrixBoYut == 0)
            {
                kalan = beginDot / maTrixBoYut;
                var zundi = Convert.ToInt32(lenght*(kalan + 1))/2;
                graphics.DrawLine(System.Drawing.Pens.Linen, 250, Convert.ToInt32(lenght * kalan) + 50, 250, Convert.ToInt32(lenght * (kalan + 1)) + 50);
                graphics.DrawLine(System.Drawing.Pens.Red, 250, zundi + Convert.ToInt32((lenght / 2)*kalan) + 50, 250 +Convert.ToInt32(lenght / 2) ,  zundi+ Convert.ToInt32((lenght / 2)*kalan) + 50);
            }
            else if( (beginDot + 1) % maTrixBoYut == 0)
            {
                kalan = (beginDot + 1) / maTrixBoYut;
                kalan--;
                var zundi = Convert.ToInt32(lenght * (kalan + 1)) / 2;
                graphics.DrawLine(System.Drawing.Pens.Red, 550, zundi + Convert.ToInt32((lenght / 2) * kalan) + 50, 550 - Convert.ToInt32(lenght / 2), zundi + Convert.ToInt32((lenght / 2) * kalan) + 50);
                graphics.DrawLine(System.Drawing.Pens.Linen, 550, Convert.ToInt32(lenght * kalan) + 50, 550, Convert.ToInt32(lenght * (kalan + 1)) + 50);
            }
            else if (beginDot < maTrixBoYut)
            {
                graphics.DrawLine(System.Drawing.Pens.Red, 250 + Convert.ToInt32(lenght / 2) + Convert.ToInt32(lenght * beginDot), 50, 250 + Convert.ToInt32(lenght / 2) + Convert.ToInt32(lenght * beginDot), 50 + Convert.ToInt32(lenght/2));
                graphics.DrawLine(System.Drawing.Pens.Linen, 250 + Convert.ToInt32(lenght * beginDot), 50, 250 + Convert.ToInt32(lenght * (beginDot + 1)),50);

            } else if (beginDot >= maTrixBoYut*(maTrixBoYut - 1) && beginDot <= ((maTrixBoYut*maTrixBoYut) - 1))
            {
                kalan = beginDot - ( maTrixBoYut*(maTrixBoYut - 1) );
                graphics.DrawLine(System.Drawing.Pens.Red, 250 + Convert.ToInt32(lenght * kalan), 350 - Convert.ToInt32((lenght / 2) * kalan), 250 + Convert.ToInt32(lenght * (kalan + 1)), 350);
                graphics.DrawLine(System.Drawing.Pens.Linen, 250 + Convert.ToInt32(lenght * kalan), 350, 250 + Convert.ToInt32(lenght * (kalan + 1)), 350);
            }


            if (endDot % maTrixBoYut == 0)
            {

                kalan = endDot / maTrixBoYut;
                var zundi = Convert.ToInt32(lenght * (kalan + 1)) / 2;
                graphics.DrawLine(System.Drawing.Pens.Red, 250, zundi + Convert.ToInt32((lenght / 2) * kalan) + 50, 250 + Convert.ToInt32(lenght / 2), zundi + Convert.ToInt32((lenght / 2) * kalan) + 50);
                graphics.DrawLine(System.Drawing.Pens.Linen, 250, Convert.ToInt32(lenght * kalan) + 50, 250, Convert.ToInt32(lenght * (kalan + 1)) + 50);
            }
            else if ((endDot + 1) % maTrixBoYut == 0)
            {
                kalan = (endDot + 1) / maTrixBoYut;
                kalan--;
                var zundi = Convert.ToInt32(lenght * (kalan + 1)) / 2;
                graphics.DrawLine(System.Drawing.Pens.Red, 550, zundi + Convert.ToInt32((lenght / 2) * kalan) + 50, 550 - Convert.ToInt32(lenght / 2), zundi + Convert.ToInt32((lenght / 2) * kalan) + 50);
                graphics.DrawLine(System.Drawing.Pens.Linen, 550, Convert.ToInt32(lenght * kalan) + 50, 550, Convert.ToInt32(lenght * (kalan + 1)) + 50);
            }
            else if (endDot < maTrixBoYut)
            {
                graphics.DrawLine(System.Drawing.Pens.Linen, 250 + Convert.ToInt32(lenght * endDot), 50, 250 + Convert.ToInt32(lenght * (endDot + 1)), 50);
                graphics.DrawLine(System.Drawing.Pens.Red, 250 + Convert.ToInt32(lenght * kalan), 350 - Convert.ToInt32((lenght / 2) * kalan), 250 + Convert.ToInt32(lenght * (kalan + 1)), 350);
            }
            else if (endDot >= maTrixBoYut * (maTrixBoYut - 1) && endDot <= ((maTrixBoYut * maTrixBoYut) - 1))
            {
                kalan = endDot - (maTrixBoYut * (maTrixBoYut - 1));
                graphics.DrawLine(System.Drawing.Pens.Red, 250 +Convert.ToInt32(lenght/2) + Convert.ToInt32(lenght * kalan), 350 - Convert.ToInt32((lenght / 2)), 250 + Convert.ToInt32(lenght / 2) + Convert.ToInt32(lenght * (kalan )), 350);
                graphics.DrawLine(System.Drawing.Pens.Linen, 250 + Convert.ToInt32(lenght * kalan), 350, 250 + Convert.ToInt32(lenght * (kalan + 1)), 350);
            }
            double kaldi = 0;
            double k = 0;
            using (StreamReader ty = new StreamReader("C:\\Users\\Taha\\Desktop\\ornekGirdiler\\input33.txt"))
            {
                String satir;
                
                while ((satir = ty.ReadLine()) != null)
                {
                    var komsu = satir.Split(',');
                    
                    int[] myInts = Array.ConvertAll(komsu, a => int.Parse(a));
                    for (int t = 0; t < myInts.Length; t++)
                    {
                        if (myInts[t] == (k + maTrixBoYut))
                        {
                            var gereksiz = (Convert.ToInt32(Math.Floor(k/maTrixBoYut))*maTrixBoYut);
                            kaldi = (k - gereksiz);
                            graphics.DrawLine(System.Drawing.Pens.Linen, 250+ Convert.ToInt32(lenght * kaldi), 50 + Convert.ToInt32(lenght * (Convert.ToInt32(Math.Floor(k / maTrixBoYut)) +1)), 250 + Convert.ToInt32(lenght * (kaldi+1)), 50 + Convert.ToInt32(lenght * (Convert.ToInt32(Math.Floor(k / maTrixBoYut)) + 1)));
                        }
                        else if(myInts[t] == (k+1) )
                        {
                            var gereksiz = (Convert.ToInt32(Math.Floor(k / maTrixBoYut)) * maTrixBoYut);
                            kaldi = (k - gereksiz);
                            graphics.DrawLine(System.Drawing.Pens.Linen, 250 + Convert.ToInt32(lenght * (kaldi+1)), 50 + Convert.ToInt32(lenght * (Convert.ToInt32(Math.Floor(k / maTrixBoYut)))), 250 + Convert.ToInt32(lenght * (kaldi+1)), 50 + Convert.ToInt32(lenght * (Convert.ToInt32(Math.Floor(k / maTrixBoYut)) + 1)));
                        }
                        else if (myInts[t] == (k - 1))
                        {
                            var gereksiz = (Convert.ToInt32(Math.Floor(k / maTrixBoYut)) * maTrixBoYut);
                            kaldi = (k - gereksiz);
                            graphics.DrawLine(System.Drawing.Pens.Linen, 250 + Convert.ToInt32(lenght * (kaldi)), 50 + Convert.ToInt32(lenght * (Convert.ToInt32(Math.Floor(k / maTrixBoYut)))), 250 + Convert.ToInt32(lenght * (kaldi)), 50 + Convert.ToInt32(lenght * (Convert.ToInt32(Math.Floor(k / maTrixBoYut)) + 1)));
                        }else if (myInts[t] == (k - maTrixBoYut))
                        {
                            var gereksiz = (Convert.ToInt32(Math.Floor(k / maTrixBoYut)) * maTrixBoYut);
                            kaldi = (k - gereksiz);
                            graphics.DrawLine(System.Drawing.Pens.Linen, 250 + Convert.ToInt32(lenght * (kaldi)), 50 + Convert.ToInt32(lenght * (Convert.ToInt32(Math.Floor(k / maTrixBoYut)))), 250 + Convert.ToInt32(lenght * (kaldi +1)), 50 + Convert.ToInt32(lenght * (Convert.ToInt32(Math.Floor(k / maTrixBoYut)))));
                        }
                    }
                    k++;

                }
            }
            var yol = sonucYol.Split('-');
            kaldi = 0;
            int s = Convert.ToInt32(yol[1]);
            k = Convert.ToInt32(yol[0]);
            int x1 = 0;
            int y1 = 0;
            var satirDurum = Convert.ToInt32(Convert.ToInt32(Math.Floor(k / maTrixBoYut)));
            var coordinatX = k - (satirDurum*maTrixBoYut);
            x1 = Convert.ToInt32(coordinatX*lenght) + Convert.ToInt32(lenght/2);

            var coordinatY = (satirDurum*lenght) + (lenght/2);

            y1 = Convert.ToInt32(coordinatY);
            

            for (int t = 0; t < yol.Length-1; t++)
            {
                s = Convert.ToInt32(yol[t+1]);
                k = Convert.ToInt32(yol[t]);

                if (s == (k + maTrixBoYut))
                {
                    graphics.DrawLine(System.Drawing.Pens.Red,Convert.ToInt32(250 + x1) ,Convert.ToInt32(50 + y1),Convert.ToInt32(250 + x1),Convert.ToInt32(50 + y1 + lenght));
                    y1 = Convert.ToInt32(y1 + lenght);
                }
                else if (s == (k + 1))
                {
                    graphics.DrawLine(System.Drawing.Pens.Red, Convert.ToInt32(250 + x1), Convert.ToInt32(50 + y1), Convert.ToInt32(250 +lenght + x1), Convert.ToInt32(50 + y1));
                    x1 = Convert.ToInt32(x1 + lenght);
                }
                else if (s == (k - 1))
                {
                    graphics.DrawLine(System.Drawing.Pens.Red, Convert.ToInt32(250 + x1 -lenght), Convert.ToInt32(50 + y1), Convert.ToInt32(250 + x1 ), Convert.ToInt32(50 + y1));
                    x1 = Convert.ToInt32(x1 - lenght);
                }
                else if (s == (k - maTrixBoYut))
                {
                    graphics.DrawLine(System.Drawing.Pens.Red, Convert.ToInt32(250 + x1), Convert.ToInt32(50 + y1), Convert.ToInt32(250 + x1), Convert.ToInt32(50 + y1 - lenght));
                    y1 = Convert.ToInt32(y1 - lenght);
                }

            }
        }
    }
}
