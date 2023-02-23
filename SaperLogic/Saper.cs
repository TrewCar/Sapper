using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

class Saper
{
    public Saper(PictureBox Picture ,int RectSZ, int ColVo_Bombs)
    {        
        this.picture = Picture;
        this.RectSZ = RectSZ;
        this.ColVo_Bombs = ColVo_Bombs;
        
        ResizePicture();

        
        Grid = new int[picture.Width / RectSZ, picture.Height / RectSZ];
        buttom = new Buttom[picture.Width / RectSZ, picture.Height / RectSZ];

        CreateGrid();
        AddNeighbor();
        Draw();
    }

    private void AddNeighbor()
    {
        for (int i = 0; i < buttom.GetLength(0); i++)
        {
            for (int j = 0; j < buttom.GetLength(1); j++)
            {
                buttom[i, j].AddPicture(GetPoint(new Point(i, j)));
            }
        }
    }
   
    private void Draw()
    {
        foreach (var item in buttom)
        {
            item.Draw(g);
        }
    }

    private PictureBox picture;
    private int[,] Grid;
    private int RectSZ;
    private int ColVo_Bombs;
    private Graphics g;

    private Buttom[,] buttom;

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

    private void ResizePicture()
    {
        var x = picture.Width % RectSZ;
        var y = picture.Height % RectSZ;

        int xSZ = picture.Width;
        int ySZ = picture.Height;

        if (x != 0)
        {
            xSZ -= x;
        }
        if(y != 0)
        {
            ySZ-= y;
        }
        picture.Image = new Bitmap(xSZ, ySZ);
        g = Graphics.FromImage(picture.Image);
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

