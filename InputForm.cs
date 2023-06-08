using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_processor
{
    public partial class InputForm : Form
    {
        internal static string GetString(string title, string prompt, string defaultValue)
        {
            var form = new InputForm();
            form.Text = title;
            form.captionLabel.Text = prompt;
            form.valueTextBox.Text = defaultValue;

            if (form.ShowDialog() == DialogResult.OK)
                return form.valueTextBox.Text;
            
            return null;
        }

        internal static float GetFloat(string title, string prompt, string defaultValue,
            float min, float max, string errorMessage)
        {
            var result = GetString(title, prompt, defaultValue);
            if (result is null)
                return float.NaN;

            if (float.TryParse(result, out float f) && f >= min && f <= max)
                    return f;

            ShowErrorMessage(errorMessage);
            return float.NaN;
        }

        internal static int GetInt(string title, string prompt, string defaultValue,
          int min, int max, string errorMessage)
        {
            var result = GetString(title, prompt, defaultValue);
            if (result is null)
                return int.MinValue;

            if (int.TryParse(result, out int i) && i >= min && i <= max)
                return i;
            
            ShowErrorMessage(errorMessage);
            return int.MinValue;
        }

        private static void ShowErrorMessage(string message) => MessageBox.Show(message, "Fehler bei der Eingabe", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        public InputForm()
        {
            InitializeComponent();
        }
    }
}
