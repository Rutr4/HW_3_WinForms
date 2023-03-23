using System.Drawing;
using System.Windows.Forms;
using static HW_3_WinForms.Form1;

namespace HW_3_WinForms
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer moveTimer = new();
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
        Figure currentFigure = new();
        public List<Figure> figuresLst = new(); // Список сохранённых фигур

        // Flags
        bool bPoints = true;
        bool bDrag = false;
        bool bMove = false;
        bool bSave = false;

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
            /*
            System.Windows.Forms.Timer timer = new() { Interval = 30};
            timer.Tick += MovePoints;
            Paint += ShowCurve;
            btn.Click += (o, e) => timer.Enabled = !timer.Enabled;

            DoubleBuffered = true;
            */
            #endregion
        }

        #region MouseMethods
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
                if (IsTargetPoint(currentFigure.points[i], e.Location))
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
            if (currentFigure.points.Count != 0)
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
        void BtnMove_Click(object? sender, EventArgs e)
        {
            
        } //TODO: РЕАЛИЗОВАТЬ
        void BtnSave_Click(object? sender, EventArgs e)
        {
            figuresLst.Add(currentFigure);
            currentFigure = new();
        }
        
        // Находится ли курсор над одной из точек в момент нажатия
        bool IsTargetPoint(Point pixelPoint, Point cursor)
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

        // Обработка нажатия клавиш клавиатуры
        void Form1_keyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: // Очистка формы
                    btnClear.PerformClick();
                    break;
                case Keys.Space: // Включает/выключает режим движения
                    break;
                case Keys.Oemplus: // Увеличивает  скорость движения точек
                    break;
                case Keys.OemMinus: // Уменьшает скорость движения точек
                    break;
                default:
                    break;
            }
        } // TODO: ДОДЕЛАТЬ (SPACE, +, -)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool IsHandled = true;
            switch (keyData)
            {
                case Keys.Up:
                    
                    break;
                case Keys.Down:
                    
                    break;
                case Keys.Left:
                    
                    break;
                case Keys.Right:
                    
                    break;
                default:
                    IsHandled = false;
                    break;
            }
            return IsHandled;
        } // TODO: ДОДЕЛАТЬ
    }
}