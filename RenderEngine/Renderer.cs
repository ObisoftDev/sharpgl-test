using SharpGL;
using SharpGL.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixeliumTest
{
    public class Renderer
    {

        public const float FOV = 70;
        public const float NEAR_PLANE = 0.1f;
        public const float FAR_PLANE = 10000f;


        public Matrix4 projectionMatrix;
        public StaticShader shader;

        public Renderer(StaticShader shader)
        {
            this.shader = shader;

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);


            createProjectionMatrix();
            shader.Use();
            shader.loadProjectionMatrix(projectionMatrix);
            shader.Stop();
        }


        public void prepate()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.SampleAlphaToOne);
            GL.BlendFunc(Blending.SrcAlpha, Blending.OneMinusSrcAlpha);


            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit |
                     ClearBufferMask.StencilBufferBit);
            //GL.ClearColor(new Color4(20, 20, 20, 255));
        }


        public void render(Dictionary<TexturedModel,List<Entity>> entities)
        {
            foreach(var e in entities)
            {
                prepateTextureModel(e.Key);
                List<Entity> batch = e.Value;
                for (int  i=0;i<batch.Count;i++)
                {
                    prepareInstance(batch[i]);
                    GL.DrawElements(PrimitiveType.Triangles, e.Key.rawModel.vertexCount, DrawElementsType.UnsignedInt, 0);
                }
                unbindTextureModel();
            }
        }

        public void prepateTextureModel(TexturedModel texturedModel)
        {
            RawModel model = texturedModel.rawModel;
            GL.BindVertexArray((uint)model.vaoID);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            Texture texture = texturedModel.texture;
            shader.loadShineVariables(1f, 0);
            shader.loadRenderColor(model.RenderColor);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texturedModel.texture.ID);
        }

        public void unbindTextureModel()
        {
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
            GL.BindVertexArray(0);
        }

        public void prepareInstance(Entity entity)
        {
            Matrix4 transformationMatrix = Maths.createTransformationMatric(entity.position,
               entity.rotX, entity.rotY, entity.rotZ, entity.scale);
            shader.loadTrasformationMatrix(transformationMatrix);
        }

        public void render(Entity entity,StaticShader shader)
        {
            TexturedModel texturedModel = entity.model;
            RawModel model = texturedModel.rawModel;
            GL.BindVertexArray((uint)model.vaoID);
            
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            Matrix4 transformationMatrix = Maths.createTransformationMatric(entity.position,
                entity.rotX, entity.rotY, entity.rotZ, entity.scale);
            shader.loadTrasformationMatrix(transformationMatrix);

            Texture texture = texturedModel.texture;
            shader.loadShineVariables(1f,0);
            shader.loadRenderColor(model.RenderColor);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texturedModel.texture.ID);
            GL.Color(model.RenderColor);
            GL.DrawElements(PrimitiveType.Triangles,model.vertexCount, DrawElementsType.UnsignedInt,0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
            GL.BindVertexArray(0);
        }


        public void createProjectionMatrix()
        {
            float aspectRatio = (float)Game.Current.Width / (float)Game.Current.Height;
            float y_scale = (float)(1f / Math.Tan(Mathf.Radians(FOV/2f))) * aspectRatio;
            float x_scale = y_scale / aspectRatio;
            float frustum_lenght = FAR_PLANE - NEAR_PLANE;

            projectionMatrix = Matrix4.Identity;
            projectionMatrix[0, 0] = x_scale;
            projectionMatrix[1, 1] = y_scale;
            projectionMatrix[2, 2] = -((FAR_PLANE+NEAR_PLANE) - frustum_lenght);
            projectionMatrix[2, 3] = -1;
            projectionMatrix[3, 2] = -((2 * NEAR_PLANE * FAR_PLANE) / frustum_lenght);
            projectionMatrix[3, 3] = 0;
        }
    }
}
