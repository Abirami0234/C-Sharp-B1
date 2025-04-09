using System;
using System.IO;

namespace CareerHub
{
    public class FileUploadExceptionHandler
    {
        public static void UploadResume(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("Resume file not found.");

                FileInfo fileInfo = new FileInfo(filePath);

                if (fileInfo.Length > 2 * 1024 * 1024)
                    throw new Exception("File size exceeded 2MB.");

                if (!fileInfo.Extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase) &&
                    !fileInfo.Extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Unsupported file format. Only PDF and DOCX allowed.");
                }

                Console.WriteLine("Resume uploaded successfully!");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Upload Error: {ex.Message}");
            }
        }
    }
}
