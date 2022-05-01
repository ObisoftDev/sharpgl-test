using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixeliumTest
{
    public class Maths
    {
        public static Matrix4 createTransformationMatric(Vector3 translation,
            float rx,float ry,float rz,Vector3 scale)
        {
            Matrix4 matrix = Matrix4.Identity;
            matrix *= Matrix4.CreateRotationX(Mathf.Radians(rx));
            matrix *= Matrix4.CreateRotationY(Mathf.Radians(ry));
            matrix *= Matrix4.CreateRotationZ(Mathf.Radians(rz));
            matrix *= Matrix4.CreateTranslation(translation);
            matrix *= Matrix4.CreateScaling(new Vector3(scale.X, scale.Y,scale.Z));
            return matrix;
        }

        public static Matrix4 createViewmatrix(Camera camera)
        {
            Matrix4 matrix = Matrix4.Identity;
            Vector3 position = -camera.position;
            matrix *= Matrix4.CreateTranslation(position);
            matrix *= Matrix4.CreateRotationX(Mathf.Radians(camera.pitch));
            matrix *= Matrix4.CreateRotationY(Mathf.Radians(camera.yaw));
            return matrix;
        }

    }
}
