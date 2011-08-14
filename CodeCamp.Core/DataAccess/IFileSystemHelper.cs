using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCamp.Core.DataAccess
{
    public interface IFileSystemHelper
    {
        bool FileExists(string file);
        string ReadFile(string file);
        void WriteFile(string file, string content);
    }
}
