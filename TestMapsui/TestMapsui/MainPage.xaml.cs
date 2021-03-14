using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Threading;
using Xamarin.Forms;

namespace TestMapsui
{
    public partial class MainPage : ContentPage
    {
        private int numDraws = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        private void SKGLView_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            Console.WriteLine("[DBG]   Begin Paint Surface");
            Console.WriteLine($"[DBG]   PaintSurface currentThread={Thread.CurrentThread.ManagedThreadId}");

            var canvas = e.Surface.Canvas;
            canvas.Clear();
            var width = MyGLView.CanvasSize.Width;
            var height = MyGLView.CanvasSize.Height;

            var center = new SKPoint(width / 2, height / 2);
            using var paint = new SKPaint{
                IsAntialias=true,
                Style=SKPaintStyle.Fill,
                Color=new SKColor(0xFFFF0000)
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
            Console.WriteLine("[DBG]   button clicked (before invalidate())");
            MyGLView.InvalidateSurface();
            Console.WriteLine("[DBG]   button clicked (after invalidate())");
        }


        private void Button_Clicked2(object sender, EventArgs e)
        {
            Console.WriteLine($"[DBG]   Button_Clicked2 currentThread={Thread.CurrentThread.ManagedThreadId}");

            numDraws = 0;
            Console.WriteLine("[DBG]   button clicked (before invalidate() x2)");
            MyGLView.InvalidateSurface();
            MyGLView.InvalidateSurface();
            Console.WriteLine("[DBG]   button clicked (after invalidate() x2)");
        }

        private void Button_Clicked3(object sender, EventArgs e)
        {
            Console.WriteLine($"[DBG]   Button_Clicked3 currentThread={Thread.CurrentThread.ManagedThreadId}");

            numDraws = 0;
            Console.WriteLine("[DBG]   button clicked (before invalidate() x300)");
            for (int i = 0; i < 300; ++i)
            {
                MyGLView.InvalidateSurface();
            }
            Console.WriteLine("[DBG]   button clicked (after invalidate() x300)");
        }
    }
}
