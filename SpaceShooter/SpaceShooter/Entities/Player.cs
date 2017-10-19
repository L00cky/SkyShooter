using FlatRedBall;
using FlatRedBall.Input;
using Microsoft.Xna.Framework.Input;
using SpaceShooter.Factories;

namespace SpaceShooter.Entities
{
    public partial class Player
	{
        public Player instance { get; set; }
        public I2DInput PlayerInput { get; set; }
        public IPressableInput Shoot { get; set; }

        private bool CanShoot { get; set; }
        private double LastShotFired { get; set; }
        private float SecondsToShoot { get; set; }

		private void CustomInitialize()
		{
            CanShoot = true;

            SecondsToShoot = 0.25f;

            AssignInput();
        }

		private void CustomActivity()
		{            
            HadleInput();
            HandleShooting();
        }

        private void HandleShooting()
        {
            if(TimeManager.SecondsSince(LastShotFired) > SecondsToShoot)
            {
                CanShoot = true;
            }
        }

        private void HadleInput()
        {
            this.XAcceleration = PlayerInput.X * MaxMovementSpeed;
            this.YAcceleration = PlayerInput.Y * MaxMovementSpeed;

            if (this.Shoot.IsDown && CanShoot)
            {
                BulletFactory.CreateNew(this.X, this.Y);
                LastShotFired = TimeManager.CurrentTime;
                CanShoot = false;
            }
        }

        private void AssignInput()
        {
            PlayerInput = InputManager.Keyboard.Get2DInput(Keys.A, Keys.D, Keys.W, Keys.S);
            Shoot = InputManager.Keyboard.GetKey(Keys.Space);
        }

        private void CustomDestroy()
		{
            CanShoot = false;
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
