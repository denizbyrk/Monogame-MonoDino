using Microsoft.Xna.Framework;

namespace MonoDino {
    public class Animation {

        public int currentAnimation; //current animation index
        public int currentFrame; //current frame index
        public int totalFrame; //total frames of the animation
        public float speed; //animation speed
        public bool looping; //looping
        private float timer;

        public Animation() {

            this.currentFrame = 0;
            this.looping = true;
        }

        //animate
        private void Animate(GameTime dt) {

            this.timer += (float)dt.ElapsedGameTime.TotalSeconds;

            if (this.timer > this.speed) {

                this.timer = 0f;
                this.currentFrame++;

                if ((this.currentFrame > this.totalFrame) && looping == true) this.currentFrame = 0;
            }
        }

        //draw sprite
        public Rectangle AnimateSheet(GameTime dt, int width, int height) {

            this.Animate(dt);

            return new Rectangle(this.currentFrame * width, this.currentAnimation * height, width, height);
        }
    }
}