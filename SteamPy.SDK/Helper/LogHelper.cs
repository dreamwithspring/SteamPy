using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace SteamPy.SDK.Helper
{
    public static class LogHelper
    { 
        private static object ErrorLock = new();
        private static object InfoLock = new();

        private static string BasePath => AppDomain.CurrentDomain.BaseDirectory;

        
        private static void CreateErrorFile()
        {
            lock (ErrorLock)
            {

                var dirPath = $"{BasePath}/log";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                var errorPath = $"{BasePath}/log/{DateTime.Now:yyyyMMdd}error.txt";
                if (!File.Exists(errorPath))
                {
                    File.Create(errorPath);
                }
            }
        }

        public static void LogError(string message)
        {
            var bytes = Encoding.UTF8.GetBytes($@"
{DateTime.Now:yyyy-MM-dd HH:mm:ss} 【错误】
错误信息为：{message}
");
            LogError(bytes);
        }
        private static void LogError(byte[] bytes)
        {
            var path = $"{BasePath}/log/{DateTime.Now:yyyyMMdd}error.txt";


            lock (ErrorLock)
            {

                var dirPath = $"{BasePath}/log";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                var errorPath = $"{BasePath}/log/{DateTime.Now:yyyyMMdd}error.txt";
                if (!File.Exists(errorPath))
                {
                    using var stream = File.Create(errorPath);
                    stream.Write(bytes);
                }
                else
                {
                    using Stream stream = File.OpenWrite(errorPath);
                    stream.Write(bytes);
                }
            }
        }

        public static void LogError(Exception ex)
        {

            var bytes = Encoding.UTF8.GetBytes($@"
{DateTime.Now:yyyy-MM-dd HH:mm:ss} 【错误】
错误信息为：{ex.Message}
错误堆栈为：{ex.StackTrace}
");
            LogError(bytes);
        }
        public static void LogInfo(string message)
        {
            var path = $"{BasePath}/log/{DateTime.Now:yyyyMMdd}info.txt";
            var bytes = Encoding.UTF8.GetBytes($@"
{DateTime.Now:yyyy-MM-dd HH:mm:ss} 【信息】
信息为：{message}
");
            lock (InfoLock)
            {

                var dirPath = $"{BasePath}/log";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                var infoPath = $"{BasePath}/log/{DateTime.Now:yyyyMMdd}info.txt";
                if (!File.Exists(infoPath))
                {
                    using var stream = File.Create(infoPath);
                    stream.Write(bytes);
                }
                else
                {
                    using Stream stream = File.OpenWrite(infoPath);
                    stream.Write(bytes);
                }
            }
        }


    }
}
