using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static HW_3_WinForms.Form1;

namespace HW_3_WinForms
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer = new();
        public enum LineType { None, Curve, Bezier, Polygone, FilledCurve };

        public class Figure // Класс Figure введён для сохранения фигур
        {
            public List<Point> points = new();

            public Color pointColor = Color.BlueViolet;
            public int pointRadius = 8;

            public LineType currentLineType;
            public Color lineColor = Color.DeepSkyBlue;
            public int lineWidth = 10;
        }
        public Figure currentFigure = new();
        public List<Figure> figuresLst = new(); // Список сохранённых фигур
        public List<Point> Offsets = new();     // Список отклонения точек при движении фигуры
        // Flags
        bool bPoints = true;
        bool bDrag = false;
        bool bMove = false;

        // Определение текущей точки
        int draggingPoint;

        public Form1()
        {
            InitializeComponent();

            #region Events

            // Событие происходит при перерисовке элемента управления.
            Paint += new PaintEventHandler(Form1_Paint);

            // Mouse
            MouseClick += Form1_MouseClick;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;

            // Buttons
            btnPoints.Click += BtnPoints_Click;
            btnParams.Click += BtnParams_Click;
            btnMove.Click += BtnMove_Click;
            btnCurve.Click += BtnCurve_click;
            btnBezier.Click += BtnBezier_click;
            btnPolygon.Click += BtnPolygon_click;
            btnFilled.Click += BtnFilled_click;
            btnClear.Click += BtnClear_click;
            btnSave.Click += BtnSave_Click;

            btnSave.Enabled = false;
            btnMove.Enabled = false;
            btnBezier.Enabled = false;
            btnCurve.Enabled = false;
            btnFilled.Enabled = false;
            btnPolygon.Enabled = false;

            // Keyboard
            KeyPreview = true;
            KeyDown += Form1_keyDown;
            #endregion

            #region Timer_Move

            timer.Interval = 20;
            timer.Tick += TimerTickHandler;
            DoubleBuffered = true;

            #endregion
        }

        private void BtnParams_Click(object? sender, EventArgs e)
        {
            bDrag = false;
            if (bMove)
            {
                btnMove.PerformClick();
            }

            Form2_Parameters f = new(this);
            f.ShowDialog(this);
        }

        private void TimerTickHandler(object? sender, EventArgs e)
        {
            MovePoints();
            Refresh();
        }
        void MovePoints()
        {
            int xOffset, yOffset;

            for (int i = 0; i < currentFigure.points.Count; i++)
            {
                xOffset = currentFigure.points[i].X + Offsets[i].X;
                if (xOffset >= this.ClientRectangle.Width || xOffset <= 0)
                {
                    Offsets[i] = new Point(-Offsets[i].X, Offsets[i].Y);
                    xOffset = currentFigure.points[i].X + Offsets[i].X;
                }

                yOffset = currentFigure.points[i].Y + Offsets[i].Y;
                if (yOffset >= this.ClientRectangle.Height || yOffset <= 0)
                {
                    Offsets[i] = new Point(Offsets[i].X, -Offsets[i].Y);
                    yOffset = currentFigure.points[i].Y + Offsets[i].Y;
                }
                currentFigure.points[i] = new Point(xOffset, yOffset);
            }
        }
        void BtnMove_Click(object? sender, EventArgs e)
        {
            if (currentFigure.points.Count < 2)
                return;

            bMove = !bMove;

            if (bPoints)
            {
                bPoints = !bPoints;
                btnPoints.Enabled = false;
            }
            else
            {
                bPoints = !bPoints;
                btnPoints.Enabled = true;
            }

            if (bMove)
            {
                Offsets = new List<Point>();
                Random rnd = new Random((int)DateTime.Now.Ticks);
                for (int i = 0; i < currentFigure.points.Count; i++)
                    Offsets.Add(new Point(rnd.Next(2, 10), rnd.Next(2, 10)));

                timer.Start();
            }
            else
                timer.Stop();
        }

        // Обработка нажатия клавиш клавиатуры
        void Form1_keyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: // Очистка формы
                    btnClear.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.Space: // Включает/выключает режим движения
                    btnMove.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.Oemplus: // Увеличивает  скорость движения точек
                    for (int i = 0; i < Offsets.Count; i++)
                        Offsets[i] = new Point(Offsets[i].X < 0 ? Offsets[i].X - 1 : Offsets[i].X + 1, Offsets[i].Y < 0 ? Offsets[i].Y - 1 : Offsets[i].Y + 1);
                    break;
                case Keys.OemMinus: // Уменьшает скорость движения точек
                    for (int i = 0; i < Offsets.Count; i++)
                        Offsets[i] = new Point(Offsets[i].X < 0 ? Offsets[i].X + 1 : Offsets[i].X - 1, Offsets[i].Y < 0 ? Offsets[i].Y + 1 : Offsets[i].Y - 1);
                    break;
                default:
                    break;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    if (bMove)
                    {
                        for (int i = 0; i < Offsets.Count; i++)
                            Offsets[i] = new Point(Offsets[i].X, Offsets[i].Y < 0 ? Offsets[i].Y - 1 : Offsets[i].Y + 1);

                        Refresh();
                    }
                    break;
                case Keys.Down:
                    if (bMove)
                    {
                        for (int i = 0; i < Offsets.Count; i++)
                            Offsets[i] = new Point(Offsets[i].X, Offsets[i].Y < 0 ? Offsets[i].Y + 1 : Offsets[i].Y - 1);

                        Refresh();
                    }
                    break;
                case Keys.Left:
                    if (bMove)
                    {
                        for (int i = 0; i < Offsets.Count; i++)
                            Offsets[i] = new Point(Offsets[i].X < 0 ? Offsets[i].X + 1 : Offsets[i].X - 1, Offsets[i].Y);

                        Refresh();
                    }
                    break;
                case Keys.Right:
                    if (bMove)
                    {
                        for (int i = 0; i < Offsets.Count; i++)
                            Offsets[i] = new Point(Offsets[i].X < 0 ? Offsets[i].X - 1 : Offsets[i].X + 1, Offsets[i].Y);

                        Refresh();
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }

        #region MouseClickMethods
        void Form1_MouseClick(object? sender, MouseEventArgs e)
        {
            if (bPoints && e.Button == MouseButtons.Left)
            {
                if (currentFigure.points.Count != 0)
                {
                    btnPolygon.Enabled = true;
                    if (currentFigure.points.Count >= 2)
                    {
                        btnSave.Enabled = true;
                        btnFilled.Enabled = true;
                        btnCurve.Enabled = true;
                        btnMove.Enabled = true;
                        if (currentFigure.points.Count >= 3)
                            btnBezier.Enabled = true;
                    }
                }
                // Сохраняем координаты курсора мышки
                int pointHalfRadius = currentFigure.pointRadius / 2;
                Point p = new(e.X - pointHalfRadius, e.Y - pointHalfRadius);
                // Добавляем в коллекцию точек
                currentFigure.points.Add(p);
                // Генерируем событие Paint для перерисовки точек
                Refresh();
            }
        }
        void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            for (int i = 0; i < currentFigure.points.Count; i++)
            {
                if (IsOnPoint(currentFigure.points[i], e.Location) && bMove == false)
                {
                    bPoints = !bPoints;
                    bDrag = true;

                    draggingPoint = i;
                    break;
                }
            }
        }
        void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (bDrag)
            {
                int pointHalfRadius = currentFigure.pointRadius / 2;
                currentFigure.points[draggingPoint] = new Point(e.Location.X - pointHalfRadius, e.Location.Y - pointHalfRadius);
                Refresh();
            }
        }
        void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            if (bDrag)
            {
                bDrag = false;
                bPoints = true;
            }
        }
        #endregion

        void Form1_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (figuresLst.Count != 0)
            {
                foreach (var fig in figuresLst)
                {
                    ShowPoints(g, fig);
                    if (fig.currentLineType != LineType.None)
                    {
                        ShowLines(g, fig.currentLineType);
                    }
                }
            }
            if (currentFigure.points.Count != 0)
            {
                ShowPoints(g, currentFigure);
                if (currentFigure.currentLineType != LineType.None)
                {
                    ShowLines(g, currentFigure.currentLineType);
                }
            }
        }
        void ShowPoints(Graphics g, Figure fig)
        {
            foreach (var p in fig.points)
            {
                g.FillEllipse(new SolidBrush(fig.pointColor), p.X, p.Y, fig.pointRadius, fig.pointRadius);
            }
        }
        void ShowLines(Graphics g, LineType currentLineType)
        {
            if (figuresLst.Count != 0)
            {
                foreach (var figure in figuresLst)
                {
                    ShowLineOfFigure(figure, g);
                }
            }
            if (currentFigure.points.Count > 2)
            {
                ShowLineOfFigure(currentFigure, g);
            }
        }
        void ShowLineOfFigure(Figure fig, Graphics g)
        {
            SolidBrush brush = new(fig.pointColor);
            Pen pen = new(brush, fig.lineWidth);
            Point[] pointsArray = new Point[fig.points.Count];
            int pointHalfRadius = fig.pointRadius / 2;

            for (int i = 0; i < fig.points.Count; i++)
            {
                pointsArray[i].X = fig.points[i].X + pointHalfRadius;
                pointsArray[i].Y = fig.points[i].Y + pointHalfRadius;
            }

            switch (fig.currentLineType)
            {
                // https://learn.microsoft.com/ru-ru/dotnet/api/system.drawing.graphics.drawbeziers?view=dotnet-plat-ext-7.0
                // Для Безье количество точек в массиве должно быть кратно трем плюс 1, например 4, 7 или 10.
                case LineType.Bezier:
                    if (fig.points.Count > 3 && (fig.points.Count - 1) % 3 == 0)
                        g.DrawBeziers(pen, pointsArray);
                    break;
                case LineType.Curve:
                    g.DrawClosedCurve(pen, pointsArray);
                    break;
                case LineType.Polygone:
                    g.DrawPolygon(pen, pointsArray);
                    break;
                case LineType.FilledCurve:
                    g.DrawClosedCurve(pen, pointsArray);
                    g.FillClosedCurve(brush, pointsArray);
                    break;
                default:
                    break;
            }
        }

        void BtnPoints_Click(object? sender, EventArgs e)
        {
            bPoints = !bPoints;
            bDrag = false;
            bMove = false;
            Refresh();
        }
        void BtnSave_Click(object? sender, EventArgs e)
        {
            if (bMove)
            {
                btnMove.PerformClick();
                btnPoints.Enabled = true;
            }
            if (bPoints == false)
            {
                bPoints = !bPoints;
                btnPoints.Enabled = true;
            }
            Offsets.Clear();
            figuresLst.Add(currentFigure);
            btnBezier.Enabled = false;
            btnCurve.Enabled = false;
            btnFilled.Enabled = false;
            btnPolygon.Enabled = false;
            currentFigure = new();
        }

        // Находится ли курсор над одной из точек в момент нажатия
        bool IsOnPoint(Point pixelPoint, Point cursor)
        {
            if (cursor.X >= pixelPoint.X &&
                cursor.X <= pixelPoint.X + currentFigure.pointRadius &&
                cursor.Y >= pixelPoint.Y &&
                cursor.Y <= pixelPoint.Y + currentFigure.pointRadius)
                return true;
            else
                return false;
        }

        // Отрисовка фигуры по заданным точкам
        void BtnCurve_click(object? sender, EventArgs e)
        {
            if (currentFigure.points.Count != 0)
            {
                if (currentFigure.currentLineType != LineType.Curve)
                    currentFigure.currentLineType = LineType.Curve;
                else
                    currentFigure.currentLineType = LineType.None;
                Refresh();
            }
        }
        void BtnBezier_click(object? sender, EventArgs e)
        {
            if (currentFigure.points.Count != 0)
            {
                if (currentFigure.currentLineType != LineType.Bezier)
                    currentFigure.currentLineType = LineType.Bezier;
                else
                    currentFigure.currentLineType = LineType.None;
                Refresh();
            }
        }
        void BtnPolygon_click(object? sender, EventArgs e)
        {
            if (currentFigure.points.Count != 0)
            {
                if (currentFigure.currentLineType != LineType.Polygone)
                    currentFigure.currentLineType = LineType.Polygone;
                else
                    currentFigure.currentLineType = LineType.None;
                Refresh();
            }
        }

        // Закрашивание фигуры
        void BtnFilled_click(object? sender, EventArgs e)
        {
            if (currentFigure.points.Count != 0)
            {
                if (currentFigure.currentLineType != LineType.FilledCurve)
                    currentFigure.currentLineType = LineType.FilledCurve;
                else
                    currentFigure.currentLineType = LineType.None;
                Refresh();
            }
        }

        // Очистка формы
        void BtnClear_click(object? sender, EventArgs e)
        {
            if (currentFigure.points.Count != 0)
            {
                if (bMove)
                {
                    btnMove.PerformClick();
                }
                currentFigure.points.Clear();
                btnBezier.Enabled = false;
                btnCurve.Enabled = false;
                btnFilled.Enabled = false;
                btnPolygon.Enabled = false;
                currentFigure.currentLineType = LineType.None;
            }
            if (figuresLst.Count != 0)
            {
                figuresLst.Clear();
            }
            Refresh();
        }
    }
}