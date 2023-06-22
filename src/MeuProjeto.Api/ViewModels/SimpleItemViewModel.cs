using DevIO.Api.Extensions;
using MeuProjeto.Business.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.Api.ViewModels
{
    public class SimpleItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}