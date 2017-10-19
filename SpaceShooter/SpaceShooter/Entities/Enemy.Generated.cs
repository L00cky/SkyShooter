#if ANDROID || IOS
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
using Color = Microsoft.Xna.Framework.Color;
using SpaceShooter.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using SpaceShooter.Performance;
using SpaceShooter.Entities;
using SpaceShooter.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
namespace SpaceShooter.Entities
{
    public partial class Enemy : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable, FlatRedBall.Performance.IPoolable, FlatRedBall.Math.Geometry.ICollidable
    {
        // This is made static so that static lazy-loaded content can access it.
        public static string ContentManagerName { get; set; }
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        static object mLockObject = new object();
        static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
        static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
        protected static Microsoft.Xna.Framework.Graphics.Texture2D ufo;
        protected static FlatRedBall.Graphics.Animation.AnimationChainList EnemyAnimation;
        protected static Microsoft.Xna.Framework.Graphics.Texture2D Explosion;
        
        private FlatRedBall.Sprite SpriteInstance;
        static float SpriteInstanceXReset;
        static float SpriteInstanceYReset;
        static float SpriteInstanceZReset;
        static float SpriteInstanceXVelocityReset;
        static float SpriteInstanceYVelocityReset;
        static float SpriteInstanceZVelocityReset;
        static float SpriteInstanceRotationXReset;
        static float SpriteInstanceRotationYReset;
        static float SpriteInstanceRotationZReset;
        static float SpriteInstanceRotationXVelocityReset;
        static float SpriteInstanceRotationYVelocityReset;
        static float SpriteInstanceRotationZVelocityReset;
        static float SpriteInstanceAlphaReset;
        static float SpriteInstanceAlphaRateReset;
        private FlatRedBall.Math.Geometry.AxisAlignedRectangle mAxisAlignedRectangleInstance;
        public FlatRedBall.Math.Geometry.AxisAlignedRectangle AxisAlignedRectangleInstance
        {
            get
            {
                return mAxisAlignedRectangleInstance;
            }
            private set
            {
                mAxisAlignedRectangleInstance = value;
            }
        }
        static float AxisAlignedRectangleInstanceXReset;
        static float AxisAlignedRectangleInstanceYReset;
        static float AxisAlignedRectangleInstanceZReset;
        static float AxisAlignedRectangleInstanceXVelocityReset;
        static float AxisAlignedRectangleInstanceYVelocityReset;
        static float AxisAlignedRectangleInstanceZVelocityReset;
        static float AxisAlignedRectangleInstanceRotationXReset;
        static float AxisAlignedRectangleInstanceRotationYReset;
        static float AxisAlignedRectangleInstanceRotationZReset;
        static float AxisAlignedRectangleInstanceRotationXVelocityReset;
        static float AxisAlignedRectangleInstanceRotationYVelocityReset;
        static float AxisAlignedRectangleInstanceRotationZVelocityReset;
        public float EnemySpeed = 150f;
        public string EnemyChain
        {
            get
            {
                return SpriteInstance.CurrentChainName;
            }
            set
            {
                SpriteInstance.CurrentChainName = value;
            }
        }
        public int Index { get; set; }
        public bool Used { get; set; }
        private FlatRedBall.Math.Geometry.ShapeCollection mGeneratedCollision;
        public FlatRedBall.Math.Geometry.ShapeCollection Collision
        {
            get
            {
                return mGeneratedCollision;
            }
        }
        protected FlatRedBall.Graphics.Layer LayerProvidedByContainer = null;
        public Enemy ()
        	: this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {
        }
        public Enemy (string contentManagerName)
        	: this(contentManagerName, true)
        {
        }
        public Enemy (string contentManagerName, bool addToManagers)
        	: base()
        {
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);
        }
        protected virtual void InitializeEntity (bool addToManagers)
        {
            LoadStaticContent(ContentManagerName);
            SpriteInstance = new FlatRedBall.Sprite();
            SpriteInstance.Name = "SpriteInstance";
            mAxisAlignedRectangleInstance = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
            mAxisAlignedRectangleInstance.Name = "mAxisAlignedRectangleInstance";
            
            PostInitialize();
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceXReset = SpriteInstance.X;
            }
            else
            {
                SpriteInstanceXReset = SpriteInstance.RelativeX;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceYReset = SpriteInstance.Y;
            }
            else
            {
                SpriteInstanceYReset = SpriteInstance.RelativeY;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceZReset = SpriteInstance.Z;
            }
            else
            {
                SpriteInstanceZReset = SpriteInstance.RelativeZ;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceXVelocityReset = SpriteInstance.XVelocity;
            }
            else
            {
                SpriteInstanceXVelocityReset = SpriteInstance.RelativeXVelocity;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceYVelocityReset = SpriteInstance.YVelocity;
            }
            else
            {
                SpriteInstanceYVelocityReset = SpriteInstance.RelativeYVelocity;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceZVelocityReset = SpriteInstance.ZVelocity;
            }
            else
            {
                SpriteInstanceZVelocityReset = SpriteInstance.RelativeZVelocity;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceRotationXReset = SpriteInstance.RotationX;
            }
            else
            {
                SpriteInstanceRotationXReset = SpriteInstance.RelativeRotationX;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceRotationYReset = SpriteInstance.RotationY;
            }
            else
            {
                SpriteInstanceRotationYReset = SpriteInstance.RelativeRotationY;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceRotationZReset = SpriteInstance.RotationZ;
            }
            else
            {
                SpriteInstanceRotationZReset = SpriteInstance.RelativeRotationZ;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceRotationXVelocityReset = SpriteInstance.RotationXVelocity;
            }
            else
            {
                SpriteInstanceRotationXVelocityReset = SpriteInstance.RelativeRotationXVelocity;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceRotationYVelocityReset = SpriteInstance.RotationYVelocity;
            }
            else
            {
                SpriteInstanceRotationYVelocityReset = SpriteInstance.RelativeRotationYVelocity;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstanceRotationZVelocityReset = SpriteInstance.RotationZVelocity;
            }
            else
            {
                SpriteInstanceRotationZVelocityReset = SpriteInstance.RelativeRotationZVelocity;
            }
            SpriteInstanceAlphaReset = SpriteInstance.Alpha;
            SpriteInstanceAlphaRateReset = SpriteInstance.AlphaRate;
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceXReset = AxisAlignedRectangleInstance.X;
            }
            else
            {
                AxisAlignedRectangleInstanceXReset = AxisAlignedRectangleInstance.RelativeX;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceYReset = AxisAlignedRectangleInstance.Y;
            }
            else
            {
                AxisAlignedRectangleInstanceYReset = AxisAlignedRectangleInstance.RelativeY;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceZReset = AxisAlignedRectangleInstance.Z;
            }
            else
            {
                AxisAlignedRectangleInstanceZReset = AxisAlignedRectangleInstance.RelativeZ;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceXVelocityReset = AxisAlignedRectangleInstance.XVelocity;
            }
            else
            {
                AxisAlignedRectangleInstanceXVelocityReset = AxisAlignedRectangleInstance.RelativeXVelocity;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceYVelocityReset = AxisAlignedRectangleInstance.YVelocity;
            }
            else
            {
                AxisAlignedRectangleInstanceYVelocityReset = AxisAlignedRectangleInstance.RelativeYVelocity;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceZVelocityReset = AxisAlignedRectangleInstance.ZVelocity;
            }
            else
            {
                AxisAlignedRectangleInstanceZVelocityReset = AxisAlignedRectangleInstance.RelativeZVelocity;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceRotationXReset = AxisAlignedRectangleInstance.RotationX;
            }
            else
            {
                AxisAlignedRectangleInstanceRotationXReset = AxisAlignedRectangleInstance.RelativeRotationX;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceRotationYReset = AxisAlignedRectangleInstance.RotationY;
            }
            else
            {
                AxisAlignedRectangleInstanceRotationYReset = AxisAlignedRectangleInstance.RelativeRotationY;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceRotationZReset = AxisAlignedRectangleInstance.RotationZ;
            }
            else
            {
                AxisAlignedRectangleInstanceRotationZReset = AxisAlignedRectangleInstance.RelativeRotationZ;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceRotationXVelocityReset = AxisAlignedRectangleInstance.RotationXVelocity;
            }
            else
            {
                AxisAlignedRectangleInstanceRotationXVelocityReset = AxisAlignedRectangleInstance.RelativeRotationXVelocity;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceRotationYVelocityReset = AxisAlignedRectangleInstance.RotationYVelocity;
            }
            else
            {
                AxisAlignedRectangleInstanceRotationYVelocityReset = AxisAlignedRectangleInstance.RelativeRotationYVelocity;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstanceRotationZVelocityReset = AxisAlignedRectangleInstance.RotationZVelocity;
            }
            else
            {
                AxisAlignedRectangleInstanceRotationZVelocityReset = AxisAlignedRectangleInstance.RelativeRotationZVelocity;
            }
            if (addToManagers)
            {
                AddToManagers(null);
            }
        }
        public virtual void ReAddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
        {
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mAxisAlignedRectangleInstance, LayerProvidedByContainer);
        }
        public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
        {
            PostInitialize();
            LayerProvidedByContainer = layerToAddTo;
            FlatRedBall.SpriteManager.AddPositionedObject(this);
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, LayerProvidedByContainer);
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(mAxisAlignedRectangleInstance, LayerProvidedByContainer);
            AddToManagersBottomUp(layerToAddTo);
            CustomInitialize();
        }
        public virtual void Activity ()
        {
            
            CustomActivity();
        }
        public virtual void Destroy ()
        {
            if (Used)
            {
                Factories.EnemyFactory.MakeUnused(this, false);
            }
            FlatRedBall.SpriteManager.RemovePositionedObject(this);
            
            if (SpriteInstance != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(SpriteInstance);
            }
            if (AxisAlignedRectangleInstance != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(AxisAlignedRectangleInstance);
            }
            mGeneratedCollision.RemoveFromManagers(clearThis: false);
            CustomDestroy();
        }
        public virtual void PostInitialize ()
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.CopyAbsoluteToRelative();
                SpriteInstance.AttachTo(this, false);
            }
            SpriteInstance.Texture = ufo;
            SpriteInstance.LeftTexturePixel = 22f;
            SpriteInstance.RightTexturePixel = 331f;
            SpriteInstance.TopTexturePixel = 22f;
            SpriteInstance.BottomTexturePixel = 229f;
            SpriteInstance.TextureScale = 0.125f;
            SpriteInstance.AnimationChains = EnemyAnimation;
            SpriteInstance.CurrentChainName = "Flying";
            #if FRB_MDX
            SpriteInstance.ColorOperation = Microsoft.DirectX.Direct3D.TextureOperation.Texture;
            #else
            SpriteInstance.ColorOperation = FlatRedBall.Graphics.ColorOperation.Texture;
            #endif
            if (mAxisAlignedRectangleInstance.Parent == null)
            {
                mAxisAlignedRectangleInstance.CopyAbsoluteToRelative();
                mAxisAlignedRectangleInstance.AttachTo(this, false);
            }
            AxisAlignedRectangleInstance.Width = 32f;
            AxisAlignedRectangleInstance.Height = 32f;
            AxisAlignedRectangleInstance.Visible = false;
            mGeneratedCollision = new FlatRedBall.Math.Geometry.ShapeCollection();
            mGeneratedCollision.AxisAlignedRectangles.AddOneWay(mAxisAlignedRectangleInstance);
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp (FlatRedBall.Graphics.Layer layerToAddTo)
        {
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers ()
        {
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            if (SpriteInstance != null)
            {
                FlatRedBall.SpriteManager.RemoveSpriteOneWay(SpriteInstance);
            }
            if (AxisAlignedRectangleInstance != null)
            {
                FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(AxisAlignedRectangleInstance);
            }
            mGeneratedCollision.RemoveFromManagers(clearThis: false);
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements)
        {
            if (callOnContainedElements)
            {
            }
            SpriteInstance.Texture = ufo;
            SpriteInstance.LeftTexturePixel = 22f;
            SpriteInstance.RightTexturePixel = 331f;
            SpriteInstance.TopTexturePixel = 22f;
            SpriteInstance.BottomTexturePixel = 229f;
            SpriteInstance.TextureScale = 0.125f;
            SpriteInstance.AnimationChains = EnemyAnimation;
            SpriteInstance.CurrentChainName = "Flying";
            #if FRB_MDX
            SpriteInstance.ColorOperation = Microsoft.DirectX.Direct3D.TextureOperation.Texture;
            #else
            SpriteInstance.ColorOperation = FlatRedBall.Graphics.ColorOperation.Texture;
            #endif
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.X = SpriteInstanceXReset;
            }
            else
            {
                SpriteInstance.RelativeX = SpriteInstanceXReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.Y = SpriteInstanceYReset;
            }
            else
            {
                SpriteInstance.RelativeY = SpriteInstanceYReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.Z = SpriteInstanceZReset;
            }
            else
            {
                SpriteInstance.RelativeZ = SpriteInstanceZReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.XVelocity = SpriteInstanceXVelocityReset;
            }
            else
            {
                SpriteInstance.RelativeXVelocity = SpriteInstanceXVelocityReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.YVelocity = SpriteInstanceYVelocityReset;
            }
            else
            {
                SpriteInstance.RelativeYVelocity = SpriteInstanceYVelocityReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.ZVelocity = SpriteInstanceZVelocityReset;
            }
            else
            {
                SpriteInstance.RelativeZVelocity = SpriteInstanceZVelocityReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.RotationX = SpriteInstanceRotationXReset;
            }
            else
            {
                SpriteInstance.RelativeRotationX = SpriteInstanceRotationXReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.RotationY = SpriteInstanceRotationYReset;
            }
            else
            {
                SpriteInstance.RelativeRotationY = SpriteInstanceRotationYReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.RotationZ = SpriteInstanceRotationZReset;
            }
            else
            {
                SpriteInstance.RelativeRotationZ = SpriteInstanceRotationZReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.RotationXVelocity = SpriteInstanceRotationXVelocityReset;
            }
            else
            {
                SpriteInstance.RelativeRotationXVelocity = SpriteInstanceRotationXVelocityReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.RotationYVelocity = SpriteInstanceRotationYVelocityReset;
            }
            else
            {
                SpriteInstance.RelativeRotationYVelocity = SpriteInstanceRotationYVelocityReset;
            }
            if (SpriteInstance.Parent == null)
            {
                SpriteInstance.RotationZVelocity = SpriteInstanceRotationZVelocityReset;
            }
            else
            {
                SpriteInstance.RelativeRotationZVelocity = SpriteInstanceRotationZVelocityReset;
            }
            SpriteInstance.Alpha = SpriteInstanceAlphaReset;
            SpriteInstance.AlphaRate = SpriteInstanceAlphaRateReset;
            AxisAlignedRectangleInstance.Width = 32f;
            AxisAlignedRectangleInstance.Height = 32f;
            AxisAlignedRectangleInstance.Visible = false;
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.X = AxisAlignedRectangleInstanceXReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeX = AxisAlignedRectangleInstanceXReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.Y = AxisAlignedRectangleInstanceYReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeY = AxisAlignedRectangleInstanceYReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.Z = AxisAlignedRectangleInstanceZReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeZ = AxisAlignedRectangleInstanceZReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.XVelocity = AxisAlignedRectangleInstanceXVelocityReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeXVelocity = AxisAlignedRectangleInstanceXVelocityReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.YVelocity = AxisAlignedRectangleInstanceYVelocityReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeYVelocity = AxisAlignedRectangleInstanceYVelocityReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.ZVelocity = AxisAlignedRectangleInstanceZVelocityReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeZVelocity = AxisAlignedRectangleInstanceZVelocityReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.RotationX = AxisAlignedRectangleInstanceRotationXReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeRotationX = AxisAlignedRectangleInstanceRotationXReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.RotationY = AxisAlignedRectangleInstanceRotationYReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeRotationY = AxisAlignedRectangleInstanceRotationYReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.RotationZ = AxisAlignedRectangleInstanceRotationZReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeRotationZ = AxisAlignedRectangleInstanceRotationZReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.RotationXVelocity = AxisAlignedRectangleInstanceRotationXVelocityReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeRotationXVelocity = AxisAlignedRectangleInstanceRotationXVelocityReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.RotationYVelocity = AxisAlignedRectangleInstanceRotationYVelocityReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeRotationYVelocity = AxisAlignedRectangleInstanceRotationYVelocityReset;
            }
            if (AxisAlignedRectangleInstance.Parent == null)
            {
                AxisAlignedRectangleInstance.RotationZVelocity = AxisAlignedRectangleInstanceRotationZVelocityReset;
            }
            else
            {
                AxisAlignedRectangleInstance.RelativeRotationZVelocity = AxisAlignedRectangleInstanceRotationZVelocityReset;
            }
            EnemySpeed = 150f;
            EnemyChain = "Flying";
        }
        public virtual void ConvertToManuallyUpdated ()
        {
            this.ForceUpdateDependenciesDeep();
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
            FlatRedBall.SpriteManager.ConvertToManuallyUpdated(SpriteInstance);
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
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("EnemyStaticUnload", UnloadStaticContent);
                        mRegisteredUnloads.Add(ContentManagerName);
                    }
                }
                if (!FlatRedBall.FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/ufo.png", ContentManagerName))
                {
                    registerUnload = true;
                }
                ufo = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/ufo.png", ContentManagerName);
                if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/enemy/enemyanimation.achx", ContentManagerName))
                {
                    registerUnload = true;
                }
                EnemyAnimation = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/enemy/enemyanimation.achx", ContentManagerName);
                if (!FlatRedBall.FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/enemy/explosion.png", ContentManagerName))
                {
                    registerUnload = true;
                }
                Explosion = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/enemy/explosion.png", ContentManagerName);
            }
            if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
            {
                lock (mLockObject)
                {
                    if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
                    {
                        FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("EnemyStaticUnload", UnloadStaticContent);
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
                if (ufo != null)
                {
                    ufo= null;
                }
                if (EnemyAnimation != null)
                {
                    EnemyAnimation= null;
                }
                if (Explosion != null)
                {
                    Explosion= null;
                }
            }
        }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName)
        {
            switch(memberName)
            {
                case  "ufo":
                    return ufo;
                case  "EnemyAnimation":
                    return EnemyAnimation;
                case  "Explosion":
                    return Explosion;
            }
            return null;
        }
        public static object GetFile (string memberName)
        {
            switch(memberName)
            {
                case  "ufo":
                    return ufo;
                case  "EnemyAnimation":
                    return EnemyAnimation;
                case  "Explosion":
                    return Explosion;
            }
            return null;
        }
        object GetMember (string memberName)
        {
            switch(memberName)
            {
                case  "ufo":
                    return ufo;
                case  "EnemyAnimation":
                    return EnemyAnimation;
                case  "Explosion":
                    return Explosion;
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
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(SpriteInstance);
            FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(AxisAlignedRectangleInstance);
        }
        public virtual void MoveToLayer (FlatRedBall.Graphics.Layer layerToMoveTo)
        {
            var layerToRemoveFrom = LayerProvidedByContainer;
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(SpriteInstance);
            }
            FlatRedBall.SpriteManager.AddToLayer(SpriteInstance, layerToMoveTo);
            if (layerToRemoveFrom != null)
            {
                layerToRemoveFrom.Remove(AxisAlignedRectangleInstance);
            }
            FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(AxisAlignedRectangleInstance, layerToMoveTo);
            LayerProvidedByContainer = layerToMoveTo;
        }
    }
}
