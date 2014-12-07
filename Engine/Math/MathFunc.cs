using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Engine
{
    public static class MathFunc
    {
        public static int RoundOff(this int i)
        {
            int value = ((int)Math.Round(i / 10.0)) * 10;
            return value;
        }
    }
}
