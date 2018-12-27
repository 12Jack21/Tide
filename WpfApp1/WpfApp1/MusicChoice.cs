using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class MusicChoice
    {
        public String Forest()
        {
            Random rnd = new Random();
            int n = rnd.Next(2)+1;
            return System.IO.Directory.GetCurrentDirectory() + @"\Music\Forest" + n + ".mp3";
        }
        public String ForestPicture()
        {
            Random rnd = new Random();
            return System.IO.Directory.GetCurrentDirectory() + @"\Picture\forest.jpg";
        }
        public String Rain()
        {
            Random rnd = new Random();
            int n = rnd.Next(2) +1;
            return System.IO.Directory.GetCurrentDirectory() + @"\Music\Rain" + n + ".mp3";
        }
        public String RainPicture()
        {
            return System.IO.Directory.GetCurrentDirectory() + @"\Picture\Rain.jpg";
        }
        public String Waves()
        {
            Random rnd = new Random();
            int n = rnd.Next(2)+1;
            return System.IO.Directory.GetCurrentDirectory() + @"\Music\Waves" + n + ".mp3";
        }
        public String WavesPicture()
        {
            return System.IO.Directory.GetCurrentDirectory() + @"\Picture\Wave.jpg";
        }
    }
}
