using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace CodeCamp.Core.DataAccess
{
    public class IsolatedStorageFileSystemHelper : IFileSystemHelper
    {
        public bool FileExists(string file)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                return store.FileExists(file);
            }
        }

        public string ReadFile(string file)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = store.OpenFile(file, FileMode.Open))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public void WriteFile(string file, string content)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists(file))
                    store.DeleteFile(file);

                using (var stream = store.OpenFile(file, FileMode.Create))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(content);
                    }
                }
            }
        }
    }
}
