using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delta.Models
{
    public class UserResponseWithTeam
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int? TeamId { get; set; }
        public  TeamResponse Team { get; set; }
    }
}