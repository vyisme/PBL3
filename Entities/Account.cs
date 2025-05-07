using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Entities
{
    public abstract class Account
    {

        private static PasswordHasher<Account> hasher = new PasswordHasher<Account>();

        [Key]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "SĐT phải có đúng 10 chữ số")]
        public string Sdt { get; set; }
        [Required(ErrorMessage = "This field is requierd")]
        public string Hoten { get ; set; }
        [Required]
        public string PasswordHash { get; set; }  // Đổi tên để rõ ràng

        // Băm mật khẩu và lưu
        public void SetPassword(string rawPassword)
        {
            PasswordHash = hasher.HashPassword(this, rawPassword);
        }

        // Kiểm tra mật khẩu nhập vào
        public bool VerifyPassword(string inputPassword)
        {
            var result = hasher.VerifyHashedPassword(this, PasswordHash, inputPassword);
            return result == PasswordVerificationResult.Success;
        }

        //public abstract void DisplayInfo();
        //public abstract string GetData();

    }
}
