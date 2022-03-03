using System.ComponentModel.DataAnnotations;

namespace AspNetUserManagement.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0}의 길이는 {2} 에서 {1} 사이여야 합니다.", MinimumLength = 3)]
        [Display(Name = "아이디")]
        public string Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0}의 길이는 {2} 에서 {1} 사이여야 합니다.", MinimumLength = 3)]
        [Display(Name = "패스워드")]
        public string Password { get; set; }
    }
}