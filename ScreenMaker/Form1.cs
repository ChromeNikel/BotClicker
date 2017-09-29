using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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

        Collection<Bitmap> screens;

        public Form1()
        {
            screens = new Collection<Bitmap>();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

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
            RECT rct = new RECT();
            GetWindowRect(hWnd, ref rct);
            int width = rct.Right - rct.Left;
            int height = rct.Bottom - rct.Top;
            Rectangle rectangle = new Rectangle(rct.Left, rct.Top, width, height);
            Bitmap screen = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics.FromImage(screen).CopyFromScreen(rct.Left, rct.Top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            pictureBox1.Image = screen;
            return screen;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            screens.Add(Catch());
        }
    }
}
