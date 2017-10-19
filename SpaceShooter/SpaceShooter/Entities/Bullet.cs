using FlatRedBall;
using FlatRedBall.Debugging;

namespace SpaceShooter.Entities
{
    public partial class Bullet
    {
        float bulletDestroyPosition = Camera.Main.OrthogonalWidth / 2;

        private void CustomInitialize()
		{
            this.XVelocity = this.BulletSpeed;
        }

        private void CustomActivity()
        {
            if (this.X > bulletDestroyPosition)
            {
                Debugger.CommandLineWrite("Bullet was destroyed");
                this.Destroy();
            }
        }

        private void CheckOutOfScreenBullets()
        {
            
        }

        private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
