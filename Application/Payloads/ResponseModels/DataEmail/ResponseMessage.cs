namespace Application.Payloads.ResponseModels.DataEmail
{
    public static class ResponseMessage
    {
        public static string GetEmailSuccessMessage(string email)
        {
            return $"A new email has sent to {email}";
        }
    }
}
