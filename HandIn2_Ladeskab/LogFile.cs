using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn2_Ladeskab
{
    public class LogFile : ILogFile 
    {
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        public void LogDoorLocked(int Id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", Id);
            }
        }
        public void LogDoorUnlocked(int Id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", Id);
            }
        }
    }
}
