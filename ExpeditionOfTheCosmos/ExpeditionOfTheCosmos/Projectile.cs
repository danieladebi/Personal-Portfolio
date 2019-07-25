using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ExpeditionOfTheCosmos
{
    class Projectile
    {
        Model model;
        Vector3 position = Vector3.Zero;
        float rotation;
        Matrix rotationMatrix = Matrix.Identity;
        Matrix[] boneTransformations;
        Vector3 velocity;

        float projectileLifetime = 1;
        bool isActive = true;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set
            {
                float newValue = value;
                while (newValue >= MathHelper.TwoPi)
                {
                    newValue -= MathHelper.TwoPi;
                }
                while (newValue < 0)
                {
                    newValue += MathHelper.TwoPi;
                }

                if (rotation != newValue)
                {
                    rotation = newValue;
                    rotationMatrix = Matrix.CreateRotationY(rotation);
                }
            }
        }

        public Matrix RotationMatrix
        {
            get { return rotationMatrix; }
            set { rotationMatrix = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public Projectile (Model model, Vector3 shipPosition, float shipRotation)
        {
            position = shipPosition;
            position.X += 150 * (float)Math.Sin(shipRotation);
            position.Z += 150 * (float)Math.Cos(shipRotation);
            rotationMatrix = Matrix.CreateRotationY(shipRotation- MathHelper.PiOver2);
            this.model = model;

            velocity = new Vector3((float)Math.Sin(shipRotation), 0, (float)Math.Cos(shipRotation)) * 100;
        }

        public void Update(GameTime gametime)
        {
            position += velocity;

            projectileLifetime -= (float)gametime.ElapsedGameTime.TotalSeconds;

            if (projectileLifetime <= 0)
            {
                isActive = false;
            }
        }

        public void Draw(Matrix modelTransform, Matrix projection, Matrix view)
        {
            boneTransformations = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransformations);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransformations[mesh.ParentBone.Index] * modelTransform;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                    effect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 0);
                    effect.DirectionalLight1.DiffuseColor = new Vector3(1, 1, 0);
                    effect.DirectionalLight2.DiffuseColor = new Vector3(1, 1, 0);
                    effect.EmissiveColor = new Vector3(1, 1, 0.2f);
                    effect.AmbientLightColor = new Vector3(1, .75f, 0.5f);
                }
                mesh.Draw();
            }
        }
    }
}
