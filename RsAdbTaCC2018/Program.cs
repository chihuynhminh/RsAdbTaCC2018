using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Numerics;
using System.IO;

namespace RsAdbTaCC2018
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //string path_of_path_file = Application.StartupPath + "\\path.txt";
            string path_of_path_file = Application.StartupPath + "\\" + args[0] + "_path.txt";
            string path = "";

            if (File.Exists(path_of_path_file))
                path = File.ReadAllText(path_of_path_file);
            else
            {
                MessageBox.Show(Path.GetFileName(path_of_path_file) + " not found");
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            try
            {
                doc.Load(path);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("application.xml file structure error [1]");
                return;
            }

            XmlNodeList xnList = doc.SelectNodes("/Configuration/Other/Data[@key='TrialSerialNumber']");
            if (xnList.Count != 1)
            {
                MessageBox.Show("application.xml file structure error [2]");
                return;
            }

            XmlNode xmlNode = xnList[0];

            //MessageBox.Show(xmlNode.InnerText);

            xmlNode.InnerText = (BigInteger.Parse(xmlNode.InnerText) + 1).ToString();

            try
            {
                doc.Save(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string path_of_date_file = Application.StartupPath + "\\" + args[0] + "_date.txt";
            DateTime d = DateTime.Now;
            using (StreamWriter sw = File.CreateText(path_of_date_file))
            {
                sw.WriteLine(d.Year);
                sw.WriteLine(d.Month);
                sw.WriteLine(d.Day);
                sw.WriteLine(d.Hour);
                sw.WriteLine(d.Minute);
                sw.WriteLine(d.Second);
            }
        }
    }
}