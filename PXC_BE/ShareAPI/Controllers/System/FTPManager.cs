using System;
using System.IO;
using System.Net;
using System.Web;
using FTS.Base.Systems;

namespace FTS.ShareAPI.Controllers
{
    public class FTPManager
    {
        #region var
        private static int bufferSize = 2048;
        public string Uri = string.Empty,
            UserId = string.Empty,
            Password = string.Empty;
        public string requestUriString = string.Empty, FileName = string.Empty, FileLink = string.Empty;
        private string AppCode = string.Empty;
        private FTSMain mFTSMain;
        public FTSMain FTSMain
        {
            get { return this.mFTSMain; }
            set { this.mFTSMain = value; }
        }
        #endregion

        #region contructor
        public FTPManager()
        {

        }

        public FTPManager(string pUri, string pUserId, string pPassword)
        {
            this.Uri = pUri;
            this.UserId = pUserId;
            this.Password = pPassword;
        }

        public FTPManager(FTSMain ftsMain)
        {
            this.mFTSMain = ftsMain;
            this.Uri = this.FTSMain.SystemVars.GetSystemVars("FTP_URI").ToString();
            this.UserId = this.FTSMain.SystemVars.GetSystemVars("FTP_USER_ID").ToString();
            this.Password = this.FTSMain.SystemVars.GetSystemVars("FTP_PASSWORD").ToString();
            this.AppCode = this.FTSMain.SystemVars.GetSystemVars("APP_CODE").ToString();
            this.requestUriString = string.Empty;
        }
        #endregion

        public void MakeDirectory(string tablename, Guid prkey)
        {
            string Directory = this.Uri + "/" + this.AppCode;
            MakeDirectory(Directory);
            Directory += "/" + tablename.ToUpper();
            MakeDirectory(Directory);
            Directory += "/" + prkey.ToString().ToUpper();
            MakeDirectory(Directory);
            this.requestUriString = Directory;
        }

        public void MakeDirectory(string tranid, DateTime trandate, string tranno)
        {
            string Directory = this.Uri + "/" + this.AppCode;
            MakeDirectory(Directory);
            Directory += "/" + this.FTSMain.UserInfo.OrganizationID.ToUpper();
            MakeDirectory(Directory);
            Directory += "/" + tranid.ToUpper();
            MakeDirectory(Directory);
            Directory += "/" + String.Format("{0:yyyy_MM_dd}", trandate) + "_" + tranno.ToUpper();
            MakeDirectory(Directory);
            this.requestUriString = Directory;
        }

        public bool CheckDirectory(string requesturi)
        {
            bool directoryExists;
            string directory = requesturi + "/";
            var request = (FtpWebRequest)WebRequest.Create(directory);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(this.UserId, this.Password);
            try
            {
                using (request.GetResponse())
                {
                    directoryExists = true;
                    request = null;
                }
            }
            catch (WebException)
            {
                directoryExists = false;
                request = null;
            }
            return directoryExists;
        }

        public void MakeDirectory(string requesturi)
        {
            if (CheckDirectory(requesturi) == false)
            {
                WebRequest requestFolder = WebRequest.Create(new Uri(requesturi));
                requestFolder.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestFolder.Credentials = new NetworkCredential(this.UserId, this.Password);
                FtpWebResponse resp = (FtpWebResponse)requestFolder.GetResponse();
                resp.Close();
                requestFolder = null;
            }
        }

        public static byte[] ConverToBytes(HttpPostedFile file)
        {
            var length = file.InputStream.Length; //Length: 103050706
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }
            return fileData;
        }

        public void UploadFile(string requesturi, HttpPostedFile file)
        {
            FileName = string.Empty;
            FileLink = string.Empty;
            FtpWebRequest req = null;
            FtpWebResponse res = null;
            try
            {
                byte[] data = ConverToBytes(file);
                //MakeDirectory(requestUri);
                string PureFileName = file.FileName;
                String uploadUrl = String.Format("{0}/{1}", requesturi, PureFileName);
                req = (FtpWebRequest)FtpWebRequest.Create(uploadUrl);
                req.Proxy = null;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(this.UserId, this.Password);
                req.UseBinary = true;
                req.UsePassive = true;

                req.ContentLength = data.Length;
                Stream stream = req.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                res = (FtpWebResponse)req.GetResponse();
                res.Close();
                req = null;
                FileLink = uploadUrl.Replace(this.Uri, string.Empty);
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                res.Close();
                req = null;
                throw new Exception(status);
            }
        }

        public void Download(string filepath, string localfile)
        {
            try
            {
                /* Create an FTP Request */
                FtpWebRequest WebRequest = (FtpWebRequest)FtpWebRequest.Create(this.Uri + "/" + filepath);
                /* Log in to the FTP Server with the User Name and Password Provided */
                WebRequest.Credentials = new NetworkCredential(this.UserId, this.Password);
                /* When in doubt, use these options */
                WebRequest.UseBinary = true;
                WebRequest.UsePassive = true;
                WebRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                WebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                /* Establish Return Communication with the FTP Server */
                FtpWebResponse ftpResponse = (FtpWebResponse)WebRequest.GetResponse();
                /* Get the FTP Server's Response Stream */
                Stream ftpStream = ftpResponse.GetResponseStream();
                /* Open a File Stream to Write the Downloaded File */
                FileStream localFileStream = new FileStream(localfile, FileMode.Create);
                /* Buffer for the Downloaded Data */
                byte[] byteBuffer = new byte[bufferSize];
                int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
                try
                {
                    while (bytesRead > 0)
                    {
                        localFileStream.Write(byteBuffer, 0, bytesRead);
                        bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                /* Resource Cleanup */
                localFileStream.Close();
                ftpStream.Close();
                ftpResponse.Close();
                WebRequest = null;
            }
            catch
            {
                throw;
            }
        }

        public void Delete(string filepath)
        {
            try
            {
                /* Create an FTP Request */
                FtpWebRequest WebRequest = (FtpWebRequest)System.Net.WebRequest.Create(this.Uri + "/" + filepath);
                /* Log in to the FTP Server with the User Name and Password Provided */
                WebRequest.Credentials = new NetworkCredential(this.UserId, this.Password);
                /* When in doubt, use these options */
                WebRequest.UseBinary = true;
                WebRequest.UsePassive = true;
                WebRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                WebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                /* Establish Return Communication with the FTP Server */
                FtpWebResponse ftpResponse = (FtpWebResponse)WebRequest.GetResponse();
                /* Resource Cleanup */
                ftpResponse.Close();
                WebRequest = null;
            }
            catch
            {
                throw;
            }
        }
    }
}
