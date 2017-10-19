#if ANDROID || IOS
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
using Color = Microsoft.Xna.Framework.Color;
using SpaceShooter.Entities;
using SpaceShooter.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math;
namespace SpaceShooter.Screens
{
    public partial class GameScreen : FlatRedBall.Screens.Screen
    {
        #if DEBUG
        static bool HasBeenLoadedWithGlobalContentManager = false;
        #endif
        
        private SpaceShooter.Entities.Background BackgroundInstance;
        private SpaceShooter.Entities.Player PlayerInstance;
        private FlatRedBall.Math.PositionedObjectList<SpaceShooter.Entities.Bullet> BulletList;
        private FlatRedBall.Math.PositionedObjectList<SpaceShooter.Entities.Enemy> EnemyList;
        private SpaceShooter.Entities.Scoreboard ScoreboardInstance;
        public GameScreen ()
        	: base ("GameScreen")
        {
        }
        public override void Initialize (bool addToManagers)
        {
            LoadStaticContent(ContentManagerName);
            BackgroundInstance = new SpaceShooter.Entities.Background(ContentManagerName, false);
            BackgroundInstance.Name = "BackgroundInstance";
            PlayerInstance = new SpaceShooter.Entities.Player(ContentManagerName, false);
            PlayerInstance.Name = "PlayerInstance";
            BulletList = new FlatRedBall.Math.PositionedObjectList<SpaceShooter.Entities.Bullet>();
            BulletList.Name = "BulletList";
            EnemyList = new FlatRedBall.Math.PositionedObjectList<SpaceShooter.Entities.Enemy>();
            EnemyList.Name = "EnemyList";
            ScoreboardInstance = new SpaceShooter.Entities.Scoreboard(ContentManagerName, false);
            ScoreboardInstance.Name = "ScoreboardInstance";
            
            
            PostInitialize();
            base.Initialize(addToManagers);
            if (addToManagers)
            {
                AddToManagers();
            }
        }
        public override void AddToManagers ()
        {
            BulletFactory.Initialize(BulletList, ContentManagerName);
            EnemyFactory.Initialize(EnemyList, ContentManagerName);
            BackgroundInstance.AddToManagers(mLayer);
            PlayerInstance.AddToManagers(mLayer);
            ScoreboardInstance.AddToManagers(mLayer);
            base.AddToManagers();
            AddToManagersBottomUp();
            CustomInitialize();
        }
        public override void Activity (bool firstTimeCalled)
        {
            if (!IsPaused)
            {
                
                BackgroundInstance.Activity();
                PlayerInstance.Activity();
                for (int i = BulletList.Count - 1; i > -1; i--)
                {
                    if (i < BulletList.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        BulletList[i].Activity();
                    }
                }
                for (int i = EnemyList.Count - 1; i > -1; i--)
                {
                    if (i < EnemyList.Count)
                    {
                        // We do the extra if-check because activity could destroy any number of entities
                        EnemyList[i].Activity();
                    }
                }
                ScoreboardInstance.Activity();
            }
            else
            {
            }
            base.Activity(firstTimeCalled);
            if (!IsActivityFinished)
            {
                CustomActivity(firstTimeCalled);
            }
        }
        public override void Destroy ()
        {
            base.Destroy();
            BulletFactory.Destroy();
            EnemyFactory.Destroy();
            
            BulletList.MakeOneWay();
            EnemyList.MakeOneWay();
            if (BackgroundInstance != null)
            {
                BackgroundInstance.Destroy();
                BackgroundInstance.Detach();
            }
            if (PlayerInstance != null)
            {
                PlayerInstance.Destroy();
                PlayerInstance.Detach();
            }
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                BulletList[i].Destroy();
            }
            for (int i = EnemyList.Count - 1; i > -1; i--)
            {
                EnemyList[i].Destroy();
            }
            if (ScoreboardInstance != null)
            {
                ScoreboardInstance.Destroy();
                ScoreboardInstance.Detach();
            }
            BulletList.MakeTwoWay();
            EnemyList.MakeTwoWay();
            CustomDestroy();
        }
        public virtual void PostInitialize ()
        {
            bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
            if (PlayerInstance.Parent == null)
            {
                PlayerInstance.X = -350f;
            }
            else
            {
                PlayerInstance.RelativeX = -350f;
            }
            FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
        }
        public virtual void AddToManagersBottomUp ()
        {
            CameraSetup.ResetCamera(SpriteManager.Camera);
            AssignCustomVariables(false);
        }
        public virtual void RemoveFromManagers ()
        {
            BackgroundInstance.RemoveFromManagers();
            PlayerInstance.RemoveFromManagers();
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                BulletList[i].Destroy();
            }
            for (int i = EnemyList.Count - 1; i > -1; i--)
            {
                EnemyList[i].Destroy();
            }
            ScoreboardInstance.RemoveFromManagers();
        }
        public virtual void AssignCustomVariables (bool callOnContainedElements)
        {
            if (callOnContainedElements)
            {
                BackgroundInstance.AssignCustomVariables(true);
                PlayerInstance.AssignCustomVariables(true);
                ScoreboardInstance.AssignCustomVariables(true);
            }
            if (PlayerInstance.Parent == null)
            {
                PlayerInstance.X = -350f;
            }
            else
            {
                PlayerInstance.RelativeX = -350f;
            }
        }
        public virtual void ConvertToManuallyUpdated ()
        {
            BackgroundInstance.ConvertToManuallyUpdated();
            PlayerInstance.ConvertToManuallyUpdated();
            for (int i = 0; i < BulletList.Count; i++)
            {
                BulletList[i].ConvertToManuallyUpdated();
            }
            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].ConvertToManuallyUpdated();
            }
            ScoreboardInstance.ConvertToManuallyUpdated();
        }
        public static void LoadStaticContent (string contentManagerName)
        {
            if (string.IsNullOrEmpty(contentManagerName))
            {
                throw new System.ArgumentException("contentManagerName cannot be empty or null");
            }
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
            SpaceShooter.Entities.Background.LoadStaticContent(contentManagerName);
            SpaceShooter.Entities.Player.LoadStaticContent(contentManagerName);
            SpaceShooter.Entities.Scoreboard.LoadStaticContent(contentManagerName);
            CustomLoadStaticContent(contentManagerName);
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
    }
}
