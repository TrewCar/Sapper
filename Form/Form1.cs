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
        Saper sap = new Saper(pictureBox1, 50, 20);

        pictureBox1.Invalidate();
    }
}

