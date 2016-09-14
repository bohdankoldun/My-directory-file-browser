using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace file_browser.Models
{
    /// <summary>
    /// model for hard disk drive
    /// </summary>
    public class Drive
    {
        public string Name { get; set; }
        public string RootDirectory { get; set; }
        public long AvailableFreeSpace { get; set; }
        public long TotalSize { get; set; }

        public Drive() { }

        public Drive(string Name, string RootDirectory , long AvailableFreeSpace, long TotalSize)
        {
            this.Name = Name;
            this.RootDirectory = RootDirectory;
            this.AvailableFreeSpace = AvailableFreeSpace;
            this.TotalSize = TotalSize;
        }
        
    }
}