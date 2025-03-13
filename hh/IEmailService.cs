using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Services.Interface
{
    public interface IEmailService
    {
        void SentEmail(string? email);

    }
}
