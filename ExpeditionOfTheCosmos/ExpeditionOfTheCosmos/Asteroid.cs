using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ExpeditionOfTheCosmos
{
    class Asteroid
    {
        Model model;
        Vector3 position;
        Vector3 velocity;
        float rotation;
        Matrix rotationMatrix = Matrix.Identity;
        Matrix[] boneTransformations;

        Random rand = new Random();
        float lifetimeCount = 60;
        bool hasLifetimeExpired = false;

        float velocityScaleFactor;
        float randomRotationValue;

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

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
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
                    rotationMatrix = Matrix.CreateRotationZ(rotation);
                }
            }
        }

        public Matrix RotationMatrix
        {
            get { return rotationMatrix; }
            set { rotationMatrix = value; }
        }

        public bool HasLifetimeExpired
        {
            get { return hasLifetimeExpired; }
            set { hasLifetimeExpired = value; }
        }

        public Asteroid(Model model, Vector3 position)
        {
            this.position = position;
            this.model = model;
            velocity.X = (float)rand.NextDouble() *2  - 1f;
            velocity.Y = (float)rand.NextDouble()*2 - 1f;
            velocity.Z = (float)rand.NextDouble()*2 - 1f;
            velocityScaleFactor = (float)rand.NextDouble() * 3 + 1;
            randomRotationValue = (float)rand.NextDouble() * .02f + .005f;
        }

        public void Update(GameTime gametime)
        {
            Rotation += randomRotationValue;

            position += velocity*5f;

            lifetimeCount -= (float)gametime.ElapsedGameTime.TotalSeconds;
            
            if (lifetimeCount <= 0)
            {
                hasLifetimeExpired = true;   
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
                }
                mesh.Draw();
            }
        }

    }
}
