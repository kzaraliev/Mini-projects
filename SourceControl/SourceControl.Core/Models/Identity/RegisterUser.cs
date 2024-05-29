using System.ComponentModel.DataAnnotations;
using static SourceControl.Core.Constants.MessageConstants;

namespace SourceControl.Core.Models.Identity
{
    public class RegisterUser
    {
        [Required(ErrorMessage = RequiredMessage)]
        public required string Email { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public required string Password { get; set; }
    }
}
