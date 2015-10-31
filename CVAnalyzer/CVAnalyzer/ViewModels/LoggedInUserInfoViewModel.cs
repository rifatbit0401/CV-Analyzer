using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVAnalyzer.ViewModels
{
    public class LoggedInUserInfoViewModel
    {
        public int UserId { get; set; }
        public string TokenValue { get; set; }
    }
}