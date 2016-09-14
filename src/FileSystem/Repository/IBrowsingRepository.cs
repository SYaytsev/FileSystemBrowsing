using FileSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Repository
{
    public interface IBrowsingRepository
    {
        FileSystemModel GetFileSystemModel(string path);
    }
}
