using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace ScreenMaker
{
    class Clicker
    {
        private int[] summaDefoult0;
        private int[] summaDefoult1;
        private List<Bitmap> ultes = new List<Bitmap>();
        private Stack<Rectangle> rectsUltes = new Stack<Rectangle>();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public Clicker(Bitmap image)
        {   ///279, 246, 285, 328, 258
            summaDefoult0 = new int[] {258, 328, 285, 246, 279};
            ///284, 253, 312, 327, 271
            summaDefoult1 = new int[] { 271, 327, 312, 253, 284 };
            rectsUltes.Push(new Rectangle(430, 555, 32, 32));
            rectsUltes.Push(new Rectangle(478, 555, 32, 32));
            rectsUltes.Push(new Rectangle(524, 555, 32, 32));
            rectsUltes.Push(new Rectangle(570, 555, 32, 32));
            rectsUltes.Push(new Rectangle(618, 555, 32, 32));
            ultes.Add(image.Clone(rectsUltes.Pop(), image.PixelFormat));
            ultes.Add(image.Clone(rectsUltes.Pop(), image.PixelFormat));
            ultes.Add(image.Clone(rectsUltes.Pop(), image.PixelFormat));
            ultes.Add(image.Clone(rectsUltes.Pop(), image.PixelFormat));
            ultes.Add(image.Clone(rectsUltes.Pop(), image.PixelFormat));
            ultes.Reverse();
        }
        public void ClickUlti(int leftWindow, int topWindow)
        {
            int xpos = 0;
            int ypos = 0;
            for (int i = 0; i < ultes.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            var s = getSummaOfContur(ultes[i]);
                            if (s == summaDefoult0[i] || s == summaDefoult1[i])
                            {
                                xpos = leftWindow + 446;
                                ypos = topWindow + 571;
                                SetCursorPos(xpos, ypos);
                                mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
                                Thread.Sleep(10);
                                mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
                                Thread.Sleep(1000);
                            }
                            break;
                        }
                    case 1:
                        {
                            var s = getSummaOfContur(ultes[i]);
                            if (s == summaDefoult0[i] || s == summaDefoult1[i])
                            {
                                xpos = leftWindow + 494;
                                ypos = topWindow + 571;
                                SetCursorPos(xpos, ypos);
                                mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
                                Thread.Sleep(10);
                                mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
                                Thread.Sleep(1000);

                            }
                            break;
                        }
                    case 2:
                        {
                            var s = getSummaOfContur(ultes[i]);

                            if (s == summaDefoult0[i] || s == summaDefoult1[i])
                            {
                                xpos = leftWindow + 540;
                                ypos = topWindow + 571;
                                SetCursorPos(xpos, ypos);
                                mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
                                Thread.Sleep(10);
                                mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
                                Thread.Sleep(1000);

                            }
                            break;
                        }
                    case 3:
                        {
                            var s = getSummaOfContur(ultes[i]);
                            if (s == summaDefoult0[i] || s == summaDefoult1[i])
                            {
                                xpos = leftWindow + 587;
                                ypos = topWindow + 571;
                                SetCursorPos(xpos, ypos);
                                mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
                                Thread.Sleep(10);
                                mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
                                Thread.Sleep(1000);
                            }
                            break;
                        }
                    case 4:
                        {
                            var s = getSummaOfContur(ultes[i]);
                            if (s == summaDefoult0[i] || s == summaDefoult1[i])
                            {
                                xpos = leftWindow + 635;
                                ypos = topWindow + 571;
                                SetCursorPos(xpos, ypos);
                                mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
                                Thread.Sleep(10);
                                mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
                                Thread.Sleep(1000);

                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                       
                }
            }           
            SetCursorPos(leftWindow + 1, topWindow + 1);
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

        public Bitmap getBitmap(int i)
        {
            return ultes[i];
        }
    }
}
