using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Email
{
    public interface IEmailSender
    {
        bool SendEmail(Email email);
    }
}
