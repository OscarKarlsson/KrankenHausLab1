using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PrintManager.Print
{
    class PrintToLog
    {

        private string fileName = "KrankenHaus.txt";

        public void WriteToFile(string text)
        {
            lock (this)
            {
                File.AppendAllText(fileName, text);
            }
            
        }
        
    }
}
