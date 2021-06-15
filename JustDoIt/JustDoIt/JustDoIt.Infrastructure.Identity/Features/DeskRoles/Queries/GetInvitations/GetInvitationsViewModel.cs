using System.ComponentModel.DataAnnotations.Schema;

namespace JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetPendingInvitations
{
    public class GetInvitationsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}