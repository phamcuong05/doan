using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using iTextSharp.text.pdf;

namespace FTS.ShareAPI.Controllers
{
    public class FileManager
    {
        public static string TodayForder()
        {
            return String.Format("{0:yyyy_MM_dd}", DateTime.Now);
        }

        public static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetForderTemp()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "Ex//";
        }

        public static string CreateNewForder(){
            string FolderName = Guid.NewGuid().ToString(); ;
            string path = GetForderTemp() + TodayForder() + "//"+ FolderName;
            bool exists = System.IO.Directory.Exists(path);
            if (!exists){
                CreateDirectory(path);
            }
            return path + "//";
        }

        public static string CreateForderWork()
        {
            string FolderName = TodayForder();
            //Check and Create Forder By Date
            string path = GetForderTemp() + FolderName;
            bool exists = System.IO.Directory.Exists(path);
            if (!exists){
                CreateDirectory(path);
            }
            //Check and Create Forder Work
            path = GetForderTemp() + FolderName + "//" + Guid.NewGuid().ToString();
            exists = System.IO.Directory.Exists(path);
            if (!exists){
                CreateDirectory(path);
            }
            //Return Path
            return path + "//";
        }

        public static string GetPathFileServer(string pathfile)
        {
            string path = string.Empty;
            path = pathfile.Replace(AppDomain.CurrentDomain.BaseDirectory, "");//.Replace("//", "\\");
            return path;
        }

        public static void DeleteDirectory(string path)
        {
            System.IO.Directory.Delete(path, true);
        }

        public static void CreateDirectory(string path)
        {
            System.IO.Directory.CreateDirectory(path);
        }

        public static void DeleteAllSubDirectory(string path)
        {
            try{
                string[] subdirectoryEntries = Directory.GetDirectories(path);
                foreach (string subdirectory in subdirectoryEntries){
                    string FolderName = new DirectoryInfo(subdirectory).Name;
                    if (FolderName.ToUpper() == TodayForder().ToUpper()){
                        continue;
                    }
                    DeleteDirectory(subdirectory);
                }
            }
            catch{

            }
        }

        public static void SetPassword(string pathinput, string pathoutput, string passwordpdffile)
        {
            using (Stream input = new FileStream(pathinput, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream output = new FileStream(pathoutput, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfReader reader = new PdfReader(input);
                PdfEncryptor.Encrypt(reader, output, true, passwordpdffile, passwordpdffile, PdfWriter.ALLOW_PRINTING);
            }
        }

        public static void ZipForder(string path, string filezip)
        {
            ZipOutputStream zipOut = new ZipOutputStream(File.Create(filezip));
            foreach (string fName in Directory.GetFiles(path))
            {
                FileInfo fi = new FileInfo(fName);
                ZipEntry entry = new ZipEntry(fi.Name);
                FileStream sReader = File.OpenRead(fName);
                byte[] buff = new byte[Convert.ToInt32(sReader.Length)];
                sReader.Read(buff, 0, (int)sReader.Length);
                entry.DateTime = fi.LastWriteTime;
                entry.Size = sReader.Length;
                sReader.Close();
                zipOut.PutNextEntry(entry);
                zipOut.Write(buff, 0, buff.Length);
            }
            zipOut.Finish();
            zipOut.Close();
        }
    }
}