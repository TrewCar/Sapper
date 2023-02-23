using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Buttom
{
    public Buttom(int Size, Point pos)
    {
        RectSize = Size;
        Position = pos;
        image = Image.FromFile(Directory.GetCurrentDirectory() + $@"\Picture\bomb.png");
    }

    private Point Position;

    private Image image;

    private int RectSize = 10;

    public bool Bomb = false;
    public bool Select = false;
    public int NumberNeighbour { get; private set; }
    public bool Open = false;

    public void AddPicture(int Neighbor)
    {
        if(Bomb == true)
        {
            image = Image.FromFile(Directory.GetCurrentDirectory() + $@"\Picture\bomb.png");
            return;
        }
        
        image = Image.FromFile(Directory.GetCurrentDirectory() + $@"\Picture\num{Neighbor}.png");
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(image, Position.X * RectSize, Position.Y * RectSize, RectSize, RectSize);
    }
}


