using System;
using System.Collections.Generic;
using System.Text;

namespace MeuProjeto.Business.DTOs
{
    public class ResultProcessing
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
