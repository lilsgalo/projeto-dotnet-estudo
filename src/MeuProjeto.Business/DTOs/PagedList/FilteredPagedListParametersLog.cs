using MeuProjeto.Business.Enums;

namespace MeuProjeto.Business.DTOs
{
    public class FilteredPagedListParametersLog : FilteredPagedListParameters
    {
        public LogTypeEnum? FilterType { get; set; }
        public string TableName { get; set; }
    }
}
