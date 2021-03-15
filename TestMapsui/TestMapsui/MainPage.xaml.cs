using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestMapsui
{
    public partial class MainPage : ContentPage
    {
        private int numDraws = 0;
        private uint[] myColorArray = new uint[1]{0xFFFF0000 };


        public MainPage()
        {
            InitializeComponent();
        }

        //private void SKGLView_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        private void SKGLView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)

        {
            Console.WriteLine("[DBG]   Begin Paint Surface");
            Console.WriteLine($"[DBG]   PaintSurface currentThread={Thread.CurrentThread.ManagedThreadId}");
            if (myColorArray == null)
            {
                Console.WriteLine("array is null, returning gracefully");
                return;
            }

            var canvas = e.Surface.Canvas;
            canvas.Clear();
            var width = MySKView.CanvasSize.Width;
            var height = MySKView.CanvasSize.Height;

            var center = new SKPoint(width / 2, height / 2);
 

            Thread.Sleep(5000); // simulate (very long) rendering

            using var paint = new SKPaint{
                IsAntialias=true,
                Style=SKPaintStyle.Fill,
                Color=new SKColor(myColorArray[0])
            };

            canvas.DrawCircle(center, 40, paint);
            Device.BeginInvokeOnMainThread(
                () => {
                    numDraws += 1;
                    DrawCountLabel.Text = $"draw count:{numDraws}";
                }
            );
            Console.WriteLine("[DBG]   End Paint Surface");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine($"[DBG]   Button_Clicked currentThread={Thread.CurrentThread.ManagedThreadId}");
            numDraws = 0;
            DrawCountLabel.Text = $"draw count:{numDraws}";
            myColorArray = new uint[1];
            myColorArray[0] = 0xFFFF0000; // red

            Console.WriteLine("[DBG]   button clicked (before invalidate())");
            MySKView.InvalidateSurface();
            Console.WriteLine("[DBG]   button clicked (after invalidate())");

            UpdateMyUI();
        }


        private void Button_Clicked2(object sender, EventArgs e)
        {
            Console.WriteLine($"[DBG]   Button_Clicked2 currentThread={Thread.CurrentThread.ManagedThreadId}");

            numDraws = 0;
            DrawCountLabel.Text = $"draw count:{numDraws}";
            myColorArray = new uint[1];
            myColorArray[0] = 0xFFFF0000; // red

            Console.WriteLine("[DBG]   button clicked (before invalidate() x2)");
            MySKView.InvalidateSurface();
            MySKView.InvalidateSurface();
            Console.WriteLine("[DBG]   button clicked (after invalidate() x2)");

            UpdateMyUI();
        }


        private void Button_Clicked3(object sender, EventArgs e)
        {
            Console.WriteLine($"[DBG]   Button_Clicked3 currentThread={Thread.CurrentThread.ManagedThreadId}");

            numDraws = 0;
            DrawCountLabel.Text = $"draw count:{numDraws}";
            myColorArray = new uint[1];
            myColorArray[0] = 0xFFFF0000; // red

            Console.WriteLine("[DBG]   button clicked (before invalidate() x300)");
            for (int i = 0; i < 300; ++i)
            {
                MySKView.InvalidateSurface();
            }
            Console.WriteLine("[DBG]   button clicked (after invalidate() x300)");

            UpdateMyUI();
        }


        private async void UpdateMyUI()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            myColorArray = null;
        }
    }
}
