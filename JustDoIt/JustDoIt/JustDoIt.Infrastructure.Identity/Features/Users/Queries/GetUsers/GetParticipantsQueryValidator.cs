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

namespace JustDoIt.Infrastructure.Identity.Features.Users.Queries
{
    public class GetParticipantsQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetParticipantsQueryValidator()
        {
            RuleFor(p => p.SearchQuery)
                .MinimumLength(3).WithMessage("To use search enter at least 3 characters.");
        }
    }
}