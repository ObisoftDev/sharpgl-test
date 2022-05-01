using SharpGL.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixeliumTest
{
    public class TexturedModel
    {
        public RawModel rawModel;
        public Texture texture;


        public TexturedModel(RawModel rawModel, Texture texture)
        {
            this.rawModel = rawModel;
            this.texture = texture;
        }
    }
}
