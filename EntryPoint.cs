using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpGL;
using SharpGL.Desktop;
using SharpGL.Graphics;

namespace PixeliumTest
{
    class EntryPoint
    {
        [STAThread]

        static void Main()
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
     
    }

    public class Game : RenderWindow
    {
        Loader loader;
        MasterRender masterRender;


        Camera camera;
        Light light;

        RawModel model;
        Texture texture;
        TexturedModel texturedModel;
        Entity entity;


        public static List<Entity> Entities = new List<Entity>();


        public override void OnLoad()
        {
            loader = new Loader();
            masterRender = new MasterRender();


            model = ObjLoader.loadObjModel("Models/model.obj", loader);
            texture = loader.loadTexture("Models/model.jpg");
            texturedModel = new TexturedModel(model, texture);
            camera = new Camera();
            light = new Light(new Vector3(2000,3000,2000), new Vector3(0.1f, 0.1f, 0.1f));


           Entities.Add(entity = new Entity(texturedModel,new Vector3(0,-2,-50),0,0,0,1));

          
           targetEntity = Entities[0];
        }
        public override void OnUpdate(RenderLoop loop)
        {
            camera.move(targetEntity, loop.DeltaTime);

            if (InputDevice.IsKeyDown(KeyCode.Space))
            {
                int index = Mathf.RandomInt(0, Entities.Count);
                targetEntity = Entities[index];
            }
            if (InputDevice.IsKeyDown(KeyCode.E))
            {
                camera.pitch++;
            }
            if (InputDevice.IsKeyDown(KeyCode.Q))
            {
                camera.pitch--;
            }
            Title = $"MX:{InputDevice.GetAxisValue("Mouse X")} MY:{InputDevice.GetAxisValue("Mouse Y")}";
        }
        Entity targetEntity;
        public override void OnRender()
        {
            camera.move(targetEntity,1);

            for (int i = 0; i < Entities.Count; i++)
            {
                //Entities[i].increaseRotation(0, 1, 0);
                masterRender.processEntity(Entities[i]);
            }
            masterRender.Render(light, camera);
        }

        public override void OnResize(int w, int h)
        {
            GL.Viewport(0, 0, w, h);
        }
    }

}
