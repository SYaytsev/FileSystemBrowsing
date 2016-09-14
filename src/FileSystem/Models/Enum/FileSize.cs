using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Models.Enum
{
    public enum FileSize : long
    {
        _10Mb = 1024 * 1024 * 10,
        _50Mb = 1024 * 1024 * 50,
        _100Mb = 1024 * 1024 * 100
    }
}
