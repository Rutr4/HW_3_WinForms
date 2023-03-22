using System.Windows.Forms;

namespace HW_3_WinForms
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer moveTimer = new();

        // Flags
        bool bPoints = true;
        bool bMove = false;
        bool bDrag = false;

        #region Properties

        // PointsProps
        public List<Point> pointsLst = new();
        int draggingPoint;
        int pointRadius { get; set; } = 8;
        Color pointColor = Color.Red;

        // LineProps
        public enum LineType { None, Curve, Bezier, Polygone, FilledCurve };
        public LineType currentLineType;
        int lineWidth = 5;
        Color lineColor = Color.Green;

        #endregion

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

            // buttons
            btnPoints.Click += BtnPoints_Click;
            btnMove.Click += BtnMove_Click;
            btnCurve.Click += BtnCurve_click;
            btnBezier.Click += BtnBezier_click;
            btnPolygon.Click += BtnPolygon_click;
            btnFilled.Click += BtnFilled_click;
            btnClear.Click += BtnClear_click;

            btnBezier.Enabled = true;
            btnCurve.Enabled = false;
            btnFilled.Enabled = false;
            btnPolygon.Enabled = false;
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
        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int pointHalfRadius = pointRadius / 2;

            if (bPoints && e.Button == MouseButtons.Left)
            {
                // Сохраняем координаты курсора мышки
                Point p = new(e.X - pointHalfRadius, e.Y - pointHalfRadius);

                // Добавляем в коллекцию точек
                pointsLst.Add(p);

                // Генерируем событие Paint для перерисовки точек
                Refresh();
            }
        }
        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < pointsLst.Count; i++)
            {
                if (IsTargetPoint(pointsLst[i], e.Location))
                {
                    bDrag = true;
                    draggingPoint = i;
                    break;
                }
            }
        }
        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bDrag)
            {
                pointsLst[draggingPoint] = new Point(e.Location.X - pointRadius / 2, e.Location.Y - pointRadius / 2);
                Refresh();
            }
        }
        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            bDrag = false;
        }
        #endregion

        void Form1_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (pointsLst.Count != 0)
            {
                ShowPoints(g);
                if (currentLineType != LineType.None)
                {
                    ShowLine(g, currentLineType);
                }
            }
        }
        void ShowPoints(Graphics g)
        {
            foreach (var p in pointsLst)
            {
                g.FillEllipse(new SolidBrush(pointColor), p.X, p.Y, pointRadius, pointRadius);
            }
        }
        void ShowLine(Graphics g, LineType currentLineType)
        {
            SolidBrush brush = new SolidBrush(lineColor);
            Pen pen = new Pen(brush, lineWidth);

            Point[] pointsArray = new Point[pointsLst.Count];
            int pointHalfRadius = pointRadius / 2;

            for (int i = 0; i < pointsLst.Count; i++)
            {
                pointsArray[i].X = pointsLst[i].X + pointHalfRadius;
                pointsArray[i].Y = pointsLst[i].Y + pointHalfRadius;
            } 

            switch (currentLineType)
            {
                // https://learn.microsoft.com/ru-ru/dotnet/api/system.drawing.graphics.drawbeziers?view=dotnet-plat-ext-7.0
                // Количество точек в массиве должно быть кратно трем плюс 1, например 4, 7 или 10.
                case LineType.Bezier:
                    if (pointsLst.Count > 3 && (pointsLst.Count - 1) % 3 == 0)
                        g.DrawBeziers(pen, pointsArray);
                    break;
                case LineType.Curve:
                    g.DrawClosedCurve(pen, pointsArray);
                    break;
                case LineType.Polygone:
                    g.DrawPolygon(pen, pointsArray);
                    break;
                case LineType.FilledCurve:
                    g.FillClosedCurve(brush, pointsArray);
                    break;
                default:
                    break;
            }
        }
        void BtnPoints_Click(object sender, EventArgs e)
        {
            bPoints = !bPoints;
            bDrag = false;
            bMove = false;
            Refresh();
        }
        void BtnMove_Click(object? sender, EventArgs e)
        {
            
        } //TODO: РЕАЛИЗОВАТЬ
        
        // Находится ли курсор над одной из точек в момент нажатия
        bool IsTargetPoint(Point pixelPoint, Point cursor)
        {
            if (cursor.X >= pixelPoint.X &&
                cursor.X <= pixelPoint.X + pointRadius &&
                cursor.Y >= pixelPoint.Y &&
                cursor.Y <= pixelPoint.Y + pointRadius)
                return true;
            else
                return false;
        }

        // Отрисовка фигуры по заданным точкам
        void BtnCurve_click(object? sender, EventArgs e)
        {
            if (pointsLst.Count != 0)
            {
                if (currentLineType != LineType.Curve)
                    currentLineType = LineType.Curve;
                else
                    currentLineType = LineType.None;
                Refresh();
            }
        }
        void BtnBezier_click(object? sender, EventArgs e)
        {
            if (pointsLst.Count != 0)
            {
                if (currentLineType != LineType.Bezier)
                    currentLineType = LineType.Bezier;
                else
                    currentLineType = LineType.None;
                Refresh();
            }
        }
        void BtnPolygon_click(object? sender, EventArgs e)
        {
            if (pointsLst.Count != 0)
            {
                if (currentLineType != LineType.Polygone)
                    currentLineType = LineType.Polygone;
                else
                    currentLineType = LineType.None;
                Refresh();
            }
        }
        
        // Закрашивание фигуры
        void BtnFilled_click(object? sender, EventArgs e)
        {
            if (pointsLst.Count != 0)
            {
                if (currentLineType != LineType.FilledCurve)
                    currentLineType = LineType.FilledCurve;
                else
                    currentLineType = LineType.None;
                Refresh();
            }
        }

        // Очистка формы
        void BtnClear_click(object? sender, EventArgs e)
        {
            if (pointsLst.Count != 0)
            {
                pointsLst.Clear();
                btnBezier.Enabled = false;
                btnCurve.Enabled = false;
                btnFilled.Enabled = false;
                btnPolygon.Enabled = false;
            }
            Refresh();
        }
    }
}