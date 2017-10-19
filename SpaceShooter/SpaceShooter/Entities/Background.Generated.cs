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
    public partial class Background : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable
    {
        // This is made static so that static lazy-loaded content can access it.
        public static string ContentManagerName { get; set; }
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        static object mLockObject = new object();
        static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
        static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
        protected static Microsoft.Xna.Framework.Graphics.Texture2D sky1;
        
        private FlatRedBall.Sprite BackgroundSprite;
        protected FlatRedBall.Graphics.Layer LayerProvidedByContainer = null;
        public Background ()
        	: this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {
        }
        public Background (string contentManagerName)
        	: this(contentManagerName, true)
        {
        }
        public Background (string contentManagerName, bool addToManagers)
        	: base()
        {
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);
        }
        protected virtual void InitializeEntity (bool addToManagers)
        {
            LoadStaticContent(ContentManagerName);
            BackgroundSprite = new FlatRedBall.Sprite();
            BackgroundSprite.Name = "BackgroundSprite";
            
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
            FlatRedBall.SpriteManager.AddToLayer(BackgroundSprite, LayerProvidedByContainer);
        }
        public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(BackgroundSprite, LayerProvidedByContainer);
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
            
            if (BackgroundSprite != null)
            {
                FlatRedBall.SpriteManager.RemoveSprite(BackgroundSprite);
            }
            CustomDestroy();
        }
        public virtual void PostInitialize ()
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            if (BackgroundSprite.Parent == null)
            {
                BackgroundSprite.CopyAbsoluteToRelative();
                BackgroundSprite.AttachTo(this, false);
            }
            if (BackgroundSprite.Parent == null)
            {
                BackgroundSprite.X = -161f;
            }
            else
            {
                BackgroundSprite.RelativeX = -161f;
            }
            if (BackgroundSprite.Parent == null)
            {
                BackgroundSprite.Y = 24f;
            }
            else
            {
                BackgroundSprite.RelativeY = 24f;
            }
            BackgroundSprite.Texture = sky1;
            BackgroundSprite.TextureScale = 1f;
            BackgroundSprite.UseAnimationRelativePosition = true;
            BackgroundSprite.Animate = true;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp (FlatRedBall.Graphics.Layer layerToAddTo)
        {
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers ()
        {
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            if (BackgroundSprite != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(BackgroundSprite);
            }
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements)
        {
            if (callOnContainedElements)
            {
            }
            if (BackgroundSprite.Parent == null)
            {
                BackgroundSprite.X = -161f;
            }
            else
            {
                BackgroundSprite.RelativeX = -161f;
            }
            if (BackgroundSprite.Parent == null)
            {
                BackgroundSprite.Y = 24f;
            }
            else
            {
                BackgroundSprite.RelativeY = 24f;
            }
            BackgroundSprite.Texture = sky1;
            BackgroundSprite.TextureScale = 1f;
            BackgroundSprite.UseAnimationRelativePosition = true;
            BackgroundSprite.Animate = true;
        }
        public virtual void ConvertToManuallyUpdated ()
        {
            this.ForceUpdateDependenciesDeep();
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(BackgroundSprite);
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
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("BackgroundStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
                if (!FlatRedBall.FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/sky1.png", ContentManagerName))
                {
                    registerUnload = true;
                }
                sky1 = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/sky1.png", ContentManagerName);
            }
            if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("BackgroundStaticUnload", UnloadStaticContent);
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
                if (sky1 != null)
                {
                    sky1= null;
                }
            }
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName)
        {
            switch(memberName)
            {
                case  "sky1":
                    return sky1;
            }
            return null;
        }
        public static object GetFile (string memberName)
        {
            switch(memberName)
            {
                case  "sky1":
                    return sky1;
            }
            return null;
        }
        object GetMember (string memberName)
        {
            switch(memberName)
            {
                case  "sky1":
                    return sky1;
            }
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
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(BackgroundSprite);
        }
        public virtual void MoveToLayer (FlatRedBall.Graphics.Layer layerToMoveTo)
        {
            var layerToRemoveFrom = LayerProvidedByContainer;
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(BackgroundSprite);
            }
            FlatRedBall.SpriteManager.AddToLayer(BackgroundSprite, layerToMoveTo);
            LayerProvidedByContainer = layerToMoveTo;
        }
    }
}
