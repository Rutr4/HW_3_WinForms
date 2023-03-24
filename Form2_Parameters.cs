using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HW_3_WinForms.Form1;
using static System.Windows.Forms.DataFormats;

namespace HW_3_WinForms
{
    public partial class Form2_Parameters : Form
    {
        Form1 form1;
        int pointResize = 1;
        int lineWidhtResize = 1;
        public Form2_Parameters(Form1 owner)
        {
            form1 = owner;
            InitializeComponent();

            Paint += new PaintEventHandler(Form2_Parameters_Paint);
            btnOk.Click += btnOk_Click;
            btnClose.Click += btnClose_Click;
            trackBarLineWidth.Scroll += refreshData;
            TrackBarPointSize.Scroll += refreshData;
        }

        private void Form2_Parameters_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Point[] points = { new Point(250, 50),
                               new Point(200, 150),
                               new Point(300, 125) };

            Pen p = new Pen(color: Color.BlueViolet, lineWidhtResize);
            foreach (var point in points)
            {
                g.FillEllipse(new SolidBrush(form1.currentFigure.pointColor), point.X, point.Y, pointResize, pointResize);
            }
            g.DrawClosedCurve(p, points);

        }

        private void refreshData(object? sender, EventArgs e)
        {
            pointResize = TrackBarPointSize.Value;
            lineWidhtResize = trackBarLineWidth.Value;
            Refresh();
        }

        void btnOk_Click(object? sender, EventArgs e)
        {
            form1.currentFigure.pointRadius = TrackBarPointSize.Value;
            form1.currentFigure.lineWidth = trackBarLineWidth.Value;
            form1.Refresh();
        }
        void btnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
