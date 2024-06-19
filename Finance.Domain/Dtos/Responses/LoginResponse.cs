namespace Finance.Domain.Dtos.Responses
{
    public class LoginResponse
    {
        public LoginResponse(string tokenType, string token)
        {
            TokenType = tokenType;
            Token = token;
        }

        public string TokenType { get; set; }
        public string Token { get; set; }
    }
}
