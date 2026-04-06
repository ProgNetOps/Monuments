namespace Monuments.API.Services;

public class CloudMailService(IConfiguration configuration) : IMailService
{
    private string _mailTo = configuration["mailSettings:mailToAddress"];
    private string _mailFrom = configuration["mailSettings:mailFromAddress"];


    public void Send(string subject, string message)
    {
        //send mail - output to console window
        Console.WriteLine($"Mail from {_mailFrom} to {_mailTo} " +
            $"with {nameof(CloudMailService)}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
}