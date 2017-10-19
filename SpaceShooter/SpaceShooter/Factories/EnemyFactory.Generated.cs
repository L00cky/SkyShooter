using SpaceShooter.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using SpaceShooter.Performance;

namespace SpaceShooter.Factories
{
    public class EnemyFactory : IEntityFactory
    {
        public static FlatRedBall.Math.Axis? SortAxis { get; set;}
        public static Enemy CreateNew (float x = 0, float y = 0)
        {
            return CreateNew(null, x, y);
        }
        public static Enemy CreateNew (Layer layer, float x = 0, float y = 0)
        {
            if (string.IsNullOrEmpty(mContentManagerName))
            {
                throw new System.Exception("You must first initialize the factory to use it. You can either add PositionedObjectList of type Enemy (the most common solution) or call Initialize in custom code");
            }
            Enemy instance = null;
            instance = mPool.GetNextAvailable();
            if (instance == null)
            {
                mPool.AddToPool(new Enemy(mContentManagerName, false));
                instance =  mPool.GetNextAvailable();
            }
            instance.AddToManagers(layer);
            instance.X = x;
            instance.Y = y;
            if (mScreenListReference != null)
            {
                if (SortAxis == FlatRedBall.Math.Axis.X)
                {
                    var index = mScreenListReference.GetFirstAfter(x, Axis.X, 0, mScreenListReference.Count);
                    mScreenListReference.Insert(index, instance);
                }
                else if (SortAxis == FlatRedBall.Math.Axis.Y)
                {
                    var index = mScreenListReference.GetFirstAfter(y, Axis.Y, 0, mScreenListReference.Count);
                    mScreenListReference.Insert(index, instance);
                }
                else
                {
                    // Sort Z not supported
                    mScreenListReference.Add(instance);
                }
            }
            if (EntitySpawned != null)
            {
                EntitySpawned(instance);
            }
            return instance;
        }
        
        public static void Initialize (FlatRedBall.Math.PositionedObjectList<Enemy> listFromScreen, string contentManager)
        {
            mContentManagerName = contentManager;
            mScreenListReference = listFromScreen;
            FactoryInitialize();
        }
        
        public static void Destroy ()
        {
            mContentManagerName = null;
            mScreenListReference = null;
            SortAxis = null;
            mPool.Clear();
            EntitySpawned = null;
        }
        
        private static void FactoryInitialize ()
        {
            const int numberToPreAllocate = 20;
            for (int i = 0; i < numberToPreAllocate; i++)
            {
                Enemy instance = new Enemy(mContentManagerName, false);
                mPool.AddToPool(instance);
            }
        }
        
        /// <summary>
        /// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
        /// by generated code.  Use Destroy instead when writing custom code so that your code will behave
        /// the same whether your Entity is pooled or not.
        /// </summary>
        public static void MakeUnused (Enemy objectToMakeUnused)
        {
            MakeUnused(objectToMakeUnused, true);
        }
        
        /// <summary>
        /// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
        /// by generated code.  Use Destroy instead when writing custom code so that your code will behave
        /// the same whether your Entity is pooled or not.
        /// </summary>
        public static void MakeUnused (Enemy objectToMakeUnused, bool callDestroy)
        {
            mPool.MakeUnused(objectToMakeUnused);
            
            if (callDestroy)
            {
                objectToMakeUnused.Destroy();
            }
        }
        
        
            static string mContentManagerName;
            static PositionedObjectList<Enemy> mScreenListReference;
            static PoolList<Enemy> mPool = new PoolList<Enemy>();
            public static Action<Enemy> EntitySpawned;
            object IEntityFactory.CreateNew ()
            {
                return EnemyFactory.CreateNew();
            }
            object IEntityFactory.CreateNew (Layer layer)
            {
                return EnemyFactory.CreateNew(layer);
            }
            public static FlatRedBall.Math.PositionedObjectList<Enemy> ScreenListReference
            {
                get
                {
                    return mScreenListReference;
                }
                set
                {
                    mScreenListReference = value;
                }
            }
            static EnemyFactory mSelf;
            public static EnemyFactory Self
            {
                get
                {
                    if (mSelf == null)
                    {
                        mSelf = new EnemyFactory();
                    }
                    return mSelf;
                }
            }
    }
}
