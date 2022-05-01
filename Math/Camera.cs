using SharpGL;
using SharpGL.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixeliumTest
{
    public class Camera
    {

        public Vector3 position = new Vector3(0,0,0);

        public float Zoom = -2000;
        public float angleAround = 0;



        public float pitch = 0;
        public float yaw = 0;
        public float roll = 0;

       


        public void move(Entity e,float dt)
        {
            float h = InputDevice.Current.GetAxisValue("Horizontal");
            float v = InputDevice.Current.GetAxisValue("Vertical");
            float y = (InputDevice.Current.IsKeyPressed(KeyCode.E)) ? 1 : (InputDevice.Current.IsKeyPressed(KeyCode.Q))?-1:0;

            position += new Vector3(h,y,v);



              calcAngleAround(dt);
              float hDist = calcHDistance();
              float vDist = calcVDistance();
              calcCameraPos(e, hDist, vDist);

             yaw = (360 - (e.rotY + angleAround));
        }




        public void calcCameraPos(Entity e,float hdist,float vdist)
        {
            float theta = e.rotY + angleAround;
            float offcetX =  hdist * Mathf.Sin(Mathf.Radians(theta));
            float offcetZ =  hdist * Mathf.Cos(Mathf.Radians(theta));
            position.X = e.position.X - offcetX;
            position.Z = e.position.Z - offcetZ;
            position.Y = e.position.Y + vdist + 100;
        }

        public float calcHDistance()
        {
            return Zoom * Mathf.Cos(Mathf.Radians(pitch));
        }
        public float calcVDistance()
        {
            return Zoom * Mathf.Sin(Mathf.Radians(pitch));
        }

        public void calcAngleAround(float dt)
        {
            if (InputDevice.Current.IsMousePressed(MouseCode.Right))
            {
                float angleChange = InputDevice.Current.GetAxisValue("Mouse X") * 5;
                angleAround -= angleChange;

                float pitchChange = InputDevice.Current.GetAxisValue("Mouse Y") * 0.1f;
                //pitch -= pitchChange;
            }
        }
    }
}
