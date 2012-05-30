using System;
using System.Collections.Generic;
using System.Text;

namespace HexKoordTest
{
  public class clsHexFeld
  {
    public clsHexFeld(int intXmon, int intYmon)
    {
      kod = new clsKoordinaten();
      this.kod.SetHexPos(intXmon, intYmon, 50);
    }

    public clsKoordinaten kod;
  }
}
