using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerLogs
{
    /// <summary>
    /// Captura los errores y los graba en un archivo txt.
    /// </summary>
    public static class FileLogger
    {
        private static string filePath = @"C:\Servinte\StoreShopingERROR.txt";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            DateTime dtError = DateTime.Now;
            string textfile = string.Empty;

            //Sino existe el archivo no hay ke leerlo.
            if (File.Exists(filePath))
                textfile =  ReadTextFromTextFile();
            
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(textfile);
                streamWriter.WriteLine(string.Empty);
                streamWriter.WriteLine(dtError.ToString("dd/MM/yyyy HH:mm:ss"));
                streamWriter.WriteLine(message);
                streamWriter.WriteLine("_____________________________________________________________________");
                streamWriter.Close();
            }
        }

        /// <summary>
        /// Lee el contenido del archivo errores para ke no se pierda su contenido
        /// </summary>
        /// <returns></returns>
        public static string ReadTextFromTextFile()
        {
            string text = string.Empty;
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    StreamReader srd = new StreamReader(fs);
                    string line = srd.ReadLine();
                    text = line;
                    while (line != null)
                    {
                        line = srd.ReadLine();
                        text += line + Environment.NewLine;
                    }
                    srd.Close();
                    fs.Close();
                }

            }
            catch (Exception er)
            {
                throw er;
            }
            return text;

        }
    }
}
