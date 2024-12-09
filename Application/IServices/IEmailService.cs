﻿using Application.Handle.HandleEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IEmailService
    {
        string SendEmail(EmailMessage emailMessage);
    }
}