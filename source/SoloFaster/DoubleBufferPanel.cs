using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SoloFaster
{
    public partial class DoubleBufferPanel : UserControl
    {
        public DoubleBufferPanel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
        }
    }
}
