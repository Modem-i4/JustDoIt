using AutoMapper;
using JustDoIt.Application.Features.Columns.Commands.CreateColumn;
using JustDoIt.Application.Features.Columns.Queries.GetDeskColumn;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Features.Products.Queries.GetAllProducts;
using JustDoIt.Application.Features.Tasks.Commands.CreateTask;
using JustDoIt.Application.Features.Tasks.Queries.GetColumnTasks;
using JustDoIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Desk, GetAllDesksViewModel>().ReverseMap();
            CreateMap<CreateDeskCommand, Desk>();
            CreateMap<GetAllDesksQuery, GetAllDesksParameter>();

            CreateMap<Column, GetDeskColumnsViewModel>().ReverseMap();
            CreateMap<CreateColumnCommand, Column>();
            CreateMap<GetDeskColumnsQuery, GetDeskColumnsParameter>();

            CreateMap<TaskModel, GetColumnTasksViewModel>().ReverseMap();
            CreateMap<CreateTaskCommand, TaskModel>();
            CreateMap<GetColumnTasksQuery, GetColumnTasksParameter>();
        }
    }
}
