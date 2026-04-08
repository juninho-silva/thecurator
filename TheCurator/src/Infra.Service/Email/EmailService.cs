using System.Text;
using Infra.Service.Email.Models;
using Infra.Service.Tmdb.Models;
using Microsoft.Extensions.Configuration;
using Resend;

namespace Infra.Service.Email
{
    internal sealed class EmailService : IEmailService
    {
        private readonly IResend _resend;
        private readonly string _fromAddr;
        private readonly string _baseUrl;

        public EmailService(IResend resend, IConfiguration config)
        {
            _resend = resend;
            _fromAddr = config["Resend:From"]!;
            _baseUrl = config["App:BaseUrl"]!;
        }

        public async Task SendNewsletterAsync(
            ResendEmailRequest request)
        {
            var html = BuildHtml(request.Name, request.UnsubscribeToken, request.Movies, request.Series);
            var subject = $"🎬 O curador — Suas indicações da semana";
            var message = new EmailMessage()
            {
                From = _fromAddr,
                Subject = subject,
                HtmlBody = html
            };
            message.To.Add(new EmailAddress() { DisplayName = request.Name, Email = request.Email });
            await _resend.EmailSendAsync(message);
        }

        private string BuildHtml(string name, string unsubscribeToken, List<MovieCard> movies, List<MovieCard> series)
        {
            var unsubscribeUrl = $"{_baseUrl}/subscriber/unsubscribe?token={unsubscribeToken}";
            var sb = new StringBuilder();

            sb.Append($$"""
        <!DOCTYPE html>
        <html lang="pt-BR">
        <head>
          <meta charset="UTF-8"/>
          <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
          <title>O curador</title>
        </head>
        <body style="margin:0;padding:0;background:#0d0d0d;font-family:'Helvetica Neue',Arial,sans-serif;">
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr><td align="center" style="padding:40px 16px;">
              <table width="600" cellpadding="0" cellspacing="0" style="max-width:600px;width:100%;">
 
                <!-- Header -->
                <tr><td style="padding:0 0 32px;">
                  <h1 style="margin:0;font-size:28px;color:#e8c97e;font-weight:700;letter-spacing:0.04em;">
                    CineNota 🎬
                  </h1>
                  <p style="margin:8px 0 0;font-size:13px;color:#888;letter-spacing:0.1em;text-transform:uppercase;">
                    Sua dose semanal de cinema
                  </p>
                </td></tr>
 
                <!-- Greeting -->
                <tr><td style="padding:0 0 32px;">
                  <p style="margin:0;font-size:16px;color:#c8c5be;line-height:1.7;">
                    Olá, <strong style="color:#f0ede6;">{{name.Split(' ')[0]}}</strong>! Separei as melhores indicações dessa semana pra você.
                  </p>
                </td></tr>
        """);

            // Filmes
            if (movies.Count > 0)
                AppendSection(sb, "🎥 Filmes", movies);

            // Séries
            if (series.Count > 0)
                AppendSection(sb, "📺 Séries", series);

            sb.Append($$"""
                <!-- Footer -->
                <tr><td style="padding:40px 0 0;border-top:1px solid #222;">
                  <p style="margin:0;font-size:12px;color:#555;line-height:1.8;">
                    Você está recebendo este email porque se inscreveu na CineNota.<br/>
                    <a href="{{unsubscribeUrl}}" style="color:#888;text-decoration:underline;">
                      Cancelar inscrição
                    </a>
                  </p>
                </td></tr>
 
              </table>
            </td></tr>
          </table>
        </body>
        </html>
        """);

            return sb.ToString();
        }

        private static void AppendSection(StringBuilder sb, string title, List<MovieCard> items)
        {
            sb.Append($"""
        <tr><td style="padding:0 0 8px;">
          <h2 style="margin:0 0 20px;font-size:18px;color:#e8c97e;font-weight:600;">{title}</h2>
        </td></tr>
        """);

            foreach (var item in items)
            {
                sb.Append($$"""
            <tr><td style="padding:0 0 24px;">
              <table width="100%" cellpadding="0" cellspacing="0"
                     style="background:#161616;border-radius:8px;overflow:hidden;">
                <tr>
                  {{(item.PosterUrl.Length > 0 ? $"""
                  <td width="100" valign="top" style="padding:0;">
                    <img src="{item.PosterUrl}" width="100" alt="{item.Title}"
                         style="display:block;border-radius:8px 0 0 8px;"/>
                  </td>
                  """ : "")}}
                  <td valign="top" style="padding:16px;">
                    <p style="margin:0 0 4px;font-size:16px;font-weight:600;color:#f0ede6;">
                      {{item.Title}} <span style="color:#888;font-weight:400;font-size:14px;">({{item.ReleaseYear}})</span>
                    </p>
                    <p style="margin:0 0 8px;font-size:12px;color:#888;">
                      ⭐ {{item.Rating}} &nbsp;·&nbsp; {{item.Genres}}
                    </p>
                    <p style="margin:0;font-size:14px;color:#a8a5a0;line-height:1.6;">
                      {{item.Overview}}
                    </p>
                  </td>
                </tr>
              </table>
            </td></tr>
            """);
            }
        }
    }
}
