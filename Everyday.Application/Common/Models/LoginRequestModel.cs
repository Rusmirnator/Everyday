using System.ComponentModel.DataAnnotations;

namespace Everyday.Application.Common.Models
{
    public class LoginRequestModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Provided login cannot be empty!")]
        public string? UserUniqueIdentifier { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Provided password cannot be empty!")]
        [MinLength(8, ErrorMessage = "Provided password is too short - minimum of 8 characters required!")]
        [MaxLength(254, ErrorMessage = "Provided password is too long - maximum of 254 characters allowed!")]
        public string? EncodedPassword { get; set; }
    }
}
