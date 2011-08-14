using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeCamp.Core.DataAccess
{
    public class StandardFileSystemHelper : IFileSystemHelper
    {
        private readonly string _rootPath;

        public StandardFileSystemHelper(string rootPath)
        {
            _rootPath = rootPath;
        }

        public bool FileExists(string file)
        {
            return File.Exists(Path.Combine(_rootPath, file));
        }

        public string ReadFile(string file)
        {
            using (var reader = new StreamReader(Path.Combine(_rootPath, file)))
            {
                return reader.ReadToEnd();
            }
        }

        public void WriteFile(string file, string content)
        {
            using (var writer = new StreamWriter(Path.Combine(_rootPath, file)))
            {
                writer.Write(content);
            }
        }
    }
}
