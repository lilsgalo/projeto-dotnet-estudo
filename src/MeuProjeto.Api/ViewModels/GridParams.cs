using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuProjeto.Api.ViewModels
{
    public class GridParams
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string Search { get; set; }
        public string Id { get; set; }

        public string Sort { get; set; }
        public string Order { get; set; }

    }

}
