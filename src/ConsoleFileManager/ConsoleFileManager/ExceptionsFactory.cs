using System;

namespace ConsoleFileManager
{
    public static class ExceptionsFactory
    {
        public static Exception PathNotExist(string path, string propertyName) =>
            new ArgumentException($"Path not exist {path}", propertyName);
        public static Exception SamePathAlreadyExist(string path, string propertyName) =>
            new ArgumentException($"Path already exist {path}", propertyName);
    }
}