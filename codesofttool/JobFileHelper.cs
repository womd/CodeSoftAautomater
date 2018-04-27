using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace codesofttool
{
    public static class JobFileHelper
    {
        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        public static void save_testfile()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(JobFile));
            var subReq = new JobFile();
            subReq.LabelDocument = "Document1.lab";
            subReq.PrinterName = "TestPrinter";
            subReq.UseDefaultPrinter = true;
            subReq.NrOfCopies = 1;
            subReq.Variables = new List<Variable>();
            subReq.Variables.Add(new Variable() { Name = "var0", Value = "var 0 valule" });
            subReq.Variables.Add(new Variable() { Name = "var1", Value = "var 1 value" });
            var xml = "";

            using (var sww = new Utf8StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, subReq);
                    xml = sww.ToString(); 
                    System.IO.File.WriteAllText(Path.Combine(Properties.Settings.Default.SourcesFolderPath, "samplefile.xml"), xml);
                }
            }
        }

        public static JobFile loadJob(string FileName)
        {
            using (var stream = System.IO.File.OpenRead(FileName))
            {
                var serializer = new XmlSerializer(typeof(JobFile));
                return serializer.Deserialize(stream) as JobFile;
            }
        }

    }

    public class JobFile
    {
        /// <summary>
        /// The CodeSoft LabelDocument to Print
        /// </summary>
        public string LabelDocument { get; set; }
        public string PrinterName { get; set; }
        public bool UseDefaultPrinter { get; set; }
        public int  NrOfCopies { get; set; }
        /// <summary>
        /// Variable will be overwritten in printed document...
        /// </summary>
        public List<Variable> Variables { get; set; }
    }


    public class Variable
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
