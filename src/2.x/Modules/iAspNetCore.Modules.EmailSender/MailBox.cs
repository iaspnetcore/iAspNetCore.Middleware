using System;
using System.Collections.Generic;
using System.Text;

namespace iAspNetCore.Modules.EmailSender
{
  public  class MailBox
    {
      
        public string Body { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public bool IsHtml { get; set; }
        public string Subject { get; set; }
        public IEnumerable<string> To { get; set; }
    }
}
