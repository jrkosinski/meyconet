using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace WSGUtilitieslib
{
    internal interface IPdfStore
    {
        Task<FileInfo> GetFile(string fileName, string subfolder);
        Task<bool> OpenPdf(string fileName, string subfolder);
        Task<bool> OpenPdfs(string filePattern, string subfolder);
        Task<bool> FileIsAvailable(string fileName, string subfolder);
        Task WriteFileFromReport(ReportClass report, string fileName, string subfolder);
    }

    internal class AzurePdfStore : IPdfStore
    {
        private const string SHARE_NAME = "meycostore-fileshare";

        public async Task<FileInfo> GetFile(string fileName, string subfolder)
        {
            string folderPath = ConfigurationManager.AppSettings[subfolder];
            string localTempPath = Path.Combine(ConfigurationManager.AppSettings["PdfLocalTempPath"], fileName);

            return await this.DownloadFile(folderPath, localTempPath, fileName);
        }

        public async Task<bool> OpenPdf(string fileName, string subfolder)
        {
            FileInfo tempFile = await this.GetFile(fileName, subfolder);
            if (tempFile != null)
            {
                System.Diagnostics.Process.Start(tempFile.FullName);
                return true;
            }

            return false;
        }

        public async Task<bool> OpenPdfs(string filePattern, string subfolder)
        {
            DirectoryInfo localDir = await this.DownloadMultipleFiles(ConfigurationManager.AppSettings["PdfLocalTempPath"], ConfigurationManager.AppSettings[subfolder], filePattern);

            FileInfo[] files = localDir.GetFiles(filePattern);
            if (files.Length > 0)
            {
                foreach (FileInfo f in files)
                {
                    System.Diagnostics.Process.Start(f.FullName);
                }
                return true;
            }
            return false;
        }

        public async Task WriteFileFromReport(ReportClass report, string fileName, string subfolder)
        {
            string folderPath = ConfigurationManager.AppSettings[subfolder];
            string localPath = Path.Combine(ConfigurationManager.AppSettings["PdfLocalTempPath"], fileName);

            //TODO: delete file after? is it a good idea? should they be shared or local to machine? 

            if (await this.FileIsAvailable(fileName, subfolder))
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = localPath;
                CrExportOptions = report.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                report.Export();
                await WriteAzureStorageFile(ConfigurationManager.AppSettings["PdfLocalTempPath"], folderPath, fileName);

                //TODO: delete the temp file 
            }
        }

        public async Task<bool> FileIsAvailable(string fileName, string subfolder)
        {
            string localTempPath = Path.Combine(ConfigurationManager.AppSettings["PdfLocalTempPath"], fileName);
            return !(new AppUtilities().IsFileOpen(localTempPath));
        }

        private async Task<FileInfo> DownloadFile(string azureFolderPath, string localFilePath, string fileName)
        {
            byte[] fileContents = await this.ReadAzureStorageFile(azureFolderPath, fileName);

            if (fileContents != null && fileContents.Length > 0)
            {
                File.WriteAllBytes(localFilePath, fileContents);
                return new FileInfo(localFilePath);
            }

            return null;
        }

        private async Task<DirectoryInfo> DownloadMultipleFiles(string localPath, string directoryName, string filePattern)
        {
            string fileNameBase = filePattern.Replace("*", String.Empty).Replace(".pdf", String.Empty);
            string fullPath = directoryName.Trim().EndsWith("/") ? $"{directoryName.Trim()}{fileNameBase.Trim()}" : $"{directoryName.Trim()}/{fileNameBase.Trim()}";
            string[] fileNames = AzureFileStore.ListFilesInDirectory(SHARE_NAME, fullPath);

            string localDirectory = Path.Combine(localPath, fileNameBase);
            if (fileNames.Length > 0)
            {
                if (!Directory.Exists(localDirectory))
                    Directory.CreateDirectory(localDirectory);

                //TODO: do async all at once 
                foreach (string name in fileNames)
                {
                    await this.DownloadFile(fullPath, Path.Combine(localDirectory, name), name);
                }
            }

            return new DirectoryInfo(localDirectory);
        }

        private async Task WriteAzureStorageFile(string localPath, string directoryName, string fileName)
        {
            await AzureFileStore.WriteFile(SHARE_NAME, directoryName, localPath, fileName);
        }

        private async Task<byte[]> ReadAzureStorageFile(string directoryName, string fileName)
        {
            return await AzureFileStore.ReadFile(SHARE_NAME, directoryName, fileName);
        }
    }

    internal class LocalPdfStore : IPdfStore
    {
        public async Task<FileInfo> GetFile(string fileName, string subfolder)
        {
            DirectoryInfo pdfDir = new DirectoryInfo(ConfigurationManager.AppSettings[subfolder]);
            FileInfo[] pdfFiles = pdfDir.GetFiles(fileName);
            if (pdfFiles.Length > 0)
            {
                return pdfFiles[0];
            }
            return null;
        }

        public async Task<bool> OpenPdf(string fileName, string subfolder)
        {
            DirectoryInfo pdfDir = new DirectoryInfo(ConfigurationManager.AppSettings[subfolder]);
            FileInfo[] pdffiles = pdfDir.GetFiles(fileName);
            if (pdffiles.Length > 0)
            {
                System.Diagnostics.Process.Start(pdffiles[0].FullName);
                return true;
            }
            return false;
        }

        public async Task<bool> OpenPdfs(string filePattern, string subfolder)
        {
            DirectoryInfo pdfDir = new DirectoryInfo(ConfigurationManager.AppSettings[subfolder]);
            FileInfo[] files = pdfDir.GetFiles(filePattern);
            if (files.Length > 0)
            {
                foreach (FileInfo f in files)
                {
                    System.Diagnostics.Process.Start(f.FullName);
                }
                return true;
            }
            return false;
        }

        public async Task WriteFileFromReport(ReportClass report, string fileName, string subfolder)
        {
            string filePath = System.IO.Path.Combine(ConfigurationManager.AppSettings[subfolder], fileName);
            if (await this.FileIsAvailable(fileName, subfolder))
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = filePath;
                CrExportOptions = report.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }

                report.Export();
            }
        }

        public async Task<bool> FileIsAvailable(string fileName, string subfolder)
        {
            string filePath = System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], fileName);
            return !(new AppUtilities().IsFileOpen(filePath));
        }
    }

    public static class PdfStorage
    {
        private static IPdfStore pdfStore = CreatePdfStore();

        public static async Task<FileInfo> GetFile(string fileName, string subfolder = "SOPDFPath")
        {
            return pdfStore != null ? await pdfStore.GetFile(fileName, subfolder) : null;
        }

        public static async Task<bool> OpenPdf(string fileName, string subfolder = "SOPDFPath")
        {
            return pdfStore != null ? await pdfStore.OpenPdf(fileName, subfolder) : false;
        }

        public static async Task<bool> OpenPdfs(string filePattern, string subfolder = "SOPDFPath")
        {
            return pdfStore != null ? await pdfStore.OpenPdfs(filePattern, subfolder) : false;
        }

        public static async Task WriteFileFromReport(ReportClass report, string fileName, string subfolder = "SOPDFPath")
        {
            if (pdfStore != null)
                await pdfStore.WriteFileFromReport(report, fileName, subfolder);
        }

        public static async Task<bool> FileIsAvailable(string fileName, string subfolder = "SOPDFPath")
        {
            return pdfStore != null && await pdfStore.FileIsAvailable(fileName, subfolder);
        }

        private static IPdfStore CreatePdfStore()
        {
            string storageType = ConfigurationManager.AppSettings["PdfStorageType"];
            if (storageType == "Local")
            {
                return new LocalPdfStore();
            }
            else if (storageType == "Azure")
            {
                return new AzurePdfStore();
            }

            return null;
        }
    }
}