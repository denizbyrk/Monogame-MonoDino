using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDino {
    public class Dino {

        //sprite for dino
        private Sprite dino;

        //animator
        private AnimationDino animator;

        //velocity (just used for Y, so you can just use a float value called velocity_Y)
        private Vector2 Velocity;

        //jump strength
        private float jumpStrength;

        //gravity
        private float gravity;

        //check jump and grounded
        private bool jump = false;
        private bool grounded = true;

        //width and height of the sprite (for hitbox)
        private int width = 88;
        private int height = 94;

        public Dino() {

            this.Velocity = Vector2.Zero;
            this.jumpStrength = 80000f;
            this.gravity = 4000f;
        }

        //load content
        public void LoadContent() {

            //load dino sprite
            string dinoPath = "Assets\\Dino";
            var dinoImg = Main.contentManager.Load<Texture2D>(dinoPath);
            var dinoPos = new Vector2(24, (Main.screenHeight / 2) - 64);

            this.dino = new Sprite(dinoImg, dinoPos);

            this.dino.Rectangle = new Rectangle(0, 0, this.width, this.height);

            this.animator = new AnimationDino();
        }

        //update
        public void Update(GameTime dt, int score) {

            float deltaTime = (float)dt.ElapsedGameTime.TotalSeconds;

            //set animation to idle when jumping
            if (this.jump == true && this.grounded == false) {

                this.animator.SetAnimation("Idle");
            }

            //set animation to run when on ground
            if (this.jump == false && this.grounded == true) {

                this.animator.SetAnimation("Run");
            }

            //set animation to crouch if grounded && down button is pressed
            if ((Input.IsKeyHold(Microsoft.Xna.Framework.Input.Keys.Down) && this.grounded == true)) {

                this.animator.SetAnimation("Crouch");

                //change the hitbox accordingly
                this.width = 118;
                this.height = 94;

            } else {

                this.width = 88;
            }

            //stop movement and change the animation to death
            if (Main.gameOver == true) {

                this.animator.SetAnimation("GameOver");
                this.Velocity.Y = 0;
                this.gravity = 0;
            }

            //allow movement if not game over
            if (Main.gameOver == false) {

                //press space or up to jump
                if ((Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) || Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up)) && this.jump == false && Main.resetGame == false) {

                    this.jump = true;
                    this.grounded = false;
                    this.Velocity.Y -= this.jumpStrength * deltaTime;

                    if (Main.gameStarted == true) Sounds.SoundEffectInstances["jump"].Play();
                }

                //if down is pressed while on air, get back down faster
                if (Input.IsKeyHold(Microsoft.Xna.Framework.Input.Keys.Down) && this.jump == true) {

                    this.Velocity.Y += 3 * this.gravity * deltaTime;
                }
            }

            //apply jump physics
            this.Velocity.Y += this.gravity * deltaTime;
            this.dino.Position.Y += this.Velocity.Y * deltaTime;

            //don't let the dino to go below ground
            if (this.dino.Position.Y >= Main.screenHeight / 2 - 64) {

                this.dino.Position.Y = Main.screenHeight / 2 - 64;
                this.Velocity.Y = 0;
                this.jump = false;
                this.grounded = true;
            }

            //animate the dino
            if(Main.gameStarted == true) this.dino.Rectangle = this.animator.Animate(dt, this.width, this.height);

            if (Main.resetGame == true) {

                this.animator.SetAnimation("Run");

                this.dino.Position.Y = (Main.screenHeight / 2) - 64;

                this.gravity = 4000f;

                Main.gameStarted = true;
                Main.gameOver = false;
                Main.resetGame = false;
                score = 0;
            }
        }

        //draw dino
        public void Draw(SpriteBatch b) {

            b.Draw(this.dino.Texture, this.dino.Position, this.dino.Rectangle, this.dino.Hue, this.dino.Rotation,
                        this.dino.Origin, this.dino.Scale, this.dino.Effect, this.dino.Depth);
        }

        //get dino rectangle (for collision checking)
        public Rectangle GetRectangle() {

            return new Rectangle((int)this.dino.Position.X, (int)this.dino.Position.Y, this.dino.Rectangle.Width - 8, this.dino.Rectangle.Height - 16);
        }
    }
}