using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVAnalyzer.ViewModels
{
    public class SignupInfoViewModel
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
    }
}