using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileSystem.Models;
using System.IO;
using FileSystem.Models.Enum;
using System.Security;
using System.Security.Principal;

namespace FileSystem.Repository
{
    public class BrowsingRepository : IBrowsingRepository
    {        
        public FileSystemModel GetFileSystemModel(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@path);

            FileSystemModel fileSystemModel = new FileSystemModel()
            {
                CurrentDir = new DirUnit()
                {
                    Name = dirInfo.Name,
                    FullName = dirInfo.FullName
                },
                ParentDir = GetParrentDir(dirInfo),

                Dirs = dirInfo.GetDirectories().Select(d => new DirUnit()
                {
                    Name = d.Name,
                    FullName = d.FullName
                }).ToList(),
                Files = dirInfo.GetFiles().Select(f => new FileUnit()
                {
                    Name = f.Name,
                    FullName = f.FullName
                }).ToList(),

                FilesCount = CountFiles(dirInfo, new FilesCount())   
            };
            return fileSystemModel;
        }

        private DirUnit GetParrentDir(DirectoryInfo dirInfo)
        {
            DirUnit dirUnit = new DirUnit();
            if (dirInfo.Parent == null)
            {
                return dirUnit;
            }
            else
            {
                dirUnit.Name = dirInfo.Parent.Name;
                dirUnit.FullName = dirInfo.Parent.FullName;
                return dirUnit;
            }
        }



        private FilesCount CountFiles(DirectoryInfo dirInfo, FilesCount filesCount)
        {
            // .Where(d => d.Attributes != FileAttributes.System)
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                CountFiles(dir, filesCount);
            }
            FileInfo[] files = dirInfo.GetFiles();
            filesCount.MinSizeFilesCount += files.Count(file => file.Length <= (long)FileSize._10Mb);
            filesCount.MiddleSizeFilesCount += files.Count(file => file.Length > (long)FileSize._10Mb && file.Length <= (long)FileSize._50Mb);
            filesCount.MaxSizeFilesCount += files.Count(file => file.Length > (long)FileSize._100Mb);

            return filesCount;
        }
    }
}
