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
            using (StreamWriter wr = new StreamWriter("C:\\Users\\Taha\\Desktop\\Rmatrisi.txt",false))
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
            int hedef = Convert.ToInt32(EndDot.Text);
            var Qmatris = Qmatris_Olustur();

            for (int z = 0; z < Convert.ToInt32(iterasyon.Text); z++)
            {
                int baslangicDot = Convert.ToInt32(beginDot.Text);

                string yol = "";

                for (i = 0; i < 9; i++)
                {
                    if (Rmatris[baslangicDot][i] >= 0)
                    {
                        yol = yol + i + "|";
                    }
                }

                var yollar = yol.Split('|');

                Random lRandom = new Random();
                int randomIndex = lRandom.Next(0, yollar.Length - 1);

                int qbaslangic = Convert.ToInt32(yollar[randomIndex]);
                bool control = false;
                yol = "";
                while (true)
                {
                    var sonuc = zincir(Rmatris, baslangicDot, qbaslangic, Qmatris);
                    
                    
                    Qmatris[baslangicDot][qbaslangic] = sonuc[2];

                    baslangicDot  =  Convert.ToInt32(sonuc[0]);
                    qbaslangic    =  Convert.ToInt32(sonuc[1]);

                    if (control == true)
                    {
                        break;
                    }
                    if (hedef == qbaslangic)
                    {
                        control = true;
                    }

                }

            }

            using (StreamWriter wr = new StreamWriter("C:\\Users\\Taha\\Desktop\\Qmatris.txt", false))
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
            MessageBox.Show("Tamamlandı ve Bitti");
        }

        protected double[] zincir(double[][] Rmatris, int indexQbegin,int indexQend,double[][] Qmatris)
        {
            double[] ReturnSonuc = new double[3];

            ReturnSonuc[0] = indexQend; //-> bir sonrakinin başlangici

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
                if (Qmatris[indexQend][myInts[i]] > 0)
                {
                    var dummy = 1;
                }
                yol = yol + Qmatris[indexQend][myInts[i]] + "*";  
            }
            Random randomYol = new Random();
            int randomYolIndex = randomYol.Next(0, myInts.Length);

            ReturnSonuc[1] = myInts[randomYolIndex]; // -> devam yolunun rastgele belirlenmesi

            var SonucYollar = yol.Split('*');
            SonucYollar = SonucYollar.Take(SonucYollar.Count() - 1).ToArray();

            double[] MaxQ = Array.ConvertAll(SonucYollar, s => double.Parse(s));

            double maxValue = MaxQ.Max();
            
            double sonuc = (maxValue * 0.8) + Rmatris[indexQbegin][indexQend];


            ReturnSonuc[2] = sonuc;

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
