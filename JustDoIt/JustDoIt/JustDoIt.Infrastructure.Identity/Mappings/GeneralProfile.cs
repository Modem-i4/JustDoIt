using AutoMapper;
using JustDoIt.Application.Columns.Columns.Commands.CreateColumn;
using JustDoIt.Application.Features.Columns.Queries.GetDeskColumn;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Features.Products.Queries.GetAllProducts;
using JustDoIt.Domain.Entities;
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
        }
    }
}
