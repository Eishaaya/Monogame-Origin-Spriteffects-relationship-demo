using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Net.Http.Headers;

namespace Origin_Test
{
    /// <summary>
    /// non sarcastic name (trust)
    /// </summary>
    class HighQualitySprite
    {
        Texture2D image;
        Rectangle[] frames;
        Vector2 pos;
        int curFrame = 0;
        SpriteEffects effect;

        public HighQualitySprite(Vector2 pos, SpriteEffects effect, Rectangle[] frames, Texture2D image)
        {
            this.frames = frames;
            this.image = image;
            this.effect = effect;
            this.pos = pos;
        }

        public void IncrementAnimation() => curFrame = (curFrame + 1) % frames.Length;

        public void Draw(SpriteBatch batch) => batch.Draw(image, pos, frames[curFrame], Color.White, 0, /*frames[curFrame].Size.ToVector2() / 2*/ new Vector2(effect == SpriteEffects.None? 76 : frames[curFrame].Width - 76, frames[curFrame].Height / 2), 1, effect, 1);
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        HighQualitySprite topRect;
        HighQualitySprite bottomRect;

        TimeSpan timer = TimeSpan.Zero;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //pos = GraphicsDevice.Viewport.Bounds.Center.ToVector2();
            var frames = new Rectangle[] { new(0, 0, 473, 163), new(0, 339, 294, 163), new(0, 637, 150, 163), new(0, 339, 294, 163), };
            var recty = Content.Load<Texture2D>("quality rectangle");

            topRect = new(new(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 - frames[0].Height / 2), SpriteEffects.None, frames, recty);
            bottomRect = new(new(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + frames[0].Height / 2), SpriteEffects.FlipHorizontally, frames, recty);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if ((timer += gameTime.ElapsedGameTime).TotalMilliseconds > 500)
            {
                timer = TimeSpan.Zero;
                topRect.IncrementAnimation();
                bottomRect.IncrementAnimation();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            topRect.Draw(spriteBatch);
            bottomRect.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}