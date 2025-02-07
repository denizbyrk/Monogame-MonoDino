using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDino {
    public class Score {

        //font for score
        public SpriteFont Font;

        //color for score
        private Color grayColor = new Color(83, 83, 83);

        //color for highscore
        private Color lightGrayColor = new Color(103, 103, 103);

        //check if "score" sound has been played
        private bool soundPlayed = false;

        //score counter
        public int score;

        //high score
        private int highScore;

        //the score that is being drawn per 100 scores (100, 200, 300...)
        private int scoreTemp;

        //current iteration of score (1 for 100, 2 for 200, 3 for 300...)
        private int iteration;
        
        //timer for score gaining
        private int timer;

        //timer for blinking animation for the socre
        private int timer2;
        
        //check if the score is a multiple of 100
        private bool cutscene;

        //blink the score 5 times
        private int maxBlinkCount = 5;

        //current blink
        private int currentBlink = 0;

        //check if should draw (between each blink this is set to false, creating the blinking effect)
        private bool draw;
 
        public Score() {

            this.score = 0;
            this.highScore = 0;
            this.timer = 0;
            this.cutscene = false;
            this.draw = false;
            this.scoreTemp = 0;
            this.iteration = 0;
        }

        //load content
        public void LoadContent() {

            this.Font = Main.contentManager.Load<SpriteFont>("Assets\\Font");
        }

        //update highscore
        private void highScoreCheck() {

            if (Main.gameOver == true && this.score > this.highScore) {

                this.highScore = this.score;
            }
        }

        //manage game speed according to score
        private void manageSpeed() {

            if (Main.gameOver == false) {

                if (this.score > 250) {

                    Main.gameSpeed = 22;

                } else if (this.score > 100) {

                    Main.gameSpeed = 20;

                } else if (this.score > 70) {

                    Main.gameSpeed = 17;
                }
            }
        }

        //update
        public void Update(GameTime dt) {

            //on game over, check highscore
            if (Main.gameOver == true) this.highScoreCheck();

            //reset the values on game reset
            if (Main.resetGame == true && Main.gameStarted) {

                this.score = 0;
                this.timer = 0;
                this.cutscene = false;
                this.draw = false;
                this.scoreTemp = 0;
                this.iteration = 0;
            }

            //manage running speed
            this.manageSpeed();

            //increase score
            if (Main.gameStarted == true && Main.gameOver == false) {

                this.timer += (int)dt.ElapsedGameTime.TotalMilliseconds;

                if (this.timer > 70 - Main.gameSpeed) {

                    this.score++;
                    this.timer = 0;
                }
            }

            //on each 100 score, start blinking animation
            if (this.score % 100 == 0 && this.score != 0 && Main.gameOver == false) {

                this.cutscene = true;
                this.iteration += 1;           
            }

            //play blink animation
            if (this.cutscene == true) {

                //play the sound effect
                if (this.soundPlayed == false) {

                    Sounds.SoundEffectInstances["point"].Play();
                    this.soundPlayed = true;
                }
                
                this.timer2 += (int)dt.ElapsedGameTime.TotalMilliseconds;
             
                //animation for blinking effect
                if (this.currentBlink <= this.maxBlinkCount) {

                    if (this.timer2 > 240 && this.draw == true || this.timer2 > 240 && this.draw == false) {

                        this.draw = !this.draw;
                        this.timer2 = 0;
                        this.currentBlink++;
                    }
                } else if (this.currentBlink > this.maxBlinkCount) {

                        this.currentBlink = 0;
                        this.draw = false;
                        this.cutscene = false;
                        this.soundPlayed = false;
                }
            }

            //the multiple of 100 that appears on each 100*x score
            this.scoreTemp = (this.iteration / 4) * 100;
        }

        //draw
        public void Draw(SpriteBatch b) {

            //draw the score with 5-digits
            string scoreText = this.score.ToString().PadLeft(5, '0');

            //draw high score
            if (this.highScore > 0) {

                string highScoreText = "HI " + this.highScore.ToString().PadLeft(5, '0');
                Vector2 highScorePosition = new Vector2(Main.screenWidth - this.Font.MeasureString(highScoreText).X - 180, 32);

                b.DrawString(this.Font, highScoreText, highScorePosition, this.lightGrayColor);
            }

            //draw the real-time score during gameplay
            if (Main.gameStarted == true && this.cutscene == false && this.draw == false) {

                Vector2 gameScorePosition = new Vector2(Main.screenWidth - this.Font.MeasureString(scoreText).X - 20, 32);
                b.DrawString(this.Font, scoreText, gameScorePosition, this.grayColor);
            }

            //draw the temporary score during blinking animation
            if (this.cutscene == true && this.draw == true) {

                string tempScoreText = this.scoreTemp.ToString().PadLeft(5, '0');
                Vector2 tempScorePosition = new Vector2(Main.screenWidth - this.Font.MeasureString(scoreText).X - 20, 32);

                b.DrawString(this.Font, tempScoreText, tempScorePosition, this.grayColor);
            }
        }
    }
}