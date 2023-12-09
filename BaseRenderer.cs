using System.Drawing.Imaging;

namespace ShadowsOfInfinity
{
    public abstract class BaseRenderer
    {
        protected ImageFormat _imageFormat = ImageFormat.Png;

        public BaseRenderer()
        {
            Console.WriteLine("╔═╗┬ ┬┌─┐┌┬┐┌─┐┬ ┬┌─┐  ┌─┐┌─┐  ╦┌┐┌┌─┐┬┌┐┌┬┌┬┐┬ ┬");
            Console.WriteLine("╚═╗├─┤├─┤ │││ ││││└─┐  │ │├┤   ║│││├┤ │││││ │ └┬┘");
            Console.WriteLine("╚═╝┴ ┴┴ ┴─┴┘└─┘└┴┘└─┘  └─┘└    ╩┘└┘└  ┴┘└┘┴ ┴  ┴ ");
        }

        public string GetTimestamp()
        {
            return ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        public abstract string RenderFileName();
    }
}
