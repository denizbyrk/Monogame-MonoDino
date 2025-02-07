using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDino {
    public class Level : GameState {

        //the rectangle that goes to the right after starting the game
        private Rectangle WhiteRectangle;

        //level objects
        private Score Score;
        private Cactus Cactus;
        private Bird Bird;
        private Background BG;
        private Dino Dino;

        //timer for WhiteRectangle's movement
        private float timer = 0f;

        public Level() {

            this.WhiteRectangle = new Rectangle(132, 0, Main.screenWidth, Main.screenHeight);

            this.Score = new Score();
            this.BG = new Background();
            this.Dino = new Dino();
            this.Cactus = new Cactus();
            this.Bird = new Bird();
        }

        //load content
        public override void LoadContent() {
           
            this.Score.LoadContent();
            this.BG.LoadContent();
            this.Cactus.LoadContent();
            this.Bird.LoadContent();
            this.Dino.LoadContent();
        }

        //update
        public override void Update(GameTime dt) {

            //start the game
            if (Main.gameStarted == false) {

                if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) || Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up)) {

                    Main.cutscene = true;
                }
            }

            //begin running after the white rectangle is outside of the screen
            if (this.WhiteRectangle.X >= Main.screenWidth && Main.cutscene == true) {

                Main.gameSpeed = 15;
                Main.gameStarted = true;
                Main.cutscene = false;
            }

            //white rectangle movement
            if (Main.cutscene == true) {

                this.timer += (float)dt.ElapsedGameTime.TotalSeconds;

                if (this.timer > 0.66f) {

                    this.WhiteRectangle.X += 64;
                }
            }

            //update score
            this.Score.Update(dt);

            //update background
            this.BG.Update(dt);

            //update cactus
            this.Cactus.Update(dt);

            //update bird
            this.Bird.Update(dt, this.Score.score);

            //update dino
            this.Dino.Update(dt, this.Score.score);

            //check cactus collision
            if (Main.gameStarted && Cactus.checkArray() && Main.gameOver != true) {

                if (this.Dino.GetRectangle().Intersects(this.Cactus.getCactus())) {

                    Sounds.SoundEffectInstances["die"].Play();

                    Main.gameSpeed = 0;
                    Main.gameOver = true;
                }
            }

            //check bird collision
            if (Main.gameStarted && Bird.checkArray() && Main.gameOver != true) {

                if (this.Dino.GetRectangle().Intersects(this.Bird.getBird())) {

                    Sounds.SoundEffectInstances["die"].Play();

                    Main.gameSpeed = 0;
                    Main.gameOver = true;
                }
            }
        }

        //draw
        public override void Draw(SpriteBatch b) {

            //draw background
            this.BG.Draw(b);

            //draw score
            this.Score.Draw(b);

            //draw WhiteRectangle
            if (Main.gameStarted == false) {

                b.Draw(Main.p, this.WhiteRectangle, Color.White);
            }

            //draw cactus
            this.Cactus.Draw(b);

            //draw bird
            this.Bird.Draw(b);

            //draw dino
            this.Dino.Draw(b);
        }
    }
}