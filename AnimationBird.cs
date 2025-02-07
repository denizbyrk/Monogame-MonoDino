using Microsoft.Xna.Framework;

namespace MonoDino {
    public class AnimationBird {

        //create animator
        private Animation Animator;

        public AnimationBird() {

            this.Animator = new Animation();
        }

        //set animation properties
        private void SetAnimationProperties(int currentAnimation, int totalFrame, float speed) {

            this.Animator.currentAnimation = currentAnimation;
            this.Animator.totalFrame = totalFrame;
            this.Animator.speed = speed;
        }

        //set animation to play
        public void SetAnimation(string animationToPlay) {

            switch (animationToPlay) {

                case "Fly":
                    this.SetAnimationProperties(0, 1, 0.2f);
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("Invalid Animation");
                    break;
            }
        }

        //get the sprite to draw
        public Rectangle Animate(GameTime dt, int width, int height) {

            return Animator.AnimateSheet(dt, width, height);
        }
    }
}