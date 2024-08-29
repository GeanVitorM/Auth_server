using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.Extensions.Logging;

namespace Authetication.Server.Api.Services;

public class EmailService : IEmailService
{
    private readonly string _sendGridApiKey;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _sendGridApiKey = configuration["SendGridApiKey"];
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("geanv7820@gmail.com", "LADS");
            var toEmail = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, body, body);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Body.ReadAsStringAsync();
                throw new Exception($"Failed to send email: {response.StatusCode} - {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending email.");
            throw;
        }
    }
}
