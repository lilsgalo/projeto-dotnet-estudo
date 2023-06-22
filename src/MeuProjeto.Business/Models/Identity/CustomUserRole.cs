using Microsoft.AspNetCore.Identity;
using System;

namespace MeuProjeto.Business.Models.Identity
{
    public class CustomUserRole : IdentityUserRole<Guid>
    {
        //public override Guid RoleId { get; set; }
        public virtual CustomRole Role { get; set; }
        //public override Guid UserId { get; set; }
        public virtual CustomUser User { get; set; }
    }

}
