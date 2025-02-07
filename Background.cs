using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MonoDino {
    public class Background {

        //create sprites for ground and clouds
        private List<Sprite> groundArray;
        private List<Sprite> cloudArray;
        private Texture2D cloudTexture;

        //min and max height for clouds
        private int maxCloudHeight;
        private int minCloudHeight;

        //min and max cloud spawn frequency
        private int minCloudFrequency;
        private int maxCloudFrequency;
        private int timer;

        public Background() {

            this.groundArray = new List<Sprite>();
            this.cloudArray = new List<Sprite>();

            this.minCloudHeight = Main.screenHeight / 3;
            this.maxCloudHeight = Main.screenHeight / 6;
            this.maxCloudFrequency = 50000;
            this.minCloudFrequency = 20000;
        }

        //load content
        public void LoadContent() {

            Sprite ground1, ground2;

            string GroundPath = "Assets\\Ground";
            var groundImg = Main.contentManager.Load<Texture2D>(GroundPath);
            var groundPos = new Vector2(0, (Main.screenHeight / 2) + 8);
            var groundPos2 = new Vector2(groundImg.Bounds.Width, (Main.screenHeight / 2) + 8);

            ground1 = new Sprite(groundImg, groundPos);
            ground2 = new Sprite(groundImg, groundPos2);

            this.groundArray.Add(ground1);
            this.groundArray.Add(ground2);

            string cloudPath = "Assets\\Cloud";
            this.cloudTexture = Main.contentManager.Load<Texture2D>(cloudPath);
        }

        //create cloud and add to array
        public void CreateCloud() {

            Sprite cloud;

            int cloudPosition = Main.random.Next(this.maxCloudHeight, this.minCloudHeight);
            var cloudPos = new Vector2(Main.screenWidth, cloudPosition);

            cloud = new Sprite(this.cloudTexture, cloudPos);
            this.cloudArray.Add(cloud);
        }

        //update
        public void Update(GameTime dt) {

            this.timer += (int)dt.ElapsedGameTime.TotalMilliseconds;

            //move the ground
            foreach (var g in this.groundArray) {

                var firstG = this.groundArray.First();
                var secondG = this.groundArray.Last();

                g.Position.X -= Main.gameSpeed;

                if (firstG.Position.X < -2385) {

                    firstG.Position.X = 0;
                    secondG.Position.X = firstG.Texture.Width;
                }
            }

            int randomFrequency = this.maxCloudFrequency + 1;

            //set random spawn time for the next cloud
            if (Main.gameStarted == true && Main.gameSpeed != 0) randomFrequency = Main.random.Next(this.minCloudFrequency / Main.gameSpeed, this.maxCloudFrequency / Main.gameSpeed);

            //create cloud
            if (this.timer > randomFrequency) {

                this.timer = 0;
                this.CreateCloud();
            }

            //move clouds
            foreach (var c in this.cloudArray) {

                c.Position.X -= Main.gameSpeed / 3;
            }

            if (this.cloudArray.Count > 0) {

                //remove cloud if it goes outside of the screen
                if (this.cloudArray.First().Position.X < 0 - this.cloudTexture.Width) {

                    this.cloudArray.Remove(this.cloudArray.First());
                }
            }

            //clear the clouds on game reset
            if (Main.resetGame) {

                this.cloudArray.Clear();
            }
        }

        //draw
        public void Draw(SpriteBatch b) {

            //draw ground(s)
            foreach (var g in this.groundArray) {

                b.Draw(g.Texture, g.Position, null, g.Hue, g.Rotation,
                          g.Origin, g.Scale, g.Effect, g.Depth);
            }

            //draw cloud(s)
            foreach (var c in this.cloudArray) {

                b.Draw(c.Texture, c.Position, null, c.Hue, c.Rotation,
                          c.Origin, c.Scale, c.Effect, c.Depth);
            }
        }
    }
}