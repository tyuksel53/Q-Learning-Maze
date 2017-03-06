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
    public partial class Form1 : Form
    {
        public static string path;
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            textBox4.Text = file.FileName;
            path = file.FileName;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            
            String line;
            double[][] Rmatris = new double[9][];
            int i = 0,j=0;
            for (i = 0; i < 9; i++)
            {
                Rmatris[i] = new double[9];
                for (j = 0; j < 9; j++)
                {
                    Rmatris[i][j] = -1;
                }
            }
            i = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                while ( (line = sr.ReadLine() ) != null)
                {                   
                    var satir = line.Split(',');
                    foreach (var item in satir)
                    {
                        if (i == Convert.ToDouble(EndDot.Text))
                        {
                            foreach (var mundi in satir)
                            {
                                Rmatris[Convert.ToInt32(mundi)][i] = 100;
                            }
                        }
                        if (Rmatris[i][Convert.ToInt32(item)] != 100)
                        {
                            Rmatris[i][Convert.ToInt32(item)] = 0;
                        }
                        
                    }
                    i++;
                }
            }
            using (StreamWriter wr = new StreamWriter("C:\\Users\\Ramazan Demir\\Desktop\\Rmatrisi.txt",false))
            {
                for (i = 0; i < 9; i++)
                {
                    for (j = 0; j < 9; j++)
                    {
                        wr.Write(Rmatris[i][j] +  " ");
                    }
                    wr.WriteLine();
                }
            }
            MessageBox.Show("Tamamlandı ve Bitti");
            var Qmatris = Qmatris_Olustur();

            for (int z = 0; z < Convert.ToInt32(iterasyon.Text); z++)
            {
                int baslangicDot = Convert.ToInt32(beginDot.Text);

                //baslangic random olarak seçilir vve olaylar gelişir
                string yol = "";
                while (true)
                {


                    for (i = 0; i < 9; i++)
                    {
                        if (Rmatris[baslangicDot][i] >= 0)
                        {
                            yol = yol + i + "|";
                        }
                    }

                    var yollar = yol.Split('|');

                    if (baslangicDot == 4)
                    {
                        var dummy = 1;
                    }
                    Random lRandom = new Random();
                    int randomIndex = lRandom.Next(0, yollar.Length - 1);
                    int qbaslangic = Convert.ToInt32(yollar[randomIndex]);

                    /*if (baslangicDot == Convert.ToInt32(EndDot.Text))
                    {
                        break;
                    }*/

                    var sonuc = zincir(Rmatris, baslangicDot, qbaslangic, Qmatris);
                    yol = "";
                    
                    Qmatris[baslangicDot][qbaslangic] = Qmatris[baslangicDot][qbaslangic] +  sonuc[1];
                    baslangicDot = Convert.ToInt32(sonuc[0]);
                }

            }

            using (StreamWriter wr = new StreamWriter("C:\\Users\\Ramazan Demir\\Desktop\\Qmatris.txt", false))
            {
                for (i = 0; i < 9; i++)
                {
                    for (j = 0; j < 9; j++)
                    {
                        wr.Write(Qmatris[i][j] + " ");
                    }
                    wr.WriteLine();
                }
            }
            


        }

        protected double[] zincir(double[][] Rmatris, int indexQbegin,int indexQend,double[][] Qmatris)
        {
            int i;
            string yol = "";
            for (i = 0; i < 9; i++)
            {
                if (Rmatris[indexQend][i] >= 0)
                {
                    yol = yol + i + "|";
                }
            }
            var yollar = yol.Split('|');
            yollar = yollar.Take(yollar.Count() - 1).ToArray();

            int[] myInts = Array.ConvertAll(yollar, s => int.Parse(s));

            yol = "";

            for (i = 0; i < myInts.Length; i++)
            {
                yol = yol + Qmatris[indexQend][myInts[i]] + "|";  
            }

            var SonucYollar = yol.Split('|');
            SonucYollar = SonucYollar.Take(SonucYollar.Count() - 1).ToArray();

            int[] MaxQ = Array.ConvertAll(SonucYollar, s => int.Parse(s));

            int maxValue = MaxQ.Max();

            double[] ReturnSonuc = new double[2];

            string randomSec = "";

            for (i = 0; i < myInts.Length; i++)
            {
                if (Qmatris[indexQend][myInts[i]] == maxValue)
                {
                    //aynı max degerler gelirse
                    ReturnSonuc[0] = myInts[i];
                    randomSec = randomSec + myInts[i] + "|";
                    
                }
            }
            // maxlar aynı olursa random seç
            var SonucRandomSec = randomSec.Split('|');

            SonucRandomSec = SonucRandomSec.Take(SonucRandomSec.Count() - 1).ToArray();
            
            Random lRandom = new Random();
            int randomIndex = lRandom.Next(0, SonucRandomSec.Length);

            ReturnSonuc[0] = Convert.ToDouble(SonucRandomSec[randomIndex]);
            // random seç bitti
            
            
            double sonuc = (maxValue * 0.8) + Rmatris[indexQbegin][indexQend];


            ReturnSonuc[1] = sonuc;

            return ReturnSonuc;
        }
        protected double[][] Qmatris_Olustur()
        {
            double[][] Qmatris = new double[9][];
            int i, j;
            for (i = 0; i < 9; i++)
            {
                Qmatris[i] = new double[9];
                for (j = 0; j < 9; j++)
                {
                    Qmatris[i][j] = 0;
                }
            }
            return Qmatris;
        }

    }
}
