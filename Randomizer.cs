using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrigorievC_tema04
{
    internal class Randomizer
    {
        private Random rand;

        public Randomizer()
        {
            rand = new Random();
        }

        public int GetRandomOffsetPositive(int maxval)
        {
            int getInteger = rand.Next(0, maxval);
            return getInteger;
        }

        public int GetRandomOffset(int minval, int maxval)
        {
            int getInteger = rand.Next(minval, maxval);
            return getInteger;
        }

        public Color getRandomColor()
        {
            int genR = rand.Next(0, 255);
            int genG = rand.Next(0, 255);
            int genB = rand.Next(0, 255);
            return Color.FromArgb(genR, genG, genB);
        }
    }
}
