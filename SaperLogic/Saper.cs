using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

class Saper
{
    public Saper(PictureBox Picture, int RectSZ, int ColVo_Bombs)
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

    private List<Point> PosBombs = new List<Point>();
    private List<Point> PosSelect = new List<Point>();

    private Image Closed = Image.FromFile(Directory.GetCurrentDirectory() + @"\Picture\closed.png");

    private bool EndGame = false;

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
        for (int i = 0; i < buttom.GetLength(0); i ++)
        {
            for (int j = 0; j < buttom.GetLength(1); j ++)
            {
                buttom[i, j].Draw(g, SelectImage.Closed);
            }
        }
    }

    public void ClickOpen(Point pos)
    {
        if (buttom[pos.X, pos.Y].Open == true || EndGame == true) return;
        if(buttom[pos.X, pos.Y].Bomb == true)
        {
            foreach (var item in PosBombs)
            {
                buttom[item.X, item.Y].Open = true;
                buttom[item.X, item.Y].Draw(g);
            }
            EndGame = true;
        }
        else if (buttom[pos.X, pos.Y].NumberNeighbour != 0)
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
    public void ClickSelect(Point pos)
    {
        if (buttom[pos.X, pos.Y].Open == true || EndGame == true) return;

        if(buttom[pos.X, pos.Y].Select == false && buttom[pos.X, pos.Y].I_DontKnow == false)
        {
            buttom[pos.X, pos.Y].Select = true;

            PosSelect.Add(pos);

            CheckWin();

            buttom[pos.X, pos.Y].Draw(g, SelectImage.Flaget);
        }
        else if (buttom[pos.X, pos.Y].Select == true)
        {
            buttom[pos.X, pos.Y].Select = false;
            buttom[pos.X, pos.Y].I_DontKnow = true;

            PosSelect.Remove(pos);

            buttom[pos.X, pos.Y].Draw(g, SelectImage.Inform);
        }
        else
        {
            buttom[pos.X, pos.Y].I_DontKnow = false;

            buttom[pos.X, pos.Y].Draw(g, SelectImage.Closed);
        }

        picture.Invalidate();
    }

    private void CheckWin()
    {
        if(PosSelect.Count == ColVo_Bombs)
        {
            bool temp = false;
            foreach (var item in PosSelect)
            {
                if (buttom[item.X, item.Y].Bomb == true)
                    temp = true;
                else
                {
                    temp = false;
                    break;
                }
            }

            if(temp == true)
            {
                EndGame = true;
                // СООБЩЕНИЕ ПОБЕДЫ
            }
        }
    }

    private void OpenButtomZero(Point Now)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
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
                buttom[i, j] = new Buttom(RectSZ, new Point(i, j));
            }
        }

        while (CreateBombs < ColVo_Bombs)
        {
            int x = rnd.Next(buttom.GetLength(0));
            int y = rnd.Next(buttom.GetLength(1));

            if (Grid[x, y] == 1) continue;

            buttom[x, y] = new Buttom(RectSZ, new Point(x, y)) { Bomb = true };
            PosBombs.Add(new Point(x,y));
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
                if (Grid[NowPosition.X + i, NowPosition.Y + j] == 1)
                    Count++;
            }
        }
        return Count;
    }
}

