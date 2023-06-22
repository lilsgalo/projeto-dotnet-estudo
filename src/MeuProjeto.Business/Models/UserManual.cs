using System;
using MeuProjeto.Business.Models.Identity;

namespace MeuProjeto.Business.Models
{
    public class UserManual : Entity
    {
        public UserManual()
        {
            ReviewDate = DateTime.Now;
            Revision = 0;
        }
        public string Code { get; set; }
        public Guid ContentId { get; set; }
        public Text Content { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Revision { get; set; }
        public Guid? LastReviewerId { get; set; }
        public CustomUser LastReviewer { get; set; }
    }
}
