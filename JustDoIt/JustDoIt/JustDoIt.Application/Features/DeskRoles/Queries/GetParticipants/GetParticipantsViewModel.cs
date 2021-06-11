using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Application.Features.Products.Queries.GetAllProducts
{
    public class GetParticipantsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Enums.DeskRoles Role { get; set; }
    }
}
