using System;
using System.Collections.Generic;

namespace MeuProjeto.Business.DTOs
{
    public class UserFilteredPagedListParameters : FilteredPagedListParameters
    {
        public string Status { get; set; }
        public List<Guid> ExcludedIds { get; set; }
    }
}
