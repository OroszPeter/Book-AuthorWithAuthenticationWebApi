using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Book_AuthorWIthAuthenticationWebApi.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

    }
}
