namespace Sel_b10_hw
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SubmitButton { get; set; }

        public LoginData(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public LoginData(string username, string password, string submitButton)
        {
            Username = username;
            Password = password;
            SubmitButton = submitButton;
        }
    }
}
