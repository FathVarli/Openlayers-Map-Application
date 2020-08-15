namespace Core.Models
{
    public class RegisterModel
    {
        public string NameSurname { get; set; }
        //[Required(ErrorMessage = "Lütfen adınızı giriniz")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Adınızı 3-50 karakter arasında girebilirsiniz")]
        //[Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
        //[Display(Name = "Şifre")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

    }
}