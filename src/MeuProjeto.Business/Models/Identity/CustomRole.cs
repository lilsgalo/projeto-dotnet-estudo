using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MeuProjeto.Business.Models.Identity
{
    public class CustomRole : IdentityRole<Guid>
    {
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool Admin { get; set; }
        public virtual ICollection<CustomUserRole> UserRoles { get; set; }
        public virtual ICollection<CustomRoleClaim> Claims { get; set; }
    }

}
