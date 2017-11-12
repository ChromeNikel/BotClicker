using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ScreenMaker
{
    class WorkerWithPicture
    {

        public Bitmap getBlackWhite(Bitmap Bmp)
        {
            int rgb;
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return Bmp;
        }
        public double[,] getBright(Bitmap Bmp)
        {
            double[,] mbright = new double[Bmp.Width, Bmp.Height];
            for (int i = 0; i < Bmp.Width; i++)
            {
                for (int j = 0; j < Bmp.Height; j++)
                {
                    mbright[i, j] = 0.229 * Bmp.GetPixel(i, j).R + 0.587 * Bmp.GetPixel(i, j).G
                        + 0.114 * Bmp.GetPixel(i, j).B;
                }
            }
            return mbright;
        }
        public Bitmap getRoberts(double[,] bri, Bitmap Bmp, int threshold = 0)
        {
            Bitmap rob = new Bitmap(Bmp.Width, Bmp.Height);
            double Gx = 0;
            double Gy = 0;
            byte[,] grad = new byte[Bmp.Width, Bmp.Height];
            for (int i = 0; i < Bmp.Width - 1; i++)
            {
                for (int j = 0; j < Bmp.Height - 1; j++)
                {
                    Gx = bri[i + 1, j + 1] - bri[i, j];
                    Gy = bri[i + 1, j] - bri[i, j + 1];
                    grad[i, j] = (byte)Math.Sqrt(Gx * Gx + Gy * Gy);
                    if (grad[i, j] <= threshold)
                    {
                        rob.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        rob.SetPixel(i, j, Color.White);
                    }
                }
            }
            return rob;
        }
        public Bitmap getSobel(double[,] bri, Bitmap Bmp, int threshold = 0)
        {
            Bitmap sob = new Bitmap(Bmp.Width, Bmp.Height);
            double Gx = 0;
            double Gy = 0;          
            byte[,] grad = new byte[Bmp.Width, Bmp.Height];
            for (int i = 1; i < Bmp.Width - 1; i++)
            {
                for (int j = 1; j < Bmp.Height - 1; j++)
                {
                    Gx = (bri[i - 1, j + 1] + 2 * bri[i, j + 1] + bri[i + 1, j + 1]) - (bri[i - 1, j - 1] + 2 * bri[i, j - 1] + bri[i + 1, j - 1]);
                    Gy = (bri[i + 1, j - 1] + 2 * bri[i, j] + bri[i + 1, j + 1]) - (bri[i - 1, j - 1] + 2 * bri[i - 1, j] + bri[i - 1, j + 1]);
                    grad[i, j] = (byte)Math.Sqrt(Gx * Gx + Gy * Gy);
                   
                    if (grad[i, j] <= threshold)
                    {
                        sob.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        sob.SetPixel(i, j, Color.White);
                    }
                }
            }
            return sob;
        }
        public Bitmap getPrevitt(double[,] bri, Bitmap Bmp, int threshold = 0)
        {
            Bitmap pre = new Bitmap(Bmp.Width, Bmp.Height);
            double Gx = 0;
            double Gy = 0;
            byte[,] grad = new byte[Bmp.Width, Bmp.Height];
            for (int i = 1; i < Bmp.Width - 1; i++)
            {
                for (int j = 1; j < Bmp.Height - 1; j++)
                {
                    Gx = (bri[i - 1, j + 1] + bri[i, j + 1] + bri[i + 1, j + 1]) - (bri[i - 1, j - 1] + bri[i, j - 1] + bri[i + 1, j - 1]);
                    Gy = (bri[i + 1, j - 1] + bri[i + 1, j] + bri[i + 1, j + 1]) - (bri[i - 1, j - 1] + bri[i - 1, j] + bri[i - 1, j + 1]);
                    grad[i, j] = (byte)Math.Sqrt(Gx * Gx + Gy * Gy);

                    if (grad[i, j] <= threshold)
                    {
                        pre.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        pre.SetPixel(i, j, Color.White);
                    }
                }
            }
            return pre;
        }
        public Bitmap getLoG(double[,] bri, Bitmap Bmp, int threshold = 0)
        {
            Bitmap log = new Bitmap(Bmp.Width, Bmp.Height);
            double Gx = 0;
            double Gy = 0;
            byte[,] grad = new byte[Bmp.Width, Bmp.Height];
            for (int i = 1; i < Bmp.Width - 1; i++)
            {
                for (int j = 1; j < Bmp.Height - 1; j++)
                {
                    Gx = 4 * bri[i, j]  - (bri[i, j - 1] + bri[i, j + 1] + bri[i + 1, j] + bri[i - 1, j]);
                    Gy = 8 * bri[i, j] - (bri[i, j - 1] + bri[i, j + 1] + bri[i + 1, j] + bri[i - 1, j] + bri[i - 1, j - 1] + bri[i + 1, j + 1] + bri[i + 1, j - 1] + bri[i - 1, j + 1]);
                    grad[i, j] = (byte)Math.Sqrt(Gx * Gx + Gy * Gy);

                    if (grad[i, j] <= threshold)
                    {
                        log.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        log.SetPixel(i, j, Color.White);
                    }
                }
            }
            return log;
        }
        public Dictionary<double[,], Bitmap> getRazm(double[,] bri, Bitmap Bmp)
        {
            Bitmap razm = new Bitmap(Bmp.Width, Bmp.Height);
            double Gx = 0;
            double Gy = 0;            
            for (int i = 2; i < Bmp.Width - 2; i++)
            {
                for (int j = 2; j < Bmp.Height - 2; j++)
                {
                    bri[i, j] = bri[i - 2, j - 2] * 0.000789 + bri[i - 1, j - 2] * 0.006581 + bri[i, j - 2] * 0.013347 + bri[i + 1, j - 2] * 0.006581 + bri[i + 2, j - 2] * 0.000789 +
                        bri[i - 2, j - 1] * 0.006581 + bri[i - 1, j - 1] * 0.054901 + bri[i, j - 1] * 0.111345 + bri[i + 1, j - 1] * 0.054901 + bri[i + 2, j - 1] * 0.006581 +
                        bri[i - 2, j] * 0.013347 + bri[i - 1, j] * 0.111345 + bri[i, j] * 0.225821 + bri[i + 1, j] * 0.111345 + bri[i + 2, j] * 0.013347 +
                         bri[i - 2, j + 1] * 0.006581 + bri[i - 1, j + 1] * 0.054901 + bri[i, j + 1] * 0.111345 + bri[i + 1, j + 1] * 0.054901 + bri[i + 2, j + 1] * 0.006581 +
                         bri[i - 2, j + 2] * 0.000789 + bri[i - 1, j + 2] * 0.006581 + bri[i, j + 2] * 0.013347 + bri[i + 1, j + 2] * 0.006581 + bri[i + 2, j + 2] * 0.000789;
                    razm.SetPixel(i, j, Color.FromArgb((int)bri[i, j], (int)bri[i, j], (int)bri[i, j]));
                }
            }
            Dictionary<double[,], Bitmap> d = new Dictionary<double[,], Bitmap>();
            d[bri] = razm;
            return d;
        }
        private List<double [,]> getSobelD(double[,] bri)
        {
            List < double[,] > l = new List<double[,]>();
            double Gx = 0;
            double Gy = 0;
            double[,] grad = new double[bri.GetLength(0), bri.GetLength(1)];
            double[,] angle = new double[bri.GetLength(0), bri.GetLength(1)];
            for (int i = 1; i < bri.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < bri.GetLength(1) - 1; j++)
                {
                    Gx = (bri[i - 1, j + 1] + 2 * bri[i, j + 1] + bri[i + 1, j + 1]) - (bri[i - 1, j - 1] + 2 * bri[i, j - 1] + bri[i + 1, j - 1]);
                    Gy = (bri[i + 1, j - 1] + 2 * bri[i, j] + bri[i + 1, j + 1]) - (bri[i - 1, j - 1] + 2 * bri[i - 1, j] + bri[i - 1, j + 1]);
                    grad[i, j] = Math.Sqrt(Gx * Gx + Gy * Gy);
                    //if (grad[i, j] > 255)
                    //{
                    //    grad[i, j] = 255;
                    //}
                    if ((byte)Gx == 0 && (byte)Gy == 0)
                    {
                        angle[i, j] = 0;
                    }
                    else if ((byte)Gx == 0)
                    {
                        angle[i, j] = 90;
                    }
                    else
                    {
                        angle[i, j] = Math.Atan2(Gy, Gx) * 57;
                    }
                }
            }
            l.Add(grad);
            l.Add(angle);
            return l;
        }
        private List<double[,]> getNormAngle(List<double[,]> l0)
        {
            List<double[,]> l1 = new List<double[,]>();
            double[,] angle = l0.Last();
            for (int i = 0; i < angle.GetLength(0); i++)
            {
                for (int j = 0; j < angle.GetLength(1); j++)
                {
                    if (angle[i, j] <= 22.5 || (angle[i, j] <= 180 && angle[i, j] >= 157.5))
                    {
                        angle[i, j] = 0;
                    }
                    else if (angle[i, j] > 22.5 && angle[i, j] <=67.5)
                    {
                        angle[i, j] = 45;
                    }
                    else if (angle[i, j] > 67.5 && angle[i, j] <= 112.5)
                    {
                        angle[i, j] = 90;
                    }
                    else if (angle[i, j] > 112.5 && angle[i, j] < 157.5)
                    {
                        angle[i, j] = 135;
                    }
                }
            }
            l1.Add(l0.First());
            l1.Add(angle);
            return l1;
        }
        private double[,] getLocalMax(List<double[,]> l)
        {
            double[,] grad0 = l.First();
            double[,] grad1 = new double[grad0.GetLength(0), grad0.GetLength(1)];
            double[,] angle = l.Last();
            for (int i = 1; i < grad0.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < grad0.GetLength(1) - 1; j++)
                {
                    switch(angle[i, j])
                    {
                        case 0:
                            {
                                List<double> arr = new List<double>();
                                arr.Add(grad0[i, j]);
                                for (int k = - 1; k < 2; k++)
                                {
                                    for (int n = -1; n < 2; n++)
                                    {
                                        if (angle[i - k, j - n] == 0 &&  k != 0 && n != 0)
                                        {
                                            arr.Add(grad0[i - k, j - n]);
                                        }
                                    }
                                }
                                grad1[i, j] = arr.IndexOf(arr.Max()) != 0 ? 0 : grad0[i, j];
                                break;
                            }
                        case 45:
                            {
                                List<double> arr = new List<double>();
                                arr.Add(grad0[i, j]);
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int n = - 1; n < 2; n++)
                                    {
                                        if (angle[i - k, j - n] == 45 && k != 0 && n != 0)
                                        {
                                            arr.Add(grad0[i - k, j - n]);
                                        }
                                    }
                                }
                                grad1[i, j] = arr.IndexOf(arr.Max()) != 0 ? 0 : grad0[i, j];
                                break;
                            }
                        case 90:
                            {
                                List<double> arr = new List<double>();
                                arr.Add(grad0[i, j]);
                                for (int k = - 1; k < 2; k++)
                                {
                                    for (int n = -1; n < 2; n++)
                                    {
                                        if (angle[i - k, j - n] == 90 &&  k != 0 && n != 0)
                                        {
                                            arr.Add(grad0[i - k, j - n]);
                                        }
                                    }
                                }
                                grad1[i, j] = arr.IndexOf(arr.Max()) != 0 ? 0 : grad0[i, j];
                                break;
                            }
                        case 135:
                            {
                                List<double> arr = new List<double>();
                                arr.Add(grad0[i, j]);
                                for (int k = - 1; k < 2; k++)
                                {
                                    for (int n = -1; n < 2; n++)
                                    {
                                        if (angle[i - k, j - n] == 135 && k != 0 && n != 0)
                                        {
                                            arr.Add(grad0[i - k, j - n]);
                                        }
                                    }
                                }
                                grad1[i, j] = arr.IndexOf(arr.Max()) != 0 ? 0 : grad0[i, j];
                                break;
                            }
                        default :
                            {
                                grad1[i, j] = grad0[i, j];
                                break;
                            }
                    }
                }
            }
            return grad1;
        }
        private double[,] getDoubleThreshold(double[,] bri, int bottThreshold = 0, int topThreshold = 255)
        {
            double [,]bri0 = new double[bri.GetLength(0), bri.GetLength(1)];
            for(int i = 0; i < bri.GetLength(0); i++)
            {
                for (int j = 0; j < bri.GetLength(1); j++)
                {
                    if (bri[i, j] < bottThreshold)
                    {
                        bri0[i, j] = 0;
                    }else if (bri[i, j] > topThreshold)
                    {
                        bri0[i, j] = 255;
                    }
                    else
                    {
                        bri0[i, j] = bri[i, j];
                    }
                }
               
            }
            return bri0;
        }
        private double[,] getTrackingByHysteresys(double [,] bri)
        {
            double [,]bri0 = new double[bri.GetLength(0), bri.GetLength(1)];
            for (int i = 1; i < bri.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < bri.GetLength(1) - 1; j++)
                {
                    if (bri[i, j] == 0)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            for (int l = -1; l < 2; l++)
                            {
                               bri[i - k, j - l] = (bri[i - k, j - l] != 255) ? 0 : 255;

                            }
                        }
                    }
                    
                }
            }
            return bri;
        }      
        public int getSummaOfContur(Bitmap Bmp)
        {
            int summa = 0;

            for (int i = 0; i < Bmp.Width; i++)
            {
                for (int j = 0; j < Bmp.Height; j++)
                {
                    if (Bmp.GetPixel(i, j) == Color.FromArgb(255, 255, 255))
                    {
                        summa++;
                    }
                }
            }
            return summa;
        }
        public Bitmap Canny(Bitmap Bmp, int bottThreshold = 0, int topThreshold = 255)
        {
            var bri = getBright(Bmp);
            var d = getRazm(bri, Bmp);
            var a = getSobelD(d.Keys.First());
            var s = getNormAngle(a);
            var ss = getLocalMax(s);
            var sss = getDoubleThreshold(ss, bottThreshold, topThreshold);
            ///var ssss = getTrackingByHysteresys(sss);


            Bitmap b = new Bitmap(sss.GetLength(0), sss.GetLength(1));
            for (int i = 0; i < sss.GetLength(0); i++)
            {
                for (int j = 0; j < sss.GetLength(1); j++)
                {
                    b.SetPixel(i, j, Color.FromArgb((int)sss[i, j], (int)sss[i, j], (int)sss[i, j]));
                }
            }
            int aa = 4 + 3;
            return b;
        }
        /// <summary>
        /// не дописан, не используется
        /// </summary>
        /// <param name="Bmp"></param>
        /// <returns></returns>
        public List<Dictionary<int, int>> getConturas(double[,] Bmp)
        {
            double[,] pi = new double[Bmp.GetLength(0), Bmp.GetLength(1)];

            List<Dictionary<int, int>> contur = new List<Dictionary<int, int>>();

            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 1; i < Bmp.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < Bmp.GetLength(1) - 1; j++)
                {
                    if (Bmp[i, j] == 255)
                    {
                        Bmp[i, j] = 1;
                        dic[i] = j;
                        for (int k = i; k < i + 1; i++)
                        {
                            for (int l = j; l < j + 1; l++)
                            {
                                if (k != i && l != j)
                                {
                                    
                                }
                            }
                        }

                    }
                }
            }

            return null;
        }
        /// <summary>
        /// не дописан, не используется
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        static bool findBorder(int i, int j)
        {
            for (int k = i; k < i + 1; i++)
            {
                for (int l = j; l < j + 1; l++)
                {
                    if (k != i && l != j)
                    {
                        if (true)
                        {

                        }
                    }
                }
            }
            return true;
        }

    }
}
