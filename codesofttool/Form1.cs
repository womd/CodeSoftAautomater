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
using CefSharp;
using CefSharp.WinForms;
using System.Globalization;

namespace codesofttool
{
   
    public partial class Form1 : Form
    {
        BindingList<LogMessage>LogMessages { get; set; }
        private System.DateTime lastRead;

    
        FileSystemWatcher watcher = new FileSystemWatcher();
        private void watch(string directory)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                Task.Run(() => Log("Directory: " + directory + " does not extis", EnumLogType.Error));
            }
            else
            {
                watcher.Changed -= new FileSystemEventHandler(Watcher_Changed);
                watcher.Path = directory;
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Filter = "*.*";
                watcher.Changed += new FileSystemEventHandler(Watcher_Changed);
                watcher.EnableRaisingEvents = true;
                Task.Run(() => Log("Watching for changes in directory: " + directory, EnumLogType.Info));
            }
        }

        
        private void DoPrintJob(FileSystemEventArgs e)
        {

            var fileInfo = new System.IO.FileInfo(e.FullPath);
            string acceptpattern = Properties.Settings.Default.JobFilePattern;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(acceptpattern);

            Log("SourceDirectory " + e.ChangeType.ToString() + " File: " + e.Name, EnumLogType.Info);
            if (regex.IsMatch(fileInfo.Name))
            {
                try
                {
                    Log("executing print....", EnumLogType.Info);
                    if (executePrintJob(JobFileHelper.loadJob(fileInfo.FullName)))
                    {
                        Log("executing print finished!", EnumLogType.Info);
                        MoveFileToArchive(fileInfo);
                    }
                    else
                    {
                        MoveFileToFailed(fileInfo);
                    }

                }
                catch (Exception ex)
                {
                    Log("Error executing print: " + ex.Message, EnumLogType.Error);
                    MoveFileToFailed(fileInfo);

                    //System.Diagnostics.Process.Start(System.Windows.Forms.Application.ExecutablePath); // to start new instance of application
                    //this.Close(); //to turn off current app

                }

                //  System.IO.File.Delete(fileInfo.FullName);

            }
            else
            {
                Log("Filename not matching pattern - ignored." + e.Name, EnumLogType.Debug);
            }
            return;
        }

        
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if ( e.ChangeType == WatcherChangeTypes.Changed || e.ChangeType == WatcherChangeTypes.Created )
            {
                if (!File.Exists(e.FullPath))
                    return;

                System.DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
                if (lastWriteTime == lastRead)
                {
                    return;
                }

                lastRead = lastWriteTime;

                Task.Run(() => DoPrintJob(e));

            }
        }

        private void MoveFileToArchive(FileInfo file)
        {
            var now = System.DateTime.Now;
            string archivepath = Path.Combine(Properties.Settings.Default.SourcesFolderPath, "archive");
            if (!System.IO.Directory.Exists(archivepath))
            {
                System.IO.Directory.CreateDirectory(archivepath);
            }
            var targetfilename = file.Name + "__" + now.Year + now.Month + now.Day + "_" + now.Hour + now.Minute + now.Second;
            System.IO.File.Move(file.FullName, archivepath + "\\" + targetfilename);
        }

        private void MoveFileToFailed(FileInfo file)
        {
            var now = System.DateTime.Now;
            string archivepath = Path.Combine(Properties.Settings.Default.SourcesFolderPath, "failed");
            if (!System.IO.Directory.Exists(archivepath))
            {
                System.IO.Directory.CreateDirectory(archivepath);
            }
            var targetfilename = file.Name + "__" + now.Year + now.Month + now.Day + "_" + now.Hour + now.Minute + now.Second;
            System.IO.File.Move(file.FullName, archivepath + "\\" + targetfilename);

        }


        public async Task LogLocal(string message, EnumLogType type)
        {
            LogMessages.Add(new LogMessage()
            {
                LogTime = System.DateTime.Now.ToString("h:mm:ss tt"),
                Message = message,
                Type = type
            });
        }

        public async Task Log(string message, EnumLogType type)
        {

            this.Invoke(new Action(() => {
                LogMessages.Add(new LogMessage()
                {
                    LogTime = System.DateTime.Now.ToString("h:mm:ss tt"),
                    Message = message,
                    Type = type
                });
            }));

            
            
          
        }

        private bool executePrintJob(JobFile job)
        {

            string strFile = Path.Combine(Properties.Settings.Default.LabFilesPath,job.LabelDocument);
            if (!System.IO.File.Exists(strFile))
            {
                 Log(".Lab file: " + strFile + " not found: ",EnumLogType.Error);
                return false;
                
            }

            var lbl = new LabelManager2.Application();
                lbl.Documents.Open(strFile, false);

            if(lbl.ActiveDocument == null)
            {
                Log("Could not get active document from Labfile ( CodeSoft Version? )", EnumLogType.Error);
                return false;
            }
           
            var doc = lbl.ActiveDocument;
            //doc.Printer.SwitchTo(targetPrinterName, targetPrinterPort);

            //Get all the name of the printer Strings 
            var allPrinterVars = lbl.PrinterSystem().Printers(enumKindOfPrinters.lppxAllPrinters);
            //To obtain the printer name printer can be fixed directly to the name value //
            string printName = allPrinterVars.Item(2);
            PrintDocument prtdoc = new PrintDocument();

            //use default system printer
            //string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
            string usedprinter = "";
            string strDefaultPrinter = Properties.Settings.Default.PrinterName;

            if(job.UseDefaultPrinter == false)
            {
                doc.Printer.SwitchTo(job.PrinterName);
                usedprinter = job.PrinterName;
            }
            else
            {
                usedprinter = strDefaultPrinter;
            }


            ////Gets the default printer name
            //bool foundPrinter = false;
            //for (int j = 0; j < allPrinterVars.Count; j++)
            //{
            //    string[] arryString = allPrinterVars.Item(j).Split(',');
            //    if (!string.IsNullOrEmpty(job.PrinterName))
            //    {
            //        if (arryString[0] == job.PrinterName)
            //        {
            //            foundPrinter = true;
            //            doc.Printer.SwitchTo(job.PrinterName, arryString[1], true);
            //            usedprinter = job.PrinterName;
            //            break;
            //        }
            //    }
            //    if (arryString[0] == strDefaultPrinter)
            //    {
            //        foundPrinter = true;
            //        doc.Printer.SwitchTo(strDefaultPrinter, arryString[1], true);
            //        usedprinter = strDefaultPrinter;
            //        break;
            //    }

            //}


            var ci = new CultureInfo("en-US");
            Log("start processing document", EnumLogType.Debug);
            foreach (var vitem in job.Variables)
            {
                string beginswith = vitem.Name.Substring(0, vitem.Name.Length - 1);
                //when % is in variable name, overwrite all matching vars
                if (vitem.Name.EndsWith("%"))
                {
                    var varcnt = doc.Variables.Count;
                    for (int i = 1; i <= varcnt; i++)
                    {
                        var vx = doc.Variables.Item(i);
                        if(vx != null)
                        {
                            if(vx.Name.StartsWith(beginswith, true, ci))
                            {
                                vx.Value = vitem.Value;
                            }
                        }
                    }
                }
                else
                {
                    //try to find exact value           
                    var varInDoc = doc.Variables.Item(vitem.Name);
                    if (varInDoc != null)
                    {
                        if (string.IsNullOrEmpty(vitem.Value) || vitem.Printable == false)
                            doc.Variables.Remove(vitem.Name);
                        else
                            doc.Variables.Item(vitem.Name).Value = vitem.Value;
                    }
                }


                //look for images
                if (vitem.Name.EndsWith("%"))
                {
                    var varcnt = doc.DocObjects.Images.Count;
                    for (int i = 1; i <= varcnt; i++)
                    {
                        var aimgInDoc = doc.DocObjects.Images.Item(i);
                        if (aimgInDoc != null)
                        {
                            if (aimgInDoc.Name.StartsWith(beginswith, true, ci))
                            {
                                if (vitem.Printable)
                                    aimgInDoc.Printable = 1;
                                else
                                    aimgInDoc.Printable = 0;
                            }
                        }
                    }
                }
                else
                {
                    var imgInDoc = doc.DocObjects.Images.Item(vitem.Name);
                    if (imgInDoc != null)
                    {

                        if (vitem.Printable)
                            imgInDoc.Printable = 1;
                        else
                            imgInDoc.Printable = 0;

                    }
                }

                //look for texts
                if (vitem.Name.EndsWith("%"))
                {
                    var varcnt = doc.DocObjects.Texts.Count;
                    for (int i = 1; i <= varcnt; i++)
                    {
                        var atextInDoc = doc.DocObjects.Texts.Item(i);
                        if (atextInDoc != null)
                        {
                            if (atextInDoc.Name.StartsWith(beginswith, true, ci))
                            {
                                if (vitem.Printable)
                                    atextInDoc.Printable = 1;
                                else
                                    atextInDoc.Printable = 0;
                            }
                        }
                    }
                }
                else
                {
                    var textInDoc = doc.DocObjects.Texts.Item(vitem.Name);
                    if (textInDoc != null)
                    {
                        if (vitem.Printable)
                            textInDoc.Printable = 1;
                        else
                            textInDoc.Printable = 0;
                    }
                }

                //look for barcodes
                if (vitem.Name.EndsWith("%"))
                {
                    var varcnt = doc.DocObjects.Barcodes.Count;
                    for (int i = 1; i <= varcnt; i++)
                    {
                        var abarcodeInDoc = doc.DocObjects.Barcodes.Item(i);
                        if (abarcodeInDoc != null)
                        {
                            if (abarcodeInDoc.Name.StartsWith(beginswith, true, ci))
                            {
                                if (vitem.Printable)
                                    abarcodeInDoc.Printable = 1;
                                else
                                    abarcodeInDoc.Printable = 0;
                            }
                        }
                    }
                }
                else
                {
                    var barcodeInDoc = doc.DocObjects.Barcodes.Item(vitem.Name);
                    if (barcodeInDoc != null)
                    {
                        if (vitem.Printable)
                            barcodeInDoc.Printable = 1;
                        else
                            barcodeInDoc.Printable = 0;
                    }
                }

                Log("element not found in document " + vitem.Name , EnumLogType.Debug);
            }

            
            Log("end processing document", EnumLogType.Debug);

            //doc.PrintLabel(job.NrOfCopies);
            Log("printing " + job.NrOfCopies + " items on " + usedprinter, EnumLogType.Info);

            doc.HorzPrintOffset = job.MoveX;
            doc.VertPrintOffset = job.MoveY;
            doc.Rotate(job.Rotate);

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
            return true;
        }

    

        public Form1()
        {
            InitializeComponent();
           // if(Properties.Settings.Default.MTestEnabled)
           //     InitializeChromium();


            this.LogMessages = new BindingList<LogMessage>();
            this.dataGridViewLogs.DataSource = this.LogMessages;
            Start();
        }

        public void Start()
        {
            try
            {
                textBoxSourcesFolder.Text = Properties.Settings.Default.SourcesFolderPath;
                textBoxJobFilePattern.Text = Properties.Settings.Default.JobFilePattern;
                textBoxLabFilesPath.Text = Properties.Settings.Default.LabFilesPath;
                textBoxPrinter.Text = Properties.Settings.Default.PrinterName;
                watch(Properties.Settings.Default.SourcesFolderPath);

                lastRead = System.DateTime.MinValue;
              
            }
            catch(Exception ex)
            {
                Log(ex.Message, EnumLogType.Error);
            }
            
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
           //     watch(Properties.Settings.Default.SourcesFolderPath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          

        }

  
        private void btnSelectLabFiles(object sender, EventArgs e)
        {
            var res = folderBrowserDialog.ShowDialog();
            if (res == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                textBoxLabFilesPath.Text = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.LabFilesPath = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonSelectPrinter_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog1 = new PrintDialog();
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(printDialog1.PrinterSettings.PrinterName))
            {
                textBoxPrinter.Text = printDialog1.PrinterSettings.PrinterName;
                Properties.Settings.Default.PrinterName = printDialog1.PrinterSettings.PrinterName;
                Properties.Settings.Default.Save();
            }

        }

        private void textBoxSourcesFolder_TextChanged(object sender, EventArgs e)
        {
            string strDir = ((TextBox) sender).Text;
            if (System.IO.Directory.Exists(strDir))
            {
                Properties.Settings.Default.SourcesFolderPath = strDir;
                Properties.Settings.Default.Save();
                watch(strDir);
            }
            else
            {
                Task.Run(() => Log(strDir + " is not a directory.", EnumLogType.Error));
            }
        }

        private void textBoxJobFilePattern_TextChanged(object sender, EventArgs e)
        {
            string strPattern = (sender as TextBox).Text;
            Properties.Settings.Default.JobFilePattern = strPattern;
            Properties.Settings.Default.Save();
        }

    }
}
