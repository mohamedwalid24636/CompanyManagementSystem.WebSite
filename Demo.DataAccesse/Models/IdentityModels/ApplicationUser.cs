using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Models.Shared;
using Microsoft.AspNetCore.Identity;

namespace Demo.DataAccesse.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser 
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
    }
}
