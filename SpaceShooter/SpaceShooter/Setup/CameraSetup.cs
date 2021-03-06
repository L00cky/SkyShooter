    // This is a generated file created by Glue. To change this file, edit the camera settings in Glue.
    // To access the camera settings, push the camera icon.
    using Camera = FlatRedBall.Camera;
    namespace SpaceShooter
    {
        internal static class CameraSetup
        {
            const float Scale = 1f;
            internal static void ResetCamera (Camera cameraToReset)
            {
                FlatRedBall.Camera.Main.Orthogonal = true;
                FlatRedBall.Camera.Main.OrthogonalHeight = 600;
                FlatRedBall.Camera.Main.OrthogonalWidth = 800;
                FlatRedBall.Camera.Main.FixAspectRatioYConstant();
            }
            internal static void SetupCamera (Camera cameraToSetUp, Microsoft.Xna.Framework.GraphicsDeviceManager graphicsDeviceManager, int width = 800, int height = 600)
            {
                #if WINDOWS || DESKTOP_GL
                FlatRedBall.FlatRedBallServices.Game.Window.AllowUserResizing = false;
                FlatRedBall.FlatRedBallServices.GraphicsOptions.SetResolution(width, height);
                #elif IOS || ANDROID
                FlatRedBall.FlatRedBallServices.GraphicsOptions.SetFullScreen(FlatRedBall.FlatRedBallServices.GraphicsOptions.ResolutionWidth, FlatRedBall.FlatRedBallServices.GraphicsOptions.ResolutionHeight);
                #elif UWP
                FlatRedBall.FlatRedBallServices.GraphicsOptions.SetResolution(width, height);
                #endif
                ResetCamera(cameraToSetUp);
            }
            private static void HandleResolutionChange (object sender, System.EventArgs args)
            {
                SetAspectRatioTo(16 / 9m);
            }
            private static void SetAspectRatioTo (decimal aspectRatio)
            {
                var resolutionAspectRatio = FlatRedBall.FlatRedBallServices.GraphicsOptions.ResolutionWidth / (decimal)FlatRedBall.FlatRedBallServices.GraphicsOptions.ResolutionHeight;
                int destinationRectangleWidth;
                int destinationRectangleHeight;
                int x = 0;
                int y = 0;
                if (aspectRatio > resolutionAspectRatio)
                {
                    destinationRectangleWidth = FlatRedBall.FlatRedBallServices.GraphicsOptions.ResolutionWidth;
                    destinationRectangleHeight = FlatRedBall.Math.MathFunctions.RoundToInt(destinationRectangleWidth / (float)aspectRatio);
                    y = (FlatRedBall.FlatRedBallServices.GraphicsOptions.ResolutionHeight - destinationRectangleHeight) / 2;
                }
                else
                {
                    destinationRectangleHeight = FlatRedBall.FlatRedBallServices.GraphicsOptions.ResolutionHeight;
                    destinationRectangleWidth = FlatRedBall.Math.MathFunctions.RoundToInt(destinationRectangleHeight * (float)aspectRatio);
                    x = (FlatRedBall.FlatRedBallServices.GraphicsOptions.ResolutionWidth - destinationRectangleWidth) / 2;
                }
                FlatRedBall.Camera.Main.DestinationRectangle = new Microsoft.Xna.Framework.Rectangle(x, y, destinationRectangleWidth, destinationRectangleHeight);
                FlatRedBall.Camera.Main.FixAspectRatioYConstant();
            }
        }
    }
