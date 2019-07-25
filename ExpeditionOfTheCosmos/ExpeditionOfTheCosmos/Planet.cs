using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ExpeditionOfTheCosmos
{
    class Planet
    {
        Model model;
        Vector3 position;
        float rotationY;
        float rotationZ;
        Matrix rotationMatrix = Matrix.Identity;
        Matrix[] boneTransformations;
        float scaleFactor = 90f;

        bool hasBeenReached;

        // only for six solar system planets
        PlanetID planetName;

        public enum PlanetID
        {
            Earth,
            Mars,
            Jupiter,
            Saturn,
            Uranus, 
            Neptune
        }

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

        public float PlanetRotation
        {
            get { return rotationY; }
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

                if (rotationY != newValue)
                {
                    rotationY = newValue;
                    rotationMatrix = Matrix.CreateRotationY(rotationY) * Matrix.CreateRotationZ(rotationZ);
                }
            }
        }

        public float RotationZ
        {
            get { return rotationZ; }
            set
            { rotationZ = value; }
            
        }

        public Matrix RotationMatrix
        {
            get { return rotationMatrix; }
            set { rotationMatrix = value; }
        }

        public float ScaleFactor
        {
            get { return scaleFactor; }
            set { scaleFactor = value; }
        }

        public PlanetID PlanetName
        {
            get { return planetName; }
            set { planetName = value; }
        }

        public bool HasBeenReached
        {
            get { return hasBeenReached; }
            set { hasBeenReached = value; }
        }

        public Planet()
        {
            position = new Vector3(0, 0, 0);
        }

        public Planet(int planetName)
        {
            this.planetName = (PlanetID)planetName;
        }

        public void LoadContent(ContentManager c, string planetName)
        {
            model = c.Load<Model>(planetName); 
        }

        public void Draw(Model planetModel, Matrix planetTransform, Matrix projection, Matrix view)
        {
            boneTransformations = new Matrix[planetModel.Bones.Count];
            planetModel.CopyAbsoluteBoneTransformsTo(boneTransformations);
            foreach (ModelMesh mesh in planetModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransformations[mesh.ParentBone.Index] * planetTransform * Matrix.CreateScale(scaleFactor);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }

    }
}
