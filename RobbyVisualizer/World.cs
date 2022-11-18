using GeneticAlgorithm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;
using RobbyTheRobot;

namespace RobbyVisualizer
{
    public class World : DrawableGameComponent
    {
        private int[][] chromosomes;
        private ContentsOfGrid[][,] testGrids;
        private SpriteBatch spriteBatch;
        private Texture2D tileTexture;
        private Texture2D canTexture;
        private Texture2D robbyTexture;
        private SpriteFont spriteFont;
        private RobbyVisualizerGame game;
        private int currentGrid;
        private int throttle;
        private int move;
        private int currentScore;
        private int[] maxMove;
        private int[] maxScore;
        private int maxThrottle;
        private int x;
        private int y;
        private int tileSize;
        private string generation;
        private static Random rand = new Random();
        private IRobbyTheRobot robby = Robby.CreateRobby(1000, 200, 100,0.05,0.05);
        public World(RobbyVisualizerGame game, GraphicsDeviceManager graphicsDeviceManager)
          : base((Game)game)
        {
            this.chromosomes = new int[6][];
            this.testGrids = new ContentsOfGrid[10][,];
            this.maxMove = new int[6];
            this.maxScore = new int[6];
            this.game = game;
            this.currentGrid = 0;
            this.move = 0;
            this.currentScore = 0;
            this.throttle = 0;
            this.maxThrottle = 15;
        }

        public override void Initialize()
        {
            int[] savedGenerations = { 1, 20, 100, 200, 500, 1000 };
            for (int index1 = 0; index1 < savedGenerations.Length; ++index1)
            {
                string[] strArray = File.ReadAllText("./generations/generation" + savedGenerations[index1] + ".txt").Split(',');
                this.maxScore[index1] = int.Parse(strArray[0]);
                this.maxMove[index1] = int.Parse(strArray[1]);

                setGeneToChromosome(index1, strArray);
            }
            for (int index = 0; index < this.testGrids.Length; ++index)
                this.testGrids[index] = robby.GenerateRandomTestGrid();
            this.x = World.rand.Next(0, 10);
            this.y = World.rand.Next(0, 10);
            this.generation = "One";
            base.Initialize();
        }

        private void setGeneToChromosome(int index1, string[] strArray)
        {
            string genesStr = strArray[2];
            char[] genesChar = genesStr.ToCharArray();
            int[] genes = Array.ConvertAll(genesChar, c => (int)Char.GetNumericValue(c));
            this.chromosomes[index1] = genes;
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.tileTexture = this.game.Content.Load<Texture2D>("bluetile");
            this.canTexture = this.game.Content.Load<Texture2D>("fish");
            this.robbyTexture = this.game.Content.Load<Texture2D>("cat");
            this.spriteFont = this.game.Content.Load<SpriteFont>("File");
            this.tileSize = 65;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ++this.throttle;
            if (this.throttle == this.maxThrottle)
            {
                if (this.maxThrottle == 50)
                    this.maxThrottle = 15;
                if (this.move <= this.maxMove[this.currentGrid])
                {
                    this.currentScore += (int)RobbyHelper.ScoreForAllele(this.chromosomes[this.currentGrid], this.testGrids[this.currentGrid], World.rand, ref this.x, ref this.y);
                    ++this.move;
                }
                else
                {
                    this.maxThrottle = 50;
                    this.move = 0;
                    this.currentScore = 0;
                    this.x = World.rand.Next(0, 10);
                    this.y = World.rand.Next(0, 10);
                    ++this.currentGrid;
                    switch (this.currentGrid)
                    {
                        case 0:
                            this.generation = "1";
                            break;
                        case 1:
                            this.generation = "10";
                            break;
                        case 2:
                            this.generation = "50";
                            break;
                        case 3:
                            this.generation = "200";
                            break;
                        case 4:
                            this.generation = "500";
                            break;
                        case 5:
                            this.generation = "1000";
                            break;
                        case 6:
                            this.game.Components.Remove((IGameComponent)this);
                            break;
                    }
                }
                this.throttle = 0;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin();
            for (int index1 = 0; index1 < this.testGrids[this.currentGrid].GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < this.testGrids[this.currentGrid].GetLength(1); ++index2)
                {
                    switch (this.testGrids[this.currentGrid][index1, index2])
                    {
                        case ContentsOfGrid.Empty:
                            this.spriteBatch.Draw(this.tileTexture, new Rectangle(index1 * this.tileSize, index2 * this.tileSize, this.tileSize, this.tileSize), Color.White);
                            break;
                        case ContentsOfGrid.Can:
                            Color aquamarine = Color.Aquamarine;
                            this.spriteBatch.Draw(this.canTexture, new Rectangle(index1 * this.tileSize, index2 * this.tileSize, this.tileSize, this.tileSize), aquamarine);
                            break;
                    }
                }
            }
            this.spriteBatch.Draw(this.robbyTexture, new Rectangle(this.x * this.tileSize, this.y * this.tileSize, this.tileSize, this.tileSize), Color.White);
            string text1 = "Generation " + this.generation;
            string text2 = "Move:   " + this.move.ToString() + "/" + this.maxMove[this.currentGrid].ToString();
            string text3 = "Points: " + this.currentScore.ToString() + "/" + this.maxScore[this.currentGrid].ToString();
            this.spriteBatch.DrawString(this.spriteFont, text1, new Vector2(30f, 650f), Color.White);
            this.spriteBatch.DrawString(this.spriteFont, text2, new Vector2(30f, 680f), Color.White);
            this.spriteBatch.DrawString(this.spriteFont, text3, new Vector2(30f, 710f), Color.White);
            this.spriteBatch.End();
        }
    }
}
