using System;

namespace CVAnalyzer.Authentication.Model
{
    public class AuthToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TokenValue { get; set; }
        public DateTime LastAccessTime { get; set; }
    }
}