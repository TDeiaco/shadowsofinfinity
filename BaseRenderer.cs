using System.Drawing;
using System.Drawing.Imaging;

namespace ShadowsOfInfinity
{
    public abstract class BaseRenderer
    {
        protected ImageFormat _imageFormat = ImageFormat.Png;
        protected Bitmap? _bitmap;

        public BaseRenderer()
        {
            Console.WriteLine("╔═╗┬ ┬┌─┐┌┬┐┌─┐┬ ┬┌─┐  ┌─┐┌─┐  ╦┌┐┌┌─┐┬┌┐┌┬┌┬┐┬ ┬");
            Console.WriteLine("╚═╗├─┤├─┤ │││ ││││└─┐  │ │├┤   ║│││├┤ │││││ │ └┬┘");
            Console.WriteLine("╚═╝┴ ┴┴ ┴─┴┘└─┘└┴┘└─┘  └─┘└    ╩┘└┘└  ┴┘└┘┴ ┴  ┴ ");
        }

        public void InitBitmap(int w, int h, int r, int g, int b)
        {
            _bitmap = new Bitmap(w, h);
            for (int i = 0; i < _bitmap.Width; i++)
            {
                for (int j = 0; j < _bitmap.Height; j++)
                {
                    var color = Color.FromArgb(r, g, b);
                    _bitmap.SetPixel(i, j, color);
                }
            }
        }

        public void BlitPixel(int x, int y, int r, int g, int b)
        {
            Color color = Color.FromArgb(r, g, b);
            _bitmap?.SetPixel(x, y, color);
        }

        public void SaveBmp()
        {
            _bitmap?.Save(RenderFileName(), _imageFormat);
        }

        public string GetTimestamp()
        {
            return ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        public abstract string RenderFileName();

    }
}
