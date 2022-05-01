using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixeliumTest
{
    public class Entity
    {

        public TexturedModel model;
        public Vector3 position;
        public float rotX, rotY, rotZ;
        public Vector3 scale = new Vector3(1,1,1);

        public Entity(TexturedModel model, Vector3 position, float rotX, float rotY, float rotZ, float scale)
        {
            this.model = model;
            this.position = position;
            this.rotX = rotX;
            this.rotY = rotY;
            this.rotZ = rotZ;
            this.scale = new Vector3(scale, scale, scale);
        }
        public void increasePosition(float dx,float dy,float dz)
        {
            position += new Vector3(dx,dy,dz);
        }
        public void increaseRotation(float rx, float ry, float rz)
        {
            rotX += rx;
            rotY += ry;
            rotZ += rz;
        }


    }
}
