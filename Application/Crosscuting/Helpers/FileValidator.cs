using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;

namespace Application.Crosscuting.Helpers
{
    public static class FileValidator
    {
        public static ServiceResult<bool> ValidateFile(byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    MessageError = "O arquivo está vazio."
                };
            }

            if (!IsFileSizeValid(fileBytes))
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    MessageError = "O tamanho do arquivo excede 10 MB."
                };
            }

            if (!IsJpegOrPng(fileBytes))
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    MessageError = "O arquivo não é um JPEG ou PNG válido."
                };
            }

            return new ServiceResult<bool>
            {
                Success = true,
                Data = true // O arquivo é válido
            };
        }

        private static bool IsFileSizeValid(byte[] fileBytes)
        {
            const long TenMegabytes = 10 * 1024 * 1024; // 10 MB

            return fileBytes.Length <= TenMegabytes;
        }

        private static bool IsJpegOrPng(byte[] imageBytes)
        {
            try
            {
                using (var stream = new MemoryStream(imageBytes))
                {
                    var imageFormat = Image.DetectFormat(stream);
                    return imageFormat == JpegFormat.Instance || imageFormat == PngFormat.Instance;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}