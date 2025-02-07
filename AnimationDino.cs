using Microsoft.Xna.Framework;

namespace MonoDino {
    public class AnimationDino {

        //create animator
        private Animation Animator;

        public AnimationDino() {

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

                case "Idle":
                    this.SetAnimationProperties(0, 0, 0f);
                    break;
                case "Run":
                    this.SetAnimationProperties(1, 1, 0.1f);
                    break;
                case "Crouch":
                    this.SetAnimationProperties(2, 1, 0.1f);
                    break;
                case "GameOver":
                    this.SetAnimationProperties(3, 0, 0f);
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