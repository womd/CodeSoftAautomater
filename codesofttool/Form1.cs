using LabelManager2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TkxOleCtrlExLib;

namespace codesofttool
{
    public partial class Form1 : Form
    {
        
        FileSystemWatcher watcher = new FileSystemWatcher();
        private void watch(string directory)
        {
            watcher.Path = directory;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(Watcher_Changed);
            watcher.EnableRaisingEvents = true;
        }

        private void executePrintJob(JobFile job)
        {

            string strFile = job.LabelDocument;
            if (!System.IO.File.Exists(strFile))
            {
                throw new FileNotFoundException();
            }

            var lbl = new LabelManager2.Application();
            if(job.LabelDocument.Contains("\\"))
            {
                lbl.Documents.Open(strFile, false);
            }
            else
            {
                strFile = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath.ToString(), strFile);
                lbl.Documents.Open(strFile, false);
            }
            

            var doc = lbl.ActiveDocument;
            //doc.Printer.SwitchTo(targetPrinterName, targetPrinterPort);

            //Get all the name of the printer Strings 
            var vars = lbl.PrinterSystem().Printers(enumKindOfPrinters.lppxAllPrinters);
            //To obtain the printer name printer can be fixed directly to the name value //
            string printName = vars.Item(2);
            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
            //Gets the default printer name
            for (int j = 0; j < vars.Count; j++)
            {
                string[] arryString = vars.Item(j).Split(',');
                if (arryString[0] == strDefaultPrinter)
                {

                    doc.Printer.SwitchTo( strDefaultPrinter, arryString[1], true);
                   
                    break;
                    
                }
            }

            foreach(var vitem in job.Variables)
            {
                doc.Variables.Item(vitem.Name).Value = vitem.Value;
            }

            //var varx2 = doc.Variables.FormVariables.Count;

            //doc.PrintLabel(job.NrOfCopies);
            doc.PrintDocument(job.NrOfCopies);

            //  PrintDocument pd = new PrintDocument();
            //  pd.Print();

            lbl.Documents.CloseAll();

            lbl.Quit();
            //DataTable dt = codeInfo_DAL.GetData(this.cbb.SelectedValue.ToString());
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    { //codesoftLabel variables in the template 
            //        doc.Variables.FormVariables.Item("var0").Value = dr["CodeID"].ToString();
            //        doc.Variables.FormVariables.Item("var1").Value =dr["Name"].ToString();
            //        // doc.PrintDocument(3);
            //        doc.PrintLabel(1, 1, 1, 1, 1, "");
            //    }
            //    //Continuous batch print labels. FormFeed()The parameters such as the output variable must be after the execution, output to the printer.
            //    doc.FormFeed();
            //    lbl.Quit();
            //}
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if(e.ChangeType == WatcherChangeTypes.Changed || e.ChangeType == WatcherChangeTypes.Created || e.ChangeType ==  WatcherChangeTypes.Renamed)
            {
                var fileInfo = new System.IO.FileInfo(e.FullPath);
                string acceptpattern = Properties.Settings.Default.JobFilePattern;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(acceptpattern);
                if(regex.IsMatch(e.Name))
                {
                    try
                    {
                        executePrintJob(JobFileHelper.loadJob(e.FullPath));
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
               
                    System.IO.File.Delete(e.FullPath);

                }
                
            }
        }

        public Form1()
        {
            InitializeComponent();
            Start();
        }

        public void Start()
        {
            textBoxSourcesFolder.Text = Properties.Settings.Default.SourcesFolderPath;
            textBoxJobFilePattern.Text = Properties.Settings.Default.JobFilePattern;
            watch(Properties.Settings.Default.SourcesFolderPath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JobFileHelper.save_testfile();

        }

        private void btnSetFolder_Click(object sender, EventArgs e)
        {
            var res = folderBrowserDialog.ShowDialog();
            if(res == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                textBoxSourcesFolder.Text = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.SourcesFolderPath = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

     
    }
}
