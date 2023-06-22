using System;
using System.Collections.Generic;
using System.Text;

namespace MeuProjeto.Business.DTOs
{
    public abstract class PagedListParameters
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        private string _sort = "Id";
        public string Sort { 
            get
            {
                return _sort +" "+Order;
            }
            set
            {
                _sort = value;
            }
        }
        private string _order = "";
        public string Order {
            get { return (_order.ToUpper().Equals("DESC") ? " DESCENDING" : ""); }
            set {
                _order = value;
            } 
        }
    }
}
