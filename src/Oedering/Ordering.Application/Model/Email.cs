using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Model
{
    public class Email
    {
        public string To { get; set; }
        public string From { get; set; } = "amir.2002.ba@gmail.com"; 
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}