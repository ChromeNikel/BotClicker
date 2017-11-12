using OpenCvSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ScreenMaker
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);       
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
               
        WorkerWithPicture wp;
        Clicker cl;
        int left;
        int top;
        public Form1()
        {
            wp = new WorkerWithPicture();           
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            Bitmap pic = Catch();
            Bitmap pic0 = new Bitmap(pic.Width, pic.Height);
            var inimage = OpenCvSharp.Extensions.BitmapConverter.ToMat(pic);
            var outimage = OpenCvSharp.Extensions.BitmapConverter.ToMat(pic0);
            OpenCvSharp.Cv2.Canny(inimage, outimage, Convert.ToInt32(0), Convert.ToInt32(255), 3);
            var cannyImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outimage);
           
            Application.DoEvents();
            inimage.Dispose();
            outimage.Dispose();
            pic.Dispose();
            pic0.Dispose();
           
            Bitmap Bmp = (Bitmap)cannyImage;
            cl = new Clicker(Bmp);
            cl.ClickUlti(left, top);
            
        }

        public Bitmap Catch()
        {
            this.SendToBack();           
            Process[] prArray = Process.GetProcesses();
            int numberOfDragons = 0;
            for (int i = 0; i < prArray.Length; i++)
            {
                if (prArray[i].ProcessName == "IdleDragons")
                {
                    numberOfDragons = i;
                }
            }
            IntPtr hWnd = prArray[numberOfDragons].MainWindowHandle;
            SetForegroundWindow(hWnd);
            ShowWindow(hWnd, numberOfDragons);
            Thread.Sleep(200);
            RECT rct = new RECT();
            GetWindowRect(hWnd, ref rct);
            int width = rct.Right - rct.Left;
            int height = rct.Bottom - rct.Top;
            left = rct.Left;
            top = rct.Top;
            Rectangle rectangle = new Rectangle(rct.Left, rct.Top, width, height);
            Bitmap screen = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics.FromImage(screen).CopyFromScreen(rct.Left, rct.Top, 0, 0, new System.Drawing.Size(width, height), CopyPixelOperation.SourceCopy);
            
            return screen;
        }  

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap pic = Catch();
            Bitmap pic0 = new Bitmap(pic.Width, pic.Height);
            var inimage = OpenCvSharp.Extensions.BitmapConverter.ToMat(pic);
            var outimage = OpenCvSharp.Extensions.BitmapConverter.ToMat(pic0);
            OpenCvSharp.Cv2.Canny(inimage, outimage, Convert.ToInt32(0), Convert.ToInt32(255), 3);
            var cannyImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outimage);
           
            Application.DoEvents();
            inimage.Dispose();
            outimage.Dispose();
            pic.Dispose();
            pic0.Dispose();
           
            Bitmap Bmp = (Bitmap)cannyImage;
            cl = new Clicker(Bmp);
            cl.ClickUlti(left, top);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            Bitmap pic = Catch();
            Bitmap pic0 = new Bitmap(pic.Width, pic.Height);
            var inimage = OpenCvSharp.Extensions.BitmapConverter.ToMat(pic);
            var outimage = OpenCvSharp.Extensions.BitmapConverter.ToMat(pic0);
            OpenCvSharp.Cv2.Canny(inimage, outimage, Convert.ToInt32(0), Convert.ToInt32(255), 3);
            var cannyImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outimage);

            Application.DoEvents();
            inimage.Dispose();
            outimage.Dispose();
            pic.Dispose();
            pic0.Dispose();

            Bitmap Bmp = (Bitmap)cannyImage;
            cl = new Clicker(Bmp);
            cl.ClickUlti(left, top);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Visible = false;
            }
        }
    }
}
