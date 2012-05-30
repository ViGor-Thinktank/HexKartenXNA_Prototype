using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HexKoordTest
{
  public partial class frmCursorInfo : Form
  {
    public clsKoordinaten kod = new clsKoordinaten();

    public frmCursorInfo(bool Info)
    {
      InitializeComponent();

      txtHexX.ReadOnly = true;
      txtHexY.ReadOnly = true;
      txtXHex.ReadOnly = true;
      txtYHex.ReadOnly = true;
      txtYDiff.ReadOnly = true;

      txtXMonitor.ReadOnly = Info;
      txtYMonitor.ReadOnly = Info;
      
    }

    public void SetMonPos(int X, int Y)
    {      
      kod.SetHexPos(X, Y, 50);

      txtXMonitor.Text = X.ToString();
      txtYMonitor.Text = Y.ToString();

      txtXHex.Text = kod.dblXhex.ToString();
      txtYHex.Text = kod.dblYhex.ToString();

      txtHexY.Text = kod.intHexY.ToString();
      txtHexX.Text = kod.intHexX.ToString();

      txtYDiff.Text = kod.dblYdiff.ToString("0.00");
    }

    private void txt_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        int X = Convert.ToInt32(txtXMonitor.Text);
        int Y = Convert.ToInt32(txtYMonitor.Text);
        SetMonPos(X, Y);
      }
    }    

   


  }
}
