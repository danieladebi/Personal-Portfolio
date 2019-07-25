using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpeditionOfTheCosmos
{
    class Skybox
    {
        Model skyBox;
        TextureCube skyBoxTexture;
        Effect skyBoxEffect;
        float size = 50f;

        public Skybox(string skyboxTexture, ContentManager c)
        {
            skyBox = c.Load<Model>("skyboxBox");
            skyBoxTexture = c.Load<TextureCube>(skyboxTexture);
            skyBoxEffect = c.Load<Effect>("Skybox");
        }

        public void Draw(Matrix view, Matrix projection, Vector3 cameraPosition)
        {
            foreach (EffectPass pass in skyBoxEffect.CurrentTechnique.Passes)
            {
                foreach (ModelMesh mesh in skyBox.Meshes)
                {  
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = skyBoxEffect;
                        part.Effect.Parameters["World"].SetValue(Matrix.CreateScale(size) * Matrix.CreateTranslation(cameraPosition));
                        part.Effect.Parameters["View"].SetValue(view);
                        part.Effect.Parameters["Projection"].SetValue(projection);
                        part.Effect.Parameters["SkyBoxTexture"].SetValue(skyBoxTexture);
                        part.Effect.Parameters["CameraPosition"].SetValue(cameraPosition);
                    }

                    mesh.Draw();
                }
            }
        }
    }
}
