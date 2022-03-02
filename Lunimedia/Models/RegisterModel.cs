using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TugberkUg.MVC.Validation;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Lunimedia.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "사용자명")]
        [StringLength(20, ErrorMessage = "{0}의 길이는 {2} 에서 {1} 사이여야 합니다.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "아이디")]
        [StringLength(20, ErrorMessage = "{0}의 길이는 {2} 에서 {1} 사이여야 합니다.", MinimumLength = 3)]
        [ServerSideRemote("Auth", "VerifyId", ErrorMessage = "이미 존재하는 아이디입니다.")]
        [Remote("VerifyId", "Auth", HttpMethod = "POST", ErrorMessage = "이미 존재하는 아이디입니다.")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "패스워드")]
        [StringLength(20, ErrorMessage = "{0}의 길이는 {2} 에서 {1} 사이여야 합니다.", MinimumLength = 3)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "패스워드 확인")]
        [Compare("Password", ErrorMessage = "입력하신 패스워드와 일치하지 않습니다.")]
        public string ConfirmPassword { get; set; }
    }
}