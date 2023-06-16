using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace image_processor
{
    public partial class KernelForm : Form
    {
        public KernelForm()
        {
            InitializeComponent();
        }

        // The preset kernels.
        private string[,] Presets =
        {
            {"Emboss 1", "-1, 0, 0\r\n 0, 0, 0\r\n 0, 0, 1", "1", "127"},
            {"Emboss 2", " 2, 0, 0\r\n 0,-1, 0\r\n 0, 0,-1", "1", "127"},
            {"Gaussian Blur 5x5", "1,  4,  7,  4, 1\r\n4, 16, 26, 16, 4\r\n7, 26, 41, 26, 7\r\n4, 16, 26, 16, 4\r\n1,  4,  7,  4, 1", "273", "0"},
            {"Box Blur 5x5", "1, 1, 1, 1, 1\r\n1, 1, 1, 1, 1\r\n1, 1, 1, 1, 1\r\n1, 1, 1, 1, 1\r\n1, 1, 1, 1, 1", "25", "0"},
            {"High Pass 1", "-1,  2, -1\r\n-2, 12, -2\r\n-1,  2, -1", "16", "63"},
            {"High Pass 2", " 0, -1, 0\r\n-1,  4, -1\r\n 0, -1, 0", "1", "128"},
            {"Edge Detector NW", "-5, 0, 0\r\n 0, 0, 0\r\n 0, 0, 5", "1", "0"},
            {"Edge Detector N",  "-1,-1,-1\r\n 0, 0, 0\r\n 1, 1, 1", "1", "0"},
            {"Edge Detector NE", " 0, 0,-5\r\n 0, 0, 0\r\n 5, 0, 0", "1", "0"},
            {"Edge Detector E",  " 1, 0,-1\r\n 1, 0,-1\r\n 1, 0,-1", "1", "0"},
            {"Edge Detector SE", " 5, 0, 0\r\n 0, 0, 0\r\n 0, 0,-5", "1", "0"},
            {"Edge Detector S",  " 1, 1, 1\r\n 0, 0, 0\r\n-1,-1,-1", "-1", "0"},
            {"Edge Detector SW", " 0, 0, 5\r\n 0, 0, 0\r\n-5, 0, 0", "1", "0"},
            {"Edge Detector W",  "-1, 0, 1\r\n-1, 0, 1\r\n-1, 0, 1", "1", "0"},
        };

        // Initialize the presets and select the first one.
        private void KernelForm_Load(object sender, EventArgs e)
        {
            int numPresets = Presets.GetUpperBound(0) + 1;
            for (int i = 0; i < numPresets; i++)
            {
                presetsComboBox.Items.Add(Presets[i, 0]);
            }
            presetsComboBox.SelectedIndex = 0;
        }

        //  Display the selected preset's values.
        private void presetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = presetsComboBox.SelectedIndex;
            valueTextBox.Text = Presets[i, 1];  // Kernel
            weightTextBox.Text = Presets[i, 2]; // Weight
            offsetTextBox.Text = Presets[i, 3]; // Offset
        }
    }
}
