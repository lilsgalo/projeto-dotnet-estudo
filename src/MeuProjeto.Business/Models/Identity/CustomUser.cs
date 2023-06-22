using MeuProjeto.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace MeuProjeto.Business.Models.Identity
{
    public class CustomUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public Guid? ImageId { get; set; }
        public Picture Image { get; set; }
        public Guid? IconId { get; set; }
        public Picture Icon { get; set; }
        public virtual ICollection<CustomUserRole> UserRoles { get; set; }
        public virtual ICollection<CustomUserClaim> Claims { get; set; }
        public string ObjectSid { get ; set ; }
        public string ObjectGuid { get ; set ; }
        public string ObjectCategory { get ; set ; }
        public string ObjectClass { get ; set ; }
        public string CommonName { get ; set ; }
        public string DistinguishedName { get ; set ; }        
    }
}
