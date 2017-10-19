#if ANDROID || IOS
// Android doesn't allow background loading. iOS doesn't allow background rendering (which is used by converting textures to use premult alpha)
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif
using System.Collections.Generic;
using System.Threading;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using FlatRedBall.Localization;

namespace SpaceShooter
{
    public static partial class GlobalContent
    {
        
        public static Microsoft.Xna.Framework.Graphics.Texture2D sky1 { get; set; }
        public static Microsoft.Xna.Framework.Graphics.Texture2D M484BulletCollection1 { get; set; }
        public static Microsoft.Xna.Framework.Graphics.Texture2D Explosion { get; set; }
        public static Microsoft.Xna.Framework.Graphics.Texture2D tiny_ship { get; set; }
        [System.Obsolete("Use GetFile instead")]
        public static object GetStaticMember (string memberName)
        {
            switch(memberName)
            {
                case  "sky1":
                    return sky1;
                case  "M484BulletCollection1":
                    return M484BulletCollection1;
                case  "Explosion":
                    return Explosion;
                case  "tiny_ship":
                    return tiny_ship;
            }
            return null;
        }
        public static object GetFile (string memberName)
        {
            switch(memberName)
            {
                case  "sky1":
                    return sky1;
                case  "M484BulletCollection1":
                    return M484BulletCollection1;
                case  "Explosion":
                    return Explosion;
                case  "tiny_ship":
                    return tiny_ship;
            }
            return null;
        }
        public static bool IsInitialized { get; private set; }
        public static bool ShouldStopLoading { get; set; }
        const string ContentManagerName = "Global";
        public static void Initialize ()
        {
            
            sky1 = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/sky1.png", ContentManagerName);
            M484BulletCollection1 = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/m484bulletcollection1.png", ContentManagerName);
            Explosion = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/explosion.png", ContentManagerName);
            tiny_ship = FlatRedBall.FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/globalcontent/tiny_ship.png", ContentManagerName);
            			IsInitialized = true;
            #if DEBUG && WINDOWS
            InitializeFileWatch();
            #endif
        }
        public static void Reload (object whatToReload)
        {
        }
        #if DEBUG && WINDOWS
        static System.IO.FileSystemWatcher watcher;
        private static void InitializeFileWatch ()
        {
            string globalContent = FlatRedBall.IO.FileManager.RelativeDirectory + "content/globalcontent/";
            if (System.IO.Directory.Exists(globalContent))
            {
                watcher = new System.IO.FileSystemWatcher();
                watcher.Path = globalContent;
                watcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;
                watcher.Filter = "*.*";
                watcher.Changed += HandleFileChanged;
                watcher.EnableRaisingEvents = true;
            }
        }
        private static void HandleFileChanged (object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(500);
                var fullFileName = e.FullPath;
                var relativeFileName = FlatRedBall.IO.FileManager.MakeRelative(FlatRedBall.IO.FileManager.Standardize(fullFileName));
                if (relativeFileName == "content/globalcontent/sky1.png")
                {
                    Reload(sky1);
                }
                if (relativeFileName == "content/globalcontent/m484bulletcollection1.png")
                {
                    Reload(M484BulletCollection1);
                }
                if (relativeFileName == "content/globalcontent/explosion.png")
                {
                    Reload(Explosion);
                }
                if (relativeFileName == "content/globalcontent/tiny_ship.png")
                {
                    Reload(tiny_ship);
                }
            }
            catch{}
        }
        #endif
        
        
    }
}
