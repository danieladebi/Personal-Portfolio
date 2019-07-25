using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Coding4Fun.Kinect.Wpf;
using Microsoft.Kinect;


namespace MyKinectGame
{
    public class MyGame : Microsoft.Xna.Framework.Game
    {
        // This is a reference to your game and all of its data:
        public static MyGame instance;

        public static class Screen
        {
            public static int Width
            {
                get
                {
                    if (instance == null)
                        return 0;

                    return instance.GraphicsManager.PreferredBackBufferWidth;
                }
            }
            public static int Height
            {
                get
                {
                    if (instance == null)
                        return 0;

                    return instance.GraphicsManager.PreferredBackBufferHeight;
                }
            }
        }

        public static class World
        {
            public static int Width
            {
                get
                {
                    return 480;
                }
            }
            public static int Height
            {
                get
                {
                    return 360;
                }
            }
        }

        // The amount of time that has passed since the last Update() call:
        public static float UpdateDelta = 0f;

        // The amount of time that has passed since the last Draw() call:
        public static float DrawDelta = 0f;

        // Determines if the skeleton data will be interpolated from the camera to provide smoother results:
        // Default 15
        // 0 = No Smoothing (Faster Performance)
        // 1 = Slow
        // 100 = Jittery
        public static float Smoothing = 15f;

        // These strings are displayed as debug messages in the top-left corner of the screen:
        public static string Debug_Camera = "";
        public static string Debug_Skeleton = "";
        public static string Debug_State = "";

        // This value determines if the Debug Messages and Skeleton outline will be displayed onscreen:
        public static bool ShowDebug = false;

        public static bool SkeletonActive = false;

        /**********************************************************************************************/
        // Example GameState System:

        public void SetupGameStates()
        {
            GameState.Add("titlescreen", OnUpdate_TitleScreen);
            GameState.Add("surgeryChoiceScreen", OnUpdate_SurgeryChoice);
            GameState.Add("orthopedicMain", OnUpdate_Orthopedic);
            GameState.Add("plasticMain", OnUpdate_Plastic);
            GameState.Add("cancerMain", OnUpdate_Cancer);
            GameState.Add("pauseScreen", OnUpdate_Pause);
            GameState.Add("endscreen", OnUpdate_Endscreen);

            // Set the initial GameState:
            GameState.Set("plasticMain");
        }

        // GameState Handlers:


        Color defaultColor = Color.Black;
        Color titleTextColor = new Color(74, 100, 130);

        #region Title Screen Image Sizes
        int titlePictureWidth = 510;
        int titlePictureHeight = 340;

        int titlePicture2Width = 230;
        int titlePicture2Height = 340;

        int startButtonWidth = 150;
        int startButtonHeight = 60;
        #endregion

        Vector3 handLeftPosition;
        Vector3 handRightPosition;
   
        int handWidth = 50;
        int handHeight = 100;

        float elapsedGameTime = 0;
        float timeHoveringOverButton = 0;

        #region Surgery Choice Screen
        int orthopedicImageWidth = 180;
        int orthopedicImageHeight = 180;

        int plasticImageWidth = 225;
        int plasticImageHeight = 180;

        int cancerImageWidth = 135;
        int cancerImageHeight = 180;

        float orthopedicImageHoverTime = 0;
        float plasticImageHoverTime = 0;
        float cancerImageHoverTime = 0;

        List<Point> incisionPoints = new List<Point>();
        List<Point> stitchPoints = new List<Point>();
        #endregion

        #region Orthopedic Info
        float sinkHoverTimer = 0;
        float timeWashingHands = 0;
        float handGloveTimer = 0;
        float timeLeftHandOverSyringe = 0;
        float timeRightHandOverSyringe = 0;
        float timeLeftHandOverKnife = 0;
        float timeRightHandOverKnife = 0;

        int rectangleLoaderWidth = 20;
        int rectangleLoaderLength = 80;

        int handWashLoaderWidth = 325;
        int handWashLoaderLength = 30;

        int step = 2;
        bool hasLeftGlove = false;
        bool hasRightGlove = false;
        bool hasGloves = false;
        bool hasWashedHands = false;

        int armWidth = 650;
        int armLength = 320;

        bool syringePickedUpByLeftHand = false;
        bool syringePickedUpByRightHand = false;

        bool knifePickedUpByLeftHand = false;
        bool knifePickedUpByRightHand = false;

        bool suturePickedUpByLeftHand = false;
        bool suturePickedUpByRightHand = false;

        float previousZHandPosition = 0;

        int syringeAmount = 80;

        float delayGoingToCaseTwo = 0;
        float delayGoingToCaseThree = 0;
        float delayGoingToCaseFour = 0;
        float delayGoingToCaseFive = 0;
        float delayGoingToMainMenu = 0;

        bool leftHandPickedUpBone1 = false;
        bool leftHandPickedUpBone2 = false;
        bool rightHandPickedUpBone1 = false;
        bool rightHandPickedUpBone2 = false;

        bool bone1InCorrectPosition = false;
        bool bone2InCorrectPosition = false; 

        float timeLeftHandOverBone1 = 0;
        float timeLeftHandOverBone2 = 0;
        float timeRightHandOverBone1 = 0;
        float timeRightHandOverBone2 = 0;

        float timeLeftHandOverSuture = 0;
        float timeRightHandOverSuture = 0;
        #endregion

        #region Plastic Info

        #endregion

        public void OnUpdate_TitleScreen()
        {
            GraphicsDevice.Clear(Color.Wheat);

            elapsedGameTime += DrawDelta;

            Renderer.DrawString(Resources.Fonts.Load("Title Screen"), "Surgery for Dummies", new Vector2(Screen.Width / 2 - 175, 40), titleTextColor);
            Renderer.DrawString(Resources.Fonts.Load("font_20"), "by Daniel Adebi", new Vector2(Screen.Width / 2 - 75, 100), titleTextColor);
            Renderer.Draw(Resources.Images.Load("surgeryTitleScreen"), new Rectangle(Screen.Width / 2 - titlePictureWidth / 2, Screen.Height / 2 - titlePictureHeight / 2 + 20, titlePictureWidth, titlePictureHeight), Color.White);
            Renderer.Draw(Resources.Images.Load("titleScreenSurgery2"), new Rectangle(60, 175, titlePicture2Width, titlePicture2Height), Color.White);
            Renderer.Draw(Resources.Images.Load("titleScreenSurgery3"), new Rectangle(Screen.Width - 300, 175, titlePicture2Width, titlePicture2Height), Color.White);

            Rectangle startButtonRect = new Rectangle(Screen.Width / 2 - startButtonWidth / 2, Screen.Height - 85, startButtonWidth, startButtonHeight);
            Renderer.Draw(Resources.Images.Load("startButton"), startButtonRect, Color.White);

            handLeftPosition = GetJointPosition(JointType.HandLeft, ScreenSpace.Screen);
            handRightPosition = GetJointPosition(JointType.HandRight, ScreenSpace.Screen);
            
            Rectangle handCursorRect = new Rectangle((int)handRightPosition.X, (int)handRightPosition.Y, handWidth, handHeight);

            if (SkeletonActive)
            {
                //   Renderer.Draw(Resources.Images.Load("handGloveLeft"), new Rectangle((int)handLeftPosition.X, (int)handLeftPosition.Y, handWidth, handHeight), Color.White);

                if (handCursorRect.Intersects(startButtonRect))
                {

                    if (elapsedGameTime - timeHoveringOverButton > 2f)
                    {
                        Renderer.Draw(Resources.Images.Load("startButton_2"), startButtonRect, Color.White);
                        if (elapsedGameTime - timeHoveringOverButton > 2.5f)
                        {
                            elapsedGameTime = 0;
                            GameState.Set("surgeryChoiceScreen");
                        }
                    }
                    else
                    {
                        Renderer.Draw(Resources.Images.Load("startButton_1"), startButtonRect, Color.White);
                    }
                }
                else
                {
                    Renderer.Draw(Resources.Images.Load("startButton"), startButtonRect, Color.White);
                    timeHoveringOverButton = elapsedGameTime;
                }
                Renderer.Draw(Resources.Images.Load("handRight"), new Rectangle((int)handRightPosition.X, (int)handRightPosition.Y, handWidth, handHeight), Color.White);

            }
        }

        public void OnUpdate_SurgeryChoice()
        {
            //if (GetJointPosition(JointType.HandLeft, ScreenSpace.World).Y 
            //    < GetJointPosition(JointType.Head, ScreenSpace.World).Y && GetJointPosition(JointType.HandRight, ScreenSpace.World).Y
            //    > GetJointPosition(JointType.Head, ScreenSpace.World).Y)
            //{
            //    GameState.Set("pauseScreen");
            //}
          
            GraphicsDevice.Clear(new Color(80, 150, 120));

            handLeftPosition = GetJointPosition(JointType.HandLeft, ScreenSpace.Screen);
            handRightPosition = GetJointPosition(JointType.HandRight, ScreenSpace.Screen);

            Rectangle handCursorLeftRect = new Rectangle((int)handLeftPosition.X, (int)handLeftPosition.Y, handWidth, handHeight);
            Rectangle handCursorRightRect = new Rectangle((int)handRightPosition.X, (int)handRightPosition.Y, handWidth, handHeight);

            Renderer.DrawString(Resources.Fonts.Load("font_20"), "Welcome! This program is meant to be a surgery learning tool.", new Vector2(250, 30), Color.White);
            Renderer.DrawString(Resources.Fonts.Load("font_20"), "With this tool, you can learn how different types of surgeries are performed.", new Vector2(180, 60), Color.White);
            Renderer.DrawString(Resources.Fonts.Load("font_20"), "Pick which surgery you want to learn about below by placing both hands on the picture.", new Vector2(130, 90), Color.White);

            Rectangle orthopedicRect = new Rectangle(150, 250, orthopedicImageWidth, orthopedicImageHeight);
            Rectangle plasticRect = new Rectangle(Screen.Width / 2 - plasticImageWidth / 2, 250, plasticImageWidth, plasticImageHeight);
            Rectangle cancerRect = new Rectangle(850, 250, cancerImageWidth, cancerImageHeight);

            Renderer.DrawString(Resources.Fonts.Load("font_copy_default"), "Orthopedic Surgery", new Vector2(140, 440), Color.White);
            Renderer.DrawString(Resources.Fonts.Load("font_copy_default"), "Plastic Surgery", new Vector2(520, 440), Color.White);
            Renderer.DrawString(Resources.Fonts.Load("font_copy_default"), "Cancer Surgery", new Vector2(840, 440), Color.White);

            Renderer.Draw(Resources.Images.Load("OrthopedicSurgeryLogo"), orthopedicRect, Color.White);
            Renderer.Draw(Resources.Images.Load("PlasticSurgeryLogo"), plasticRect, Color.White);
            Renderer.Draw(Resources.Images.Load("CancerSurgeryLogo"), cancerRect, Color.White);
   
            // Will only display and/or do certain actions if the skeleton is detected
            if (SkeletonActive)
            {
                elapsedGameTime += DrawDelta;

                #region Orthopedic Selection
                if (handCursorLeftRect.Intersects(orthopedicRect) || handCursorRightRect.Intersects(orthopedicRect))
                {
                    orthopedicRect.Width += 20;
                    orthopedicRect.Height += 20;
                    orthopedicRect.X -= 10;
                    orthopedicRect.Y -= 10;
                    if (handCursorLeftRect.Intersects(orthopedicRect) && handCursorRightRect.Intersects(orthopedicRect))
                    {
                        if (elapsedGameTime - orthopedicImageHoverTime > 2)
                        {
                            Renderer.Draw(Resources.Images.Load("OrthopedicSurgeryLogo2"), orthopedicRect, Color.White);
                            if (elapsedGameTime - orthopedicImageHoverTime > 2.5f)
                            {
                                elapsedGameTime = 0;
                                GameState.Set("orthopedicMain");
                            }
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("OrthopedicSurgeryLogo"), orthopedicRect, Color.White);
                        }
                    }
                    else
                    {
                        orthopedicImageHoverTime = elapsedGameTime;
                        Renderer.Draw(Resources.Images.Load("OrthopedicSurgeryLogo"), orthopedicRect, Color.White);
                    }

                }
                #endregion

                #region Plastic Selection
                if (handCursorLeftRect.Intersects(plasticRect) || handCursorRightRect.Intersects(plasticRect))
                {
                    plasticRect.Width += 20;
                    plasticRect.Height += 20;
                    plasticRect.X -= 10;
                    plasticRect.Y -= 10;
                    if (handCursorLeftRect.Intersects(plasticRect) && handCursorRightRect.Intersects(plasticRect))
                    {
                        if (elapsedGameTime - plasticImageHoverTime > 2)
                        {
                            Renderer.Draw(Resources.Images.Load("PlasticSurgeryLogo2"), plasticRect, Color.White);
                            if (elapsedGameTime - plasticImageHoverTime > 2.5f)
                            {
                                elapsedGameTime = 0;
                                GameState.Set("plasticMain");
                            }                          
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("PlasticSurgeryLogo"), plasticRect, Color.White);
                        }
                    }
                    else
                    {
                        plasticImageHoverTime = elapsedGameTime;
                        Renderer.Draw(Resources.Images.Load("PlasticSurgeryLogo"), plasticRect, Color.White);
                    }

                }
                #endregion

                #region Cancer Selection
                if (handCursorLeftRect.Intersects(cancerRect) || handCursorRightRect.Intersects(cancerRect))
                {
                    cancerRect.Width += 20;
                    cancerRect.Height += 20;
                    cancerRect.X -= 10;
                    cancerRect.Y -= 10;
                    if (handCursorLeftRect.Intersects(cancerRect) && handCursorRightRect.Intersects(cancerRect))
                    {
                        if (elapsedGameTime - cancerImageHoverTime > 2)
                        {
                            Renderer.Draw(Resources.Images.Load("CancerSurgeryLogo2"), cancerRect, Color.White);
                            if (elapsedGameTime - cancerImageHoverTime > 2.5f)
                            {
                                elapsedGameTime = 0;
                                GameState.Set("cancerMain");
                            }
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("CancerSurgeryLogo"), cancerRect, Color.White);
                        }
                    }
                    else
                    {
                        cancerImageHoverTime = elapsedGameTime;
                        Renderer.Draw(Resources.Images.Load("CancerSurgeryLogo"), cancerRect, Color.White);
                    }

                }
                #endregion
                 
                Renderer.Draw(Resources.Images.Load("handLeft"), handCursorLeftRect, Color.White);
                Renderer.Draw(Resources.Images.Load("handRight"), handCursorRightRect, Color.White);
            }
        }

        bool reachedIncisionLocation1 = false;
        bool reachedIncisionLocation2 = false;
        bool reachedIncisionLocation3 = false;
        bool reachedIncisionLocation4 = false;
        bool reachedIncisionLocation5 = false;

        bool reachedStitchLocation1 = false;
        bool reachedStitchLocation2 = false;
        bool reachedStitchLocation3 = false;
        bool reachedStitchLocation4 = false;
        bool reachedStitchLocation5 = false;

        /// <summary>
        /// Orthopedic Code Logic
        /// </summary>
        public void OnUpdate_Orthopedic()
        {
            GraphicsDevice.Clear(Color.Azure);
            Color orthopedicTextColor = new Color(40, 80, 80);

            elapsedGameTime += DrawDelta;

            Rectangle sinkRectangle = new Rectangle(Screen.Width/2-175, 200, 325, 290);

            handLeftPosition = GetJointPosition(JointType.HandLeft, ScreenSpace.Screen);
            handRightPosition = GetJointPosition(JointType.HandRight, ScreenSpace.Screen);

            Rectangle handCursorLeftRect = new Rectangle((int)handLeftPosition.X, (int)handLeftPosition.Y, handWidth, handHeight);
            Rectangle handCursorRightRect = new Rectangle((int)handRightPosition.X, (int)handRightPosition.Y, handWidth, handHeight);

            Rectangle rectangleLoaderRect = new Rectangle(handCursorRightRect.X + 80, handCursorRightRect.Y, rectangleLoaderWidth, rectangleLoaderLength);

            Rectangle syringeRectangle = new Rectangle(100, 200, 40, 160);

            Rectangle armRectangle = new Rectangle(300, 170, armWidth, armLength);

            Rectangle surgeryKnifeRect = new Rectangle(70, 270, 120, 100);
            Rectangle guideLineRect = new Rectangle(600, 360, 200, 45);

            Rectangle bonePieceOneRect = new Rectangle(605, 340, 110, 40);
            Rectangle bonePieceTwoRect = new Rectangle(705, 370, 100, 45);

            Rectangle stitchMark1 = new Rectangle(605, 385, 15, 15);
            Rectangle stitchMark2 = new Rectangle(655, 335, 15, 15);
            Rectangle stitchMark3 = new Rectangle(695, 410, 15, 15);
            Rectangle stitchMark4 = new Rectangle(735, 345, 15, 15);
            Rectangle stitchMark5 = new Rectangle(775, 410, 15, 15);

            Rectangle sutureRect = new Rectangle(100, 250, 80, 120);

            switch (step) {

                #region O - Step 1
                case 0:
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Orthopedic Surgeries are some of the most common types of surgeries that are performed.", new Vector2(100, 50), orthopedicTextColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "The example you will be learning about is how to repair a bone fracture in the arm.", new Vector2(130, 80), orthopedicTextColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "But before we begin, you must be sure you're as clean as possible.", new Vector2(200, 110), orthopedicTextColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "So reach out and wash your hands, wear your gloves, then we can begin.", new Vector2(180, 140), orthopedicTextColor);

                    if (handCursorLeftRect.Intersects(sinkRectangle) && handCursorRightRect.Intersects(sinkRectangle) &&
                        Math.Abs(GetJointPosition(JointType.Spine, ScreenSpace.Screen).Z - handLeftPosition.Z) < 1.1f &&
                          Math.Abs(GetJointPosition(JointType.Spine, ScreenSpace.Screen).Z - handRightPosition.Z) < 1.1f)
                    {
                        if (elapsedGameTime - sinkHoverTimer > .5f)
                        {
                            Renderer.Draw(Resources.Images.Load("SinkRunning"), sinkRectangle, Color.White);
                            timeWashingHands += DrawDelta;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("SinkNormal"), sinkRectangle, Color.White);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(rectangleLoaderRect.X - 5, rectangleLoaderRect.Y - 5,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Blue);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(rectangleLoaderRect.X, rectangleLoaderRect.Y, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - sinkHoverTimer)*2))), Color.White);

                        }
                        if (timeWashingHands > 5)
                        {
                            hasWashedHands = true;
                        } else if (timeWashingHands < 5 && elapsedGameTime - sinkHoverTimer > 1)
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(Screen.Width / 2 - 185, 520, handWashLoaderWidth + 10, handWashLoaderLength + 10), Color.DarkCyan);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(Screen.Width / 2 - 180, 525, (int)(handWashLoaderWidth * (timeWashingHands / 5)), handWashLoaderLength), Color.White);
                        }
                    }
                    else {
                        sinkHoverTimer = elapsedGameTime;
                        Renderer.Draw(Resources.Images.Load("SinkNormal"), sinkRectangle, Color.White);
                    }

                    Rectangle staticGloveLeftRect = new Rectangle(200, 300, handWidth, handHeight);
                    Rectangle staticGloveRightRect = new Rectangle(Screen.Width - 250, 300, handWidth, handHeight);

                    if (hasWashedHands)
                    {
                        Renderer.DrawString(Resources.Fonts.Load("font_20"), "Hands are clean", new Vector2(500, 525), Color.Black);
                        Renderer.DrawString(Resources.Fonts.Load("font_20"), "Now you may wear your gloves.", new Vector2(430, 550), Color.Black);

                        if (handCursorLeftRect.Intersects(staticGloveLeftRect)) {
                            hasLeftGlove = true;
                        }
                        if (handCursorRightRect.Intersects(staticGloveRightRect))
                        {
                            hasRightGlove = true;
                        }

                        if (hasRightGlove && hasLeftGlove)
                        {
                            hasGloves = true;
                        }
                    }

                    if (hasGloves) {
                        if (elapsedGameTime - handGloveTimer > .5f)
                        {
                            step = 1;
                            elapsedGameTime = 0;
                        }
                    }
                    else
                    {
                        if (!hasLeftGlove)
                            Renderer.Draw(Resources.Images.Load("handGloveLeft"), staticGloveLeftRect, Color.White);
                        if (!hasRightGlove)
                            Renderer.Draw(Resources.Images.Load("handGloveRight"), staticGloveRightRect, Color.White);
                        handGloveTimer = elapsedGameTime;
                    }

                    break;
#endregion

                #region O - Step 2
                case 1:
                    Renderer.Draw(Resources.Images.Load("armInjured"), armRectangle, Color.White);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "The first step is to provide anesthesia for the patient.", new Vector2(300, 30), orthopedicTextColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Pick up the syringe by hovering over it until the bar is full.", new Vector2(270, 60), orthopedicTextColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Inject by moving your hand forward over the arm.", new Vector2(320, 90), orthopedicTextColor);

                    bool hasPainkillerBeenInjected = false;

                    // Rectangle loader will be used regardless of which hand had picked it up.
                    // Timers reset if player stops hovering over item.
                    // left hand logic

                    if (handCursorLeftRect.Intersects(syringeRectangle) && !syringePickedUpByRightHand && !syringePickedUpByLeftHand)
                    {
                        timeRightHandOverSyringe = elapsedGameTime;
                        if (elapsedGameTime - timeLeftHandOverSyringe > 1f)
                        {
                            syringePickedUpByLeftHand = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 50, syringeRectangle.Y + 25,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Blue);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X+55, syringeRectangle.Y+30, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeLeftHandOverSyringe)))), Color.White);
                        }
                     }
                    // right hand logic
                    else if (handCursorRightRect.Intersects(syringeRectangle) && !syringePickedUpByRightHand && !syringePickedUpByLeftHand) {
                        timeLeftHandOverSyringe = elapsedGameTime;
                        if (elapsedGameTime - timeRightHandOverSyringe > 1f)
                        {
                            syringePickedUpByRightHand = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 50, syringeRectangle.Y + 25,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Blue);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 55, syringeRectangle.Y + 30, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeRightHandOverSyringe)))), Color.White);
                        }
                    }
                     else
                     {
                          timeLeftHandOverSyringe = elapsedGameTime;
                          timeRightHandOverSyringe = elapsedGameTime;
                     }

                    // follows left hand position
                    // if syringe is picked up, when hand is hovering over arm and moves
                    // forward a certain amount (.25 m), the anesthesia will be injected.
                    // Renderer.DrawString(Resources.Fonts.Load("font_20"), handLeftPosition.Z + " " + previousZHandPosition, new Vector2(0, 300), Color.Black);
                    if (syringePickedUpByLeftHand)
                    {
                        syringeRectangle.X = (int)handLeftPosition.X;
                        syringeRectangle.Y = (int)handLeftPosition.Y;
                       
                        if (syringeRectangle.Intersects(armRectangle))
                        {                                                     
                            if (previousZHandPosition - handLeftPosition.Z > .25f)
                            {
                                Renderer.DrawString(Resources.Fonts.Load("font_20"), "Anesthesia Has Been Injected", new Vector2(450, 500), defaultColor);
                                hasPainkillerBeenInjected = true;
                            }
                            else
                            {
                                Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 50, syringeRectangle.Y + 25,
                                    rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Coral);
                                if ((previousZHandPosition - handLeftPosition.Z) / .25f * 80 < 80)
                                {
                                    syringeAmount = (int)((previousZHandPosition - handLeftPosition.Z ) / .25f * 80);
                                } else if ((previousZHandPosition - handLeftPosition.Z) / .25f * 80 < 0)
                                {
                                    syringeAmount = 0;
                                }
                                else
                                {
                                    syringeAmount = 80;
                                }
                                Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 55, syringeRectangle.Y + 30, rectangleLoaderWidth,
                                    (syringeAmount)), Color.White);

                            }         
                        }
                        else
                        {
                            previousZHandPosition = handLeftPosition.Z;
  
                        }

                    }
                    // same thing for right hand
                    else if (syringePickedUpByRightHand)
                    {
                        syringeRectangle.X = (int)handRightPosition.X;
                        syringeRectangle.Y = (int)handRightPosition.Y;

                        if (syringeRectangle.Intersects(armRectangle))
                        {
                            if (previousZHandPosition - handRightPosition.Z > .2f)
                            {
                                Renderer.DrawString(Resources.Fonts.Load("font_20"), "Anesthesia Has Been Injected", new Vector2(450, 500), defaultColor);
                                hasPainkillerBeenInjected = true;
                                
                            }
                            else
                            {
                                Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 50, syringeRectangle.Y + 25,
                                    rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Coral);
                                if ((previousZHandPosition - handRightPosition.Z) / .2f * 80 < 80)
                                {
                                    syringeAmount = (int)((previousZHandPosition - handRightPosition.Z) / .2f * 80);
                                }
                                else if ((previousZHandPosition - handRightPosition.Z) / .2f * 80 < 0)
                                {
                                    syringeAmount = 0;
                                }
                                else
                                {
                                    syringeAmount = 80;
                                }
                                Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 55, syringeRectangle.Y + 30, rectangleLoaderWidth,
                                    (syringeAmount)), Color.White);

                            }
                        }
                        else
                        {
                            previousZHandPosition = handRightPosition.Z;

                        }
                    }

                    if (hasPainkillerBeenInjected)
                    {
                        if (elapsedGameTime - delayGoingToCaseTwo > .5f)
                        {
                            step = 2;
                            elapsedGameTime = 0;
                        }
                    } else
                    {
                        delayGoingToCaseTwo = elapsedGameTime;
                    }

                    Renderer.Draw(Resources.Images.Load("painkiller"), syringeRectangle, Color.White);

                    break;
                #endregion

                #region O - Step 3
                case 2:
                    Renderer.Draw(Resources.Images.Load("armInjured"), armRectangle, Color.White);

                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Now that the anesthesia has been injected, we can now make our incisions", new Vector2(200, 30), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "to reduce this fracture. Pick up the incision knife with the same method", new Vector2(220, 60), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "you used to pick up the syringe.", new Vector2(450, 90), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "NOTE: this will be how you pick up items for the entirety of this program.", new Vector2(200, 120), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Then, move the hand holding the knife back near your chest to continue.", new Vector2(200, 520), defaultColor);

                    // If the knife is picked up by either hand, follow this logic. 
                    // Otherwise, reset timers as necessary.
                    if (handCursorLeftRect.Intersects(surgeryKnifeRect) && !knifePickedUpByLeftHand && !knifePickedUpByRightHand)
                    {
                        timeRightHandOverKnife = elapsedGameTime;
                        if (elapsedGameTime - timeLeftHandOverKnife > 1f) 
                        {
                            knifePickedUpByLeftHand = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(surgeryKnifeRect.X + 135, surgeryKnifeRect.Y + 10,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Gray);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(surgeryKnifeRect.X + 140, surgeryKnifeRect.Y + 15, rectangleLoaderWidth,
                              (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeLeftHandOverKnife)))), Color.White);
                        }
                    }
                    // right hand logic
                    else if (handCursorRightRect.Intersects(surgeryKnifeRect) && !knifePickedUpByRightHand && !knifePickedUpByLeftHand)
                    {
                        timeLeftHandOverKnife = elapsedGameTime;
                        if (elapsedGameTime - timeRightHandOverKnife > 1f)
                        {
                            knifePickedUpByRightHand = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(surgeryKnifeRect.X + 135, surgeryKnifeRect.Y + 10,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Gray);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(surgeryKnifeRect.X + 140, surgeryKnifeRect.Y + 15, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeRightHandOverKnife)))), Color.White);
                        }
                    }
                    else
                    {
                        timeLeftHandOverKnife = elapsedGameTime;
                        timeRightHandOverKnife = elapsedGameTime;
                    }

                    // If knife is picked up by either hand, follow the position of the corresponding hand.
                    if (knifePickedUpByLeftHand)
                    {
                        surgeryKnifeRect.X = (int)handLeftPosition.X-40;
                        surgeryKnifeRect.Y = (int)handLeftPosition.Y;

                        if (Math.Abs(handLeftPosition.Z - GetJointPosition(JointType.ShoulderCenter, ScreenSpace.Screen).Z) < 0.3f)
                        {
                            if (elapsedGameTime - delayGoingToCaseThree > 1f)
                            {
                                elapsedGameTime = 0;
                                step = 3;
                            }
                            else
                            {
                                Renderer.DrawString(Resources.Fonts.Load("font_20"), "Syncing. Please don't move.", new Vector2(50, 440), Color.Blue);
                            }
                        } else
                        {
                            delayGoingToCaseThree = elapsedGameTime;
                        }
                    }
                    else if (knifePickedUpByRightHand)
                    {
                        surgeryKnifeRect.X = (int)handRightPosition.X-40;
                        surgeryKnifeRect.Y = (int)handRightPosition.Y;

                        if (Math.Abs(handRightPosition.Z - GetJointPosition(JointType.ShoulderCenter, ScreenSpace.Screen).Z) < 0.3f)
                        {
                            if (elapsedGameTime - delayGoingToCaseThree > 1f)
                            {
                                elapsedGameTime = 0;
                                step = 3;
                            }
                            else {
                                Renderer.DrawString(Resources.Fonts.Load("font_20"), "Syncing. Please don't move", new Vector2(50, 440), Color.Blue);
                            }
                        } else
                        {
                            delayGoingToCaseThree = elapsedGameTime;
                        }
                    }

                    Renderer.Draw(Resources.Images.Load("surgicalKnife"), surgeryKnifeRect, Color.White);

                    break;
                #endregion

                #region O - Step 4
                case 3:
                    Renderer.Draw(Resources.Images.Load("armInjured"), armRectangle, Color.White);

                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Now, we can make an incision in the injured area.", new Vector2(250, 30), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Holding the knife as shown in the image on the right,", new Vector2(230, 60), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "make an incision across the line below.", new Vector2(300, 90), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Move your hand forward and follow the line on the arm.", new Vector2(200, 120), defaultColor);

                    Renderer.Draw(Resources.Images.Load("SurgicalKnifeInstruction"), new Rectangle(850, 30, 225, 140), Color.LightBlue);
                    Renderer.Draw(Resources.Images.Load("guideLine"), new Rectangle(600, 360, 200, 45), Color.Brown);

                    // For both left and right hand, user can make an incision on the arm by moving their hand forward 
                    // and cutting along the guideline.
                    if (knifePickedUpByLeftHand)
                    {
                        surgeryKnifeRect.X = (int)handLeftPosition.X - 40;
                        surgeryKnifeRect.Y = (int)handLeftPosition.Y;
                        Rectangle knifeBladeRectDetection = new Rectangle(surgeryKnifeRect.X + 5, surgeryKnifeRect.Y + 90, 25, 20);
                        Renderer.Draw(Resources.Images.Load("knifeDetectionRect"), knifeBladeRectDetection, Color.Black*0);

                        if (!(reachedIncisionLocation1 &&
                         reachedIncisionLocation2 &&
                         reachedIncisionLocation3 &&
                         reachedIncisionLocation4 &&
                         reachedIncisionLocation5))
                        {
                            if (GetJointPosition(JointType.ShoulderLeft, ScreenSpace.Screen).Z - handLeftPosition.Z > .5f)
                            {
                                incisionPoints.Add(new Point(knifeBladeRectDetection.X, knifeBladeRectDetection.Y));
                                if (!guideLineRect.Contains(new Point(knifeBladeRectDetection.X, knifeBladeRectDetection.Y)))
                                {
                                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Follow the guideline!", new Vector2(100, 450), defaultColor);
                                }

                            }
                        }
                    }
                    else if (knifePickedUpByRightHand)
                    {
                        surgeryKnifeRect.X = (int)handRightPosition.X - 40;
                        surgeryKnifeRect.Y = (int)handRightPosition.Y;
                        Rectangle knifeBladeRectDetection = new Rectangle(surgeryKnifeRect.X + 5, surgeryKnifeRect.Y + 90, 25, 20);
                        Renderer.Draw(Resources.Images.Load("knifeDetectionRect"), knifeBladeRectDetection, Color.Black*0);

                        if (!(reachedIncisionLocation1 &&
                       reachedIncisionLocation2 &&
                       reachedIncisionLocation3 &&
                       reachedIncisionLocation4 &&
                       reachedIncisionLocation5))
                        {
                            if (GetJointPosition(JointType.ShoulderRight, ScreenSpace.Screen).Z - handRightPosition.Z > .5f)
                            {
                                incisionPoints.Add(new Point(knifeBladeRectDetection.X, knifeBladeRectDetection.Y));
                                if (!guideLineRect.Contains(new Point(knifeBladeRectDetection.X, knifeBladeRectDetection.Y)))
                                {
                                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Follow the guideline!", new Vector2(100, 450), defaultColor);
                                }
                            }
                        }
                    }

                    for (int i = 0; i < incisionPoints.Count; i++)
                    {
                        if (armRectangle.Contains(incisionPoints[i]))
                        {
                            Renderer.Draw(Resources.Images.Load("Ink"), new Rectangle(incisionPoints[i].X, incisionPoints[i].Y, 8, 8), Color.Red);
                        }
                        Rectangle knifeBladeRectDetection = new Rectangle(surgeryKnifeRect.X + 5, surgeryKnifeRect.Y + 90, 25, 20);

                        if (!(reachedIncisionLocation1 &&
                        reachedIncisionLocation2 &&
                        reachedIncisionLocation3 &&
                        reachedIncisionLocation4 &&
                        reachedIncisionLocation5))
                        {
                            // If user makes incisions on each of the five locations, the incision will be considered successful.
                            if (guideLineRect.Contains(new Point(knifeBladeRectDetection.X, knifeBladeRectDetection.Y)))
                            {
                                if (incisionPoints[i].X < guideLineRect.X + 20 && incisionPoints[i].Y < guideLineRect.Y + 20)
                                {
                                   // Renderer.DrawString(Resources.Fonts.Load("font_20"), "Is in Range 1", new Vector2(1000, 400), defaultColor);
                                    reachedIncisionLocation1 = true;
                                }
                                if (incisionPoints[i].X > guideLineRect.X + 40 && incisionPoints[i].X < guideLineRect.X + 60)
                                {
                                   // Renderer.DrawString(Resources.Fonts.Load("font_20"), "Is in Range 2", new Vector2(1000, 430), defaultColor);
                                    reachedIncisionLocation2 = true;
                                }
                                if (incisionPoints[i].X > guideLineRect.X + 90 && incisionPoints[i].X < guideLineRect.X + 110
                                    && incisionPoints[i].Y > guideLineRect.Y + 10 && incisionPoints[i].Y < guideLineRect.Y + 35)
                                {
                                   // Renderer.DrawString(Resources.Fonts.Load("font_20"), "Is in Range 3", new Vector2(1000, 460), defaultColor);
                                    reachedIncisionLocation3 = true;
                                }
                                if (incisionPoints[i].X > guideLineRect.X + 140 && incisionPoints[i].X < guideLineRect.X + 160
                                    && incisionPoints[i].Y > guideLineRect.Y + 15)
                                {
                                    //Renderer.DrawString(Resources.Fonts.Load("font_20"), "Is in Range 4", new Vector2(1000, 490), defaultColor);
                                    reachedIncisionLocation4 = true;
                                }
                                if (incisionPoints[i].X > guideLineRect.X + 180 && incisionPoints[i].Y > guideLineRect.Y + 20)
                                {
                                   // Renderer.DrawString(Resources.Fonts.Load("font_20"), "Is in Range 5", new Vector2(1000, 520), defaultColor);
                                    reachedIncisionLocation5 = true;
                                }
                            }
                        }
                    }

                    if (reachedIncisionLocation1 &&
                        reachedIncisionLocation2 &&
                        reachedIncisionLocation3 &&
                        reachedIncisionLocation4 &&
                        reachedIncisionLocation5)
                    {
                        Renderer.DrawString(Resources.Fonts.Load("font_20"), "Incision Complete", new Vector2(500, 530), defaultColor * 2);
                        if (elapsedGameTime - delayGoingToCaseFour > 1f)
                        {
                            step = 4;
                            elapsedGameTime = 0;
                        } 
                    }
                    else
                    {
                        delayGoingToCaseFour = elapsedGameTime;
                    }

                    Renderer.Draw(Resources.Images.Load("surgicalKnife"), surgeryKnifeRect, Color.White);            

                    break;
                #endregion

                #region O - Step 5
                case 4:
                    Renderer.Draw(Resources.Images.Load("armOpened"), armRectangle, Color.White);

                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Now, we are going to reduce this fracture in order", new Vector2(330, 30), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "to help the bones grow back together correctly.", new Vector2(360, 60), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "This patient suffers from a displaced bone fracture.", new Vector2(310, 90), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Use one hand to move one bone, and the other hand to move the other.", new Vector2(210, 120), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Move both hands near your chest when this is complete.", new Vector2(300, 150), defaultColor);

                    Renderer.Draw(Resources.Images.Load("BonePiece1Outline"), new Rectangle(605, 345,
                        bonePieceOneRect.Width, bonePieceOneRect.Height + 10), Color.White);
                    Renderer.Draw(Resources.Images.Load("BonePiece2Outline"), new Rectangle(710, 360,
                        bonePieceOneRect.Width - 15, bonePieceOneRect.Height + 10), Color.White);

                    #region Bone 1 Logic
                    // Moving the left bone left hand logic
                    if (handCursorLeftRect.Intersects(bonePieceOneRect) && !leftHandPickedUpBone2
                        && !leftHandPickedUpBone1 && !rightHandPickedUpBone1 && !bone1InCorrectPosition)
                    {
                        timeRightHandOverBone1 = elapsedGameTime;
                        if (elapsedGameTime - timeLeftHandOverBone1 > 1f)
                        {
                            leftHandPickedUpBone1 = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(bonePieceOneRect.X - 50, bonePieceOneRect.Y - 30,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Tan);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(bonePieceOneRect.X - 45, bonePieceOneRect.Y - 25, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeLeftHandOverBone1)))), Color.White);
                        }
                    }
                    // Moving the left bone right hand
                    else if (handCursorRightRect.Intersects(bonePieceOneRect) && !rightHandPickedUpBone2
                        && !rightHandPickedUpBone1 && !leftHandPickedUpBone1 && !bone1InCorrectPosition)
                    {
                        timeLeftHandOverBone1 = elapsedGameTime;
                        if (elapsedGameTime - timeRightHandOverBone1 > 1f)
                        {
                            rightHandPickedUpBone1 = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(bonePieceOneRect.X - 50, bonePieceOneRect.Y - 30,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Tan);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(bonePieceOneRect.X - 45, bonePieceOneRect.Y - 25, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeRightHandOverBone1)))), Color.White);
                        }
                    }
                    else
                    {
                        timeLeftHandOverBone1 = elapsedGameTime;
                        timeRightHandOverBone1 = elapsedGameTime;
                    }
                    
                    // Bone 1 follows left/right hand until is placed in correct position
                    if (leftHandPickedUpBone1 && !bone1InCorrectPosition)
                    {
                        bonePieceOneRect.X = (int)handLeftPosition.X - 25;
                        bonePieceOneRect.Y = (int)handLeftPosition.Y + 30;
                        
                        if (bonePieceOneRect.Y < 356 && bonePieceOneRect.Y > 354)
                        {
                            bone1InCorrectPosition = true;
                        } 
                        if (bonePieceOneRect.X != 605)
                        {
                            bonePieceOneRect.X = 605;
                            handCursorLeftRect.X = 630;
                        } 
                        if (bonePieceOneRect.Y < 335)
                        {
                            bonePieceOneRect.Y = 335;
                            handCursorLeftRect.Y = 305;
                        }
                        if (bonePieceOneRect.Y > 365)
                        {
                            bonePieceOneRect.Y = 365;
                            handCursorLeftRect.Y = 335;
                        }     
                    } 
                    else if (rightHandPickedUpBone1 && !bone1InCorrectPosition)
                    {
                        bonePieceOneRect.X = (int)handRightPosition.X - 25;
                        bonePieceOneRect.Y = (int)handRightPosition.Y + 30;

                        if (bonePieceOneRect.Y < 356 && bonePieceOneRect.Y > 354)
                        {
                            bone1InCorrectPosition = true;
                        }
                        if (bonePieceOneRect.X != 605)
                        {
                            bonePieceOneRect.X = 605;
                            handCursorRightRect.X = 630;
                        }
                        if (bonePieceOneRect.Y < 335)
                        {
                            bonePieceOneRect.Y = 335;
                            handCursorRightRect.Y = 305;
                        }
                        if (bonePieceOneRect.Y > 365)
                        {
                            bonePieceOneRect.Y = 365;
                            handCursorRightRect.Y = 335;
                        }
                    }

                    // if bone is in the correct position, leave it there
                    if (bone1InCorrectPosition)
                    {
                        bonePieceOneRect.Y = 350;
                    }
                    #endregion

                    #region Bone 2 Logic
                    // Moving the right bone left hand logic
                    if (handCursorLeftRect.Intersects(bonePieceTwoRect) && !leftHandPickedUpBone2
                        && !leftHandPickedUpBone1 && !rightHandPickedUpBone2 && !bone2InCorrectPosition)
                    {
                        timeRightHandOverBone2 = elapsedGameTime;
                        if (elapsedGameTime - timeLeftHandOverBone2 > 1f)
                        {
                            leftHandPickedUpBone2 = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(bonePieceTwoRect.X + 100, bonePieceTwoRect.Y - 30,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Tan);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(bonePieceTwoRect.X + 105, bonePieceTwoRect.Y - 25, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeLeftHandOverBone2)))), Color.White);
                        }
                    }
                    // Moving the right bone right hand
                    else if (handCursorRightRect.Intersects(bonePieceTwoRect) && !rightHandPickedUpBone2
                        && !rightHandPickedUpBone1 && !leftHandPickedUpBone2 && !bone2InCorrectPosition)
                    {
                        timeLeftHandOverBone2 = elapsedGameTime;
                        if (elapsedGameTime - timeRightHandOverBone2 > 1f)
                        {
                            rightHandPickedUpBone2 = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(bonePieceTwoRect.X + 100, bonePieceTwoRect.Y - 30,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Tan);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(bonePieceTwoRect.X + 105, bonePieceTwoRect.Y - 25, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeRightHandOverBone2)))), Color.White);
                        }
                    }
                    else
                    {
                        timeLeftHandOverBone2 = elapsedGameTime;
                        timeRightHandOverBone2 = elapsedGameTime;
                    }

                    // Bone 2 follows left/right hand until is placed in correct position
                    if (leftHandPickedUpBone2 && !bone2InCorrectPosition)
                    {
                        bonePieceTwoRect.X = (int)handLeftPosition.X - 25;
                        bonePieceTwoRect.Y = (int)handLeftPosition.Y + 30;

                        if (bonePieceTwoRect.Y > 364 && bonePieceTwoRect.Y < 366)
                        {
                            bone2InCorrectPosition = true;
                        }
                        if (bonePieceTwoRect.X != 710)
                        {
                            bonePieceTwoRect.X = 710;
                            handCursorLeftRect.X = 735;
                        }
                        if (bonePieceTwoRect.Y < 340)
                        {
                            bonePieceTwoRect.Y = 340;
                            handCursorLeftRect.Y = 310;
                        }
                        if (bonePieceTwoRect.Y > 375)
                        {
                            bonePieceTwoRect.Y = 375;
                            handCursorLeftRect.Y = 345;
                        }
                    }
                    else if (rightHandPickedUpBone2 && !bone2InCorrectPosition)
                    {
                        bonePieceTwoRect.X = (int)handRightPosition.X - 25;
                        bonePieceTwoRect.Y = (int)handRightPosition.Y + 30;

                        if (bonePieceTwoRect.Y > 364 && bonePieceTwoRect.Y < 366)
                        {
                            bone2InCorrectPosition = true;
                        }
                        if (bonePieceTwoRect.X != 710)
                        {
                            bonePieceTwoRect.X = 710;
                            handCursorRightRect.X = 735;
                        }
                        if (bonePieceTwoRect.Y < 340)
                        {
                            bonePieceTwoRect.Y = 340;
                            handCursorRightRect.Y = 310;
                        }
                        if (bonePieceTwoRect.Y > 375)
                        {
                            bonePieceTwoRect.Y = 375;
                            handCursorRightRect.Y = 345;
                        }
                    }

                    // if bone is in the correct position, leave it there
                    if (bone2InCorrectPosition)
                    {
                        bonePieceTwoRect.Y = 365;
                    }
                    #endregion

                    if (bone1InCorrectPosition && bone2InCorrectPosition)
                    {
                        Renderer.DrawString(Resources.Fonts.Load("font_20"), "Fracture successfully reduced!", new Vector2(450, 500), defaultColor);
                        if (Math.Abs(handLeftPosition.Z - GetJointPosition(JointType.ShoulderCenter, ScreenSpace.Screen).Z) < 0.3f &&
                            Math.Abs(handRightPosition.Z - GetJointPosition(JointType.ShoulderCenter, ScreenSpace.Screen).Z) < 0.3f)
                        {
                            if (elapsedGameTime- delayGoingToCaseFive > 1f)
                            {
                                step = 5;
                                elapsedGameTime = 0;
                            }
                            else
                            {
                                Renderer.DrawString(Resources.Fonts.Load("font_20"), "Loading...", new Vector2(550, 530), Color.Blue);
                            }
                        }
                        else
                        {
                            delayGoingToCaseFive = elapsedGameTime;
                        }
                    }
                    Renderer.Draw(Resources.Images.Load("BonePiece1"), bonePieceOneRect, Color.White);
                    Renderer.Draw(Resources.Images.Load("BonePiece2"), bonePieceTwoRect, Color.White);

                    break;
                #endregion

                #region O - Step 6
                case 5:
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Our last step is to stitch up this incision.", new Vector2(400, 30), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Pick up the suture, move your hand forward, and follow", new Vector2(310, 60), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "the green marks in order from left to right.", new Vector2(380, 90), defaultColor);

                    Renderer.Draw(Resources.Images.Load("armBeforeStiches"), armRectangle, Color.White);

                    Renderer.Draw(Resources.Images.Load("Stitchmark"), stitchMark1, Color.Lime);
                    Renderer.Draw(Resources.Images.Load("Stitchmark"), stitchMark2, Color.Lime);
                    Renderer.Draw(Resources.Images.Load("Stitchmark"), stitchMark3, Color.Lime);
                    Renderer.Draw(Resources.Images.Load("Stitchmark"), stitchMark4, Color.Lime);
                    Renderer.Draw(Resources.Images.Load("Stitchmark"), stitchMark5, Color.Lime);

                    // Picking up suture logic with left and right hand
                    if (handCursorLeftRect.Intersects(sutureRect) && !suturePickedUpByLeftHand && !suturePickedUpByRightHand)
                    {
                        timeRightHandOverSuture = elapsedGameTime;
                        if (elapsedGameTime - timeLeftHandOverSuture > 1f)
                        {
                            suturePickedUpByLeftHand = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(sutureRect.X + 125, sutureRect.Y + 10,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Gray);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(sutureRect.X + 130, sutureRect.Y + 15, rectangleLoaderWidth,
                               (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeLeftHandOverSuture)))), Color.White);
                        }
                    } else if (handCursorRightRect.Intersects(sutureRect) && !suturePickedUpByRightHand && !suturePickedUpByLeftHand)
                    {
                        timeLeftHandOverSuture = elapsedGameTime;
                        if (elapsedGameTime - timeRightHandOverSuture > 1f)
                        {
                            suturePickedUpByRightHand = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(sutureRect.X + 125, sutureRect.Y + 10,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Gray);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(sutureRect.X + 130, sutureRect.Y + 15, rectangleLoaderWidth,
                               (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeRightHandOverSuture)))), Color.White);
                        }
                    } else
                    {
                        timeLeftHandOverSuture = elapsedGameTime;
                        timeRightHandOverSuture = elapsedGameTime;
                    }

                    // Stitching with suture with left or right hand
                    if (suturePickedUpByLeftHand)
                    {
                        sutureRect.X = (int)handLeftPosition.X-20;
                        sutureRect.Y = (int)handLeftPosition.Y;

                        Rectangle sutureDetectionRect = new Rectangle(sutureRect.X + 70, sutureRect.Y + 110, 10, 10);
                        Renderer.Draw(Resources.Images.Load("sutureDetectionRect"), sutureDetectionRect, Color.Black*0);

                        if (!(reachedStitchLocation1 &&
                         reachedStitchLocation2 &&
                         reachedStitchLocation3 &&
                         reachedStitchLocation4 &&
                         reachedStitchLocation5))
                        {
                            if (GetJointPosition(JointType.ShoulderLeft, ScreenSpace.Screen).Z - handLeftPosition.Z > .4f)
                            {
                                stitchPoints.Add(new Point(sutureDetectionRect.X, sutureDetectionRect.Y));
                                if (sutureDetectionRect.X < 610 || sutureDetectionRect.X > 800 
                                    || sutureDetectionRect.Y < 320 || sutureDetectionRect.Y > 435)
                                {
                                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Follow the points on the arm!", new Vector2(100, 450), defaultColor);
                                }

                            }
                        }
                    }
                    else if (suturePickedUpByRightHand)
                    {
                        sutureRect.X = (int)handRightPosition.X-20;
                        sutureRect.Y = (int)handRightPosition.Y;
                        Rectangle sutureDetectionRect = new Rectangle(sutureRect.X + 70, sutureRect.Y + 110, 10, 10);
                        Renderer.Draw(Resources.Images.Load("sutureDetectionRect"), sutureDetectionRect, Color.Black*0);

                        if (!(reachedStitchLocation1 &&
                         reachedStitchLocation2 &&
                         reachedStitchLocation3 &&
                         reachedStitchLocation4 &&
                         reachedStitchLocation5))
                        {
                            if (GetJointPosition(JointType.ShoulderRight, ScreenSpace.Screen).Z - handRightPosition.Z > .4f)
                            {
                                stitchPoints.Add(new Point(sutureDetectionRect.X, sutureDetectionRect.Y));
                                if (sutureDetectionRect.X < 610 || sutureDetectionRect.X > 800
                                    || sutureDetectionRect.Y < 320 || sutureDetectionRect.Y > 435)
                                {
                                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Follow the points on the arm!", new Vector2(100, 450), defaultColor);
                                }

                    }
                            }
                        }

                    for (int i = 0; i < stitchPoints.Count; i++)
                    {
                        if (armRectangle.Contains(stitchPoints[i]))
                        {
                            Renderer.Draw(Resources.Images.Load("Ink"), new Rectangle(stitchPoints[i].X, stitchPoints[i].Y, 8, 8), Color.LimeGreen);
                        }
                        Rectangle sutureDetectionRect = new Rectangle(sutureRect.X + 70, sutureRect.Y + 110, 10, 10);
                    
                        if (!(reachedStitchLocation1 &&
                         reachedStitchLocation2 &&
                         reachedStitchLocation3 &&
                         reachedStitchLocation4 &&
                         reachedStitchLocation5))
                        {
                            // Checks if each stitch mark contains a drawpoint from the stitchPoints List. 
                            // When all of the stitch marks are crossed, the process will be completed.
                            if (stitchMark1.Contains(stitchPoints[i]))
                            {
                                reachedStitchLocation1 = true;
                            } 
                            if (stitchMark2.Contains(stitchPoints[i]) && reachedStitchLocation1)
                            {
                                reachedStitchLocation2 = true;
                            } 
                            if (stitchMark3.Contains(stitchPoints[i]) && reachedStitchLocation2)
                            {
                                reachedStitchLocation3 = true;
                            } 
                            if (stitchMark4.Contains(stitchPoints[i]) && reachedStitchLocation3)
                            {
                                reachedStitchLocation4 = true;
                            } 
                            if (stitchMark5.Contains(stitchPoints[i]) && reachedStitchLocation4)
                            {
                                reachedStitchLocation5 = true;
                            }
                        }
                    }
                    if (reachedStitchLocation1 &&
                         reachedStitchLocation2 &&
                         reachedStitchLocation3 &&
                         reachedStitchLocation4 &&
                         reachedStitchLocation5)
                    {
                        Renderer.DrawString(Resources.Fonts.Load("font_20"), "Stitching Complete", new Vector2(500, 500), defaultColor);
                        Renderer.DrawString(Resources.Fonts.Load("font_20"), "Congratulations! You have successfully completed this surgery!", new Vector2(240, 530), defaultColor);
                        Renderer.DrawString(Resources.Fonts.Load("font_20"), "Go back to main screen by moving both hands to your chest.", new Vector2(260, 560), defaultColor);

                        if (Math.Abs(handLeftPosition.Z - GetJointPosition(JointType.ShoulderCenter, ScreenSpace.Screen).Z) < 0.3f &&
                            Math.Abs(handRightPosition.Z - GetJointPosition(JointType.ShoulderCenter, ScreenSpace.Screen).Z) < 0.3f)
                        {
                            if (elapsedGameTime - delayGoingToMainMenu > 1f)
                            {
                                step = 0;
                                elapsedGameTime = 0;
                                GameState.Set("surgeryChoiceScreen");
                            }
                          
                        }
                        else
                        {
                            delayGoingToMainMenu = elapsedGameTime;
                        }
                    }

                    Renderer.Draw(Resources.Images.Load("suture"), sutureRect, Color.White);

                    break;
                #endregion
            }

            if (SkeletonActive)
            {
                
                if(hasGloves)
                {
                    Renderer.Draw(Resources.Images.Load("handGloveLeft"), handCursorLeftRect, Color.White);
                    Renderer.Draw(Resources.Images.Load("handGloveRight"), handCursorRightRect, Color.White);
                }
                else
                {
                    if(!hasLeftGlove && step == 0)
                        Renderer.Draw(Resources.Images.Load("handLeft"), handCursorLeftRect, Color.White);
                    else
                        Renderer.Draw(Resources.Images.Load("handGloveLeft"), handCursorLeftRect, Color.White);
                    if (!hasRightGlove && step == 0)
                        Renderer.Draw(Resources.Images.Load("handRight"), handCursorRightRect, Color.White);
                    else 
                        Renderer.Draw(Resources.Images.Load("handGloveRight"), handCursorRightRect, Color.White);

                }

            }

        }

        public void OnUpdate_Plastic()
        {
            GraphicsDevice.Clear(Color.Gray*2f);
            elapsedGameTime += DrawDelta;
           
            Rectangle sinkRectangle = new Rectangle(Screen.Width / 2 - 175, 200, 325, 290);

            handLeftPosition = GetJointPosition(JointType.HandLeft, ScreenSpace.Screen);
            handRightPosition = GetJointPosition(JointType.HandRight, ScreenSpace.Screen);

            Rectangle handCursorLeftRect = new Rectangle((int)handLeftPosition.X, (int)handLeftPosition.Y, handWidth, handHeight);
            Rectangle handCursorRightRect = new Rectangle((int)handRightPosition.X, (int)handRightPosition.Y, handWidth, handHeight);

            Rectangle rectangleLoaderRect = new Rectangle(handCursorRightRect.X + 80, handCursorRightRect.Y, rectangleLoaderWidth, rectangleLoaderLength);

            Rectangle fatLegRectangle = new Rectangle(300, 200, 640, 280);

            Rectangle syringeRectangle = new Rectangle(100, 200, 40, 160);

            switch (step)
            {
                #region P - Step 1
                case 0:
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Plastic Surgeries are surgeries involving the restoration, the reconstruction", new Vector2(200, 30), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "The example we will be learning about is liposuction, which is the process of", new Vector2(190, 60), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "removing excess fat from the body. Before you begin, make sure you are as clean", new Vector2(170, 90), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "as possible. So reach out and wash your hands, wear your gloves, and we can begin.", new Vector2(160, 120), defaultColor);

                    if (handCursorLeftRect.Intersects(sinkRectangle) && handCursorRightRect.Intersects(sinkRectangle) &&
                       Math.Abs(GetJointPosition(JointType.Spine, ScreenSpace.Screen).Z - handLeftPosition.Z) < 1.1f &&
                         Math.Abs(GetJointPosition(JointType.Spine, ScreenSpace.Screen).Z - handRightPosition.Z) < 1.1f)
                    {
                        if (elapsedGameTime - sinkHoverTimer > .5f)
                        {
                            Renderer.Draw(Resources.Images.Load("SinkRunning"), sinkRectangle, Color.White);
                            timeWashingHands += DrawDelta;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("SinkNormal"), sinkRectangle, Color.White);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(rectangleLoaderRect.X - 5, rectangleLoaderRect.Y - 5,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Blue);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(rectangleLoaderRect.X, rectangleLoaderRect.Y, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - sinkHoverTimer) * 2))), Color.White);

                        }
                        if (timeWashingHands > 5)
                        {
                            hasWashedHands = true;
                        }
                        else if (timeWashingHands < 5 && elapsedGameTime - sinkHoverTimer > 1)
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(Screen.Width / 2 - 185, 520, handWashLoaderWidth + 10, handWashLoaderLength + 10), Color.DarkCyan);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(Screen.Width / 2 - 180, 525, (int)(handWashLoaderWidth * (timeWashingHands / 5)), handWashLoaderLength), Color.White);
                        }
                    }
                    else
                    {
                        sinkHoverTimer = elapsedGameTime;
                        Renderer.Draw(Resources.Images.Load("SinkNormal"), sinkRectangle, Color.White);
                    }

                    Rectangle staticGloveLeftRect = new Rectangle(200, 300, handWidth, handHeight);
                    Rectangle staticGloveRightRect = new Rectangle(Screen.Width - 250, 300, handWidth, handHeight);

                    if (hasWashedHands)
                    {
                        Renderer.DrawString(Resources.Fonts.Load("font_20"), "Hands are clean", new Vector2(500, 525), Color.Black);
                        Renderer.DrawString(Resources.Fonts.Load("font_20"), "Now you may wear your gloves.", new Vector2(430, 550), Color.Black);

                        if (handCursorLeftRect.Intersects(staticGloveLeftRect))
                        {
                            hasLeftGlove = true;
                        }
                        if (handCursorRightRect.Intersects(staticGloveRightRect))
                        {
                            hasRightGlove = true;
                        }

                        if (hasRightGlove && hasLeftGlove)
                        {
                            hasGloves = true;
                        }
                    }

                    if (hasGloves)
                    {
                        if (elapsedGameTime - handGloveTimer > .5f)
                        {
                            step = 1;
                            elapsedGameTime = 0;
                        }
                    }
                    else
                    {
                        if (!hasLeftGlove)
                            Renderer.Draw(Resources.Images.Load("handGloveLeft"), staticGloveLeftRect, Color.White);
                        if (!hasRightGlove)
                            Renderer.Draw(Resources.Images.Load("handGloveRight"), staticGloveRightRect, Color.White);
                        handGloveTimer = elapsedGameTime;
                    }

                    break;
                #endregion

                #region P - Step 2
                case 1:
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "The first step is to provide anesthesia for the patient.", new Vector2(300, 30), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Pick up the syringe by hovering over it until the bar is full.", new Vector2(270, 60), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Inject by moving your hand forward over the leg.", new Vector2(320, 90), defaultColor);

                    Renderer.Draw(Resources.Images.Load("fatLeg"), fatLegRectangle, Color.White);

                    bool hasPainkillerBeenInjected = false;

                    // Rectangle loader will be used regardless of which hand had picked it up.
                    // Timers reset if player stops hovering over item.
                    // left hand logic

                    if (handCursorLeftRect.Intersects(syringeRectangle) && !syringePickedUpByRightHand && !syringePickedUpByLeftHand)
                    {
                        timeRightHandOverSyringe = elapsedGameTime;
                        if (elapsedGameTime - timeLeftHandOverSyringe > 1f)
                        {
                            syringePickedUpByLeftHand = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 50, syringeRectangle.Y + 25,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Blue);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 55, syringeRectangle.Y + 30, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeLeftHandOverSyringe)))), Color.White);
                        }
                    }
                    // right hand logic
                    else if (handCursorRightRect.Intersects(syringeRectangle) && !syringePickedUpByRightHand && !syringePickedUpByLeftHand)
                    {
                        timeLeftHandOverSyringe = elapsedGameTime;
                        if (elapsedGameTime - timeRightHandOverSyringe > 1f)
                        {
                            syringePickedUpByRightHand = true;
                        }
                        else
                        {
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 50, syringeRectangle.Y + 25,
                                rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Blue);
                            Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 55, syringeRectangle.Y + 30, rectangleLoaderWidth,
                                (int)(rectangleLoaderLength * (1 - (elapsedGameTime - timeRightHandOverSyringe)))), Color.White);
                        }
                    }
                    else
                    {
                        timeLeftHandOverSyringe = elapsedGameTime;
                        timeRightHandOverSyringe = elapsedGameTime;
                    }

                    // follows left hand position
                    // if syringe is picked up, when hand is hovering over leg and moves
                    // forward a certain amount (.25 m), the anesthesia will be injected.
                    if (syringePickedUpByLeftHand)
                    {
                        syringeRectangle.X = (int)handLeftPosition.X;
                        syringeRectangle.Y = (int)handLeftPosition.Y;

                        if (syringeRectangle.Intersects(fatLegRectangle))
                        {
                            if (previousZHandPosition - handLeftPosition.Z > .25f)
                            {
                                Renderer.DrawString(Resources.Fonts.Load("font_20"), "Anesthesia Has Been Injected", new Vector2(450, 500), defaultColor);
                                hasPainkillerBeenInjected = true;
                            }
                            else
                            {
                                Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 50, syringeRectangle.Y + 25,
                                    rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Coral);
                                if ((previousZHandPosition - handLeftPosition.Z) / .25f * 80 < 80)
                                {
                                    syringeAmount = (int)((previousZHandPosition - handLeftPosition.Z) / .25f * 80);
                                }
                                else if ((previousZHandPosition - handLeftPosition.Z) / .25f * 80 < 0)
                                {
                                    syringeAmount = 0;
                                }
                                else
                                {
                                    syringeAmount = 80;
                                }
                                Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 55, syringeRectangle.Y + 30, rectangleLoaderWidth,
                                    (syringeAmount)), Color.White);

                            }
                        }
                        else
                        {
                            previousZHandPosition = handLeftPosition.Z;

                        }

                    }
                    // same thing for right hand
                    else if (syringePickedUpByRightHand)
                    {
                        syringeRectangle.X = (int)handRightPosition.X;
                        syringeRectangle.Y = (int)handRightPosition.Y;

                        if (syringeRectangle.Intersects(fatLegRectangle))
                        {
                            if (previousZHandPosition - handRightPosition.Z > .2f)
                            {
                                Renderer.DrawString(Resources.Fonts.Load("font_20"), "Anesthesia Has Been Injected", new Vector2(450, 500), defaultColor);
                                hasPainkillerBeenInjected = true;

                            }
                            else
                            {
                                Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 50, syringeRectangle.Y + 25,
                                    rectangleLoaderWidth + 10, rectangleLoaderLength + 10), Color.Coral);
                                if ((previousZHandPosition - handRightPosition.Z) / .2f * 80 < 80)
                                {
                                    syringeAmount = (int)((previousZHandPosition - handRightPosition.Z) / .2f * 80);
                                }
                                else if ((previousZHandPosition - handRightPosition.Z) / .2f * 80 < 0)
                                {
                                    syringeAmount = 0;
                                }
                                else
                                {
                                    syringeAmount = 80;
                                }
                                Renderer.Draw(Resources.Images.Load("RectangleLoader"), new Rectangle(syringeRectangle.X + 55, syringeRectangle.Y + 30, rectangleLoaderWidth,
                                    (syringeAmount)), Color.White);

                            }
                        }
                        else
                        {
                            previousZHandPosition = handRightPosition.Z;

                        }
                    }

                    if (hasPainkillerBeenInjected)
                    {
                        if (elapsedGameTime - delayGoingToCaseTwo > 1f)
                        {
                            step = 2;
                            elapsedGameTime = 0;
                        }
                    }
                    else
                    {
                        delayGoingToCaseTwo = elapsedGameTime;
                    }

                    Renderer.Draw(Resources.Images.Load("painkiller"), syringeRectangle, Color.White);

                    break;
                #endregion

                #region P - Step 3
                case 2:
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "Now it's time to insert what is called tumescent fluid into the leg.", new Vector2(250, 30), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "This solution is typically made of saline solution (or diluted salt water), ", new Vector2(220, 60), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "lidocaine, and epinephrine, which help to numb the treatment area, while", new Vector2(210, 90), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "controlling blood loss, and facilitating fat removal. Inject this solution", new Vector2(230, 120), defaultColor);
                    Renderer.DrawString(Resources.Fonts.Load("font_20"), "using the same method you used for the syringe with the cannula to your right. ", new Vector2(190, 150), defaultColor);



                    Renderer.Draw(Resources.Images.Load("fatLeg"), fatLegRectangle, Color.White);

                    break;
                #endregion
            }
            if (SkeletonActive)
            {

                if (hasGloves)
                {
                    Renderer.Draw(Resources.Images.Load("handGloveLeft"), handCursorLeftRect, Color.White);
                    Renderer.Draw(Resources.Images.Load("handGloveRight"), handCursorRightRect, Color.White);
                }
                else
                {
                    if (!hasLeftGlove && step == 0)
                        Renderer.Draw(Resources.Images.Load("handLeft"), handCursorLeftRect, Color.White);
                    else
                        Renderer.Draw(Resources.Images.Load("handGloveLeft"), handCursorLeftRect, Color.White);
                    if (!hasRightGlove && step == 0)
                        Renderer.Draw(Resources.Images.Load("handRight"), handCursorRightRect, Color.White);
                    else
                        Renderer.Draw(Resources.Images.Load("handGloveRight"), handCursorRightRect, Color.White);

                }

            }
        }

        public void OnUpdate_Cancer()
        {
            GraphicsDevice.Clear(Color.LightPink);

           
        }

        public void OnUpdate_Pause()
        {
            GraphicsDevice.Clear(Color.Gray);
            if (GetJointPosition(JointType.HandRight, ScreenSpace.World).Y
                 < GetJointPosition(JointType.Head, ScreenSpace.World).Y && GetJointPosition(JointType.HandLeft, ScreenSpace.World).Y
                 > GetJointPosition(JointType.Head, ScreenSpace.World).Y)
            {
                GameState.Set("surgeryScreen");
            }


        }


        public void OnUpdate_Endscreen()
        {
            // Do something;

        }

        /**********************************************************************************************/
        // Utility Methods:

        public enum ScreenSpace
        {
            World = 0,
            Screen
        }

        public static Color GetRandomColor()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble(), 1f);
        }

        public static bool IsTouching(JointType joint1, JointType joint2)
        {
            return Vector3.Distance(GetJointPosition(joint1, ScreenSpace.World), GetJointPosition(joint2, ScreenSpace.World)) < 60f;
        }

        /// <summary>
        /// Returns the screen position of the target joint.
        /// </summary>
        /// <param name="joint">The joint to return position data for.</param>
        /// <param name="skeleton">If not set then the first available skeleton will be selected.</param>
        /// <returns>The joint position.</returns>
        public static Vector3 GetJointPosition(JointType joint, ScreenSpace type, CustomSkeleton skeleton = null)
        {
            if (instance == null)
                return Vector3.Zero;

            // If the skeleton provided is null then grab the first available skeleton and use it:
            if (skeleton == null && instance.Skeletons != null && instance.Skeletons.Count > 0)
            {
                // This is what was being done by default:
                skeleton = instance.Skeletons.FirstOrDefault(o => o.Joints.Count > 0 && o.State == SkeletonTrackingState.Tracked);
            }
            else
            {
                // No skeleton exists in the array so return default data:
                return Vector3.Zero;
            }

            if (type == ScreenSpace.Screen)
            {
                if (instance.Skeletons != null && skeleton != null)
                    return skeleton.ScaleTo(joint, Screen.Width, Screen.Height);
                else
                    return Vector3.Zero;
            }
            else
            {
                if (instance.Skeletons != null && skeleton != null)
                    return skeleton.ScaleTo(joint, World.Width, World.Height);
                else
                    return Vector3.Zero;
            }
        }


        public float Interpolate(float a, float b, float speed)
        {
            return MathHelper.Lerp(a, b, DrawDelta * speed);
        }

        public void DrawAllSkeletons()
        {
            if (Skeletons == null)
                return;

            foreach (CustomSkeleton skeleton in Skeletons)
            {
                DrawSkeleton(skeleton);
            }
        }
        public void DrawSkeleton(CustomSkeleton skeleton)
        {
            if (skeleton == null)
                return;

            DrawJointConnection(skeleton, JointType.Head, JointType.ShoulderCenter);
            DrawJointConnection(skeleton, JointType.ShoulderCenter, JointType.ShoulderRight);
            DrawJointConnection(skeleton, JointType.ShoulderRight, JointType.ElbowRight);
            DrawJointConnection(skeleton, JointType.ElbowRight, JointType.HandRight);
            DrawJointConnection(skeleton, JointType.ShoulderCenter, JointType.ShoulderLeft);
            DrawJointConnection(skeleton, JointType.ShoulderLeft, JointType.ElbowLeft);
            DrawJointConnection(skeleton, JointType.ElbowLeft, JointType.HandLeft);
            DrawJointConnection(skeleton, JointType.ShoulderCenter, JointType.HipCenter);
            DrawJointConnection(skeleton, JointType.HipCenter, JointType.HipRight);
            DrawJointConnection(skeleton, JointType.HipRight, JointType.KneeRight);
            DrawJointConnection(skeleton, JointType.KneeRight, JointType.FootRight);
            DrawJointConnection(skeleton, JointType.HipCenter, JointType.HipLeft);
            DrawJointConnection(skeleton, JointType.HipLeft, JointType.KneeLeft);
            DrawJointConnection(skeleton, JointType.KneeLeft, JointType.FootLeft);
        }
        public void DrawJointConnection(CustomSkeleton skeleton, JointType joint1, JointType joint2)
        {
            DrawLine(Renderer, 4, Color.Blue, jointToVector(skeleton, joint1), jointToVector(skeleton, joint2));
        }
        public void DrawLine(SpriteBatch spriteBatch, float width, Color color, Vector2 p1, Vector2 p2)
        {
            spriteBatch.Draw(Resources.Images.Pixel, p1, null, color, (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X), Vector2.Zero, new Vector2(Vector2.Distance(p1, p2), width), SpriteEffects.None, 0);
        }

        #region Math Internal

        protected Vector2 jointToVector(CustomSkeleton skeleton, JointType type)
        {
            return jointToVector(skeleton, type, World.Width, World.Height);
        }
        protected Vector2 jointToVector(CustomSkeleton skeleton, JointType type, int Width, int Height)
        {
            Vector3 position = skeleton.ScaleTo(type, Width, Height);
            return new Vector2(position.X, position.Y);
        }

        #endregion

        #region System

        GraphicsDeviceManager GraphicsManager;
        SpriteBatch Renderer;
        KinectSensor Camera;
        List<CustomSkeleton> Skeletons;   //this array will hold all skeletons that are found in the video frame

        #endregion

        #region XNA Framework Overrides:

        public MyGame()
        {
            GraphicsManager = new GraphicsDeviceManager(this);

            GraphicsManager.PreferredBackBufferHeight = 600;
            GraphicsManager.PreferredBackBufferWidth = 1200;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Set the instance so that it is available to other classes:
            instance = this;

            // The Kinect has a built in system that handles 6 skeletons at once:
            Skeletons = new List<CustomSkeleton>();

            // Find the Kinect Camera:
            if (KinectSensor.KinectSensors.Count > 0)
            {
                Camera = KinectSensor.KinectSensors[0];

                if (Camera != null)
                {
                    // Initialize the Kinect sensor:
                    Camera.SkeletonStream.Enable();
                    Camera.Start();
                    Camera.SkeletonStream.Enable(new TransformSmoothParameters()
                    {
                        Smoothing = 0.5f,
                        Correction = 0.5f,
                        Prediction = 0.5f,
                        JitterRadius = 0.05f,
                        MaxDeviationRadius = 0.04f
                    });

                    // When the camera senses a skeleton "event" in realtime , it pulls in new video frames that we can interpret:
                    Camera.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(OnSkeletonUpdated);
                    Debug_Camera = "Camera Connected!";
                }
                else
                {
                    Debug_Camera = "No Camera";
                }
                Camera.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
            }
            // Set the debug message to update whenever the game state changes:
            GameState.OnStateActivated += (name) => { Debug_State = name; };

            // Setup the custom GameStates:
            SetupGameStates();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Renderer = new SpriteBatch(GraphicsDevice);

            // KM - this was missing:
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            if (Camera != null)
                Camera.Stop();

            // KM - Release resources that were loaded when unloading:
            Resources.Unload();

            // KM - this was missing:
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            UpdateDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                // If the user taps the 'X' key then exit the app:
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // KM: This is the amount of time (in seconds) that has elapsed since the last frame.
            //     Use this to properly interpolate the skeleton changes or for rendering.
            //     I have added a public static accessor called FrameDelta which should be used to 
            //     access this information from anywhere in the game.
            //     I also added a utility method Interpolate which will smoothly interpolate between
            //     two values using the delta.
            DrawDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            GraphicsDevice.Clear(Color.LightGray);

            Renderer.Begin();

            if (ShowDebug)
            {
                Renderer.DrawString(Resources.Fonts.Load("font_copy_custom"), Debug_State, new Vector2(20, 20), Color.Green);
                Renderer.DrawString(Resources.Fonts.Load("font_copy_default"), Debug_Camera, new Vector2(20, 50), Color.Blue);
                Renderer.DrawString(Resources.Fonts.Load("font_copy_default"), Debug_Skeleton, new Vector2(20, 80), Color.Red);

                // Renders the Skeleton on screen for Debugging:

            }

            // Execute the appropriate game state handler each frame:
            if (GameState.ActiveState != null)
                GameState.ActiveState.OnUpdate();
            //          DrawAllSkeletons();
            // Complete the rendering of this frame:
            Renderer.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Kinect Event Handlers:

        public static Vector2 screenMod
        {
            get
            {
                if (_screenMod == null || _screenMod == Vector2.Zero)
                {
                    _screenMod = new Vector2(Screen.Width / 2, Screen.Height / 2);
                }
                return _screenMod;
            }
        }
        private static Vector2 _screenMod;

        public class CustomSkeleton
        {
            public Dictionary<JointType, Vector3> Joints = new Dictionary<JointType, Vector3>();

            public SkeletonTrackingState State = SkeletonTrackingState.NotTracked;

            public void Set(Skeleton skeleton)
            {
                State = skeleton.TrackingState;

                foreach (Joint joint in skeleton.Joints)
                {
                    Vector3 pos = new Vector3(joint.Position.X, -joint.Position.Y, joint.Position.Z);
                    if (Joints.ContainsKey(joint.JointType))
                    {
                        Joints[joint.JointType] = pos;
                    }
                    else
                    {
                        Joints.Add(joint.JointType, pos);
                    }
                }
            }

            public Vector3 ScaleTo(JointType type, float width, float height)
            {
                if (!Joints.ContainsKey(type))
                {
                    return Vector3.Zero;
                }
                return new Vector3(Joints[type].X * width + screenMod.X, Joints[type].Y * height + screenMod.Y, Joints[type].Z);
            }

            public CustomSkeleton() { }
        }

        private void OnSkeletonUpdated(object sender, SkeletonFrameReadyEventArgs e)
        {
            // The live feed returns updates from the camera:
            SkeletonFrame skeletonFrame = e.OpenSkeletonFrame();
            if (skeletonFrame == null)
                return;

            // Add smoothing to prevent glitching in the skeletons' movements:
            Skeleton[] raw = new Skeleton[6];
            skeletonFrame.CopySkeletonDataTo(raw);
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i] == null)
                {
                    continue;
                }

                if (Skeletons.Count <= i || Skeletons[i] == null)
                {
                    // If this skeleton was just added then set it:
                    CustomSkeleton newSkeleton = new CustomSkeleton();
                    newSkeleton.Set(raw[i]);
                    Skeletons.Add(newSkeleton);
                    continue;
                }

                if (Smoothing > 0f)
                {
                    // Set the state of the skeleton:
                    Skeletons[i].State = raw[i].TrackingState;

                    // Add smoothing interpolation to each point:
                    foreach (Joint joint in raw[i].Joints)
                    {
                        Vector3 pos = new Vector3();
                        pos.X = Interpolate(Skeletons[i].Joints[joint.JointType].X, joint.Position.X, Smoothing);
                        pos.Y = Interpolate(Skeletons[i].Joints[joint.JointType].Y, -joint.Position.Y, Smoothing);
                        pos.Z = Interpolate(Skeletons[i].Joints[joint.JointType].Z, joint.Position.Z, Smoothing);

                        Skeletons[i].Joints[joint.JointType] = pos;
                    }
                }
                else
                {
                    // Store the raw data for the skeleton:
                    Skeletons[i].Set(raw[i]);
                }
            }

            skeletonFrame.Dispose();

            if ((raw != null && raw.Any(o => o.TrackingState == SkeletonTrackingState.Tracked)))
            {
                SkeletonActive = true;
                Debug_Skeleton = "Skeleton Found";
            }
            else
            {
                SkeletonActive = false;
                Debug_Skeleton = "No Skeleton Found";
            }
        }

        #endregion
    }
}

//challenges
//2 modify isTouching so that it accepts a parameter instead of 60f
//3. only draw the head and hands if the values for the coordinates are not zero