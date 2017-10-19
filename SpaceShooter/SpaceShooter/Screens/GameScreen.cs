
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework.Input;

using SpaceShooter.Entities;
using FlatRedBall.Debugging;

namespace SpaceShooter.Screens
{
    public partial class GameScreen
	{

        public IPressableInput QuitGame { get; set; }
        public float RightBorder = Camera.Main.OrthogonalWidth / 2;
        public float LeftBorder = -(Camera.Main.OrthogonalWidth / 2);

        private int maxEnemies = 3;
        private int currentEnemies = 0;

        void CustomInitialize()
		{
            AssignGeneralInput();
            ScoreboardInstance.Score = 0;
        }

		void CustomActivity(bool firstTimeCalled)
		{
            if (firstTimeCalled)
            {

            }

            HandleInput();

            if(currentEnemies <= maxEnemies)
            {
                SpawnEnemies();
            }

            HandleCollisions();
        }

        private void HandleCollisions()
        {
            foreach(var enemy in EnemyList)
            {
                if (PlayerInstance.CollideAgainst(enemy))
                {
                    PlayerInstance.Destroy();
                }

                if (enemy.CollideAgainst<Bullet>(BulletList))
                {
                    enemy.SetState("Die");
                    ScoreboardInstance.Score = ScoreboardInstance.Score + 1;
                    break;
                }
            }
        }

        void CustomDestroy()
		{
            
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void HandleInput()
        {
            if (this.QuitGame.WasJustPressed)
            {
                FlatRedBallServices.Game.Exit();
            }
        }

        private void AssignGeneralInput()
        {
            QuitGame = InputManager.Keyboard.GetKey(Keys.Escape);
        }

        private void SpawnEnemies()
        {
            for(int i = 0; i < maxEnemies; i++)
            {
                Debugger.CommandLineWrite("Spawned Enemy");

                var x = FlatRedBallServices.Random.Next((int)RightBorder, (int)RightBorder + 200);
                var y = FlatRedBallServices.Random.Next(-270, 270);
                
                Factories.EnemyFactory.CreateNew(x, y);
                currentEnemies++;
            }
            
        }
    }
}
