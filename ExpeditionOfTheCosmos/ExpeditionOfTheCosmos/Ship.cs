using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ExpeditionOfTheCosmos
{
    class Ship
    {
        Model model;
        Vector3 position = Vector3.Zero;
        float rotation;
        Vector3 cameraPosition;
        Matrix view;
        Matrix[] boneTransformations;
        Matrix rotationMatrix = Matrix.Identity;

        Vector3 velocity = Vector3.Zero;
        float velocityScale = 1.0f;

        float rotationDecelerationRate = 0f;
        float rotationChange;

        bool thumbstickHasBeenTouched = false;
        bool posRotationChange;

        float vibrateFactor = 0.25f;

        float shipHealth = 1000;
        float damageDelay = 1;
        float fuelCount = 1000;
        float regenRate = 3;

        float chanceOfLosingFuel;

        bool canSpawnAsteroids = true;

        Random rand = new Random();

        public Model Model
        { 
            get { return model; }
            set { model = value; }
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
                    rotationMatrix =  Matrix.CreateRotationY(rotation);
                }
            }
        }

        public Matrix RotationMatrix
        {
            get
            {
                return rotationMatrix;
            }
            set { rotationMatrix = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        public Vector3 CameraPosition
        {
            get { return cameraPosition; }
        }

        public float ShipHealth
        {
            get { return shipHealth; }
            set { shipHealth = value; }
        }

        public float FuelCount
        {
            get { return fuelCount; }
            set { fuelCount = value; }
        }

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public bool CanSpawnAsteroids
        {
            get { return canSpawnAsteroids; }
            set { canSpawnAsteroids = value; }
        }

        public Ship()
        {
            cameraPosition = new Vector3(position.X, position.Y, position.Z);
            view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
        }

        public void LoadContent(ContentManager c) {
            model = c.Load<Model>("spaceship");
        }

        public void Update(GamePadState pad, GameTime gametime) {
            cameraPosition.X = position.X - 400 * (float)Math.Sin(Rotation);
            cameraPosition.Z = position.Z - 400 * (float)Math.Cos(Rotation);
            cameraPosition.Y = position.Y + 200;

            // Rotate the model using the left thumbstick, and scale it down
            // Rotation variable smoothing amount
            rotationChange = 0.005f * (float)Math.Pow(0.95, rotationDecelerationRate);
            
            // Rotation of the model and rotation smoothing
            if (pad.ThumbSticks.Left.X > 0)
            {
                Rotation -= 0.008f;
                thumbstickHasBeenTouched = true;
                posRotationChange = true;
                rotationDecelerationRate = 0;
            }
            else if (pad.ThumbSticks.Left.X < 0)
            {
                Rotation += 0.008f;
                thumbstickHasBeenTouched = true;
                posRotationChange = false;
                rotationDecelerationRate = 0;
            }
            else
            {
                if (thumbstickHasBeenTouched == true)
                {
                    if (posRotationChange)
                    {
                        Rotation -= rotationChange;
                    }
                    else
                    {
                        Rotation += rotationChange;
                    }
                    rotationDecelerationRate += 1;
                }
            }


            if (fuelCount <= 0)
            {
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
            }
            else
            {
                GamePad.SetVibration(PlayerIndex.One, pad.Triggers.Right * vibrateFactor, pad.Triggers.Right * vibrateFactor);

                // Finally, add this vector to our velocity.         
                velocity += rotationMatrix.Backward * velocityScale * pad.Triggers.Right;
                velocity.Y += pad.ThumbSticks.Left.Y * .5f;

                fuelCount -= (CalculateSpeed(gametime)*5 / (56600 * 16f)) * ((Math.Abs(pad.Triggers.Right) + 1)) * ((Math.Abs(pad.ThumbSticks.Left.Y) + 1)/10) ;
            }
            position += velocity;

            velocity *= .985f;
            velocity.Y *= .95f;

            view = Matrix.CreateLookAt(cameraPosition, position + Vector3.Up*100, Vector3.Up);

            damageDelay -= (float)gametime.ElapsedGameTime.TotalSeconds;
            regenRate -= (float)gametime.ElapsedGameTime.TotalSeconds;

            if (shipHealth > 1000)
            {
                shipHealth = 1000;
            }

            if (regenRate <= 0 && CalculateSpeed(gametime) <= 28300f)
            {
                if ((shipHealth + -.4f * ((CalculateSpeed(gametime)) / 28300f) + .4f <= 1000))
                {
                    shipHealth += (-.4f*((CalculateSpeed(gametime)) / 28300f) + .4f);
                }
                else
                {
                    shipHealth = 1000;
                }
            }
        }

        public void CheckCollisionsWithAsteroids(Asteroid asteroid, Matrix otherAsteroidWorld, GameTime gametime)
        {    
            if (CollidesWith(asteroid.Model, otherAsteroidWorld) && damageDelay <= 0)
            {
                asteroid.Velocity *= -10 * CalculateSpeed(gametime)/56600f;
                shipHealth -= 80 * CalculateSpeed(gametime)/56600f + 20;
                damageDelay = .5f;
                chanceOfLosingFuel = rand.Next(1, 6);
                velocity *= .6f;
                if (chanceOfLosingFuel == 1 || chanceOfLosingFuel == 2)
                {
                    if (fuelCount / 1000 > .2)
                    {
                        if (fuelCount / 1000 > .3)
                        {
                            fuelCount -= rand.Next(50, 100);
                        } else
                        {
                            fuelCount -= 50;
                        }
                    } else
                    {
                        fuelCount *= .75f;
                    }
                }
                return;
            }
            else
            {
                if (damageDelay >= 0.3f) // Collision sound effect
                {
                    vibrateFactor = 1;
                    GamePad.SetVibration(PlayerIndex.One, vibrateFactor, vibrateFactor);
                }
                else
                {
                    vibrateFactor = .25f;
                }
            }
        }

        public void CheckIfShipHasReachedPlanet(Planet planet, Matrix planetWorld)
        {
            switch (planet.PlanetName)
            {
                case Planet.PlanetID.Mars:
                    if (CollidesWithPlanet(planet.Model, planetWorld, 2.5f))
                    {
                        canSpawnAsteroids = false;
                        planet.HasBeenReached = true;
                    }
                    else
                    {
                        canSpawnAsteroids = true;
                    }
                    break;
                case Planet.PlanetID.Jupiter:
                    if (CollidesWithPlanet(planet.Model, planetWorld, 700)) // For some reason, this mesh came out weird
                    {
                        canSpawnAsteroids = false;
                        planet.HasBeenReached = true;
                    }
                    else
                    {
                        canSpawnAsteroids = true;
                    }
                    break;
                case Planet.PlanetID.Saturn:
                    if (CollidesWithPlanet(planet.Model, planetWorld, 1.25f))
                    {
                        canSpawnAsteroids = false;
                        planet.HasBeenReached = true;
                    }
                    else
                    {
                        canSpawnAsteroids = true;
                    }
                    break;
                case Planet.PlanetID.Uranus:
                    if (CollidesWithPlanet(planet.Model, planetWorld, 1.5f))
                    {
                        canSpawnAsteroids = false;
                        planet.HasBeenReached = true;
                    }
                    else
                    {
                        canSpawnAsteroids = true;
                    }
                    break;
                case Planet.PlanetID.Neptune:
                    if (CollidesWithPlanet(planet.Model, planetWorld, 350f)) // And this one
                    {
                        canSpawnAsteroids = false;
                        planet.HasBeenReached = true;
                    }
                    else
                    {
                        canSpawnAsteroids = true;
                    }
                    break;
            }
        }

        bool CollidesWith(Model otherModel, Matrix otherWorld)
        {
            foreach (ModelMesh myModelMeshes in model.Meshes)
            {
                foreach (ModelMesh itsModelMeshes in otherModel.Meshes)
                {
                    Matrix shipWorldMatrix = boneTransformations[myModelMeshes.ParentBone.Index] * rotationMatrix * Matrix.CreateTranslation(position);
                    BoundingSphere asteroidBoundingSphere = itsModelMeshes.BoundingSphere;
                    asteroidBoundingSphere.Radius = itsModelMeshes.BoundingSphere.Radius * 0.7f;
                    if (myModelMeshes.BoundingSphere.Transform(shipWorldMatrix).Intersects(asteroidBoundingSphere.Transform(otherWorld)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool CollidesWithPlanet(Model planetModel, Matrix planetWorld, float scaleFactor)
        {
            foreach (ModelMesh myModelMeshes in model.Meshes)
            {
                foreach (ModelMesh itsModelMeshes in planetModel.Meshes)
                {
                    Matrix shipWorldMatrix = boneTransformations[myModelMeshes.ParentBone.Index] * rotationMatrix * Matrix.CreateTranslation(position);
                    BoundingSphere planetBoundingSphere = itsModelMeshes.BoundingSphere;
                    planetBoundingSphere.Radius = itsModelMeshes.BoundingSphere.Radius * scaleFactor;
                    if (myModelMeshes.BoundingSphere.Transform(shipWorldMatrix).Intersects(planetBoundingSphere.Transform(planetWorld)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ResetValues()
        {
            shipHealth = 1000;
            fuelCount = 1000;
            position = Vector3.Zero;
            velocity = Vector3.Zero;
            rotation = 0;
            rotationMatrix = Matrix.Identity;
            thumbstickHasBeenTouched = false;
        }

        public void Draw(Model shipModel, Matrix modelTransform, Matrix projection) {
            boneTransformations = new Matrix[shipModel.Bones.Count];
            shipModel.CopyAbsoluteBoneTransformsTo(boneTransformations);
            foreach (ModelMesh mesh in shipModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransformations[mesh.ParentBone.Index] * modelTransform;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                    effect.DirectionalLight0.Direction = new Vector3(0, -0.5f, 0);
                    effect.DirectionalLight0.SpecularColor = Vector3.One;
                }
                mesh.Draw();
            }
        }

        public float CalculateSpeed(GameTime gameTime)
        {
            float distance = velocity.Length();
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float speed = distance / time;
            speed = (float)Math.Round(speed *14.37f* 100f) / 100f;

            if (speed < 56600f)
                return speed;
            else
                return 56600.00f;
        }
    }
}
