﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Model
{
    public class EmailSetting
    {
        //stmp email
        public string HOST { get; set; } = "smtp.elasticemail.com";
        public int PORT { get; set; } = 2525;
        public string User { get; set; } = "amir.2002.ba@gmail.com";
        public string Password { get; set; } = "736D51DDC5BCD2F78D4C39F498F1BBCCB022";

        // sandGrid
        // public string ApiKey { get; set; } = "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";
        // public string FromAddress { get; set; } = "amir.2002.ba@gmail.com";
        // public string FromName { get; set; } = "Coin";
    }
}