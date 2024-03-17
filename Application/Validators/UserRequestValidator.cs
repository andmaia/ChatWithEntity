using Application.Crosscuting.DTO.User;
using FluentValidation;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace Application.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("O nome deve ser fornecido e ter no máximo 50 caracteres.");

            RuleFor(x => x.IdCredentials)
                .NotEmpty()
                .Length(36)
                .WithMessage("O ID de credenciais deve ser fornecido e ter exatamente 36 caracteres.");
        }

        private bool BeAValidPhoto(byte[] photoUser)
        {
            if (photoUser == null || photoUser.Length == 0)
            {
                return false;
            }

            try
            {
                if (!IsJpegOrPng(photoUser))
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsJpegOrPng(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return false;
            }

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
