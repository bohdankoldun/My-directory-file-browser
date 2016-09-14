using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace file_browser.Models
{
    /// <summary>
    /// model for folder of file system
    /// </summary>
    public class Folder
    {
        public string Path { get; set; }
        public string ParentDirectory { get; set; }
        public string[] Directories { get; set;}
        public string[] Files { get; set; }
        public int countLess10mb { get; set; }
        public int countBetween10mb_50mb { get; set; }
        public int More100mb { get; set; }
    }
}