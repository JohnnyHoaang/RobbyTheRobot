
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RobbyVisualizer
{
  public class Game1 : Game
  {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private World sprite;

    public Game1()
    {
      this.graphics = new GraphicsDeviceManager((Game) this);
      this.Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
      this.sprite = new World(this, this.graphics);
      this.Components.Add((IGameComponent) this.sprite);
      this.graphics.PreferredBackBufferHeight = 750;
      this.graphics.PreferredBackBufferWidth = 650;
      this.graphics.ApplyChanges();
      base.Initialize();
    }

    protected override void LoadContent() => this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        this.Exit();
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      this.GraphicsDevice.Clear(Color.Black);
      base.Draw(gameTime);
    }
  }
}
