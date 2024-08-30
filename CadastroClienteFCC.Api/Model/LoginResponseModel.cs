namespace CadastroClienteFCC.Api.Model
{
    public class LoginResponseModel
    {
        public string? Token { get; set; }
        public string? UserName { get; set; }
        public int Expired { get; set; }
    }
}
