using System;
using FlatRedBall.Debugging;

namespace SpaceShooter.Entities
{
    public partial class Enemy
	{
        private void CustomInitialize()
		{
            this.XVelocity = -this.EnemySpeed;
		}

		private void CustomActivity()
		{
            if (this.SpriteInstance.JustCycled && this.SpriteInstance.CurrentChainName == "Die")
            {
                Debugger.CommandLineWrite("Time to destroy");
                this.Destroy();
            }
        }

		private void CustomDestroy()
		{

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        // Set animation states
        public void SetState(string state)
        {
            Debugger.CommandLineWrite(String.Format("Changing state to: {0}", state));
            switch (state)
            {
                case "Flying":
                    this.SpriteInstance.CurrentChainName = "Flying";
                    break;

                case "Die":
                    this.SpriteInstance.CurrentChainName = "Die";
                    this.SpriteInstance.TextureScale = 3;
                    this.XVelocity = XVelocity / 1.25f; // slow it down
                    this.YVelocity = -50; // and fall down
                    break;
            }
        }
    }
}