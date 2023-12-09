using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowsOfInfinity
{
    public abstract class BaseRenderer
    {
        protected ImageFormat _imageFormat = ImageFormat.Png;

        public BaseRenderer()
        {
            Console.WriteLine("  _                                            ");
            Console.WriteLine(" /_`/_ _   _/_       _  _ _/|  /_ _/|._  ._/_  ");
            Console.WriteLine("._// //_|/_//_/|/|/_\\  /_//   // // // // / /_/");
            Console.WriteLine("                                            _/ ");
        }

        public string GetTimestamp()
        {
            return ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        public abstract string RenderFileName();
    }
}
