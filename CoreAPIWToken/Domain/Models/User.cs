using System;
using System.Collections.Generic;

namespace CoreAPIWToken.Domain.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}
