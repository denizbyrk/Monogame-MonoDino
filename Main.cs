using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoDino {
    public class Main : Game {

        //running speed
        public static int gameSpeed = 0;

        //create graphics manager, content manager, sprite batch for drawing, and p(pixel) for shape drawing
        public static GraphicsDeviceManager graphics;
        public static ContentManager contentManager;
        public static Texture2D p;
        private SpriteBatch b;

        //define window width and height
        public const int screenWidth = 1280;
        public const int screenHeight = 720;
          
        //current game state, this game has just one state, so you can just use Level object instead
        private GameState currentState;

        //check for game events
        public static bool gameStarted = false;
        public static bool cutscene = false;
        public static bool gameOver = false;
        public static bool resetGame = false;

        //create object for generating random values
        public static Random random = new Random();

        //textures for game over button and text
        private Texture2D gameOverTexture;
        private Texture2D gameOverText;

        public Main() {

            Main.graphics = new GraphicsDeviceManager(this);
            Main.graphics.SynchronizeWithVerticalRetrace = false;

            this.Window.AllowAltF4 = true;
            this.Window.AllowUserResizing = false;
            
            this.IsFixedTimeStep = true;
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize() {

            //set window width and height
            Main.graphics.PreferredBackBufferWidth = Main.screenWidth;
            Main.graphics.PreferredBackBufferHeight = Main.screenHeight;
            Main.graphics.ApplyChanges();

            this.currentState = new Level();

            base.Initialize();
        }

        //load content
        protected override void LoadContent() {

            this.b = new SpriteBatch(this.GraphicsDevice);

            //create pixel
            Main.p = new Texture2D(this.GraphicsDevice, 1, 1);
            Main.p.SetData(new Color[] { Color.White });

            Main.contentManager = new ContentManager(this.Content.ServiceProvider, "Content");

            //load game statte and sounds
            this.currentState.LoadContent();
            Sounds.Load();

            this.gameOverTexture = Main.contentManager.Load<Texture2D>("Assets\\Misc");
            this.gameOverText = Main.contentManager.Load<Texture2D>("Assets\\Misc2");
        }

        //update
        protected override void Update(GameTime dt) {

            if (Main.resetGame == true) {

                Main.gameSpeed = 15;
            }

            //update inputs
            Input.Update();

            if (Main.gameOver == true) {

                //start the game by pressing space or up after game over
                if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) || Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up)) {

                    Main.resetGame = true;
                }
            } else if (Main.gameOver == false && Main.resetGame == false && Main.gameStarted == true) {

                Main.gameSpeed = 15;
            }

            //update current state and sounds
            this.currentState.Update(dt);
            Sounds.Update();

            base.Update(dt);
        }

        //draw
        protected override void Draw(GameTime dt) {

            //set background to white
            this.GraphicsDevice.Clear(Color.White);

            this.b.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);

            //draw current state
            this.currentState.Draw(this.b);

            //draw game over button and texture after game over
            if (Main.gameOver == true) {

                this.b.Draw(this.gameOverText, new Vector2(Main.screenWidth / 2 - this.gameOverText.Width / 2, Main.screenHeight / 4), Color.White);

                this.b.Draw(this.gameOverTexture, new Vector2(Main.screenWidth / 2 - this.gameOverTexture.Width / 2, Main.screenHeight / 3), Color.White);
            }

            this.b.End();

            base.Draw(dt);
        }
    }
}