using file_browser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace file_browser.Controllers
{
    public class FileBrowserController : ApiController
    {

        /// <summary>
        /// Get all drives of hard disk. GET: api/FileBrowser
        /// </summary>
        /// <returns>json objects of all hard disk drives</returns>
        public IHttpActionResult Get()
        {
            DriveInfo[] drivesInfo = DriveInfo.GetDrives();
            List<Drive> drives = new List<Drive>();

            for (int i = 0; i < drivesInfo.Length; i++)
            {
                //choose only hard disk drive
                if (drivesInfo[i].DriveType.ToString() == "Fixed")
                    drives.Add(new Drive
                        (
                            drivesInfo[i].Name,
                            drivesInfo[i].RootDirectory.ToString(),
                            drivesInfo[i].AvailableFreeSpace,
                            drivesInfo[i].TotalSize
                        ));
            }

            return Json(drives);
        }


        /// <summary>
        ///Get all directories and files of any path. GET: api/FileBrowser?path=D:\
        /// </summary>
        /// <param name="path">path of folder</param>
        /// <returns>json object of folder</returns>
        public IHttpActionResult Get(string path)
        {
            try
            {
                Folder folder = new Folder();

                if (Directory.Exists(path))
                {
                    folder.Path = path;

                    // (char)92 = '\'
                    if (folder.Path[folder.Path.Length - 1] != (char)92)
                        folder.Path += (char)92;

                    if (Directory.GetParent(path) != null)
                        folder.ParentDirectory = Directory.GetParent(path).FullName;


                    folder.Directories = Directory.GetDirectories(path).Select(directory => Path.GetFileName(directory)).ToArray();
                    folder.Files = Directory.GetFiles(path).Select(file => Path.GetFileName(file)).ToArray();

                    int countLess10mb = 0, countBetween10mb_50mb = 0, More100mb = 0;
                    DistributeFilesOfFolder(path, ref countLess10mb, ref countBetween10mb_50mb, ref More100mb);

                    folder.countLess10mb = countLess10mb;
                    folder.countBetween10mb_50mb = countBetween10mb_50mb;
                    folder.More100mb = More100mb;

                }

                return Json(folder);
            }
            catch (UnauthorizedAccessException e)
            {
                System.Diagnostics.Debug.WriteLine("Unable to calculate the count folder files: {0}", e.Message);
                return InternalServerError(e);
            }     
        }

    /// <summary>
    ///get count of files in  directory and subdirectories that are: <= 10mb;  > 10mb AND <= 50mb;  >= 100mb.
    /// </summary>
    /// <param name="path">path of directory</param>
    /// <param name="countLess10mb"> conter of file that are <= 10mb</param>
    /// <param name="countBetween10mb_50mb">conter of file that are > 10mb AND <= 50mb</param>
    /// <param name="More100mb">conter of file that are >= 100mb</param>
    private static void DistributeFilesOfFolder(string path, ref int countLess10mb, ref int countBetween10mb_50mb, ref int More100mb)
    {
        try
        {
            //Checks if the path is valid or not
            if (!Directory.Exists(path))
                return;
            else
            {
                try
                {
                    foreach (string file in Directory.GetFiles(path))
                    {
                        if (File.Exists(file))
                        {
                            FileInfo finfo = new FileInfo(file);
                            long fileSize = finfo.Length;
                            // <= 10mb
                            if (fileSize <= 10485760)
                                countLess10mb++;
                            // > 10mb AND <= 50mb
                            else if (fileSize > 10485760 && finfo.Length <= 52428800)
                                countBetween10mb_50mb++;
                            // >= 100mb
                            else if (fileSize >= 104857600)
                                More100mb++;
                        }
                    }

                    foreach (string dir in Directory.GetDirectories(path))
                        DistributeFilesOfFolder(dir, ref countLess10mb, ref countBetween10mb_50mb, ref More100mb);
                }
                catch (NotSupportedException e)
                {
                        System.Diagnostics.Debug.WriteLine("Unable to calculate the count  folder files: {0}", e.Message);
                }
            }
        }
        catch (UnauthorizedAccessException e)
        {
                System.Diagnostics.Debug.WriteLine("Unable to calculate the count folder files: {0}", e.Message);
        }

    }

}
}
