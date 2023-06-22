using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuProjeto.Api.ViewModels
{
    public class UserCurrentUserViewModel : SimpleItemViewModel
    {
        public string Email { get; set; }
        public ICollection<SimpleItemViewModel> Profiles { get; set; }
    }
}
