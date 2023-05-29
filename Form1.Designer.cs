namespace image_processor
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileMontage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeometry = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeometryRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeometryScale = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeometryStretch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeometryRotateFlip = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeometryCrop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeometryCropToAspect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointInvert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointColorCutoff = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointClearRed = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointClearGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointClearBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointAverage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointGrayscale = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointSepiaTone = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointColorTone = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPointSaturate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnhancements = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnhancementsColor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnhancementsContrast = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnhancementsBrightness = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilters = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFiltersBoxBlur = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFiltersUnsharpMask = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFiltersRankFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFiltersMedianFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFiltersMinFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFiltersMaxFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFiltersCustomKernel = new System.Windows.Forms.ToolStripMenuItem();
            this.resultPictureBox = new System.Windows.Forms.PictureBox();
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdFile = new System.Windows.Forms.SaveFileDialog();
            this.cdColorTone = new System.Windows.Forms.ColorDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mnuGeometry,
            this.mnuPointOperations,
            this.mnuEnhancements,
            this.mnuFilters});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen,
            this.mnuFileSaveAs,
            this.toolStripMenuItem1,
            this.mnuFileReset,
            this.toolStripMenuItem2,
            this.mnuFileMontage,
            this.toolStripSeparator1,
            this.mnuFileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileOpen.Size = new System.Drawing.Size(176, 22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileSaveAs.Size = new System.Drawing.Size(176, 22);
            this.mnuFileSaveAs.Text = "&Save As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(173, 6);
            // 
            // mnuFileReset
            // 
            this.mnuFileReset.Name = "mnuFileReset";
            this.mnuFileReset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnuFileReset.Size = new System.Drawing.Size(176, 22);
            this.mnuFileReset.Text = "&Reset";
            this.mnuFileReset.Click += new System.EventHandler(this.mnuFileReset_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(173, 6);
            // 
            // mnuFileMontage
            // 
            this.mnuFileMontage.Name = "mnuFileMontage";
            this.mnuFileMontage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mnuFileMontage.Size = new System.Drawing.Size(176, 22);
            this.mnuFileMontage.Text = "&Montage...";
            this.mnuFileMontage.Click += new System.EventHandler(this.mnuFileMontage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(176, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuGeometry
            // 
            this.mnuGeometry.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGeometryRotate,
            this.mnuGeometryScale,
            this.mnuGeometryStretch,
            this.mnuGeometryRotateFlip,
            this.mnuGeometryCrop,
            this.mnuGeometryCropToAspect});
            this.mnuGeometry.Name = "mnuGeometry";
            this.mnuGeometry.Size = new System.Drawing.Size(71, 20);
            this.mnuGeometry.Text = "&Geometry";
            // 
            // mnuGeometryRotate
            // 
            this.mnuGeometryRotate.Name = "mnuGeometryRotate";
            this.mnuGeometryRotate.Size = new System.Drawing.Size(162, 22);
            this.mnuGeometryRotate.Text = "&Rotate...";
            this.mnuGeometryRotate.Click += new System.EventHandler(this.mnuGeometryRotate_Click);
            // 
            // mnuGeometryScale
            // 
            this.mnuGeometryScale.Name = "mnuGeometryScale";
            this.mnuGeometryScale.Size = new System.Drawing.Size(162, 22);
            this.mnuGeometryScale.Text = "&Scale...";
            this.mnuGeometryScale.Click += new System.EventHandler(this.mnuGeometryScale_Click);
            // 
            // mnuGeometryStretch
            // 
            this.mnuGeometryStretch.Name = "mnuGeometryStretch";
            this.mnuGeometryStretch.Size = new System.Drawing.Size(162, 22);
            this.mnuGeometryStretch.Text = "S&tretch...";
            this.mnuGeometryStretch.Click += new System.EventHandler(this.mnuGeometryStretch_Click);
            // 
            // mnuGeometryRotateFlip
            // 
            this.mnuGeometryRotateFlip.Name = "mnuGeometryRotateFlip";
            this.mnuGeometryRotateFlip.Size = new System.Drawing.Size(162, 22);
            this.mnuGeometryRotateFlip.Text = "Rotate/Flip...";
            this.mnuGeometryRotateFlip.Click += new System.EventHandler(this.mnuGeometryRotateFlip_Click);
            // 
            // mnuGeometryCrop
            // 
            this.mnuGeometryCrop.Name = "mnuGeometryCrop";
            this.mnuGeometryCrop.Size = new System.Drawing.Size(162, 22);
            this.mnuGeometryCrop.Text = "&Crop";
            this.mnuGeometryCrop.Click += new System.EventHandler(this.mnuGeometryCrop_Click);
            // 
            // mnuGeometryCropToAspect
            // 
            this.mnuGeometryCropToAspect.Name = "mnuGeometryCropToAspect";
            this.mnuGeometryCropToAspect.Size = new System.Drawing.Size(162, 22);
            this.mnuGeometryCropToAspect.Text = "Crop to &Aspect...";
            this.mnuGeometryCropToAspect.Click += new System.EventHandler(this.mnuGeometryCropToAspect_Click);
            // 
            // mnuPointOperations
            // 
            this.mnuPointOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPointInvert,
            this.mnuPointColorCutoff,
            this.mnuPointClearRed,
            this.mnuPointClearGreen,
            this.mnuPointClearBlue,
            this.mnuPointAverage,
            this.mnuPointGrayscale,
            this.mnuPointSepiaTone,
            this.mnuPointColorTone,
            this.mnuPointSaturate});
            this.mnuPointOperations.Name = "mnuPointOperations";
            this.mnuPointOperations.Size = new System.Drawing.Size(108, 20);
            this.mnuPointOperations.Text = "&Point Operations";
            // 
            // mnuPointInvert
            // 
            this.mnuPointInvert.Name = "mnuPointInvert";
            this.mnuPointInvert.Size = new System.Drawing.Size(149, 22);
            this.mnuPointInvert.Text = "&Invert";
            this.mnuPointInvert.Click += new System.EventHandler(this.mnuPointInvert_Click);
            // 
            // mnuPointColorCutoff
            // 
            this.mnuPointColorCutoff.Name = "mnuPointColorCutoff";
            this.mnuPointColorCutoff.Size = new System.Drawing.Size(149, 22);
            this.mnuPointColorCutoff.Text = "&Color Cutoff...";
            this.mnuPointColorCutoff.Click += new System.EventHandler(this.mnuPointColorCutoff_Click);
            // 
            // mnuPointClearRed
            // 
            this.mnuPointClearRed.Name = "mnuPointClearRed";
            this.mnuPointClearRed.Size = new System.Drawing.Size(149, 22);
            this.mnuPointClearRed.Text = "Clear &Red";
            this.mnuPointClearRed.Click += new System.EventHandler(this.mnuPointClearRed_Click);
            // 
            // mnuPointClearGreen
            // 
            this.mnuPointClearGreen.Name = "mnuPointClearGreen";
            this.mnuPointClearGreen.Size = new System.Drawing.Size(149, 22);
            this.mnuPointClearGreen.Text = "Clear &Green";
            this.mnuPointClearGreen.Click += new System.EventHandler(this.mnuPointClearGreen_Click);
            // 
            // mnuPointClearBlue
            // 
            this.mnuPointClearBlue.Name = "mnuPointClearBlue";
            this.mnuPointClearBlue.Size = new System.Drawing.Size(149, 22);
            this.mnuPointClearBlue.Text = "Clear &Blue";
            this.mnuPointClearBlue.Click += new System.EventHandler(this.mnuPointClearBlue_Click);
            // 
            // mnuPointAverage
            // 
            this.mnuPointAverage.Name = "mnuPointAverage";
            this.mnuPointAverage.Size = new System.Drawing.Size(149, 22);
            this.mnuPointAverage.Text = "&Average";
            this.mnuPointAverage.Click += new System.EventHandler(this.mnuPointAverage_Click);
            // 
            // mnuPointGrayscale
            // 
            this.mnuPointGrayscale.Name = "mnuPointGrayscale";
            this.mnuPointGrayscale.Size = new System.Drawing.Size(149, 22);
            this.mnuPointGrayscale.Text = "Gra&yscale";
            this.mnuPointGrayscale.Click += new System.EventHandler(this.mnuPointGrayscale_Click);
            // 
            // mnuPointSepiaTone
            // 
            this.mnuPointSepiaTone.Name = "mnuPointSepiaTone";
            this.mnuPointSepiaTone.Size = new System.Drawing.Size(149, 22);
            this.mnuPointSepiaTone.Text = "&Sepia Tone";
            this.mnuPointSepiaTone.Click += new System.EventHandler(this.mnuPointSepiaTone_Click);
            // 
            // mnuPointColorTone
            // 
            this.mnuPointColorTone.Name = "mnuPointColorTone";
            this.mnuPointColorTone.Size = new System.Drawing.Size(149, 22);
            this.mnuPointColorTone.Text = "Color &Tone...";
            this.mnuPointColorTone.Click += new System.EventHandler(this.mnuPointColorTone_Click);
            // 
            // mnuPointSaturate
            // 
            this.mnuPointSaturate.Name = "mnuPointSaturate";
            this.mnuPointSaturate.Size = new System.Drawing.Size(149, 22);
            this.mnuPointSaturate.Text = "Saturate";
            this.mnuPointSaturate.Click += new System.EventHandler(this.mnuPointSaturate_Click);
            // 
            // mnuEnhancements
            // 
            this.mnuEnhancements.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEnhancementsBrightness,
            this.mnuEnhancementsColor,
            this.mnuEnhancementsContrast});
            this.mnuEnhancements.Name = "mnuEnhancements";
            this.mnuEnhancements.Size = new System.Drawing.Size(97, 20);
            this.mnuEnhancements.Text = "&Enhancements";
            // 
            // mnuEnhancementsColor
            // 
            this.mnuEnhancementsColor.Name = "mnuEnhancementsColor";
            this.mnuEnhancementsColor.Size = new System.Drawing.Size(152, 22);
            this.mnuEnhancementsColor.Text = "&Color...";
            this.mnuEnhancementsColor.Click += new System.EventHandler(this.mnuEnhancementsColor_Click);
            // 
            // mnuEnhancementsContrast
            // 
            this.mnuEnhancementsContrast.Name = "mnuEnhancementsContrast";
            this.mnuEnhancementsContrast.Size = new System.Drawing.Size(152, 22);
            this.mnuEnhancementsContrast.Text = "C&ontrast...";
            this.mnuEnhancementsContrast.Click += new System.EventHandler(this.mnuEnhancementsContrast_Click);
            // 
            // mnuEnhancementsBrightness
            // 
            this.mnuEnhancementsBrightness.Name = "mnuEnhancementsBrightness";
            this.mnuEnhancementsBrightness.Size = new System.Drawing.Size(152, 22);
            this.mnuEnhancementsBrightness.Text = "&Brightness...";
            this.mnuEnhancementsBrightness.Click += new System.EventHandler(this.mnuEnhancementsBrightness_Click);
            // 
            // mnuFilters
            // 
            this.mnuFilters.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFiltersBoxBlur,
            this.mnuFiltersUnsharpMask,
            this.mnuFiltersRankFilter,
            this.mnuFiltersMedianFilter,
            this.mnuFiltersMinFilter,
            this.mnuFiltersMaxFilter,
            this.mnuFiltersCustomKernel});
            this.mnuFilters.Name = "mnuFilters";
            this.mnuFilters.Size = new System.Drawing.Size(50, 20);
            this.mnuFilters.Text = "&Filters";
            // 
            // mnuFiltersBoxBlur
            // 
            this.mnuFiltersBoxBlur.Name = "mnuFiltersBoxBlur";
            this.mnuFiltersBoxBlur.Size = new System.Drawing.Size(161, 22);
            this.mnuFiltersBoxBlur.Text = "&Box Blur...";
            this.mnuFiltersBoxBlur.Click += new System.EventHandler(this.mnuFiltersBoxBlur_Click);
            // 
            // mnuFiltersUnsharpMask
            // 
            this.mnuFiltersUnsharpMask.Name = "mnuFiltersUnsharpMask";
            this.mnuFiltersUnsharpMask.Size = new System.Drawing.Size(161, 22);
            this.mnuFiltersUnsharpMask.Text = "&Unsharp Mask...";
            this.mnuFiltersUnsharpMask.Click += new System.EventHandler(this.mnuFiltersUnsharpMask_Click);
            // 
            // mnuFiltersRankFilter
            // 
            this.mnuFiltersRankFilter.Name = "mnuFiltersRankFilter";
            this.mnuFiltersRankFilter.Size = new System.Drawing.Size(161, 22);
            this.mnuFiltersRankFilter.Text = "&Rank Filter...";
            this.mnuFiltersRankFilter.Click += new System.EventHandler(this.mnuFiltersRankFilter_Click);
            // 
            // mnuFiltersMedianFilter
            // 
            this.mnuFiltersMedianFilter.Name = "mnuFiltersMedianFilter";
            this.mnuFiltersMedianFilter.Size = new System.Drawing.Size(161, 22);
            this.mnuFiltersMedianFilter.Text = "&Median Filter...";
            this.mnuFiltersMedianFilter.Click += new System.EventHandler(this.mnuFiltersMedianFilter_Click);
            // 
            // mnuFiltersMinFilter
            // 
            this.mnuFiltersMinFilter.Name = "mnuFiltersMinFilter";
            this.mnuFiltersMinFilter.Size = new System.Drawing.Size(161, 22);
            this.mnuFiltersMinFilter.Text = "Min Filter...";
            this.mnuFiltersMinFilter.Click += new System.EventHandler(this.mnuFiltersMinFilter_Click);
            // 
            // mnuFiltersMaxFilter
            // 
            this.mnuFiltersMaxFilter.Name = "mnuFiltersMaxFilter";
            this.mnuFiltersMaxFilter.Size = new System.Drawing.Size(161, 22);
            this.mnuFiltersMaxFilter.Text = "Max Filter...";
            this.mnuFiltersMaxFilter.Click += new System.EventHandler(this.mnuFiltersMaxFilter_Click);
            // 
            // mnuFiltersCustomKernel
            // 
            this.mnuFiltersCustomKernel.Name = "mnuFiltersCustomKernel";
            this.mnuFiltersCustomKernel.Size = new System.Drawing.Size(161, 22);
            this.mnuFiltersCustomKernel.Text = "&Custom Kernel...";
            this.mnuFiltersCustomKernel.Click += new System.EventHandler(this.mnuFiltersCustomKernel_Click);
            // 
            // resultPictureBox
            // 
            this.resultPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.resultPictureBox.Location = new System.Drawing.Point(12, 27);
            this.resultPictureBox.Name = "resultPictureBox";
            this.resultPictureBox.Size = new System.Drawing.Size(100, 100);
            this.resultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.resultPictureBox.TabIndex = 1;
            this.resultPictureBox.TabStop = false;
            this.resultPictureBox.Visible = false;
            // 
            // ofdFile
            // 
            this.ofdFile.DefaultExt = "png";
            this.ofdFile.Filter = "Image Files|*.png;*.bmp;*.jpg;*.gif;*.tif;*.exif|All Files|*.*";
            // 
            // sfdFile
            // 
            this.sfdFile.DefaultExt = "png";
            this.sfdFile.Filter = "Image Files|*.png;*.bmp;*.jpg;*.gif;*.tif;*.exif|All Files|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.resultPictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "image_processor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileReset;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuGeometry;
        private System.Windows.Forms.ToolStripMenuItem mnuPointOperations;
        private System.Windows.Forms.ToolStripMenuItem mnuEnhancements;
        private System.Windows.Forms.ToolStripMenuItem mnuFilters;
        private System.Windows.Forms.PictureBox resultPictureBox;
        private System.Windows.Forms.OpenFileDialog ofdFile;
        private System.Windows.Forms.SaveFileDialog sfdFile;
        private System.Windows.Forms.ToolStripMenuItem mnuGeometryRotate;
        private System.Windows.Forms.ToolStripMenuItem mnuGeometryScale;
        private System.Windows.Forms.ToolStripMenuItem mnuGeometryStretch;
        private System.Windows.Forms.ToolStripMenuItem mnuGeometryRotateFlip;
        private System.Windows.Forms.ToolStripMenuItem mnuGeometryCrop;
        private System.Windows.Forms.ToolStripMenuItem mnuGeometryCropToAspect;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMontage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuPointInvert;
        private System.Windows.Forms.ToolStripMenuItem mnuPointColorCutoff;
        private System.Windows.Forms.ToolStripMenuItem mnuPointClearRed;
        private System.Windows.Forms.ToolStripMenuItem mnuPointClearGreen;
        private System.Windows.Forms.ToolStripMenuItem mnuPointClearBlue;
        private System.Windows.Forms.ToolStripMenuItem mnuPointAverage;
        private System.Windows.Forms.ToolStripMenuItem mnuPointGrayscale;
        private System.Windows.Forms.ToolStripMenuItem mnuPointSepiaTone;
        private System.Windows.Forms.ToolStripMenuItem mnuPointColorTone;
        private System.Windows.Forms.ToolStripMenuItem mnuPointSaturate;
        private System.Windows.Forms.ColorDialog cdColorTone;
        private System.Windows.Forms.ToolStripMenuItem mnuEnhancementsColor;
        private System.Windows.Forms.ToolStripMenuItem mnuEnhancementsContrast;
        private System.Windows.Forms.ToolStripMenuItem mnuEnhancementsBrightness;
        private System.Windows.Forms.ToolStripMenuItem mnuFiltersBoxBlur;
        private System.Windows.Forms.ToolStripMenuItem mnuFiltersUnsharpMask;
        private System.Windows.Forms.ToolStripMenuItem mnuFiltersRankFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuFiltersMedianFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuFiltersMinFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuFiltersMaxFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuFiltersCustomKernel;
    }
}

