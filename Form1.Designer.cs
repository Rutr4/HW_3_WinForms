namespace HW_3_WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPoints = new Button();
            btnMove = new Button();
            btnCurve = new Button();
            btnPolygon = new Button();
            btnBezier = new Button();
            btnFilled = new Button();
            btnClear = new Button();
            btnSave = new Button();
            btnParams = new Button();
            SuspendLayout();
            // 
            // btnPoints
            // 
            btnPoints.Location = new Point(12, 12);
            btnPoints.Name = "btnPoints";
            btnPoints.Size = new Size(100, 35);
            btnPoints.TabIndex = 0;
            btnPoints.Text = "Точки";
            btnPoints.UseVisualStyleBackColor = true;
            // 
            // btnMove
            // 
            btnMove.Location = new Point(12, 94);
            btnMove.Name = "btnMove";
            btnMove.Size = new Size(100, 35);
            btnMove.TabIndex = 1;
            btnMove.Text = "Движение";
            btnMove.UseVisualStyleBackColor = true;
            // 
            // btnCurve
            // 
            btnCurve.Location = new Point(118, 12);
            btnCurve.Name = "btnCurve";
            btnCurve.Size = new Size(100, 35);
            btnCurve.TabIndex = 2;
            btnCurve.Text = "Кривая";
            btnCurve.UseVisualStyleBackColor = true;
            // 
            // btnPolygon
            // 
            btnPolygon.Location = new Point(118, 94);
            btnPolygon.Name = "btnPolygon";
            btnPolygon.Size = new Size(100, 35);
            btnPolygon.TabIndex = 3;
            btnPolygon.Text = "Ломаная";
            btnPolygon.UseVisualStyleBackColor = true;
            // 
            // btnBezier
            // 
            btnBezier.Location = new Point(118, 53);
            btnBezier.Name = "btnBezier";
            btnBezier.Size = new Size(100, 35);
            btnBezier.TabIndex = 4;
            btnBezier.Text = "Безье";
            btnBezier.UseVisualStyleBackColor = true;
            // 
            // btnFilled
            // 
            btnFilled.Location = new Point(12, 53);
            btnFilled.Name = "btnFilled";
            btnFilled.Size = new Size(100, 35);
            btnFilled.TabIndex = 5;
            btnFilled.Text = "Заполненная";
            btnFilled.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(224, 12);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 35);
            btnClear.TabIndex = 6;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(224, 53);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 7;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnParams
            // 
            btnParams.Location = new Point(224, 94);
            btnParams.Name = "btnParams";
            btnParams.Size = new Size(100, 35);
            btnParams.TabIndex = 8;
            btnParams.Text = "Параметры";
            btnParams.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 461);
            Controls.Add(btnParams);
            Controls.Add(btnSave);
            Controls.Add(btnClear);
            Controls.Add(btnFilled);
            Controls.Add(btnBezier);
            Controls.Add(btnPolygon);
            Controls.Add(btnCurve);
            Controls.Add(btnMove);
            Controls.Add(btnPoints);
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(900, 400);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnPoints;
        private Button btnMove;
        private Button btnCurve;
        private Button btnPolygon;
        private Button btnBezier;
        private Button btnFilled;
        private Button btnClear;
        private Button btnSave;
        private Button btnParams;
    }
}