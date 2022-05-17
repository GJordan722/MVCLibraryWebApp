using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp
{
    public class User
    {
        public string? Email { get; set; } = null;
        public string? Username { get; set; } = null;
        public string? Password { get; set; } = null;
        public bool? Active { get; set; } = null;
        public int? Account_ID { get; set; } = null;
        public int? Role_ID { get; set; } = null;
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;


    }
}
