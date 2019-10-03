using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; }
    }
}
