using NotificationService.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Abstractions.Messaging
{
    public interface IEmailService
    {


        public Task SendEmailAsync(EmailMessage message);
    }
}
