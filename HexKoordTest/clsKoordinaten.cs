using System;
using System.Collections.Generic;
using System.Text;

namespace HexKoordTest
{
  public class clsKoordinaten
  {
    private int Xmon;
    public int intXmon
    {
      get { return Xmon; }
    }

    private int Ymon;
    public int intYmon
    {
      get { return Ymon; }
    }

    private double Xhex;
    public double dblXhex
    {
      get { return Xhex; }
    }
    
    private double Ydiff;
    public double dblYdiff
    {
      get { return Ydiff; }
    }
    
    private double Yhex;
    public double dblYhex
    {
      get { return Yhex; }
    }
    
    private int HexY;
    public int intHexY
    {
      get { return HexY; }
    }
    
    private int HexX;
    public int intHexX
    {
      get { return HexX; }
    }

    public void SetHexPos(int X, int Y, int Diff)
    {
      CalcHexPos(X, Y);

      HexY = Convert.ToInt32(Math.Floor(Yhex / Diff));
      HexX = Convert.ToInt32(Math.Floor(Xhex / Diff));
      int i = Convert.ToInt32(Math.Ceiling(Xhex / Diff));
       
    }

    internal void SetHexPos(int X, int Y, Microsoft.Xna.Framework.Graphics.Texture2D tex)
    {
      CalcHexPos(X, Y);

      HexY = Convert.ToInt32(Math.Floor(Yhex / tex.Height));
      HexX = Convert.ToInt32(Math.Floor(Xhex / tex.Height));    
      //HexX = Convert.ToInt32(Xhex / tex.Width);    
    }

    private void CalcHexPos(int X, int Y)
    {
      Xmon = X;
      Ymon = Y;

      Xhex = Xmon * 1.14;  //(1.14 ???)

      Ydiff = Math.Sqrt(Math.Pow(Xhex, 2) - Math.Pow(Xmon, 2));
      Yhex = Ymon - Ydiff;   
    }
  }
}
