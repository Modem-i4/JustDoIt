using AutoMapper;
using JustDoIt.Application.Features.Columns.Queries.GetDeskColumn;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Features.Products.Queries.GetAllProducts;
using JustDoIt.Domain.Entities;
using JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetParticipants;
using JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetPendingInvitations;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.Invite;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.RequestInvite;
using JustDoIt.Infrastructure.Identity.Features.Users.Queries;
using JustDoIt.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ApplicationUser, GetUsersViewModel>().ReverseMap();
            CreateMap<Desk, GetInvitationsViewModel>();
            CreateMap<UserDeskRole, GetParticipantsViewModel>().IncludeAllDerived().ReverseMap();
            CreateMap<InviteCommand, UserDeskRole>();
            CreateMap<RequestInviteCommand, UserDeskRole>();
        }
    }
}
