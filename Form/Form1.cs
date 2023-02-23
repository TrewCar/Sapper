using System;
using System.Drawing;
using System.Windows.Forms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Set();
    }
    private int RectSZ = 50;

    private Saper sap;
    private int ColVo_Bomb = 0;
    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            sap.ClickOpen(e.Location);
        else if (e.Button == MouseButtons.Right)
            sap.ClickSelect(e.Location);       
    }

    private void button1_Click(object sender, EventArgs e)
    {
        button1.Text = "Reset";

        Set();

        ResizePicture(RectSZ);

        sap = new Saper(pictureBox1, RectSZ, ColVo_Bomb);

        pictureBox1.Invalidate();
    }

    private void trackBar1_ValueChanged(object sender, EventArgs e)
    {
        SizeGrid.Text = $"Размер сетки ({trackBar1.Value} x {trackBar1.Value})";
        RectSZ = pictureBox1.Width / trackBar1.Value;
    }
    private void ResizePicture(int RectSZ)
    {
        var x = pictureBox1.Width % RectSZ;
        var y = pictureBox1.Height % RectSZ;

        int xSZ = pictureBox1.Width;
        int ySZ = pictureBox1.Height;

        if (x != 0)
        {
            xSZ -= x;
        }
        if (y != 0)
        {
            ySZ -= y;
        }
        pictureBox1.Size = new Size(xSZ, ySZ);
        pictureBox1.Image = new Bitmap(xSZ, ySZ);
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        Set();
    }

    private void Set()
    {
        ColVo_Bomb = int.TryParse(textBox1.Text, out int ColVo) ? ColVo : (RectSZ * RectSZ) / 2;
        RectSZ = pictureBox1.Width / trackBar1.Value;
    }
}

