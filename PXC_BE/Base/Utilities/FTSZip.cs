// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/28/2006
// Time:                      22:55
// Project Name:              Base
// Project Filename:          Base.csproj
// Project Item Name:         FTSZip.cs
// Project Item Filename:     FTSZip.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.IO;

#endregion

namespace FTS.Base.Utilities {
    public class FTSZip {
        public FTSZip() {}

        public static void AddFilesToZip(string zipFilename, string sourcefilepath, string sourcefilename) {
            if (sourcefilepath.Length == 0) {
                throw new ArgumentException("You must specify a source folder from which files will be added to the zip file.", "sourcefilepath");
            }
            if (File.Exists(zipFilename)) {
                File.Delete(zipFilename);
            }
            string folder = Path.GetDirectoryName(zipFilename);
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }
            var zipper = new ZipBuilder();
            zipper.CreatePackage(zipFilename, sourcefilepath);

            //Byte[] data = CompressionHelper.CompressFile(sourcefilepath, sourcefilename);
            //if (File.Exists(zipFilename)) {
            //    File.Delete(zipFilename);
            //}
            //string folder = Path.GetDirectoryName(zipFilename);
            //if (!Directory.Exists(folder)) {
            //    Directory.CreateDirectory(folder);
            //}
            //using (FileStream fs = File.Create(zipFilename)) {
            //    using (BinaryWriter w = new BinaryWriter(fs)) {
            //        w.Write(data);
            //    }
            //}
        }

        public static void AddMoreFilesToZip(string zipFilename, string sourcefilepath, string sourcefilename) {
            if (sourcefilepath.Length == 0) {
                throw new ArgumentException("You must specify a source folder from which files will be added to the zip file.", "sourcefilepath");
            }
            if (File.Exists(zipFilename)) {
                File.Delete(zipFilename);
            }
            string folder = Path.GetDirectoryName(zipFilename);
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }
            var zipper = new ZipBuilder();
            zipper.CreatePackage(zipFilename, sourcefilepath, sourcefilename);
        }

        public static void ExtractZip(string zipFilename, string desfilename) {
            using (FileStream fs = new FileStream(zipFilename, FileMode.Open, FileAccess.Read)) {
                using (BinaryReader r = new BinaryReader(fs)) {
                    byte[] buf = new byte[fs.Length];
                    r.Read(buf, 0, (int) fs.Length);
                    CompressionHelper.DecompressFile(buf, desfilename);
                }
            }
        }
    }
}