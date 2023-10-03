using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using GML_Tools.GmlFile;
using GML_Tools.xsd;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace GML_Walidator
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName + " " + Application.ProductVersion;

            buttonValidate.Enabled = false;
           
            toolStripStatusLabelMain.Text = "Gotowy";
        }

        private void AktualizacjaSchematówXSDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            aktualizacjaSchematówXSDToolStripMenuItem.Enabled = false;
            buttonSelectGMLFile.Enabled = false;
            buttonValidate.Enabled = false;
            
            backgroundWorkerAktualizacjaXSD.RunWorkerAsync();
        }

        private void BackgroundWorkerAktualizacjaXSD_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            toolStripStatusLabelMain.Text = "Aktualizacja schematów XSD...";

            NativeMethods.AllocConsole();

            XsdFile.XSDUpdate();

            NativeMethods.FreeConsole();
        }

        private void BackgroundWorkerAktualizacjaXSD_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Cursor = Cursors.Default;

            aktualizacjaSchematówXSDToolStripMenuItem.Enabled = true;
            buttonSelectGMLFile.Enabled = true;
            buttonValidate.Enabled = true;
            
            toolStripStatusLabelMain.Text = "Gotowy";
        }

        private void ButtonSelectGMLFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pliki GML (*.gml)|*.gml",
            };

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                labelFileName.Text = openFileDialog.FileName;
                
                aktualizacjaSchematówXSDToolStripMenuItem.Enabled = false;
                buttonSelectGMLFile.Enabled = false;
                buttonValidate.Enabled = false;

                backgroundWorkerLoadGML.RunWorkerAsync();
            }
        }

        private void BackgroundWorkerLoadGML_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            toolStripStatusLabelMain.Text = "Wczytywanie pliku...";

            // zdefiuniuj schematy XSD dla otwieranego pliku
            Globals.XSDFile = new XsdFile("ALL");

            // utwórz obiekt pliku GML
            Globals.GMLFile = new GmlFile(labelFileName.Text, Globals.XSDFile);

            Globals.GMLFile.LoadGml();
        }

        private void BackgroundWorkerLoadGML_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Cursor = Cursors.Default;

            FileInfo fi = new FileInfo(labelFileName.Text);

            labelFileInfo.Text = $"Rozmiar pliku: {Math.Round(fi.Length / (1024.0 * 1024.0), 2)} MB";

            aktualizacjaSchematówXSDToolStripMenuItem.Enabled = true;
            buttonSelectGMLFile.Enabled = true;
            buttonValidate.Enabled = true;
            
            toolStripStatusLabelMain.Text = "Gotowy";
        }

        private void ButtonValidate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            aktualizacjaSchematówXSDToolStripMenuItem.Enabled = false;
            buttonSelectGMLFile.Enabled = false;
            buttonValidate.Enabled = false;

            timer.Enabled = true;
            timer.Start();

            backgroundWorkerValidate.RunWorkerAsync();
        }

        private void BackgroundWorkerValidate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            toolStripStatusLabelMain.Text = "Walidacja pliku...";

            List<ErrorInfo> errors = Globals.GMLFile.ValidateSchemaErrors();

            string outputFile = Path.Combine(Path.GetDirectoryName(Globals.GMLFile.GetFileName()) ?? throw new InvalidOperationException(), Path.GetFileNameWithoutExtension(Globals.GMLFile.GetFileName()) + "_errors.xlsx"); 

            FileInfo xlsFile = new FileInfo(outputFile);
            if (xlsFile.Exists) xlsFile.Delete(); 

            ExcelPackage xlsWorkbook = new ExcelPackage(xlsFile);

            xlsWorkbook.Workbook.Properties.Title = "Wynik walidacji pliku GML";
            xlsWorkbook.Workbook.Properties.Author = "Grzegorz Gogolewski";
            xlsWorkbook.Workbook.Properties.Comments = "Wynik walidacji pliku GML";
            xlsWorkbook.Workbook.Properties.Company = "GISNET";

            ExcelWorksheet xlsSheet = xlsWorkbook.Workbook.Worksheets.Add("WALIDACJA");

            xlsSheet.Cells[1, 1].LoadFromCollection(errors, true, TableStyles.Medium2);

            xlsSheet.Cells[1, 1].Value = "Id\nBłędu";
            xlsSheet.Cells[1, 2].Value = "Typ\nbłędu";
            xlsSheet.Cells[1, 3].Value = "Linia";
            xlsSheet.Cells[1, 4].Value = "Klasa obiektu";
            xlsSheet.Cells[1, 5].Value = "gmlId";
            xlsSheet.Cells[1, 6].Value = "LokalnyId";
            xlsSheet.Cells[1, 7].Value = "Atrybut";
            xlsSheet.Cells[1, 8].Value = "ShortInfo";
            xlsSheet.Cells[1, 9].Value = "LongInfo";

            xlsSheet.Cells["A1:I1"].Style.WrapText = true;
            xlsSheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            xlsSheet.Cells["A1:I1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            //xlsSheet.Cells["A1:I" + xlsSheet.Dimension.End.Row].AutoFilter = true;
            xlsSheet.View.FreezePanes(2, 1);
            xlsSheet.Cells.Style.Font.Size = 10;
                    
            xlsSheet.Column(1).Width = 10;
            xlsSheet.Column(2).Width = 10;
            xlsSheet.Column(3).Width = 9;
            xlsSheet.Column(4).Width = 35;
            xlsSheet.Column(5).Width = 35;
            xlsSheet.Column(6).Width = 36;
            xlsSheet.Column(7).Width = 14;
            xlsSheet.Column(8).Width = 50;
            xlsSheet.Column(9).Width = 50;


            xlsWorkbook.Save();
        }

        private void BackgroundWorkerValidate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Cursor = Cursors.Default;

            aktualizacjaSchematówXSDToolStripMenuItem.Enabled = true;
            buttonSelectGMLFile.Enabled = true;
            buttonValidate.Enabled = true;

            toolStripStatusLabelMain.Text = "Walidacja pliku zakończona!";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelMain.Text = Globals.GMLFile.GetWalidacjaStatus();
        }
    }

    internal static class Globals
    {
        public static GmlFile GMLFile;
        public static XsdFile XSDFile;
    }
}
