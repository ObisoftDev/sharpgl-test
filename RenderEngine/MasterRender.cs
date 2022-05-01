using SharpGL.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixeliumTest
{
    public class MasterRender
    {
        public static StaticShader Shader = new StaticShader();
        public Renderer Renderer = new Renderer(Shader);

        public Dictionary<TexturedModel, List<Entity>> Entities = new Dictionary<TexturedModel, List<Entity>>();



        public void Render(Light sun,Camera cam)
        {
            Renderer.prepate();
            Shader.Use();
            Shader.LoadLight(sun);
            Shader.loadViewMatrix(cam);
            Renderer.render(Entities);
            Shader.Stop();
            Entities.Clear();
        }

        public void processEntity(Entity entity)
        {
            TexturedModel entityModel = entity.model;
            if(Entities.TryGetValue(entityModel,out List<Entity> batch))
            {
                batch.Add(entity);
            }
            else
            {
                List<Entity> newBatch = new List<Entity>();
                newBatch.Add(entity);
                Entities.Add(entityModel,newBatch);
            }
        }

        public void CleanUp()
        {
            

        }
    }
}
