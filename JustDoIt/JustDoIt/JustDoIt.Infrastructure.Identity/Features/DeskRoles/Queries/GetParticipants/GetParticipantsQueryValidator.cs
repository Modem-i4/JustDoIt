using AutoMapper;
using FluentValidation;
using JustDoIt.Application;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Infrastructure.Identity.Features.Users.Queries.GetParticipants;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetParticipants
{
    public class GetParticipantsQueryValidator : AbstractExtendedValidator<GetParticipantsQuery>
    {
        public GetParticipantsQueryValidator(IMemoryCache cache) : base(cache)
        {
            RuleFor(p => p.DeskId)
                .Must(ValidateDeskId).WithMessage("Open/select this desk first.");
        }
    }
}