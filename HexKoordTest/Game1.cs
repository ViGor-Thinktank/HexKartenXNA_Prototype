using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;

namespace HexKoordTest
{
  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class Game1 : Microsoft.Xna.Framework.Game
  {   
    private Rectangle recViewport;
    private GraphicsDeviceManager graphics;
    private Dictionary<string, Texture2D> dicTextures = new Dictionary<string, Texture2D>();
    private SpriteBatch spriteBatch;
    private Texture2D texBackground;
    private frmCursorInfo InfoForm = new frmCursorInfo(true);
    private frmCursorInfo Calculator = new frmCursorInfo(false);

    private Dictionary<string, clsHexFeld> dicHex = new Dictionary<string, clsHexFeld>();

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
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

      texBackground = Content.Load<Texture2D>("Sprites\\background");

      AddTex("cursor_bc");
      AddTex("cursor_cross");   
      AddTex("hex");
      AddTex("hex2");
      AddTex("hex3");
      AddTex("hex_rot");
      AddTex("hex_blau");

      recViewport = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
      
      Calculator.Text = "Calculator";
      //Calculator.Show();
      
    }

    public void AddTex(string strName)
    {      
      this.dicTextures.Add(strName, Content.Load<Texture2D>("Sprites\\" + strName));      
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

      // TODO: Add your update logic here

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Beige);

      // TODO: Add your drawing code here
      spriteBatch.Begin(/*SpriteBlendMode.AlphaBlend*/);
      
      spriteBatch.Draw(this.texBackground, this.recViewport, Color.White);

      string strAdr = this.Calculator.kod.intHexX.ToString("00") + this.Calculator.kod.intHexY.ToString("00");
      
      if (dicHex.ContainsKey(strAdr))
      {
        clsHexFeld hex = dicHex[strAdr];
        SpriteBatchDraw(dicTextures["hex_blau"], new Vector2(hex.kod.intXmon, hex.kod.intYmon), 1f);
      }      

      BuildHexMap("hex3");

      ShowMouse();     

      spriteBatch.End();

      base.Draw(gameTime);

      if (!InfoForm.Visible)
      {        
        InfoForm.Show();
      }
    }

    private void ShowMouse()
    {
      MouseState msta = Mouse.GetState();

      if (msta.X > this.recViewport.X && msta.X < this.recViewport.X + this.recViewport.Width
          && msta.Y > this.recViewport.Y && msta.Y < this.recViewport.Y + this.recViewport.Height)
      {
        //SpriteBatchDraw(dicTextures["cursor_bc"], new Vector2(msta.X, msta.Y), 1f);         
        SpriteBatchDraw(dicTextures["cursor_cross"], new Vector2(msta.X - 3, msta.Y - 3), 1f);         
        
        InfoForm.SetMonPos(msta.X, msta.Y);
      
        string strAdr = this.InfoForm.kod.intHexX.ToString("00") + this.InfoForm.kod.intHexY.ToString("00");

        //InfoForm.txtYDiff.Text = strAdr;

        if (dicHex.ContainsKey(strAdr))
        {
          clsHexFeld hex = dicHex[strAdr];
          SpriteBatchDraw(dicTextures["hex_rot"], new Vector2(hex.kod.intXmon, hex.kod.intYmon), 2f);
        }
        else
        {
          string str = strAdr;
        }        
      }
    }

    private void BuildHexMap(string strTex)
    {
      string strPetze = "";
      string strZeile = "";

      Texture2D tex = dicTextures[strTex];      
      Vector2 vec = new Vector2(0,0);
      clsKoordinaten kod = new clsKoordinaten();
      
      string strAdr = "";           

      vec.X += (0 - tex.Width / 2);
      //vec.X += (0 - tex.Height / 2);
      vec.Y += (0 - tex.Height / 2);
      bool bOffset = false;
     
      bool blnInit = this.dicHex.Count == 0;

      while (vec.X < graphics.GraphicsDevice.Viewport.Width)       
      {
        strZeile = "";

        while (vec.Y < graphics.GraphicsDevice.Viewport.Height)       
        {
          SpriteBatchDraw(tex, vec, 1f);

          if (blnInit)
          {            
            kod.SetHexPos(Convert.ToInt32(vec.X), Convert.ToInt32(vec.Y), tex);

            strAdr = kod.intHexX.ToString("00") + kod.intHexY.ToString("00");

            strZeile += strAdr + "|";

            if (!dicHex.ContainsKey(strAdr))
            {
              dicHex.Add(strAdr, new clsHexFeld(kod.intXmon, kod.intYmon));
            }
            else
            {
              string str = "X" + strAdr;
            }
          }          
          vec.Y += tex.Height;
          if (vec.Y > (tex.Height * 2))
            break;
        }        

        vec.X += tex.Width - (tex.Width / 4);
        vec.Y = (bOffset ? 0 - (tex.Height / 2) : 0);
        
        bOffset = !bOffset;
        strPetze += strZeile + "\n";

        if (vec.X > (tex.Width * 2))
          break;
      }         
    }
  
    private void SpriteBatchDraw(Texture2D texObjekt, Vector2 vecPostion, float fltLevel)
    {
      //spriteBatch.Draw(texObjekt, vecPostion, null, Color.White, 0f, vecPostion, 0.5f, SpriteEffects.None, 0);
      spriteBatch.Draw(texObjekt, vecPostion, Color.White);              
    }
  }
}
