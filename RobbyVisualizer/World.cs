using GeneticAlgorithm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using RobbyTheRobot;

namespace RobbyVisualizer
{
  public class World : DrawableGameComponent
  {
    private IChromosome[] chromosone;
    private ContentsOfGrid[][,] testGrids;
    private SpriteBatch spriteBatch;
    private Texture2D tileTexture;
    private Texture2D canTexture;
    private Texture2D robbyTexture;
    private SpriteFont spriteFont;
    private Game1 game;
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
    private IRobbyTheRobot robby = Robby.CreateRobby(1000,200,100);
    public World(Game1 game, GraphicsDeviceManager graphicsDeviceManager)
      : base((Game) game)
    {
      this.chromosone = new IChromosome[76];
      this.testGrids = new ContentsOfGrid[6][,];
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
      for (int index1 = 0; index1 < 6; ++index1)
      {
        string[] strArray = File.ReadAllText(index1.ToString() + ".txt").Split(',');
        this.maxScore[index1] = int.Parse(strArray[0]);
        this.maxMove[index1] = int.Parse(strArray[1]);
        IChromosome[] genes = new IChromosome[strArray.Length - 3];
        for (int index2 = 3; index2 < strArray.Length; ++index2)
          genes[index2 - 3] = (IChromosome) Enum.Parse(typeof (IChromosome), strArray[index2]);
        this.chromosone[index1] = new Chromosome(genes);
      }
      for (int index = 0; index < this.testGrids.Length; ++index)
        this.testGrids[index] = robby.GenerateRandomTestGrid();
      this.x = World.rand.Next(0, 10);
      this.y = World.rand.Next(0, 10);
      this.generation = "One";
      base.Initialize();
    }

    protected override void LoadContent()
    {
      this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
      this.tileTexture = this.game.Content.Load<Texture2D>("tile");
      this.canTexture = this.game.Content.Load<Texture2D>("can");
      this.robbyTexture = this.game.Content.Load<Texture2D>("robby");
      this.spriteFont = this.game.Content.Load<SpriteFont>("scoreFont");
      this.tileSize = this.tileTexture.Height;
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
          this.currentScore += (int)RobbyHelper.ScoreForAllele(this.chromosone[this.currentGrid].Genes, this.testGrids[this.currentGrid], World.rand, ref this.x, ref this.y);
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
              this.game.Components.Remove((IGameComponent) this);
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
