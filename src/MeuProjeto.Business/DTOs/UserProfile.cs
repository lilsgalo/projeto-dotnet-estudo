using MeuProjeto.Business.Models;
using System;
using System.Collections.Generic;

namespace MeuProjeto.Business.DTOs
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Active { get; set; }
        public bool Admin { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
