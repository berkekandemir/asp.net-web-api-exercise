using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delta.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}