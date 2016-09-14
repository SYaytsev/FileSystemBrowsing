using FileSystem.Models.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Models
{
    public class FileSystemModel
    {
        public DirUnit CurrentDir { get; set; }
        public DirUnit ParentDir { get; set; }

        public IList<DirUnit> Dirs { get; set; }
        public IList<FileUnit> Files { get; set; }

        public FilesCount FilesCount { get; set; }
    }

    public abstract class Unit
    {
        public string Name { get; set; }
        public string FullName { get; set; }
    }
    public class DirUnit : Unit { }
    public class FileUnit : Unit { }

    public class FilesCount
    {
        public int MinSizeFilesCount { get; set; }
        public int MiddleSizeFilesCount { get; set; }
        public int MaxSizeFilesCount { get; set; }
    }
}
