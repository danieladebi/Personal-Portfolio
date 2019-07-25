using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace ExpeditionOfTheCosmos
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int SCREENWIDTH = 1080;
        public const int SCREENHEIGHT = 700;

        GamePadState pad;

        public enum GameState
        {
            StartMenu,
            Intro,
            Continue,
            Game,
            Pause,
            Death,
            Victory,
            LoadSave
        }
        GameState currentState = GameState.StartMenu;

        SpriteFont gameFont;
        SpriteFont introFont;
        SpriteFont introTextFont;

        #region Start Menu Variables
        Texture2D mousePointer;
        Rectangle mousePointerRect;

        int mousePointerX;
        int mousePointerY;
        int mousePointerWidth = 32;
        int mousePointerHeight = 32;
        int mouseSpeed = 8;

        Texture2D titleSprite;
        Texture2D nameSprite;

        Rectangle titleRect;
        Rectangle nameRect;

        int titleWidth = 400;
        int titleHeight = 110;

        int nameWidth = 200;
        int nameHeight = 25;

        Texture2D startButton;
        Texture2D startButtonClicked;
        Texture2D exitButton;
        Texture2D exitButtonClicked;

        Rectangle startButtonRect;
        Rectangle exitButtonRect;

        int menuButtonWidth = 220;
        int menuButtonHeight = 60;

        float nameAlphaValue = 0;
        float nameFadeIncrement = .05f;
        double nameFadeDelay = .035;

        Scrolling[] backgrounds = new Scrolling[2];
        #endregion

        #region Game Variables
        Song backgroundMusic;

        Skybox skybox;

        Ship ship;
        Matrix projection;

        List<Projectile> lasers = new List<Projectile>();

        float score;
        float speed;

        DateTime thisDay = DateTime.Today;

        int hoursToAdd;
        double rateOfSecondsPer150Hours;

        Planet[] planets = new Planet[6];

        float marsZPosition;
        float jupiterZPosition;
        float saturnZPosition;
        float uranusZPosition;
        float neptuneZPosition;

        #region Planet Delays
        double timeMarsIsOutofReach = 10;
        bool isMarsTooFar = false;

        double timeJupiterIsOutofReach = 20;
        bool isJupiterTooFar = false;

        double timeSaturnIsOutofReach = 30;
        bool isSaturnTooFar = false;

        double timeUranusIsOutofReach = 45;
        bool isUranusTooFar = false;

        double timeNeptuneIsOutofReach = 60;
        bool isNeptuneTooFar = false;
        #endregion

        List<Asteroid> asteroids = new List<Asteroid>();
        double asteroidSpawnDelay = 2;

        float minRectPositionX;
        float maxRectPositionX;

        float minRectPositionY;
        float maxRectPositionY;

        float minRectPositionZ;
        float maxRectPositionZ;

        Rectangle fuelBarRectangle;
        Texture2D barSprite;
        float fuelBarLength = 220;
        float shipFuelPercentage;
        Color fuelBarColor = Color.Lime;
        Color shipHealthColorText = Color.White;

        float projectileShootDelay = .2f;
        float shootingCooldownValue = 100;

        int shootingCooldownWidthofRect;
        float shotCooldownRate = .25f;

        int nextPlanet = 1;

        int totalHours = 0;
        int timeTakenToReachPlanet = 0;

        int timeTakenToReachMars = 0;
        int timeTakenToReachJupiter = 0;
        int timeTakenToReachSaturn = 0;
        int timeTakenToReachUranus = 0;
        int timeTakenToReachNeptune = 0;

        float notificationDelay = 5;
        float notificationAlpha = 1;
        float notificationFadeDelay = .04f;

        float healthWarningDelay = 5;
        float healthWarningAlpha = 1;
        float healthWarningFadeDelay = .04f;

        Random rand = new Random();

        bool hasPlayedBefore = false;
        #endregion

        #region Pause Variables
        Texture2D pauseSprite;

        Rectangle pauseRect;
        int pauseWidth = 300;
        int pauseHeight = 75;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREENWIDTH;
            graphics.PreferredBackBufferHeight = SCREENHEIGHT;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mousePointerX = SCREENWIDTH / 2 - mousePointerWidth / 2;
            mousePointerY = SCREENHEIGHT / 2 - mousePointerHeight / 2;

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60f), SCREENWIDTH / SCREENHEIGHT, .1f, 650000.0f);
            ship = new Ship();

            for (int i = 0; i < planets.Length; i++)
            {
                planets[i] = new Planet(i);
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameFont = Content.Load<SpriteFont>("Game");
            introFont = Content.Load<SpriteFont>("IntroFont");
            introTextFont = Content.Load<SpriteFont>("IntroText");

            // TODO: use this.Content to load your game content here
            mousePointer = Content.Load<Texture2D>("pointer");

            titleSprite = Content.Load<Texture2D>("Title");
            nameSprite = Content.Load<Texture2D>("Name");

            titleRect = new Rectangle(SCREENWIDTH / 2 - titleWidth / 2, 100, titleWidth, titleHeight);
            nameRect = new Rectangle(SCREENWIDTH / 2 - nameWidth / 2, 250, nameWidth, nameHeight);

            startButton = Content.Load<Texture2D>("startButton");
            startButtonClicked = Content.Load<Texture2D>("startButtonClicked");
            exitButton = Content.Load<Texture2D>("exitButton");
            exitButtonClicked = Content.Load<Texture2D>("exitButtonClicked");

            backgrounds[0] = new Scrolling(Content.Load<Texture2D>("spaceBG1"), new Rectangle(0, 0, SCREENWIDTH, SCREENHEIGHT));
            backgrounds[1] = new Scrolling(Content.Load<Texture2D>("spaceBG2"), new Rectangle(SCREENWIDTH, 0, SCREENWIDTH, SCREENHEIGHT));

            pauseSprite = Content.Load<Texture2D>("Pause");
            pauseRect = new Rectangle(SCREENWIDTH / 2 - pauseWidth / 2, SCREENHEIGHT / 4, pauseWidth, pauseHeight);

            // Game content
            backgroundMusic = Content.Load<Song>("backgroundMusic");
            MediaPlayer.Play(backgroundMusic);

            skybox = new Skybox("EmptySpace", Content);
            ship.LoadContent(Content);
            barSprite = Content.Load<Texture2D>("HealthBar");
            fuelBarRectangle = new Rectangle(SCREENWIDTH - 250, 130, (int)fuelBarLength, 20);

            planets[0].LoadContent(Content, "Earth");
            planets[1].LoadContent(Content, "Mars");
            planets[2].LoadContent(Content, "Jupiter");
            planets[3].LoadContent(Content, "Saturn");
            planets[4].LoadContent(Content, "Uranus");
            planets[5].LoadContent(Content, "Neptune");       
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            pad = GamePad.GetState(PlayerIndex.One);

            // TODO: Add your update logic here
            switch (currentState)
            {
                case GameState.StartMenu:
                    MediaPlayer.Pause();
                    UpdateMenu(gameTime);
                    break;
                case GameState.Intro:
                    UpdateIntro();
                    break;
                case GameState.Continue:
                    UpdateContinue();
                    break;
                case GameState.Game:
                    MediaPlayer.Resume();
                    UpdateGame(gameTime);
                    break;
                case GameState.Pause:
                    MediaPlayer.Pause();
                    UpdatePause();
                    break;
                case GameState.Death:
                    MediaPlayer.Stop();
                    RunGameOver();
                    break;
                case GameState.Victory:
                    MediaPlayer.Stop();
                    UpdateVictory();
                    break;
                case GameState.LoadSave:
                    LoadInfo();
                    break;
            }

            base.Update(gameTime);
        }

        public void UpdateMenu(GameTime gameTime)
        {
            // Switch between backgrounds
            if (backgrounds[0].rectangle.X + backgrounds[0].rectangle.Width <= 0)
            {
                backgrounds[0].rectangle.X = backgrounds[1].rectangle.X + backgrounds[1].rectangle.Width;
            }
            if (backgrounds[1].rectangle.X + backgrounds[1].rectangle.Width <= 0)
            {
                backgrounds[1].rectangle.X = backgrounds[0].rectangle.X + backgrounds[0].rectangle.Width;
            }

            // Updating rectangles
            mousePointerRect = new Rectangle(mousePointerX, mousePointerY, mousePointerWidth, mousePointerHeight);
            startButtonRect = new Rectangle(SCREENWIDTH / 2 - menuButtonWidth / 2, SCREENHEIGHT / 2 - menuButtonHeight / 2 + 100,
                        menuButtonWidth, menuButtonHeight);
            exitButtonRect = new Rectangle(SCREENWIDTH / 2 - menuButtonWidth / 2, SCREENHEIGHT / 2 - menuButtonHeight / 2 + 220,
                        menuButtonWidth, menuButtonHeight);

            #region Mouse movement
            // Pointer movement
            if (pad.ThumbSticks.Left.X > 0)
            {
                mousePointerX += mouseSpeed;
            }
            if (pad.ThumbSticks.Left.X < 0)
            {
                mousePointerX -= mouseSpeed;
            }
            if (pad.ThumbSticks.Left.Y > 0)
            {
                mousePointerY -= mouseSpeed;
            }
            if (pad.ThumbSticks.Left.Y < 0)
            {
                mousePointerY += mouseSpeed;
            }

            // Check pointer collisions
            if (mousePointerX < 0)
            {
                mousePointerX = 0;
            }
            if (mousePointerX > SCREENWIDTH - mousePointerWidth)
            {
                mousePointerX = SCREENWIDTH - mousePointerWidth;
            }
            if (mousePointerY < 0)
            {
                mousePointerY = 0;
            }
            if (mousePointerY > SCREENHEIGHT - mousePointerHeight)
            {
                mousePointerY = SCREENHEIGHT - mousePointerHeight;
            }
            #endregion

            if (mousePointerRect.Intersects(startButtonRect) && pad.Buttons.A == ButtonState.Pressed)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    SetPlanetPositions(i);
                }

                currentState = GameState.Intro;
            }
            if (mousePointerRect.Intersects(exitButtonRect) && pad.Buttons.A == ButtonState.Pressed)
            {
                this.Exit();
            }

            foreach (Scrolling background in backgrounds)
            {
                background.Update();
            }

            // fade in and out name
            nameFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (nameFadeDelay <= 0)
            {
                nameFadeDelay = .035;

                nameAlphaValue += nameFadeIncrement;

                if (nameAlphaValue >= 1 || nameAlphaValue <= 0)
                {
                    nameFadeIncrement *= -1;
                }
            }

        }

        public void UpdateIntro()
        {
            #region Mouse movement
            mousePointerRect = new Rectangle(mousePointerX, mousePointerY, mousePointerWidth, mousePointerHeight);

            // Pointer movement
            if (pad.ThumbSticks.Left.X > 0)
            {
                mousePointerX += mouseSpeed;
            }
            if (pad.ThumbSticks.Left.X < 0)
            {
                mousePointerX -= mouseSpeed;
            }
            if (pad.ThumbSticks.Left.Y > 0)
            {
                mousePointerY -= mouseSpeed;
            }
            if (pad.ThumbSticks.Left.Y < 0)
            {
                mousePointerY += mouseSpeed;
            }

            // Check pointer collisions
            if (mousePointerX < 0)
            {
                mousePointerX = 0;
            }
            if (mousePointerX > SCREENWIDTH - mousePointerWidth)
            {
                mousePointerX = SCREENWIDTH - mousePointerWidth;
            }
            if (mousePointerY < 0)
            {
                mousePointerY = 0;
            }
            if (mousePointerY > SCREENHEIGHT - mousePointerHeight)
            {
                mousePointerY = SCREENHEIGHT - mousePointerHeight;
            }

            #endregion

            startButtonRect = new Rectangle(SCREENWIDTH / 2 - menuButtonWidth / 2, 600,
                       menuButtonWidth-30, menuButtonHeight-10);

            if (mousePointerRect.Intersects(startButtonRect) && pad.Buttons.A == ButtonState.Pressed)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    SetPlanetPositions(i);
                }
                currentState = GameState.Continue;
            }
        }

        public void UpdateContinue()
        {
            if (hasPlayedBefore)
            {
                if (pad.Buttons.Y == ButtonState.Pressed)
                {
                    currentState = GameState.LoadSave;
                }
            }
            if (pad.Buttons.X == ButtonState.Pressed)
            {
                ResetAllValues();
                currentState = GameState.Game;
            }
        }

        public void UpdateGame(GameTime gametime)
        {
            hasPlayedBefore = true;

            fuelBarRectangle = new Rectangle(SCREENWIDTH - 250, 130, (int)fuelBarLength, 20);

            shipFuelPercentage = ((int)Math.Round((ship.FuelCount / 1000f) * 1000)) / 10f;
            fuelBarLength = ship.FuelCount / 1000f * 220;

            fuelBarColor = Color.Lerp(Color.Lime, Color.Red, 1 - shipFuelPercentage / 100f);
            if (ship.ShipHealth < 200)
            {
                shipHealthColorText = Color.Red;
            }
            else
            {
                shipHealthColorText = Color.White;
            }

            ship.Update(pad, gametime);

            if (ship.ShipHealth <= 0 || ship.FuelCount <= 0)
            {     
                currentState = GameState.Death;
            }

            if (pad.Buttons.Start == ButtonState.Pressed)
            {
                currentState = GameState.Pause;
            }

            rateOfSecondsPer150Hours -= gametime.ElapsedGameTime.TotalSeconds;
            notificationDelay -= (float)gametime.ElapsedGameTime.TotalSeconds;
            healthWarningDelay -= (float)gametime.ElapsedGameTime.TotalSeconds;

            if (rateOfSecondsPer150Hours <= 0)
            {
                rateOfSecondsPer150Hours = 1f / 6f;
                thisDay = thisDay.AddHours(hoursToAdd);
                totalHours += hoursToAdd;
            }

            if (notificationDelay < 2)
            {
                notificationFadeDelay -= (float)gametime.ElapsedGameTime.TotalSeconds;
                if (notificationFadeDelay <= 0)
                {
                    notificationAlpha -= .04f;
                    notificationFadeDelay = .04f;
                }
            }

            if (healthWarningDelay < 2)
            {
                healthWarningFadeDelay -= (float) gametime.ElapsedGameTime.TotalSeconds;
                if (healthWarningFadeDelay <= 0)
                {
                    healthWarningAlpha -= .04f;
                    healthWarningFadeDelay = .04f;
                } 
            }

            if (ship.ShipHealth > 200)
                healthWarningDelay = 5;
                healthWarningAlpha = 1;
     
            speed = ship.CalculateSpeed(gametime);

            MovePlanets(gametime);

            #region Laser Stuff
            projectileShootDelay -= (float)gametime.ElapsedGameTime.TotalSeconds;

            if (pad.Buttons.A > 0 && projectileShootDelay <= 0 && shootingCooldownValue > 0)
            {
                lasers.Add(new Projectile(Content.Load<Model>("laserModel"), ship.Position, ship.Rotation));
                projectileShootDelay = .2f;
                shootingCooldownValue -= 10;
            }

            shotCooldownRate -= (float)gametime.ElapsedGameTime.TotalSeconds;
            if (shotCooldownRate <= 0)
            {
                if (shootingCooldownValue + 1.5 < 100)
                    shootingCooldownValue += 1.5f;
                else
                    shootingCooldownValue = 100;
                shotCooldownRate = .25f;
            }

            foreach (Projectile laser in lasers)
            {
                laser.Update(gametime);
            }

            shootingCooldownWidthofRect = (int)(shootingCooldownValue / 100 * 220);

            for (int i = 0; i < lasers.Count; i++)
            {
                CheckForInactiveLasers(i);
            }

            for (int i = 0; i < lasers.Count; i++)
            {
                for (int j = 0; j < asteroids.Count; j++)
                    CheckForLaserCollisions(lasers[i], asteroids[j]);         
            }
            #endregion

            #region Asteroid Stuff
            if (ship.CalculateSpeed(gametime) > 7000f && ship.CanSpawnAsteroids)
                SpawnAsteroids(gametime);

            foreach (Asteroid ast in asteroids)
            {
                ast.Update(gametime);
            }

            for (int i = 0; i < asteroids.Count; i++)
            {
                if (asteroids[i].HasLifetimeExpired)
                {
                    asteroids.RemoveAt(i);
                }
            }

            foreach (Asteroid ast1 in asteroids)
            {
                foreach (Asteroid ast2 in asteroids)
                {
                    if (ast1 != ast2)
                    {
                        CheckForAsteroidCollisions(ast1, ast2);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            foreach (Asteroid ast in asteroids)
            {
                Matrix astWorldMatrix = (ast.RotationMatrix * Matrix.CreateScale(.75f)) * Matrix.CreateTranslation(ast.Position);
                if (currentState == GameState.Game)
                    ship.CheckCollisionsWithAsteroids(ast, astWorldMatrix, gametime); // prevents the controller from vibrating after ship is destroyed
            }
            #endregion

            Matrix planetWorldMatrix = planets[nextPlanet].RotationMatrix * Matrix.CreateTranslation(planets[nextPlanet].Position) * Matrix.CreateScale(90f);
            ship.CheckIfShipHasReachedPlanet(planets[nextPlanet], planetWorldMatrix);
            if (planets[nextPlanet].HasBeenReached)
            {
                score += 500 * nextPlanet + (ship.FuelCount + ship.ShipHealth) / totalHours * 10000;
                nextPlanet++;
                timeTakenToReachPlanet = totalHours;

                if (nextPlanet == 2)
                {
                    timeTakenToReachMars = (int)(timeTakenToReachPlanet / 24f);
                }
                else if (nextPlanet == 3)
                {
                    timeTakenToReachJupiter = (int)(timeTakenToReachPlanet / 24f);
                }
                else if (nextPlanet == 4)
                {
                    timeTakenToReachSaturn = (int)(timeTakenToReachPlanet / 24f);
                }
                else if (nextPlanet == 5)
                {
                    timeTakenToReachUranus = (int)(timeTakenToReachPlanet / 24f);
                }
                else if (nextPlanet == 6)
                {
                    timeTakenToReachNeptune = (int)(timeTakenToReachPlanet / 24f);
                }

                totalHours = 0; 
                notificationDelay = 5;
                notificationAlpha = 1;
                notificationFadeDelay = .04f;
                if (nextPlanet > 5)
                {
                    currentState = GameState.Victory;
                }
            }
        }

        void CheckForInactiveLasers(int i)
        {
            if (!lasers[i].IsActive)
            {
                lasers.RemoveAt(i);
            }
        }

        /// <summary>
        /// Checks if any asteroid is colliding with another asteroid.
        /// </summary>
        /// <param name="ast1"></param>
        /// <param name="ast2"></param>
        void CheckForAsteroidCollisions (Asteroid ast1, Asteroid ast2)
        {
            for (int i = 0; i < ast1.Model.Meshes.Count; i++) 
            {
                BoundingSphere ast1BoundingSphere = ast1.Model.Meshes[i].BoundingSphere;
                ast1BoundingSphere.Center += ast1.Position;
                for (int j = 0; j < ast2.Model.Meshes.Count; j++)
                {
                    BoundingSphere ast2BoundingSphere = ast2.Model.Meshes[j].BoundingSphere;
                    ast2BoundingSphere.Center += ast2.Position;

                    if (ast1BoundingSphere.Intersects(ast2BoundingSphere))
                    {
                        ast1.Velocity *= -1;
                        ast2.Velocity *= -1;
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a laser hits an asteroid.
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="ast"></param>
        void CheckForLaserCollisions(Projectile laser, Asteroid ast)
        {
            for (int i = 0; i < laser.Model.Meshes.Count; i++)
            {
                BoundingSphere laserBoundingSphere = laser.Model.Meshes[i].BoundingSphere;
                laserBoundingSphere.Center += laser.Position;
                for (int j = 0; j < ast.Model.Meshes.Count; j++)
                {
                    BoundingSphere astBoundingSphere = ast.Model.Meshes[j].BoundingSphere;
                    astBoundingSphere.Center += ast.Position;

                    if (laserBoundingSphere.Intersects(astBoundingSphere))
                    {
                        laser.IsActive = false;
                        ast.HasLifetimeExpired = true;
                        int randomVal = rand.Next(10, 20);
                        if (ship.FuelCount + 20 < 1000)
                            ship.FuelCount += randomVal;                        
                        else
                            ship.FuelCount = 1000;
                        score += randomVal;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Saves Info
        /// </summary>
        void SaveInfo ()
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\Ikechukwu Dan Adebi\\Google Drive\\ExpeditionOfTheCosmos\\saveMSON.txt");
                sw.WriteLine(score);
                sw.WriteLine(ship.Position.X);
                sw.WriteLine(ship.Position.Y);
                sw.WriteLine(ship.Position.Z);
                for (int i = 0; i < planets.Length; i++)
                {
                    sw.WriteLine(planets[i].Position.X);
                    sw.WriteLine(planets[i].Position.Y);
                    sw.WriteLine(planets[i].Position.Z);
                    sw.WriteLine(planets[i].PlanetRotation);
                    sw.WriteLine(planets[i].HasBeenReached);
                }
                sw.WriteLine(ship.ShipHealth);
                sw.WriteLine(ship.FuelCount);
                sw.WriteLine(shootingCooldownValue);
                sw.WriteLine(nextPlanet);

                sw.WriteLine(isMarsTooFar);
                sw.WriteLine(isJupiterTooFar);
                sw.WriteLine(isSaturnTooFar);
                sw.WriteLine(isUranusTooFar);
                sw.WriteLine(isNeptuneTooFar);

                sw.WriteLine(totalHours);
                sw.WriteLine(timeTakenToReachPlanet);

                sw.WriteLine(projectileShootDelay);

                sw.WriteLine(notificationDelay);
                sw.WriteLine(notificationAlpha);
                sw.WriteLine(notificationFadeDelay);

                sw.WriteLine(timeTakenToReachMars);
                sw.WriteLine(timeTakenToReachJupiter);
                sw.WriteLine(timeTakenToReachSaturn);
                sw.WriteLine(timeTakenToReachUranus);
                sw.WriteLine(timeTakenToReachNeptune);

                sw.WriteLine(thisDay);

                sw.WriteLine(hasPlayedBefore);

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        /// <summary>
        /// Load Info
        /// </summary>
        void LoadInfo ()
        {
            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\Ikechukwu Dan Adebi\\Desktop\\saveMSON.txt");
                string line = sr.ReadLine();
                while (line != null)
                {
                    score = int.Parse(line);
                    ship.Position = new Vector3(float.Parse(line), float.Parse(line), float.Parse(line));
                    for (int i = 0; i < planets.Length; i++)
                    {
                        planets[i].Position = new Vector3(float.Parse(line), float.Parse(line), float.Parse(line));
                        planets[i].PlanetRotation = float.Parse(line);
                        planets[i].HasBeenReached = bool.Parse(line);
                    }
                    ship.ShipHealth = float.Parse(line);
                    ship.FuelCount = float.Parse(line);
                    shootingCooldownValue = float.Parse(line);
                    nextPlanet = int.Parse(line);

                    isMarsTooFar = bool.Parse(line);
                    isJupiterTooFar = bool.Parse(line);
                    isSaturnTooFar = bool.Parse(line);
                    isUranusTooFar = bool.Parse(line);
                    isNeptuneTooFar = bool.Parse(line);

                    totalHours = int.Parse(line);
                    timeTakenToReachPlanet = int.Parse(line);

                    projectileShootDelay = float.Parse(line);

                    notificationDelay = float.Parse(line);
                    notificationAlpha = float.Parse(line);
                    notificationFadeDelay = float.Parse(line);

                    timeTakenToReachMars = int.Parse(line);
                    timeTakenToReachJupiter = int.Parse(line);
                    timeTakenToReachSaturn = int.Parse(line);
                    timeTakenToReachUranus = int.Parse(line);
                    timeTakenToReachNeptune = int.Parse(line);

                    thisDay = DateTime.Parse(line);

                    hasPlayedBefore = bool.Parse(line);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
            }
            currentState = GameState.Game;
        }

        // Essentially initializes all the planet information
        /// <summary>
        /// Sets each planet's position
        /// </summary>
        /// <param name="planetName"></param>
        void SetPlanetPositions(int planetName)
        {       
            if (planetName == 0)
            {
                planets[0].Position = new Vector3(0, 0, -50);
            }
            // All these distances are proportional to the average distances each of these objects are from Earth
            if (planetName == 1)
            {
                planets[1].Position = new Vector3(0, 0, 1175); // real value 1175
            }
            if (planetName == 2)
            {
                planets[2].Position = new Vector3(0, 0, 4275); // calculated value 3275
            }
            if (planetName == 3)
            {
                planets[3].Position = new Vector3(0, 0, 7770); // calculated value 6770
            }
            if (planetName == 4)
            {
                planets[4].Position = new Vector3(0, 0, 15540); // calculated value 14540
            }
            if (planetName == 5)
            {
                planets[5].Position = new Vector3(0, 0, 24300); // calculated value 23300
            }

            // Planet Axes
            planets[0].RotationZ = MathHelper.ToRadians(23.5f); // Earth //
            planets[1].RotationZ = MathHelper.ToRadians(25); // Mars //
            planets[2].RotationZ = MathHelper.ToRadians(3.13f); // Jupiter //
            planets[3].RotationZ = MathHelper.ToRadians(26.7f); // Saturn //
            planets[4].RotationZ = MathHelper.ToRadians(98.6f); // Uranus //
            planets[5].RotationZ = MathHelper.ToRadians(28.32f); // Neptune //

            hoursToAdd = 25;
        }

        /// <summary>
        /// Moves and rotates each planet
        /// </summary>
        void MovePlanets(GameTime gametime)
        {
            // Rotates planets.
            // Speed is relative to Earth
            planets[0].PlanetRotation += .002f; // Earth //
            planets[1].PlanetRotation += .002f; // Mars // about the same as Earth's rotation
            planets[2].PlanetRotation += .005f; // Jupiter // 2.5 times the speed of Earth's rotation
            planets[3].PlanetRotation += .0045f; // Saturn // 2.2 times the speed of Earth's rotation
            planets[4].PlanetRotation += .0028f; // Uranus // 1.4 times the speed of Earth's rotation
            planets[5].PlanetRotation += .003f; // Neptune // 1.5 times the speed of Earth's rotation

            #region Mars Orbit Code
            // Moves planets (for the purposes of this game, Earth will not move)
            // Mars movement. Based off of function I created for the movement of Mars. This is the planet with the most realistic "orbit movement". The zPosition changes as time goes on.
            if (!isMarsTooFar)
            {
                planets[1].Position += new Vector3(.5f, 0, 0.15f * (float)Math.Cos((MathHelper.TwoPi / 2.09861f) * ((thisDay.Year + thisDay.DayOfYear / 365f) - 157.5f))); // 1 is 86,400 km/h, which is the speed of Mars in orbit.
            }

            if (planets[1].Position.X > 2250) // this is about the point in orbit where Mars is really far away regarding orbit.
            { 
                marsZPosition = planets[1].Position.Z;
                planets[1].Position = new Vector3(-200000, 0, marsZPosition);
                isMarsTooFar = true;
            }

            if (isMarsTooFar)
            {
                timeMarsIsOutofReach -= gametime.ElapsedGameTime.TotalSeconds;
            }

            if (timeMarsIsOutofReach <= 0)
            {
                timeMarsIsOutofReach = 10;
                planets[1].Position = new Vector3(-2250, 0, marsZPosition);
                isMarsTooFar = false;
            }
            #endregion

            #region Jupiter Orbit Code
            if (!isJupiterTooFar)
            {
                planets[2].Position += new Vector3(.25f, 0, .1f*((float)(MathHelper.TwoPi / 1.1) * -0.83f*(float)Math.Sin((MathHelper.TwoPi/1.1)* ((thisDay.Year + thisDay.DayOfYear / 365f) + 0.66f))
                    - (MathHelper.TwoPi / 11.99f) * 0.2f * (float)Math.Sin((MathHelper.TwoPi/11.99f)* ((thisDay.Year + thisDay.DayOfYear / 365f) - 2.8f)))); // relative to Mars
            }

            if (planets[2].Position.X > 4000)
            {
                jupiterZPosition = planets[2].Position.Z;
                planets[2].Position = new Vector3(-200000, 0, jupiterZPosition);
                isJupiterTooFar = true;
            }

            if (isJupiterTooFar)
            {
                timeJupiterIsOutofReach -= gametime.ElapsedGameTime.TotalSeconds;
            }

            if (timeJupiterIsOutofReach <= 0)
            {
                timeJupiterIsOutofReach = 20;
                planets[2].Position = new Vector3(-4000, 0, jupiterZPosition);
                isJupiterTooFar = false;
            }
            #endregion

            #region Saturn Orbit Code
            if (!isSaturnTooFar)
            {
                planets[3].Position += new Vector3(.21f, 0, .06f*((MathHelper.TwoPi / 1.096f) * (float)(-Math.Sin((MathHelper.TwoPi / 1.096f) * ((thisDay.Year + thisDay.DayOfYear / 365f) - 0.346f))
                    - 0.6f * (MathHelper.TwoPi / 30.07) * (float)Math.Sin((MathHelper.TwoPi / 30.07f) * ((thisDay.Year + thisDay.DayOfYear / 365f) - 2)))));
            }

            if (planets[3].Position.X > 5000) 
            {
                saturnZPosition = planets[3].Position.Z;
                planets[3].Position = new Vector3(-200000, 0, saturnZPosition);
                isSaturnTooFar = true;
            }

            if (isSaturnTooFar)
            {
                timeSaturnIsOutofReach -= gametime.ElapsedGameTime.TotalSeconds;
            }

            if (timeSaturnIsOutofReach <= 0)
            { 
                timeSaturnIsOutofReach = 30;
                planets[3].Position = new Vector3(-5000, 0, saturnZPosition);
                isSaturnTooFar = false;
            }
            #endregion
           
            #region Uranus Orbit Code
            if (!isUranusTooFar)
            {
                planets[4].Position += new Vector3(.14f, 0, .05f*(-(MathHelper.TwoPi / 1.01f) * (float)Math.Sin((MathHelper.TwoPi / 1.01) * ((thisDay.Year + thisDay.DayOfYear / 365f) - .34f))
                    - .3f * (MathHelper.TwoPi / 60.14f) * (float)Math.Sin((MathHelper.TwoPi / 60.14f) * ((thisDay.Year + thisDay.DayOfYear / 365f) + 30))));
            }

            if (planets[4].Position.X > 6500)
            {
                uranusZPosition = planets[4].Position.Z;
                planets[4].Position = new Vector3(-200000, 0, uranusZPosition);
                isUranusTooFar = true;
            }

            if (isUranusTooFar)
            {
                timeUranusIsOutofReach -= gametime.ElapsedGameTime.TotalSeconds;
            }

            if (timeUranusIsOutofReach <= 0)
            {
                timeUranusIsOutofReach = 45;
                planets[4].Position = new Vector3(-6500, 0, uranusZPosition);
                isUranusTooFar = false;
            }
            #endregion

            #region Neptune Orbit Code
            if (!isNeptuneTooFar)
            {
                planets[5].Position += new Vector3(.11f, 0, .03f*(-(MathHelper.TwoPi / 1.01f) * (float)Math.Sin((MathHelper.TwoPi / 1.01f) * ((thisDay.Year + thisDay.DayOfYear / 365f) - .17f))
                    - (MathHelper.TwoPi / 112.3f) * .8f * (float)Math.Sin((MathHelper.TwoPi / 112.3f) * ((thisDay.Year + thisDay.DayOfYear / 365f) + 15))));
            }

            if (planets[5].Position.X > 9000)
            {
                neptuneZPosition = planets[5].Position.Z;
                planets[5].Position = new Vector3(-200000, 0, neptuneZPosition);
                isNeptuneTooFar = true;
            }

            if (isNeptuneTooFar)
            {
                timeNeptuneIsOutofReach -= gametime.ElapsedGameTime.TotalSeconds;
            }

            if (timeNeptuneIsOutofReach <= 0)
            {
                timeNeptuneIsOutofReach = 60;
                planets[5].Position = new Vector3(-9000, 0, neptuneZPosition);
                isNeptuneTooFar = false;
            }

            #endregion
        }

        /// <summary>
        /// Every so and often, spawn an asteroid.
        /// </summary>
        void SpawnAsteroids(GameTime gametime)
        {
            minRectPositionX = 2500 * (float)Math.Sin(ship.Rotation) - 350;
            maxRectPositionX = 2500 * (float) Math.Sin(ship.Rotation) + 350;

            minRectPositionY = -200;
            maxRectPositionY = 200;

            minRectPositionZ = 2500 * (float)Math.Cos(ship.Rotation) - 350;
            maxRectPositionZ = 2500 * (float)Math.Cos(ship.Rotation) + 350;

            float asteroidX = rand.Next((int)minRectPositionX, (int)maxRectPositionX);
            float asteroidY = rand.Next((int)minRectPositionY, (int)maxRectPositionY);
            float asteroidZ = rand.Next((int)minRectPositionZ, (int)maxRectPositionZ);

            // After Saturn is reached
            float otherAstX = rand.Next((int)minRectPositionX, (int)maxRectPositionX);
            float otherAstY = rand.Next((int)minRectPositionY, (int)maxRectPositionY);
            float otherAstZ = rand.Next((int)minRectPositionZ, (int)maxRectPositionZ);

            Vector3 newAsteroidPosition;
            Vector3 otherNewAsteroidPosition;

            asteroidSpawnDelay -= gametime.ElapsedGameTime.TotalSeconds;
            if (asteroidSpawnDelay <= 0)
            {
                asteroidSpawnDelay = 3;

                if (planets[1].HasBeenReached)
                {
                    asteroidSpawnDelay = 1.5f;
                }
                if (planets[2].HasBeenReached)
                {
                    // asteroids are a lot less common beyond Jupiter
                    asteroidSpawnDelay = 4f;
                }
                if (planets[3].HasBeenReached)
                {
                    asteroidSpawnDelay = 2f;
                }
                if (planets[4].HasBeenReached)
                {
                    asteroidSpawnDelay = 2.5f;
                }

                newAsteroidPosition = new Vector3(asteroidX, asteroidY, asteroidZ) + ship.Position;
                asteroids.Add(new Asteroid(Content.Load<Model>("asteroid1"),
                    newAsteroidPosition));

                otherNewAsteroidPosition = new Vector3(otherAstX, otherAstY, otherAstZ) + ship.Position;
                if (nextPlanet == 5 || nextPlanet == 2)
                {
                    // The Kuiper belt begins about here.
                    asteroids.Add(new Asteroid(Content.Load<Model>("asteroid1"), otherNewAsteroidPosition));
                }
                
            }
        }

        public void UpdatePause()
        {
            GamePad.SetVibration(PlayerIndex.One, 0, 0);
            if (pad.Buttons.A == ButtonState.Pressed)
            {
                currentState = GameState.Game;
            }
            if (pad.Buttons.B == ButtonState.Pressed)
            {
                SaveInfo();
                currentState = GameState.StartMenu;
            }
        }

        public void RunGameOver()
        {
            GamePad.SetVibration(PlayerIndex.One, 0, 0);

            if (pad.Buttons.Start == ButtonState.Pressed)
            {
                ResetAllValues();       
                currentState = GameState.StartMenu;
            }
        }

        public void UpdateVictory()
        {
            GamePad.SetVibration(PlayerIndex.One, 0, 0);
            if (pad.Buttons.Start == ButtonState.Pressed)
            {
                ResetAllValues();
                currentState = GameState.StartMenu;
            }
        }

        void ResetAllValues()
        {
            ship.ResetValues();
            thisDay = DateTime.Today;
            nextPlanet = 1;
            foreach (Planet planet in planets)
            {
                planet.HasBeenReached = false;
            }
            asteroids.Clear();
            lasers.Clear();

            isMarsTooFar = false;
            isJupiterTooFar = false;
            isSaturnTooFar = false;
            isUranusTooFar = false;
            isNeptuneTooFar = false;

            timeMarsIsOutofReach = 10;
            timeJupiterIsOutofReach = 20;
            timeSaturnIsOutofReach = 30;
            timeUranusIsOutofReach = 45;
            timeNeptuneIsOutofReach = 60;

            score = 0;
            totalHours = 0;
            timeTakenToReachPlanet = 0;

            shootingCooldownValue = 100;
            projectileShootDelay = .2f;

            notificationDelay = 5;
            notificationAlpha = 1;
            notificationFadeDelay = .04f;

            hasPlayedBefore = false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            DepthStencilState depthStencilState = new DepthStencilState();
            depthStencilState.DepthBufferWriteEnable = true;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            switch (currentState)
            {
                case GameState.StartMenu:
                    foreach (Scrolling background in backgrounds)
                    {
                        background.Draw(spriteBatch);
                    }
                    spriteBatch.Draw(titleSprite, titleRect, Color.White);
                    // Check if pointer is hovering over button
                    if (mousePointerRect.Intersects(startButtonRect))
                    {
                        spriteBatch.Draw(startButtonClicked, startButtonRect, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(startButton, startButtonRect, Color.White);
                    }

                    if (mousePointerRect.Intersects(exitButtonRect))
                    {
                        spriteBatch.Draw(exitButtonClicked, exitButtonRect, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(exitButton, exitButtonRect, Color.White);
                    }
                    spriteBatch.Draw(nameSprite, nameRect, Color.White * nameAlphaValue);
                    spriteBatch.Draw(mousePointer, mousePointerRect, Color.White);

                    break;
                case GameState.Intro:

                    spriteBatch.DrawString(introFont, "How To Play", new Vector2(SCREENWIDTH / 2 - 80, 25), Color.MediumPurple * 1.5f);
                    spriteBatch.DrawString(introTextFont, "Welcome to the Expedition of the Cosmos, where your main goal is to explore as much as you\npossibly can!", new Vector2(80, 80), Color.White);
                    spriteBatch.DrawString(introTextFont, "Objective: ", new Vector2(80, 140), Color.MediumPurple * 1.5f);
                    spriteBatch.DrawString(introTextFont, "Get the highest score possible. The more asteroids you destroy, the "
                        + "higher your score will be. \nAlso, the faster you reach planets in the solar system, the more points you will receive.", new Vector2(80, 170), Color.White);
                    spriteBatch.DrawString(introTextFont, "Your Ship:", new Vector2(80, 230), Color.MediumPurple * 1.5f);
                    spriteBatch.DrawString(introTextFont, "Your health and your fuel are the two things you must pay attention to. If either of these\n" +
                        "run out, the game is over. You can get more fuel by shooting asteroids. You will lose health, \nand can lose fuel, by running into asteroids, and you can only repair your ship if it's going at\nhalf of maximum speed (28300 km/h) or less."
                        + " Move your ship using the right trigger, move up and \ndown using the left thumbstick, and use the start button to pause the game. Shoot by pressing A.", new Vector2(80, 260), Color.White);
                    spriteBatch.DrawString(introTextFont, "Planets:", new Vector2(80, 400), Color.MediumPurple * 1.5f);
                    spriteBatch.DrawString(introTextFont, "In this solar system, your goal will be to go to each planet from Mars to Neptune. All\n" +
                        "these planets will be in \"orbit\" (this orbit is more like a curvy line than an orbit), but\nthere's a point where if they get too far, they will" +
                        " disappear for a certain amount of time,\nthen reappear at the other side of their \"orbit\". Be mindful that each planet will get closer \nand further "
                        + "away depending on the time of year.", new Vector2(80, 430), Color.White);

                    // Check if pointer is hovering over button
                    if (mousePointerRect.Intersects(startButtonRect))
                    {
                        spriteBatch.Draw(startButtonClicked, startButtonRect, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(startButton, startButtonRect, Color.White);
                    }

                    spriteBatch.Draw(mousePointer, mousePointerRect, Color.White);

                    break;
                case GameState.Continue:
                    spriteBatch.DrawString(introFont, "Continue where you left off?", new Vector2(340, SCREENHEIGHT / 2 - 150), Color.White);
                    if (hasPlayedBefore)
                    {              
                        spriteBatch.DrawString(introFont, "Press 'Y' for Yes", new Vector2(590, SCREENHEIGHT / 2 - 50), Color.Goldenrod);
                        spriteBatch.DrawString(introFont, "Press 'X' for No", new Vector2(270, SCREENHEIGHT / 2 - 50), Color.MediumPurple);
                    }
                    else
                    {
                        spriteBatch.DrawString(introFont, "Press 'X' for No", new Vector2(430, SCREENHEIGHT / 2 - 50), Color.MediumPurple);
                    }
                    break;

                case GameState.Game:

                    RasterizerState originalRasterizeState = graphics.GraphicsDevice.RasterizerState;
                    RasterizerState rasterizerState = new RasterizerState();
                    rasterizerState.CullMode = CullMode.None;
                    graphics.GraphicsDevice.RasterizerState = rasterizerState;

                    skybox.Draw(ship.View, projection, ship.CameraPosition);

                    graphics.GraphicsDevice.RasterizerState = originalRasterizeState;

                    GraphicsDevice.BlendState = BlendState.AlphaBlend;
                    GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                    Matrix shipTransformMatrix = ship.RotationMatrix * Matrix.CreateTranslation(ship.Position);
                    ship.Draw(ship.Model, shipTransformMatrix, projection);

                    foreach (Projectile laser in lasers)
                    {
                        Matrix laserTransformMatrix = laser.RotationMatrix * Matrix.CreateTranslation(laser.Position);
                        laser.Draw(laserTransformMatrix, projection, ship.View);
                    }

                    for (int i = 0; i < planets.Length; i++)
                    {
                        Matrix planetTransformMatrix = planets[i].RotationMatrix * Matrix.CreateTranslation(planets[i].Position);
                        planets[i].Draw(planets[i].Model, planetTransformMatrix, projection, ship.View);
                    }

                    foreach (Asteroid asteroid in asteroids)
                    {
                        Matrix asteroidTransformMatrix = asteroid.RotationMatrix * Matrix.CreateTranslation(asteroid.Position);
                        asteroid.Draw(asteroidTransformMatrix, projection, ship.View);
                    }

                    //   spriteBatch.DrawString(gameFont, "X: " + ship.Position.X/90 + " " + "Y: " + ship.Position.Y / 90 + " " + "Z: " + ship.Position.Z / 90, new Vector2(30, 600), Color.White);
                    spriteBatch.DrawString(gameFont, "Score: " + (int)score, new Vector2(SCREENWIDTH - 250, 60), Color.White);
                    spriteBatch.DrawString(gameFont, "Speed: " + speed + " km/h", new Vector2(30, 60), Color.White);
                    spriteBatch.DrawString(gameFont, "Today's Date: " + thisDay.ToShortDateString(), new Vector2(30, 30), Color.White);
                    spriteBatch.DrawString(gameFont, "Ship Health: " + (int)ship.ShipHealth, new Vector2(SCREENWIDTH - 250, 30), shipHealthColorText);
                    string extraPointZero;
                    if (shipFuelPercentage % 1 == 0)
                    {
                        extraPointZero = ".0";
                    }
                    else
                    {
                        extraPointZero = "";
                    }

                    int fuelPercentageXCoord;
                    if (shipFuelPercentage >= 100)
                    {
                        fuelPercentageXCoord = 345;
                    }
                    else if (shipFuelPercentage >= 10)
                    {
                        fuelPercentageXCoord = 335;
                    }
                    else
                    {
                        fuelPercentageXCoord = 325;
                    }

                    spriteBatch.DrawString(gameFont, shipFuelPercentage + extraPointZero + "%", new Vector2(SCREENWIDTH - fuelPercentageXCoord, 125), fuelBarColor * 2);

                    spriteBatch.Draw(barSprite, new Rectangle(fuelBarRectangle.X - 3, fuelBarRectangle.Y - 3, 226, 26), Color.White * .35f);
                    spriteBatch.Draw(barSprite, fuelBarRectangle, fuelBarColor * 2);

                    spriteBatch.Draw(barSprite, new Rectangle(27, 127, 226, 26), Color.LightGoldenrodYellow * .5f);
                    spriteBatch.Draw(barSprite, new Rectangle(30, 130, shootingCooldownWidthofRect, 20), Color.LightGoldenrodYellow);

                    if (notificationDelay >= 0)
                    {
                        spriteBatch.DrawString(gameFont, "Get to planet " + planets[nextPlanet].PlanetName, new Vector2(SCREENWIDTH / 2 - 110, 130), Color.MediumPurple * 1.5f * notificationAlpha);
                        if (nextPlanet > 1)
                        {
                            spriteBatch.DrawString(gameFont, "You've made it to " + planets[nextPlanet - 1].PlanetName + " in " + timeTakenToReachPlanet + " hours", new Vector2(SCREENWIDTH / 2 - 235, 100), Color.MediumPurple * 1.5f * notificationAlpha);
                        }
                        if (nextPlanet == 2)
                        {
                            spriteBatch.DrawString(gameFont, "CAUTION: Entering the Asteroid Belt", new Vector2(SCREENWIDTH / 2 - 230, 600), Color.MediumPurple * 1.5f * notificationAlpha);
                        }
                        if (nextPlanet == 5)
                        {
                            spriteBatch.DrawString(gameFont, "CAUTION: The Kuiper Belt is Near", new Vector2(SCREENWIDTH / 2 - 230, 600), Color.MediumPurple * 1.5f * notificationAlpha);
                        }
                    }

                    if (ship.ShipHealth < 200)
                    {
                        if (healthWarningDelay >= 0)
                        {
                            spriteBatch.DrawString(gameFont, "You're health is low! \nSlow down for repair!", new Vector2(SCREENWIDTH / 2 - 140, 300), Color.Red * healthWarningAlpha);
                        }
                    }

                    spriteBatch.DrawString(gameFont, "Planets out of reach: ", new Vector2(70, 400), Color.PaleGoldenrod );
                    if (isMarsTooFar)
                    {
                        spriteBatch.DrawString(gameFont, "Mars " + (int)timeMarsIsOutofReach, new Vector2(80, 430), Color.Goldenrod);
                    }

                    if (isJupiterTooFar)
                    {
                        spriteBatch.DrawString(gameFont, "Jupiter " + (int)timeJupiterIsOutofReach, new Vector2(80, 460), Color.Goldenrod);
                    }

                    if (isSaturnTooFar)
                    {
                        spriteBatch.DrawString(gameFont, "Saturn " + (int)timeSaturnIsOutofReach, new Vector2(80, 490), Color.Goldenrod);
                    }

                    if (isUranusTooFar)
                    {
                        spriteBatch.DrawString(gameFont, "Uranus " + (int)timeUranusIsOutofReach, new Vector2(80, 520), Color.Goldenrod);
                    }

                    if (isNeptuneTooFar)
                    {
                        spriteBatch.DrawString(gameFont, "Neptune " + (int)timeNeptuneIsOutofReach, new Vector2(40, 550), Color.Goldenrod);
                    }
                    //  spriteBatch.DrawString(gameFont, "Total hours: " + totalHours, new Vector2(SCREENWIDTH / 2 +300, 500), Color.Wheat);

                    spriteBatch.DrawString(gameFont, "Travel Time: ", new Vector2(SCREENWIDTH - 250, 400), Color.White); 

                    if (planets[1].HasBeenReached)
                    {
                        spriteBatch.DrawString(gameFont, "Mars: " + timeTakenToReachMars + " days", new Vector2(SCREENWIDTH - 250, 430), Color.OrangeRed);
                    }
                    if (planets[2].HasBeenReached)
                    {
                        spriteBatch.DrawString(gameFont, "Jupiter: " + timeTakenToReachJupiter + " days", new Vector2(SCREENWIDTH - 250, 460), Color.Tan);
                    }
                    if (planets[3].HasBeenReached)
                    {
                        spriteBatch.DrawString(gameFont, "Saturn: " + timeTakenToReachSaturn + " days", new Vector2(SCREENWIDTH - 250, 490), Color.SandyBrown);
                    }
                    if (planets[4].HasBeenReached)
                    {
                        spriteBatch.DrawString(gameFont, "Uranus: " + timeTakenToReachUranus + " days", new Vector2(SCREENWIDTH - 250, 520), Color.CadetBlue);
                    }

                    break;
                case GameState.Pause:
                    spriteBatch.Draw(pauseSprite, pauseRect, Color.White);
                    spriteBatch.DrawString(introFont, "Press 'A' to continue", new Vector2(SCREENWIDTH/2-165, 300), Color.MediumPurple);
                    spriteBatch.DrawString(introFont, "Press 'B' to save and quit", new Vector2(SCREENWIDTH / 2 - 165, 350), Color.MediumPurple);

                    break;
                case GameState.Death:
                    if (ship.ShipHealth <= 0)
                    {
                        spriteBatch.DrawString(gameFont, "Your ship was destroyed, and you failed to \nbring your discoveries back to Earth. \n\nPress the start button to go to the main menu.",
                            new Vector2(260, SCREENHEIGHT/2-100), Color.White); 
                    } else if (ship.FuelCount <= 0)
                    {
                        spriteBatch.DrawString(gameFont, "Your ship ran out of fuel, and you got stranded \nin space. \n\nPress the start button to go to the main menu.",
                           new Vector2(260, SCREENHEIGHT / 2 - 100), Color.White);
                    }

                    spriteBatch.DrawString(gameFont, "Final Score: " + (int) score,
                           new Vector2(SCREENWIDTH/2-100, SCREENHEIGHT / 2 + 100), Color.White);
                    break;
                case GameState.Victory:
                    GraphicsDevice.Clear(Color.White);
                    
                    spriteBatch.DrawString(introFont, "Congratulations! You have reached every planet of the Solar System", new Vector2(50, 100), Color.Black);
                    spriteBatch.DrawString(introFont, "beyond Earth!", new Vector2(450, 130), Color.Black);

                    int totalTravelTimeInDays = timeTakenToReachMars + timeTakenToReachJupiter + timeTakenToReachSaturn + timeTakenToReachUranus + timeTakenToReachNeptune;
                    float totalTravelTimeInYears = totalTravelTimeInDays / 365f;
                    float totalTravelTimeInMonths = totalTravelTimeInDays / 30f;

                    spriteBatch.DrawString(introFont, "Total Travel Time: ", new Vector2(400, SCREENHEIGHT / 2 -50), Color.Black);

                    string yearWord = " Year";
                    string monthWord = " Month";
                    string dayWord = " Day";
                    if ((int)totalTravelTimeInYears != 1)
                    {
                        yearWord += "s";
                    }

                    if ((int)((totalTravelTimeInYears - (int)totalTravelTimeInYears) * 12) != 1)
                    {
                        monthWord += "s";
                    }

                    if ((int)((totalTravelTimeInMonths - (int)totalTravelTimeInMonths) * 30) != 1)
                    {
                        dayWord += "s";
                    }

                    spriteBatch.DrawString(introFont, (int)totalTravelTimeInYears + yearWord + " " + (int)((totalTravelTimeInYears- (int)totalTravelTimeInYears)*12) + monthWord + " "+
                        (int)((totalTravelTimeInMonths - (int)totalTravelTimeInMonths)*30) + dayWord, new Vector2(350, SCREENHEIGHT / 2), Color.Black);
                    spriteBatch.DrawString(introFont, "Final Score: " + (int)score, new Vector2(405, SCREENHEIGHT / 2 + 100), Color.Black);
                    spriteBatch.DrawString(introFont, "To play again, press the Start button.", new Vector2(250, 500), Color.Black);
                    break;
                case GameState.LoadSave:
                    LoadInfo();
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}