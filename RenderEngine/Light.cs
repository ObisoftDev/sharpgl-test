using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixeliumTest
{
    public class Light
    {
        public Vector3 Position;
        public Vector3 Color;

        public Light(Vector3 position, Vector3 color)
        {
            Position = position;
            Color = color;
        }
    }
}
