using MeuProjeto.Business.Enums;

namespace MeuProjeto.Business.DTOs
{
    public class LogFilteredPagedListParameters : FilteredPagedListParameters
    {
        public LogTypeEnum? LogType { get; set; }
    }
}
