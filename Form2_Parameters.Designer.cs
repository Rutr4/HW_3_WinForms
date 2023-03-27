namespace HW_3_WinForms
{
    partial class Form2_Parameters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOk = new Button();
            btnClose = new Button();
            label1 = new Label();
            label2 = new Label();
            TrackBarPointSize = new TrackBar();
            trackBarLineWidth = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)TrackBarPointSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).BeginInit();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(80, 200);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 30);
            btnOk.TabIndex = 0;
            btnOk.Text = "Принять";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(225, 200);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 30);
            btnClose.TabIndex = 1;
            btnClose.Text = "Закрыть";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 30);
            label1.Name = "label1";
            label1.Size = new Size(106, 15);
            label1.TabIndex = 2;
            label1.Text = "Размер точки (px)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 100);
            label2.Name = "label2";
            label2.Size = new Size(117, 15);
            label2.TabIndex = 3;
            label2.Text = "Ширина линии  (px)";
            // 
            // TrackBarPointSize
            // 
            TrackBarPointSize.LargeChange = 1;
            TrackBarPointSize.Location = new Point(12, 45);
            TrackBarPointSize.Maximum = 15;
            TrackBarPointSize.Minimum = 2;
            TrackBarPointSize.Name = "TrackBarPointSize";
            TrackBarPointSize.Size = new Size(150, 45);
            TrackBarPointSize.TabIndex = 4;
            TrackBarPointSize.Value = 2;
            // 
            // trackBarLineWidth
            // 
            trackBarLineWidth.LargeChange = 2;
            trackBarLineWidth.Location = new Point(12, 122);
            trackBarLineWidth.Maximum = 20;
            trackBarLineWidth.Minimum = 2;
            trackBarLineWidth.Name = "trackBarLineWidth";
            trackBarLineWidth.Size = new Size(150, 45);
            trackBarLineWidth.TabIndex = 5;
            trackBarLineWidth.Value = 2;
            // 
            // Form2_Parameters
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 255);
            Controls.Add(trackBarLineWidth);
            Controls.Add(TrackBarPointSize);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnClose);
            Controls.Add(btnOk);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form2_Parameters";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Параметры";
            ((System.ComponentModel.ISupportInitialize)TrackBarPointSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOk;
        private Button btnClose;
        private Label label1;
        private Label label2;
        private TrackBar TrackBarPointSize;
        private TrackBar trackBarLineWidth;
    }
}