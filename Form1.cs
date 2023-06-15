using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Drawing.Text;

namespace image_processor
{
    public partial class Form1 : Form
    {
        private const InterpolationMode _interpolationMode = InterpolationMode.High;

        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap OriginalBm = null;
        private Bitmap CurrentBm = null;

        private Point SelectionStart, SelectionEnd;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Disable menu items because no image is loaded.
            SetMenusEditable(false);
        }

        // Enable or disable menu items that are
        // appropriate when an image is loaded.
        private void SetMenusEditable(bool enabled)
        {
            ToolStripMenuItem[] items =
            {
                mnuFileSaveAs,
                mnuFileReset,
                mnuGeometry,
                mnuPointOperations,
                mnuEnhancements,
                mnuFilters,
            };
            foreach (ToolStripMenuItem item in items)
                item.Enabled = enabled;
            resultPictureBox.Visible = enabled;
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            ofdFile.Title = "Open Image File";
            ofdFile.Multiselect = false;
            if (ofdFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap bm = LoadBitmapUnlocked(ofdFile.FileName);
                    OriginalBm = bm;
                    ResetImage();

                    // Enable menu items because an image is loaded.
                    SetMenusEditable(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(
                        "Error opening file {0}.\n{1}",
                        ofdFile.FileName, ex.Message));
                }
            }
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            sfdFile.Title = "Save As";
            if (sfdFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CurrentBm.SaveImage(sfdFile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(
                        "Error saving file {0}.\n{1}",
                        sfdFile.FileName, ex.Message));
                }
            }
        }

        // Restore the original unmodified image.
        private void mnuFileReset_Click(object sender, EventArgs e)
        {
            ResetImage();
        }

        private void ResetImage()
        {
            CurrentBm = (Bitmap)OriginalBm.Clone();
            resultPictureBox.Image = CurrentBm;
        }

        // Make a montage of files.
        private void mnuFileMontage_Click(object sender, EventArgs e)
        {
            // Let the user select the files.
            ofdFile.Title = "Select Montage Files";
            ofdFile.Multiselect = true;
            if (ofdFile.ShowDialog() == DialogResult.OK)
            {
                OriginalBm = MakeMontage(ofdFile.FileNames, Color.Black);
                CurrentBm = (Bitmap)OriginalBm.Clone();
                resultPictureBox.Image = CurrentBm;

                // Enable menu items because an image is loaded.
                SetMenusEditable(true);
            }
        }

        private const int COLUMNS_PER_ROW = 4;
        // Make a montage of files, four per row.
        private Bitmap MakeMontage(string[] filenames, Color bgColor)
        {
            int maxWidth = 0;
            int maxHeight = 0;

            var images = new Bitmap[filenames.Length];
            for (int i = 0; i < filenames.Length; i++)
            {
                var image = LoadBitmapUnlocked(filenames[i]);
                images[i] = image;
                maxWidth = Math.Max(maxWidth, image.Width);
                maxHeight = Math.Max(maxHeight, image.Height);
            }

            int rows = (images.Length - 1) / COLUMNS_PER_ROW + 1;
            int cols = Math.Min(images.Length, 4);

            Bitmap result = new Bitmap(cols * maxWidth, rows * maxHeight);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(bgColor);
                for (int i = 0; i < images.Length; i++)
                {
                    int x = (i % COLUMNS_PER_ROW) * maxWidth;
                    int y = (i / COLUMNS_PER_ROW) * maxHeight;
                    g.DrawImage(images[i], x, y);
                }
            }

            return result;
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Load a bitmap without locking it.
        private Bitmap LoadBitmapUnlocked(string file_name)
        {
            using (Bitmap bm = new Bitmap(file_name))
            {
                return new Bitmap(bm);
            }
        }

        // Rotate the image.
        private void mnuGeometryRotate_Click(object sender, EventArgs e)
        {
            var angle = InputForm.GetFloat("Rotate image", "Rotation angle", 
                "0", float.NegativeInfinity, float.PositiveInfinity, "Rotation angle must be a number");
            if (angle == float.NaN)
                return;

            CurrentBm = CurrentBm.RotateAtCenter(angle, Color.Black, _interpolationMode); ;
            resultPictureBox.Image = CurrentBm;
        }

        // Scale the image uniformly.
        private void mnuGeometryScale_Click(object sender, EventArgs e)
        {
            var angle = InputForm.GetFloat("Scale image", "Scale factor", "1,0",
                0.001f, 100f, "Scale factor must be between 0.01 and 100");
            if (angle == float.NaN)
                return;

            CurrentBm = CurrentBm.Scale(angle, _interpolationMode);
            resultPictureBox.Image = CurrentBm;
        }

        private void mnuGeometryStretch_Click(object sender, EventArgs e)
        {
            var scales = InputForm
                .GetString("Stretch image", "Stretch X, Y", "3; 2")
                .Split(';')
                .Select(s => float.Parse(s.Trim()))
                .ToArray();

            if (scales.Length < 2 || scales.Any(s => s <= 0))
            {
                MessageBox.Show("Two strech factors < 0 must be privided.", "Strech image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CurrentBm = CurrentBm.Scale(scales[0], scales[1], _interpolationMode);
            resultPictureBox.Image = CurrentBm;
        }

        private static readonly IDictionary<int, RotateFlipType> RotateFlipTypeSelections = new Dictionary<int, RotateFlipType>()
        {
            [1] = RotateFlipType.RotateNoneFlipX,
            [2] = RotateFlipType.RotateNoneFlipY,
            [3] = RotateFlipType.Rotate90FlipNone,
            [4] = RotateFlipType.Rotate180FlipNone,
            [5] = RotateFlipType.Rotate270FlipNone
        };

        private void mnuGeometryRotateFlip_Click(object sender, EventArgs e)
        {
            var labelText = new StringBuilder()
                .AppendLine("1) Flip Horizontal")
                .AppendLine("2) Flip Vertical")
                .AppendLine("3) Rotate 90")
                .AppendLine("4) Rotate 180")
                .AppendLine("5) Rotate 270")
                .ToString();

            var selection = InputForm
                .GetInt("Rotate/Flip", labelText, "1", 1, 5, "Select a number from 1 to 5");
            
            if (selection == int.MinValue)
                return;

            CurrentBm.RotateFlip(RotateFlipTypeSelections[selection]);
            resultPictureBox.Image = CurrentBm;
        }

        #region Cropping

        // Let the user select an area and crop to that area.
        private void mnuGeometryCrop_Click(object sender, EventArgs e)
        {
            resultPictureBox.MouseDown += crop_MouseDown;
            resultPictureBox.Cursor = Cursors.Cross;

        }

        private void crop_MouseDown(object sender, MouseEventArgs e)
        {
            resultPictureBox.MouseDown -= crop_MouseDown;
            resultPictureBox.MouseUp += crop_MouseUp;
            resultPictureBox.MouseMove += crop_MouseMove;
            resultPictureBox.Paint += resultPictureBox_Paint;

            SelectionStart = e.Location;
            SelectionEnd = e.Location;
        }

        private Rectangle Selection => SelectionStart.ToRectangle(SelectionEnd);

        private void resultPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawDashedRectangle(Selection, Color.Red, Color.White, 1, 2);
        }

        private void crop_MouseMove(object sender, MouseEventArgs e)
        {
            SelectionEnd = e.Location;
            resultPictureBox.Refresh();
        }

        private void crop_MouseUp(object sender, MouseEventArgs e)
        {
            resultPictureBox.Cursor = Cursors.Default;
            resultPictureBox.MouseUp -= crop_MouseUp;
            resultPictureBox.MouseMove -= crop_MouseMove;
            resultPictureBox.Paint -= resultPictureBox_Paint;

            var selection = Selection;
            if (selection.Size.IsEmpty)
                return;

            CurrentBm = CurrentBm.Crop(selection, _interpolationMode);
            resultPictureBox.Image = CurrentBm;
        }

        // Let the user select an area with a desired
        // aspect ratio and crop to that area.
        private void mnuGeometryCropToAspect_Click(object sender, EventArgs e)
        {

        }

        #endregion Cropping

        #region Point Processes

        // Set each color component to 255 - the original value.
        private void mnuPointInvert_Click(object sender, EventArgs e)
        {
            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) => {
                r = (byte)(255 - r);
                g = (byte)(255 - g);
                b = (byte)(255 - b);
            });

            resultPictureBox.Image = CurrentBm;
        }

        // Set color components less than a specified value to 0.
        private void mnuPointColorCutoff_Click(object sender, EventArgs e)
        {

            int cutoff = InputForm.GetInt("Cutoff", "Specify cutoff value:", "128", 0, 255,
                "Specify a cutoff value in [0,255]");

            if (cutoff == int.MinValue)
                return;

            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) =>
            {
                if (r < cutoff) r = 0;
                if (g < cutoff) g = 0;
                if (b < cutoff) b = 0;
            });

            resultPictureBox.Image = CurrentBm;
        }

        // Set each pixel's red color component to 0.
        private void mnuPointClearRed_Click(object sender, EventArgs e)
        {
            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) => r = 0);
            resultPictureBox.Image = CurrentBm;
        }

        // Set each pixel's green color component to 0.
        private void mnuPointClearGreen_Click(object sender, EventArgs e)
        {
            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) => g = 0);
            resultPictureBox.Image = CurrentBm;

        }

        // Set each pixel's blue color component to 0.
        private void mnuPointClearBlue_Click(object sender, EventArgs e)
        {
            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) => b = 0);
            resultPictureBox.Image = CurrentBm;
        }

        // Average each pixel's color component.
        private void mnuPointAverage_Click(object sender, EventArgs e)
        {
            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) =>
            {
                byte avg = (byte)((r + g + b) / 3);
                r = b = g = avg;
            });
            resultPictureBox.Image = CurrentBm;
        }

        // Convert each pixel to grayscale.
        private void mnuPointGrayscale_Click(object sender, EventArgs e)
        {
            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) =>
            {
                byte avg = (byte)(r * 0.2126 + g * 0.7152 + b * 0.0722);
                r = b = g = avg;
            });
            resultPictureBox.Image = CurrentBm;
        }

        private static readonly float[] sepiaMatrix = new float[]
        {
            0.293f, 0.769f, 0.189f,
            0.349f, 0.686f, 0.168f,
            0.272f, 0.534f, 0.131f
        };

        // Convert each pixel to sepia tone.
        private void mnuPointSepiaTone_Click(object sender, EventArgs e)
        {
            CurrentBm.ApplyPointMatrix(sepiaMatrix);
         
            resultPictureBox.Image = CurrentBm;
        }

        // Apply a color tone to the image.
        private void mnuPointColorTone_Click(object sender, EventArgs e)
        {
            if (cdColorTone.ShowDialog() == DialogResult.OK)
            {
                float newR = cdColorTone.Color.R;
                float newG = cdColorTone.Color.G;
                float newB = cdColorTone.Color.B;

                CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) =>
                {
                    float brightness = (r + g + b) / (3f * 255f);
                    r = (byte)(brightness * newR);
                    g = (byte)(brightness * newG);
                    b = (byte)(brightness * newB);
                });
            }

            resultPictureBox.Image = CurrentBm;
        }

        // Set non-maximal color components to 0.
        private void mnuPointSaturate_Click(object sender, EventArgs e)
        {
            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) =>
            {
                int max = new int[] { r, g, b }
                    .Max();

                if (r < max) r = 0;
                if (g < max) g = 0;
                if (b < max) b = 0;
            });

            resultPictureBox.Image = CurrentBm;
        }

        #endregion Point Processes

        #region Enhancements

        private void mnuEnhancementsColor_Click(object sender, EventArgs e)
        {
            float factor = InputForm.GetFloat(
              "Color",
              "Saturation factor:",
              "1,5", 0f, 2f,
              "The saturation factor should be a floating point number beween 0.0 and 2.0");

            if (float.IsNaN(factor))
                return;

            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) =>
            {
                HSL hsl = HSL.FromRgb(r, g, b);
                hsl.S = hsl.S.AdjustValue(factor);
                hsl.ToRgb(out r, out g, out b);
            });
            resultPictureBox.Refresh();

        }

        // Use histogram stretching to modify contrast.
        private void mnuEnhancementsContrast_Click(object sender, EventArgs e)
        {
            float factor = InputForm.GetFloat(
               "Contrast",
               "Contrast factor:",
               "1,5", 0f, 2f,
               "The brightness factor should be > 0.0");

            if (float.IsNaN(factor) || factor <= 0)
                return;

            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) =>
            {
                r = ToByte(128 + (r - 128) * factor);
                g = ToByte(128 + (g - 128) * factor);
                b = ToByte(128 + (b - 128) * factor);
            });
            resultPictureBox.Refresh();
        }

        private byte ToByte(float f)
        {
            if (f< 0) return (byte)0;
            if (f > 255) return (byte)255;
            return (byte)Math.Round(f);
        }

        private void mnuEnhancementsBrightness_Click(object sender, EventArgs e)
        {
            float factor = InputForm.GetFloat(
                "Brigtness",
                "Brightness factor:",
                "1,5", 0f, 2f,
                "The brightness factor should be a floating point number beween 0.0 and 2.0");

            if (float.IsNaN(factor))
                return;

            CurrentBm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) =>
            {
                HSL hsl = HSL.FromRgb(r, g, b);
                hsl.L = hsl.L.AdjustValue(factor);
                hsl.ToRgb(out r, out g, out b);
            });
            resultPictureBox.Refresh();
        }

        #endregion Enhancements

        #region Filters

        private void mnuFiltersBoxBlur_Click(object sender, EventArgs e)
        {
            int radius = InputForm.GetInt(
                "Box Blur Filter", "Radius", "1", 1, 100, "Radius is an int between 1 and 100");

            if (radius < 0) return;

            CurrentBm = CurrentBm.BoxBlur(radius);
            resultPictureBox.Image = CurrentBm;
        }

        private void mnuFiltersUnsharpMask_Click(object sender, EventArgs e)
        {

        }

        private void mnuFiltersRankFilter_Click(object sender, EventArgs e)
        {

        }

        private void mnuFiltersMedianFilter_Click(object sender, EventArgs e)
        {

        }

        private void mnuFiltersMinFilter_Click(object sender, EventArgs e)
        {

        }

        private void mnuFiltersMaxFilter_Click(object sender, EventArgs e)
        {

        }

        // Display a dialog where the user can select
        // and modify a default kernel.
        // If the user clicks OK, apply the kernel.
        private void mnuFiltersCustomKernel_Click(object sender, EventArgs e)
        {

        }

        #endregion Filters

    }
}
