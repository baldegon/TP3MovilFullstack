using System;
using System.IO;
using Microsoft.Maui.Storage;

namespace TP3MovilFullstack.Utils
{
    public static class ImageHelper
    {
        public static string ToDataUrl(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return Fallback();
            }

            string fullPath = ResolvePath(path);
            if (!File.Exists(fullPath))
            {
                return Fallback();
            }

            try
            {
                byte[] bytes = File.ReadAllBytes(fullPath);
                string contentType = GetContentType(fullPath);
                string base64 = Convert.ToBase64String(bytes);
                return $"data:{contentType};base64,{base64}";
            }
            catch
            {
                return Fallback();
            }
        }

        private static string ResolvePath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }

            // Check in AppDataDirectory
            var appData = Path.Combine(FileSystem.AppDataDirectory, path);
            if (File.Exists(appData))
            {
                return appData;
            }

            // Check in AppPackageDirectory
            var appPackage = Path.Combine(FileSystem.AppPackageDirectory, path);
            if (File.Exists(appPackage))
            {
                return appPackage;
            }

            return path; // return original, will fail later
        }

        private static string GetContentType(string path)
        {
            return Path.GetExtension(path).ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".svg" => "image/svg+xml",
                _ => "application/octet-stream"
            };
        }

        private static string Fallback()
        {
            // 1x1 transparent GIF
            return "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==";
        }
    }
}
