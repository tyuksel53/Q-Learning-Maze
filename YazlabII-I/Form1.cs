using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            int MatrixBoyut = 0;
            using (StreamReader ty = new StreamReader(path))
            {
                String satir;
               
                while ((satir = ty.ReadLine()) != null)
                {
                    MatrixBoyut++;
                }
            }
            String line;
            double[][] Rmatris = new double[MatrixBoyut][];
            int i = 0,j=0;
            for (i = 0; i < MatrixBoyut; i++)
            {
                Rmatris[i] = new double[MatrixBoyut];
                for (j = 0; j < MatrixBoyut; j++)
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
                            Rmatris[i][i] = 100;
                        }
                        if (Rmatris[i][Convert.ToInt32(item)] != 100)
                        {
                            Rmatris[i][Convert.ToInt32(item)] = 0;
                        }
                        
                    }
                    i++;
                }
            }
            using (StreamWriter wr = new StreamWriter("C:\\Users\\Taha\\Desktop\\outR.txt",false))
            {
                for (i = 0; i < MatrixBoyut; i++)
                {
                    for (j = 0; j < MatrixBoyut; j++)
                    {
                        wr.Write(Rmatris[i][j] +  " ");
                    }
                    wr.WriteLine();
                }
            }
            int hedef = Convert.ToInt32(EndDot.Text);
            var Qmatris = Qmatris_Olustur(MatrixBoyut);

            for (int z = 0; z < Convert.ToInt32(iterasyon .Text); z++)
            {
                int baslangicDot = Convert.ToInt32(beginDot.Text);

                string yol = "";

                for (i = 0; i < MatrixBoyut; i++)
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
                bool controlX = false;
                yol = "";
                int natay = 0;
                while (true)
                {
                    var sonuc = zincir(Rmatris, baslangicDot, qbaslangic, Qmatris,MatrixBoyut);
                    
                    
                    Qmatris[baslangicDot][qbaslangic] = sonuc[2];

                    baslangicDot  =  Convert.ToInt32(sonuc[0]);
                    qbaslangic    =  Convert.ToInt32(sonuc[1]);
                    if (controlX == true)
                    {
                        break;
                    }
                    if (control == true)
                    {
                        controlX = true;
                    }
                    if (hedef == qbaslangic)
                    {
                        if (natay == 0)
                        {
                            break;
                        }
                        control = true;
                    }
                    natay++;
                }

            }

            

            ////Yol buldurulucak kısım
            /// 
            string SonucYol = beginDot.Text;
            int sonrakiAdim = Convert.ToInt32(beginDot.Text);
            while (true)
            {
                string MaxYol = "";
                double maxYol = Qmatris[sonrakiAdim].Max();
                for (i = 0; i < MatrixBoyut; i++)
                {
                    if (Qmatris[Convert.ToInt32(sonrakiAdim)][i] == maxYol)
                    {
                        MaxYol = MaxYol + i + "*";
                    }
                }
                var kundi = MaxYol.Split('*');

                Random randomYol = new Random();
                int randomMaxIndex = randomYol.Next(0, kundi.Length-1);
                sonrakiAdim = Convert.ToInt32(kundi[randomMaxIndex]);
                SonucYol = SonucYol + "-" + sonrakiAdim;
                if (sonrakiAdim == Convert.ToInt32(EndDot.Text))
                {
                    break;
                }
            }

            using (StreamWriter wr = new StreamWriter("C:\\Users\\Taha\\Desktop\\outQ.txt", false))
            {
                for (i = 0; i < MatrixBoyut; i++)
                {
                    for (j = 0; j < MatrixBoyut; j++)
                    {
                        wr.Write(Qmatris[i][j] + " ");
                    }
                    wr.WriteLine();
                }
            }

            using (StreamWriter wr = new StreamWriter("C:\\Users\\Taha\\Desktop\\outPath.txt", false))
            {
                wr.WriteLine("\n\n" + SonucYol);
            }

            Q_Maze_Info.dosyaYol = path;
            Q_Maze_Info.begin = Convert.ToInt32(beginDot.Text);
            Q_Maze_Info.end = Convert.ToInt32(EndDot.Text);
            Q_Maze_Info.matrixBoyutu = Convert.ToInt32(MatrixBoyut);
            Q_Maze_Info.yol = SonucYol;

            MessageBox.Show("Tamamlandı ve Bitti");
        }

        protected double[] zincir(double[][] Rmatris, int indexQbegin,int indexQend,double[][] Qmatris,int boyut)
        {
            double[] ReturnSonuc = new double[3];

            ReturnSonuc[0] = indexQend; //-> bir sonrakinin başlangici

            int i;
            string yol = "";
            for (i = 0; i < boyut; i++)
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
        protected double[][] Qmatris_Olustur(int boyut)
        {
            double[][] Qmatris = new double[boyut][];
            int i, j;
            for (i = 0; i < boyut; i++)
            {
                Qmatris[i] = new double[boyut];
                for (j = 0; j < boyut; j++)
                {
                    Qmatris[i][j] = 0;
                }
            }
            return Qmatris;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var myForm = new Form2();
            myForm.Show();
        }
    }
}
