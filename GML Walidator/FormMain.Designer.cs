namespace GML_Walidator
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konfiguracjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aktualizacjaSchematówXSDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorkerAktualizacjaXSD = new System.ComponentModel.BackgroundWorker();
            this.groupBoxGML = new System.Windows.Forms.GroupBox();
            this.buttonValidate = new System.Windows.Forms.Button();
            this.labelFileInfo = new System.Windows.Forms.Label();
            this.buttonSelectGMLFile = new System.Windows.Forms.Button();
            this.labelFileName = new System.Windows.Forms.Label();
            this.labelFileNameEt = new System.Windows.Forms.Label();
            this.backgroundWorkerValidate = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerLoadGML = new System.ComponentModel.BackgroundWorker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.statusStripMain.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.groupBoxGML.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMain});
            this.statusStripMain.Location = new System.Drawing.Point(0, 178);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(784, 22);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 0;
            this.statusStripMain.Text = "statusStripMain";
            // 
            // toolStripStatusLabelMain
            // 
            this.toolStripStatusLabelMain.AutoSize = false;
            this.toolStripStatusLabelMain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabelMain.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
            this.toolStripStatusLabelMain.Size = new System.Drawing.Size(769, 22);
            this.toolStripStatusLabelMain.Spring = true;
            this.toolStripStatusLabelMain.Text = "Gotowy";
            this.toolStripStatusLabelMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.konfiguracjaToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(784, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // konfiguracjaToolStripMenuItem
            // 
            this.konfiguracjaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aktualizacjaSchematówXSDToolStripMenuItem});
            this.konfiguracjaToolStripMenuItem.Name = "konfiguracjaToolStripMenuItem";
            this.konfiguracjaToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.konfiguracjaToolStripMenuItem.Text = "Konfiguracja";
            // 
            // aktualizacjaSchematówXSDToolStripMenuItem
            // 
            this.aktualizacjaSchematówXSDToolStripMenuItem.Name = "aktualizacjaSchematówXSDToolStripMenuItem";
            this.aktualizacjaSchematówXSDToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.aktualizacjaSchematówXSDToolStripMenuItem.Text = "Aktualizacja schematów XSD";
            this.aktualizacjaSchematówXSDToolStripMenuItem.Click += new System.EventHandler(this.AktualizacjaSchematówXSDToolStripMenuItem_Click);
            // 
            // backgroundWorkerAktualizacjaXSD
            // 
            this.backgroundWorkerAktualizacjaXSD.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerAktualizacjaXSD_DoWork);
            this.backgroundWorkerAktualizacjaXSD.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerAktualizacjaXSD_RunWorkerCompleted);
            // 
            // groupBoxGML
            // 
            this.groupBoxGML.Controls.Add(this.buttonValidate);
            this.groupBoxGML.Controls.Add(this.labelFileInfo);
            this.groupBoxGML.Controls.Add(this.buttonSelectGMLFile);
            this.groupBoxGML.Controls.Add(this.labelFileName);
            this.groupBoxGML.Controls.Add(this.labelFileNameEt);
            this.groupBoxGML.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBoxGML.Location = new System.Drawing.Point(12, 40);
            this.groupBoxGML.Name = "groupBoxGML";
            this.groupBoxGML.Size = new System.Drawing.Size(760, 125);
            this.groupBoxGML.TabIndex = 2;
            this.groupBoxGML.TabStop = false;
            this.groupBoxGML.Text = "DANE O PLIKU GML:";
            // 
            // buttonValidate
            // 
            this.buttonValidate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonValidate.Location = new System.Drawing.Point(604, 76);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(150, 25);
            this.buttonValidate.TabIndex = 4;
            this.buttonValidate.Text = "Walidacja";
            this.buttonValidate.UseVisualStyleBackColor = false;
            this.buttonValidate.Click += new System.EventHandler(this.ButtonValidate_Click);
            // 
            // labelFileInfo
            // 
            this.labelFileInfo.Location = new System.Drawing.Point(15, 76);
            this.labelFileInfo.Name = "labelFileInfo";
            this.labelFileInfo.Size = new System.Drawing.Size(427, 25);
            this.labelFileInfo.TabIndex = 3;
            this.labelFileInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonSelectGMLFile
            // 
            this.buttonSelectGMLFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectGMLFile.Location = new System.Drawing.Point(448, 76);
            this.buttonSelectGMLFile.Name = "buttonSelectGMLFile";
            this.buttonSelectGMLFile.Size = new System.Drawing.Size(150, 25);
            this.buttonSelectGMLFile.TabIndex = 2;
            this.buttonSelectGMLFile.Text = "Wskaż plik GML";
            this.buttonSelectGMLFile.UseVisualStyleBackColor = false;
            this.buttonSelectGMLFile.Click += new System.EventHandler(this.ButtonSelectGMLFile_Click);
            // 
            // labelFileName
            // 
            this.labelFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFileName.Location = new System.Drawing.Point(98, 35);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(656, 25);
            this.labelFileName.TabIndex = 1;
            this.labelFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelFileNameEt
            // 
            this.labelFileNameEt.AutoSize = true;
            this.labelFileNameEt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFileNameEt.Location = new System.Drawing.Point(12, 41);
            this.labelFileNameEt.Name = "labelFileNameEt";
            this.labelFileNameEt.Size = new System.Drawing.Size(80, 13);
            this.labelFileNameEt.TabIndex = 0;
            this.labelFileNameEt.Text = "Nazwa pliku:";
            // 
            // backgroundWorkerValidate
            // 
            this.backgroundWorkerValidate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerValidate_DoWork);
            this.backgroundWorkerValidate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerValidate_RunWorkerCompleted);
            // 
            // backgroundWorkerLoadGML
            // 
            this.backgroundWorkerLoadGML.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerLoadGML_DoWork);
            this.backgroundWorkerLoadGML.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerLoadGML_RunWorkerCompleted);
            // 
            // timer
            // 
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 200);
            this.Controls.Add(this.groupBoxGML);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "GML Walidator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.groupBoxGML.ResumeLayout(false);
            this.groupBoxGML.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMain;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konfiguracjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aktualizacjaSchematówXSDToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorkerAktualizacjaXSD;
        private System.Windows.Forms.GroupBox groupBoxGML;
        private System.Windows.Forms.Label labelFileNameEt;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.Button buttonSelectGMLFile;
        private System.Windows.Forms.Label labelFileInfo;
        private System.Windows.Forms.Button buttonValidate;
        private System.ComponentModel.BackgroundWorker backgroundWorkerValidate;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoadGML;
        private System.Windows.Forms.Timer timer;
    }
}

