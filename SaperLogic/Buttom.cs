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
    }

    private Point Position;

    private Image image;

    private int RectSize = 10;

    public bool Bomb = false;
    public bool Select = false;
    public bool I_DontKnow = false;

    public int NumberNeighbour { get; private set; }
    public bool Open = false;

    private Image Flaget = Image.FromFile(Directory.GetCurrentDirectory() + @"\Picture\flaged.png");
    private Image Inform = Image.FromFile(Directory.GetCurrentDirectory() + @"\Picture\inform.png");
    private Image Opened = Image.FromFile(Directory.GetCurrentDirectory() + @"\Picture\opened.png");
    private Image Closed = Image.FromFile(Directory.GetCurrentDirectory() + @"\Picture\closed.png");

    public void AddPicture(int Neighbor)
    {
        NumberNeighbour = Neighbor;
        if (Bomb == true)
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
    public void Draw(Graphics g , SelectImage selectimage)
    {
        switch (selectimage)
        {
            case SelectImage.Flaget:
                g.DrawImage(Flaget, Position.X * RectSize, Position.Y * RectSize, RectSize, RectSize);
                break;
            case SelectImage.Inform:
                g.DrawImage(Inform, Position.X * RectSize, Position.Y * RectSize, RectSize, RectSize);
                break;
            case SelectImage.Opened:
                g.DrawImage(Opened, Position.X * RectSize, Position.Y * RectSize, RectSize, RectSize);
                break;
            case SelectImage.Closed:
                g.DrawImage(Closed, Position.X * RectSize, Position.Y * RectSize, RectSize, RectSize);
                break;
            default:
                break;
        }
    }
}
public enum SelectImage
{
    Flaget = 1,
    Inform = 2,
    Opened = 3,
    Closed = 4
}


