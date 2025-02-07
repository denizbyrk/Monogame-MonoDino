using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MonoDino {
    public class Cactus {

        //cactus array for storing cactuses spawned
        private List<Sprite> cactusArray;

        //cactus texture
        private Texture2D cactusTexture;

        //check if a cactus has spawned, used to delay bird spawn, so they don't spawn at the same time
        public static bool cactusSpawned = false;
        private int cactusStyle;
        private int tempCactusStyle;
        private int timer;
        private int width;
        private int height;
        private int minTime = 1000;
        private int maxTime = 2500;
 
        public Cactus() {

            this.timer = 0;
        }

        public void LoadContent() {

            this.cactusArray = new List<Sprite>();
            this.cactusTexture = Main.contentManager.Load<Texture2D>("Assets\\Cactus");
        }

        private void spawnCactus() {

            Cactus.cactusSpawned = true;

            Sprite cactus;
            var cactusPos = new Vector2(Main.screenWidth, (Main.screenHeight / 2) - 40);
            cactus = new Sprite(this.cactusTexture, cactusPos);

            this.cactusStyle = Main.random.Next(0, 6);

            if(this.cactusStyle == this.tempCactusStyle) {

                this.cactusStyle++;
            }

            if(this.cactusStyle > 5) {

                this.cactusStyle = 0;
            }

            this.tempCactusStyle = this.cactusStyle;

            switch (this.cactusStyle) {

                case 0:
                    this.width = 34;
                    this.height = 70;
                    break;
                case 1:
                    this.width = 68;
                    this.height = 70;
                    break;
                case 2:
                    this.width = 102;
                    this.height = 70;
                    break;
                case 3:
                    this.width = 50;
                    this.height = 100;
                    break;
                case 4:
                    this.width = 100;
                    this.height = 100;
                    break;
                case 5:
                    this.width = 150;
                    this.height = 100;
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("Invalid");
                    break;
            }

            if (this.cactusStyle == 0 || this.cactusStyle == 1 || this.cactusStyle == 2) {

                cactus.Rectangle = new Rectangle(0, 70 * this.cactusStyle, this.width, this.height);
            } else {

                cactus.Rectangle = new Rectangle(0, 210 + (100 * ((this.cactusStyle) - 3)), this.width, this.height);
                cactus.Position.Y -= 24;
            }

            this.cactusArray.Add(cactus);

            Cactus.cactusSpawned = false;
        }

        private void checkGameSpeed() {

            switch (Main.gameSpeed) {

                case 15:
                    this.minTime = 1000;
                    this.maxTime = 2500;
                    break;
                case 20:
                    this.minTime = 800;
                    this.maxTime = 2200;
                    break;
                case 22:
                    this.minTime = 600;
                    this.maxTime = 1800;
                    break;

                default:
                    if (Main.gameSpeed > 22) {
                        this.minTime = 500;
                        this.maxTime = 1500;
                    } else {
                        this.minTime = 1000;
                        this.maxTime = 2500;
                    }

                    break;
            }
        }

        public void Update(GameTime dt) {

            if (Main.resetGame) {

                this.cactusArray.Clear();
            }

            if (Main.gameStarted) {

                this.checkGameSpeed();

                this.timer += (int)dt.ElapsedGameTime.TotalMilliseconds;

                if (Bird.birdSpawned == true) this.timer = -1000;

                int randomTime = Main.random.Next(this.minTime, this.maxTime);

                if (this.timer > randomTime) {

                    this.spawnCactus();
                    this.timer = 0;
                }
            }

            foreach (var c in this.cactusArray) {

                c.Position.X -= Main.gameSpeed;
            }

            if (this.cactusArray.Count > 0) {

                if (this.cactusArray.First().Position.X < 0 - this.cactusArray.First().Rectangle.Width) {

                    this.cactusArray.Remove(this.cactusArray.First());
                }
            }
        }

        public void Draw(SpriteBatch b) {
        
            foreach(var c in this.cactusArray) {

                b.Draw(c.Texture, c.Position, c.Rectangle, c.Hue, c.Rotation, c.Origin, c.Scale, c.Effect, c.Depth);
            }
        }

        public bool checkArray() {

            if (this.cactusArray.Count == 0) return false;

            if (this.cactusArray.Count > 0) return true;

            return false;
        }

        public Rectangle getCactus() {

            return new Rectangle((int)this.cactusArray.First().Position.X, (int)this.cactusArray.First().Position.Y, 
                                            this.cactusArray.First().Rectangle.Width, this.cactusArray.First().Rectangle.Height);
        }
    }
}