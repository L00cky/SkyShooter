#if ANDROID || IOS
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
using Color = Microsoft.Xna.Framework.Color;
using SpaceShooter.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using SpaceShooter.Entities;
using SpaceShooter.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
namespace SpaceShooter.Entities
{
    public partial class Scoreboard : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable
    {
        // This is made static so that static lazy-loaded content can access it.
        public static string ContentManagerName { get; set; }
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        static object mLockObject = new object();
        static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
        static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
        
        private FlatRedBall.Graphics.Text ScorePoints;
        private FlatRedBall.Graphics.Text ScoreLabel;
        public int Score
        {
            get
            {
                return int.Parse(ScorePoints.DisplayText);
            }
            set
            {
                ScorePoints.DisplayText = value.ToString();
            }
        }
        protected FlatRedBall.Graphics.Layer LayerProvidedByContainer = null;
        public Scoreboard ()
        	: this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {
        }
        public Scoreboard (string contentManagerName)
        	: this(contentManagerName, true)
        {
        }
        public Scoreboard (string contentManagerName, bool addToManagers)
        	: base()
        {
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);
        }
        protected virtual void InitializeEntity (bool addToManagers)
        {
            LoadStaticContent(ContentManagerName);
            ScorePoints = new FlatRedBall.Graphics.Text();
            ScorePoints.Name = "ScorePoints";
            ScoreLabel = new FlatRedBall.Graphics.Text();
            ScoreLabel.Name = "ScoreLabel";
            
            PostInitialize();
            if (addToManagers)
            {
                AddToManagers(null);
            }
        }
        public virtual void ReAddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.Graphics.TextManager.AddToLayer(ScorePoints, LayerProvidedByContainer);
            if (ScorePoints.Font != null)
            {
                ScorePoints.SetPixelPerfectScale(LayerProvidedByContainer);
            }
            FlatRedBall.Graphics.TextManager.AddToLayer(ScoreLabel, LayerProvidedByContainer);
            if (ScoreLabel.Font != null)
            {
                ScoreLabel.SetPixelPerfectScale(LayerProvidedByContainer);
            }
        }
        public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.Graphics.TextManager.AddToLayer(ScorePoints, LayerProvidedByContainer);
            if (ScorePoints.Font != null)
            {
                ScorePoints.SetPixelPerfectScale(LayerProvidedByContainer);
            }
            FlatRedBall.Graphics.TextManager.AddToLayer(ScoreLabel, LayerProvidedByContainer);
            if (ScoreLabel.Font != null)
            {
                ScoreLabel.SetPixelPerfectScale(LayerProvidedByContainer);
            }
            AddToManagersBottomUp(layerToAddTo);
            CustomInitialize();
        }
        public virtual void Activity ()
        {
            
            CustomActivity();
        }
        public virtual void Destroy ()
        {
            FlatRedBall.SpriteManager.RemovePositionedObject(this);
            
            if (ScorePoints != null)
            {
                FlatRedBall.Graphics.TextManager.RemoveText(ScorePoints);
            }
            if (ScoreLabel != null)
            {
                FlatRedBall.Graphics.TextManager.RemoveText(ScoreLabel);
            }
            CustomDestroy();
        }
        public virtual void PostInitialize ()
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            if (ScorePoints.Parent == null)
            {
                ScorePoints.CopyAbsoluteToRelative();
                ScorePoints.AttachTo(this, false);
            }
            ScorePoints.DisplayText = "99";
            if (ScorePoints.Parent == null)
            {
                ScorePoints.X = 20f;
            }
            else
            {
                ScorePoints.RelativeX = 20f;
            }
            if (ScorePoints.Parent == null)
            {
                ScorePoints.Y = 270f;
            }
            else
            {
                ScorePoints.RelativeY = 270f;
            }
            if (ScoreLabel.Parent == null)
            {
                ScoreLabel.CopyAbsoluteToRelative();
                ScoreLabel.AttachTo(this, false);
            }
            ScoreLabel.DisplayText = "Points";
            if (ScoreLabel.Parent == null)
            {
                ScoreLabel.X = -20f;
            }
            else
            {
                ScoreLabel.RelativeX = -20f;
            }
            if (ScoreLabel.Parent == null)
            {
                ScoreLabel.Y = 270f;
            }
            else
            {
                ScoreLabel.RelativeY = 270f;
            }
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp (FlatRedBall.Graphics.Layer layerToAddTo)
        {
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers ()
        {
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            if (ScorePoints != null)
            {
                FlatRedBall.Graphics.TextManager.RemoveTextOneWay(ScorePoints);
            }
            if (ScoreLabel != null)
            {
                FlatRedBall.Graphics.TextManager.RemoveTextOneWay(ScoreLabel);
            }
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements)
        {
            if (callOnContainedElements)
            {
            }
            ScorePoints.DisplayText = "99";
            if (ScorePoints.Parent == null)
            {
                ScorePoints.X = 20f;
            }
            else
            {
                ScorePoints.RelativeX = 20f;
            }
            if (ScorePoints.Parent == null)
            {
                ScorePoints.Y = 270f;
            }
            else
            {
                ScorePoints.RelativeY = 270f;
            }
            ScoreLabel.DisplayText = "Points";
            if (ScoreLabel.Parent == null)
            {
                ScoreLabel.X = -20f;
            }
            else
            {
                ScoreLabel.RelativeX = -20f;
            }
            if (ScoreLabel.Parent == null)
            {
                ScoreLabel.Y = 270f;
            }
            else
            {
                ScoreLabel.RelativeY = 270f;
            }
            Score = 0;
        }
        public virtual void ConvertToManuallyUpdated ()
        {
            this.ForceUpdateDependenciesDeep();
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            FlatRedBall.Graphics.TextManager.ConvertToManuallyUpdated(ScorePoints);
            FlatRedBall.Graphics.TextManager.ConvertToManuallyUpdated(ScoreLabel);
        }
        public static void LoadStaticContent (string contentManagerName)
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
            ContentManagerName = contentManagerName;
            #if DEBUG
            if (contentManagerName == FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                HasBeenLoadedWithGlobalContentManager = true;
            }
            else if (HasBeenLoadedWithGlobalContentManager)
            {
                throw new System.Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
            }
            #endif
            bool registerUnload = false;
            if (LoadedContentManagers.Contains(contentManagerName) == false)
            {
                LoadedContentManagers.Add(contentManagerName);
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("ScoreboardStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
            }
            if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("ScoreboardStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
            }
            CustomLoadStaticContent(contentManagerName);
        }
        public static void UnloadStaticContent ()
        {
            if (LoadedContentManagers.Count != 0)
            {
                LoadedContentManagers.RemoveAt(0);
                mRegisteredUnloads.RemoveAt(0);
            }
            if (LoadedContentManagers.Count == 0)
            {
            }
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName)
        {
            return null;
        }
        public static object GetFile (string memberName)
        {
            return null;
        }
        object GetMember (string memberName)
        {
            return null;
        }
        protected bool mIsPaused;
        public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
        {
            base.Pause(instructions);
            mIsPaused = true;
        }
        public virtual void SetToIgnorePausing ()
        {
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(ScorePoints);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(ScoreLabel);
        }
        public virtual void MoveToLayer (FlatRedBall.Graphics.Layer layerToMoveTo)
        {
            var layerToRemoveFrom = LayerProvidedByContainer;
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(ScorePoints);
            }
            FlatRedBall.Graphics.TextManager.AddToLayer(ScorePoints, layerToMoveTo);
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(ScoreLabel);
            }
            FlatRedBall.Graphics.TextManager.AddToLayer(ScoreLabel, layerToMoveTo);
            LayerProvidedByContainer = layerToMoveTo;
        }
    }
}
