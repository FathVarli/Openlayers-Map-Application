using System.ComponentModel.DataAnnotations;
namespace Core.Model
{
    public class RegisterModel
    {
        //[Required(ErrorMessage = "Lütfen adınızı giriniz")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Adınızı 3-50 karakter arasında girebilirsiniz")]
        //[Display(Name = "Kullanıcı Adı")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Lütfen e-posta adresinizi geçerli bir formatta giriniz")]
        public string EMail { get; set; }

        //[Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
        //[Display(Name = "Şifre")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}