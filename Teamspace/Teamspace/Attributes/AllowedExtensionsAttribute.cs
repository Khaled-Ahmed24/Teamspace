using System.ComponentModel.DataAnnotations;

namespace Teamspace.Attributes
{
    public class AllowedExtensionsAttribute:ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile; 
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult($"The file extension '{extension}' is not allowed. Allowed extensions are: {string.Join(", ", _extensions)}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
