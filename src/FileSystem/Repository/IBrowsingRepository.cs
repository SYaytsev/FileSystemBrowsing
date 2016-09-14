using FileSystem.Models;

namespace FileSystem.Repository
{
    public interface IBrowsingRepository
    {
        FileSystemModel GetFileSystemModel(string path);
    }
}
