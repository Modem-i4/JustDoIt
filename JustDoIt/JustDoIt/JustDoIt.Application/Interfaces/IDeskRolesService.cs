using JustDoIt.Application.DTOs.Account;
using JustDoIt.Application.Enums;
using JustDoIt.Application.Features.DeskRoles.Commands;
using JustDoIt.Application.Features.DeskRoles.Features.Commands.Invite;
using JustDoIt.Application.Features.DeskRoles.Queries;
using JustDoIt.Application.Features.Products.Queries.GetAllProducts;
using JustDoIt.Application.Wrappers;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Application.Interfaces
{
    public interface IDeskRolesService
    {
        public int CurrentDeskId { get; }
        public DeskRoles CurrentDeskRole { get; }
        Task<Response<string>> InviteAsync(InviteCommand command);
        Task<Response<string>> ChangeRoleAsync(ChangeRoleCommand command);
        Task<List<GetParticipantsViewModel>> GetParticipants(GetParticipantsQuery query);
    }
}
