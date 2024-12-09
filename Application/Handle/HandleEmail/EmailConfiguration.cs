
namespace Application.Handle.HandleEmail
{
    public class EmailConfiguration
    {
        public string SmtpServer { get; set; } = String.Empty;
        public int Port { get; set; }
        public string From { get; set; } = String.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
    }
}
