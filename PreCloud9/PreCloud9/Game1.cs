using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameStructure;

namespace PreCloud9
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public struct PlayerData
    {
        public Vector2 Position;
        public bool IsAlive;
        public Color Color;
        public float Angle;
        public float Power;
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        GameManager gm;

        Texture2D backgroundTile;
        Texture2D BricksBox;
        Texture2D StoneBox;
        Texture2D WaterBox;
        Texture2D CoinBox;
        Texture2D LifePackBox;
        Texture2D TankImage;

        int screenWidth;
        int screenHeight;
        int unitSize;
        float playerScalling;
        Color[] playerColors = new Color[5];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            gm = new GameManager();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 650;
            graphics.PreferredBackBufferHeight = 650;
            //unitSize = screenWidth / gm.gridSize;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Cloud 9 games alpha 4.2";
            playerScalling = 0.5f;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            backgroundTile = Content.Load<Texture2D>("Tile");
            BricksBox = Content.Load<Texture2D>("Bricks2XNA");
            StoneBox = Content.Load<Texture2D>("StoneXNA");
            WaterBox = Content.Load<Texture2D>("waterXNA");
            LifePackBox = Content.Load<Texture2D>("LifePackXNA");
            CoinBox = Content.Load<Texture2D>("CoinXNA");
            TankImage = Content.Load<Texture2D>("tank_XNA");
            initializeColors();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            processKeyBoard();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            drawBackGroundTiles();
            drawMAP();
            drawMytank();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void drawMytank()
        {
            //Vector2 Position1 = new Vector2(gm.gEngine.myTank.Xcod * unitSize + unitSize / 2, gm.gEngine.myTank.Ycod * unitSize + unitSize / 2);
            //spriteBatch.Draw(TankImage, Position, Color.White);
            //spriteBatch.Draw(TankImage, Position1, null, Color.LightBlue, MathHelper.ToRadians(90 * gm.gEngine.myTank.Direction), new Vector2(TankImage.Width / 2, TankImage.Height / 2), playerScalling, SpriteEffects.None, 0);
            List<Tank> tanklist = gm.gEngine.tankList;
            for (int i = 0; i < tanklist.Count; i++)
            {
                Vector2 position = new Vector2(tanklist[i].Xcod * unitSize + unitSize / 2, tanklist[i].Ycod * unitSize + unitSize / 2);
                spriteBatch.Draw(TankImage, position, null, playerColors[i], MathHelper.ToRadians(90 * tanklist[i].Direction), new Vector2(TankImage.Width / 2, TankImage.Height / 2), playerScalling, SpriteEffects.None, 0);
            }            
        }

        private void drawBackGroundTiles()
        {
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            unitSize = screenWidth / gm.gEngine.gridSize;
            for (int i = 0; i < screenHeight; i+=unitSize)
            {
                for (int j = 0; j < screenWidth; j+=unitSize)
                {
                    Rectangle unitRectangle = new Rectangle(j,i, unitSize, unitSize);
                    spriteBatch.Draw(backgroundTile, unitRectangle, Color.White);
                }           
            }
        }

        private void drawMAP()
        {
            String[,] map = gm.gEngine.map;
            int xcod = 0;
            int ycod = 0;
            for (int i = 0; i < gm.gEngine.gridSize; i++)
            {
                ycod = i*unitSize;
                for (int j = 0; j < gm.gEngine.gridSize; j++)
                {                   
                    if (!(map[i, j].Equals("N")))
                    {
                        xcod = j*unitSize;
                        Rectangle imageRectangle = new Rectangle(xcod+2, ycod+2, unitSize-4, unitSize-4);
                        if (map[i, j].Equals("B"))
                        {
                            spriteBatch.Draw(BricksBox, imageRectangle, Color.White);
                        }
                        if (map[i, j].Equals("S"))
                        {
                            spriteBatch.Draw(StoneBox, imageRectangle, Color.White);
                        }
                        if (map[i, j].Equals("W"))
                        {
                            spriteBatch.Draw(WaterBox, imageRectangle, Color.White);
                        }
                        if (map[i, j].Equals("L"))
                        {
                            spriteBatch.Draw(LifePackBox, imageRectangle, Color.White);
                        }
                        if (map[i, j].Equals("C"))
                        {
                            spriteBatch.Draw(CoinBox, imageRectangle, Color.White);
                        }
                    }
                }
            }
        }

        private void processKeyBoard()
        {
            KeyboardState keybState = Keyboard.GetState();
            if (keybState.IsKeyDown(Keys.Up))
            {
                gm.gEngine.con.sendDatatoServer("UP#");
            }
            else if (keybState.IsKeyDown(Keys.Right))
            {
                gm.gEngine.con.sendDatatoServer("RIGHT#");
            }
            else if (keybState.IsKeyDown(Keys.Down))
            {
                gm.gEngine.con.sendDatatoServer("DOWN#");
            }
            else if (keybState.IsKeyDown(Keys.Left))
            {
                gm.gEngine.con.sendDatatoServer("LEFT#");
            }
            else if(keybState.IsKeyDown(Keys.Space)){
                gm.gEngine.con.sendDatatoServer("SHOOT#");
            }
        }

        private void initializeColors()
        {
            this.playerColors[0] = Color.LightGreen;
            this.playerColors[1] = Color.LightPink;
            this.playerColors[2] = Color.Gold;
            this.playerColors[3] = Color.LightSkyBlue;
            this.playerColors[4] = Color.LightSalmon;
        }
    }
}
