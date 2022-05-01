using SharpGL;
using SharpGL.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixeliumTest
{
    public class StaticShader : ShaderProgram
    {
        public static string VERTEX_FILE = "Shaders/vertex.vs";
        public static string FRAGMENT_FILE = "Shaders/fragment.fs";


        private static string location_trasformationMatrix = "trasformationMatrix";
        private static string location_projectionMatrix = "projectionMatrix";
        private static string location_viewMatrix = "viewMatrix";
        private static string location_lightPosition = "lightPosition";
        private static string location_lightColour = "lightColour";
        private static string location_shineDamper = "shineDamper";
        private static string location_reflectivity = "reflectivity";
        private static string location_renderColor = "render_color";


        public StaticShader()
        {
            Create(File.ReadAllText(VERTEX_FILE), File.ReadAllText(FRAGMENT_FILE), null);
        }

        protected override void BindAttributes()
        {
            BindAttributeLocation(0, "position");
            BindAttributeLocation(1, "textureCoords");
            BindAttributeLocation(2, "normal");
        }
        protected override void GetAllUniformLocations()
        {
           
        }

        public void loadRenderColor(Color4 c)
        {
            SetUniform(location_renderColor,c.R,c.G,c.B,c.A);
        }

        public void loadShineVariables(float damper, float reflect)
        {
            SetUniform(location_shineDamper, damper);
            SetUniform(location_reflectivity, reflect);
        }

        public void loadTrasformationMatrix(Matrix4 matrix)
        {
            SetUniformMatrix4(location_trasformationMatrix, matrix.ToFloat());
        }
        public void LoadLight(Light light)
        {
            SetUniform(location_lightPosition, light.Position);
            SetUniform(location_lightColour, light.Color);
        }
        public void loadProjectionMatrix(Matrix4 matrix)
        {
            SetUniformMatrix4(location_projectionMatrix, matrix);
        }
        public void loadViewMatrix(Camera camera)
        {
            SetUniformMatrix4(location_viewMatrix, Maths.createViewmatrix(camera));
        }
    }
}
