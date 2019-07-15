using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
