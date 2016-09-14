using FileSystem.Models;
using FileSystem.Models.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystem.Repository
{
    public class BrowsingRepository : IBrowsingRepository
    {
        public FileSystemModel GetFileSystemModel(string path)
        {
            FileSystemModel fileSystemModel;
            if (path == "root")
            {
                fileSystemModel = GetFileSystemModelDrivesBases(path);
            }
            else
            {
                fileSystemModel = GetFileSystemModelDirsBases(path);
            }
            return fileSystemModel;
        }

        private FileSystemModel GetFileSystemModelDrivesBases(string path)
        {
            FileSystemModel fileSystemModel = new FileSystemModel()
            {
                CurrentDir = new DirUnit()
                {
                    Name = path,
                    FullName = path
                },
                ParentDir = new DirUnit()
                {
                    Name = path,
                    FullName = path
                },
                Dirs = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed).Select(d => new DirUnit()
                {
                    Name = d.Name,
                    FullName = d.Name
                }).ToList()
            };

            fileSystemModel.FilesCount = CountFilesForSeveralDirs(fileSystemModel.Dirs, new FilesCount());

            return fileSystemModel;
        }

        private FileSystemModel GetFileSystemModelDirsBases(string path)
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
                }).ToList()
            };

            fileSystemModel.FilesCount = CountFilesForDir(path, new FilesCount());

            return fileSystemModel;
        }

        private DirUnit GetParrentDir(DirectoryInfo dirInfo)
        {
            DirUnit dirUnit = new DirUnit();
            if (dirInfo.Parent == null)
            {
                dirUnit.Name = "root";
                dirUnit.FullName = "root";
            }
            else
            {
                dirUnit.Name = dirInfo.Parent.Name;
                dirUnit.FullName = dirInfo.Parent.FullName;
            }
            return dirUnit;
        }

        private FilesCount CountFilesForDir(string path, FilesCount filesCount)
        {
            try
            {
                foreach (string dir in Directory.EnumerateDirectories(path))
                {
                    CountFilesForDir(dir, filesCount);
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }

            FileInfo[] files = new DirectoryInfo(path).GetFiles();

            filesCount.MinSizeFilesCount += files.Count(file => file.Length <= (long)FileSize._10Mb);
            filesCount.MiddleSizeFilesCount += files.Count(file => file.Length > (long)FileSize._10Mb && file.Length <= (long)FileSize._50Mb);
            filesCount.MaxSizeFilesCount += files.Count(file => file.Length > (long)FileSize._100Mb);

            return filesCount;
        }
        private FilesCount CountFilesForSeveralDirs(IList<DirUnit> dirs, FilesCount filesCount)
        {
            foreach (DirUnit dir in dirs)
            {
                CountFilesForDir(dir.FullName, filesCount);
            }
            return filesCount;
        }
    }
}
