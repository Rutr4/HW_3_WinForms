using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_3_WinForms
{
    public partial class Form2_Parameters : Form
    {
        public Form2_Parameters(Form1 parentForm)
        {
            InitializeComponent();

            FormClosed += Form2_Closed;
            
            ColorDialog colorDialog = new ColorDialog();
            var resColorDialog = colorDialog.ShowDialog();
            if (resColorDialog == DialogResult.OK)
            {
                var color = colorDialog.Color;
                parentForm.BackColor = color;
            }
            //pointResize.ValueChanged += ptSizeUpDown_ValueChanged;
            //lineResize.ValueChanged += lineSizeUpDown_ValueChanged;
        }

        private void Form2_Closed(object? sender, FormClosedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
