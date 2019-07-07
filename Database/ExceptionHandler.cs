using Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApteanClinic.Database
{
    public static class ExceptionHandler
    {
        public static void PrintException(Exception e, StackTrace st)
        {
            string stackIndent = "\n\n Exception : " + e.Message + "\n";
            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                if (sf.GetFileName() != null)
                {
                    stackIndent += "\n------------------------------\n";
                    stackIndent += "\n Method: " + sf.GetMethod();
                    stackIndent += "\n File: " + sf.GetFileName();
                    stackIndent += "\n Line Number: " + sf.GetFileLineNumber();
                }
            }
            stackIndent += "\n**********************************************************\n\n";

            Logging.loggException(stackIndent);

            //string path = @"C:\Users\Public\Exception_"+ DateTime.Now.ToString("yyyy_MM_dd") +".txt";
            //if (!File.Exists(path))
            //{
            //    using (StreamWriter sw = File.CreateText(path))
            //    {
            //        sw.Write(stackIndent);
            //    }
            //}
            //else if (File.Exists(path))
            //{
            //    using (StreamWriter sw = File.AppendText(path))
            //    {
            //        sw.Write(stackIndent);
            //    }
            //}
        }
    }
}
