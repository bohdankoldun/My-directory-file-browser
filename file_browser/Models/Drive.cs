using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace file_browser.Models
{
    /// <summary>
    /// модель диска файловой системы
    /// </summary>
    public class Drive
    {
        public string Name { get; set; }
        public long AvailableFreeSpace { get; set; }
        public long TotalSize { get; set; }

        public Drive() { }

        public Drive(string Name, long AvailableFreeSpace, long TotalSize)
        {
            this.Name = Name;
            this.AvailableFreeSpace = AvailableFreeSpace;
            this.TotalSize = TotalSize;
        }
        
    }
}