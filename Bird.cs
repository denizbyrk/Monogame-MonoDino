using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MonoDino {
    public class Bird {

        //bird array for storing spawned birds
        private List<Sprite> birdArray;

        //create animator
        private AnimationBird animator;

        //texture for bird
        private Texture2D birdTexture;

        //check if a bird has spawned, used to delay cactus spawn, so they don't spawn at the same time
        public static bool birdSpawned = false;

        //the height of the bird's flight
        private int birdHeight;

        //speed of the bird
        private int birdSpeed;

        //max and min time for spawning
        private int minTime = 4000;
        private int maxTime = 9600;

        //timer for spawning
        private int timer;
 
        public Bird() {

            this.birdArray = new List<Sprite>();
        }

        //load content
        public void LoadContent() {

            this.birdTexture = Main.contentManager.Load<Texture2D>("Assets\\Bird");

            this.animator = new AnimationBird();
        }

        //spawn bird
        private void spawnBird() {

            Bird.birdSpawned = true;

            //get random height, from 3 different levels
            this.birdHeight = Main.random.Next(1, 4);

            //get random speed, from 3 different levels
            this.birdSpeed = Main.random.Next(1, 4);

            //create bird sprite
            Sprite bird;
            var birdPosition = new Vector2(Main.screenWidth, Main.screenHeight / 2 - (48 * this.birdHeight));
            bird = new Sprite(this.birdTexture, birdPosition);

            //add bird to the array
            this.birdArray.Add(bird);

            Bird.birdSpawned = false;
        }

        //update
        public void Update(GameTime dt, int score) {

            //clear bird array on game reset
            if (Main.resetGame == true) {

                this.birdArray.Clear();
            }

            //get random time for spawning
            int randomTime = Main.random.Next(this.minTime, this.maxTime);

            //the birds spawn after the score is higher than 200
            if (Main.gameStarted && score > 200) {

                this.timer += (int)dt.ElapsedGameTime.TotalMilliseconds;

                //if a cactus has spawned, reset the timer to delay the spawn, preventing bird and cactus spawn on the same time
                if (Cactus.cactusSpawned == true) this.timer = -2000;

                if (this.timer > randomTime) {

                    this.spawnBird();
                    this.timer = 0;
                }
            }

            double speed;

            foreach (var bird in this.birdArray) {

                //animate the bird
                if (Main.gameOver == false) {

                    //if not game over, keep animating the bird
                    this.animator.SetAnimation("Fly");
                    bird.Rectangle = this.animator.Animate(dt, 92, 80);
                } else {

                    //on player death, stop bird animation
                    bird.Rectangle = new Rectangle(0, 0, 92, 80);
                }

                //set the moving speed of the bird
                switch (this.birdSpeed) {

                    case 1:
                        speed = 1.125;
                        break;
                    case 2:
                        speed = 1.25;
                        break;
                    case 3:
                        speed = 1.375;
                        break;

                    default:
                        speed = 1.25;
                        break;
                }

                //move bird
                bird.Position.X -= Main.gameSpeed * (float)speed;
            }

            //remove bird from array if it is outside of the screen
            if (this.birdArray.Count > 0) {

                if (this.birdArray.First().Position.X < 0 - this.birdTexture.Width) {
                    
                    this.birdArray.Remove(this.birdArray.First());
                }
            }
        }

        //draw bird(s)
        public void Draw(SpriteBatch b) {

            foreach (var bird in this.birdArray) {

                b.Draw(bird.Texture, bird.Position, bird.Rectangle, bird.Hue, bird.Rotation,
                            bird.Origin, bird.Scale, bird.Effect, bird.Depth);
            }
        }

        //check if any birds have spawned
        public bool checkArray() {

            if (this.birdArray.Count == 0) return false;

            if (this.birdArray.Count > 0) return true;

            return false;
        }

        //get bird rectangle (for collision checking)
        public Rectangle getBird() {

            return new Rectangle((int)this.birdArray.First().Position.X, (int)this.birdArray.First().Position.Y,
                                    this.birdArray.First().Rectangle.Width, this.birdArray.First().Rectangle.Height - 72);
        }
    }
}