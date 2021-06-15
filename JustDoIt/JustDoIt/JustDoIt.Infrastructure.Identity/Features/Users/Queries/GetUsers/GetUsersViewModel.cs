using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Queries
{
    public class GetUsersViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
