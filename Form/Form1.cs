using System;
using System.Drawing;
using System.Windows.Forms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }
    private void Form1_Load(object sender, EventArgs e)
    {
        
        ResizePicture(RectSZ);
        sap = new Saper(pictureBox1, RectSZ, 15);

        pictureBox1.Invalidate();
    }
    int RectSZ = 50;

    private Saper sap;
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

    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        Point pos = new Point(e.X / RectSZ, e.Y / RectSZ);

        sap.Open(pos);
    }
}

