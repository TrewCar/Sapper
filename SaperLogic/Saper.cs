using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

class Saper
{
    public Saper(PictureBox Picture ,int RectSZ, int ColVo_Bombs)
    {        
        this.picture = Picture;
        this.RectSZ = RectSZ;
        this.ColVo_Bombs = ColVo_Bombs;

        g = Graphics.FromImage(picture.Image);

        Grid = new int[picture.Width / RectSZ, picture.Height / RectSZ];
        buttom = new Buttom[picture.Width / RectSZ, picture.Height / RectSZ];

        CreateGrid();
        AddNeighbor();
        Draw();
    }
    private OpenNeighbor openNeighbor;
    private PictureBox picture;
    private int[,] Grid;
    private int RectSZ;
    private int ColVo_Bombs;
    private Graphics g;

    private Buttom[,] buttom;

    private Image Closed = Image.FromFile(Directory.GetCurrentDirectory() + @"\Picture\closed.png");
    private Image Flaget = Image.FromFile(Directory.GetCurrentDirectory() + @"\Picture\flaged.png");
    private Image Inform = Image.FromFile(Directory.GetCurrentDirectory() + @"\Picture\inform.png");
    private Image Opened = Image.FromFile(Directory.GetCurrentDirectory() + @"\Picture\opened.png");

    private void AddNeighbor()
    {
        for (int i = 0; i < buttom.GetLength(0); i++)
        {
            for (int j = 0; j < buttom.GetLength(1); j++)
            {
                buttom[i, j].AddPicture(GetPoint(new Point(i, j)));
            }
        }
        openNeighbor = new OpenNeighbor(buttom);
    }
   
    private void Draw()
    {
        for (int i = 0; i < picture.Width; i+=RectSZ)
        {
            for (int j = 0; j < picture.Height; j+=RectSZ)
            {
                g.DrawImage(Closed, i, j, RectSZ, RectSZ);
            }
        }
    }

    public void Open(Point pos)
    {
        if (buttom[pos.X, pos.Y].Open == true) return;

        if(buttom[pos.X, pos.Y].NumberNeighbour != 0 || buttom[pos.X, pos.Y].Bomb == true)
        {
            buttom[pos.X, pos.Y].Open = true;
            buttom[pos.X, pos.Y].Draw(g);
        }
        else
        {
            List<Point> opened = openNeighbor.FindPath(pos);
            foreach (var item in opened)
            {
                OpenButtomZero(item);
            }
        }
       
        picture.Invalidate();
    }

    private void OpenButtomZero(Point Now)
    {
        for (int i = -1; i <=1; i++)
        {
            for (int j = -1; j <=1; j++)
            {
                if (ProvOutRangeArray(new Point(i, j), Now))
                    continue;
                buttom[Now.X + i, Now.Y + j].Open = true;
                buttom[Now.X + i, Now.Y + j].Draw(g);
            }
        }
    }

    private void CreateGrid()
    {
        int CreateBombs = 0;
        Random rnd = new Random((int)DateTime.Now.Ticks);

        for (int i = 0; i < buttom.GetLength(0); i++)
        {
            for (int j = 0; j < buttom.GetLength(1); j++)
            {              
                buttom[i, j] = new Buttom( RectSZ, new Point(i, j));              
            }
        }

        while (CreateBombs < ColVo_Bombs)
        { 
            int x = rnd.Next(buttom.GetLength(0));
            int y = rnd.Next(buttom.GetLength(1));

            if (Grid[x, y] == 1) continue;

            buttom[x, y] = new Buttom(RectSZ, new Point(x, y)) { Bomb = true };

            Grid[x, y] = 1;

            CreateBombs++;
        }
    }

   
    private bool ProvOutRangeArray(Point point, Point NowPosition) => point.X + NowPosition.X < 0 || point.X + NowPosition.X >= Grid.GetLength(0) || point.Y + NowPosition.Y < 0 || point.Y + NowPosition.Y >= Grid.GetLength(1);
    private int GetPoint(Point NowPosition)
    {
        int Count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (ProvOutRangeArray(new Point(i, j), NowPosition))
                    continue;
                if (Grid[NowPosition.X+i, NowPosition.Y + j] == 1)
                    Count++;
            }
        }
        return Count;
    }
}

