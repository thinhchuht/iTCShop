using System.ComponentModel.DataAnnotations;

namespace iTCShop.Models
{
    public class Login
    {
        [Key]
        [Display(Name = "Email")]
        public string userMail { get; set; }
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }
    }
}
