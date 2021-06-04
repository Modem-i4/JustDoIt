using AutoMapper;
using JustDoIt.Application.Filters;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Tasks.Queries.GetColumnTasks
{
    public class GetColumnTasksQuery : IRequest<Response<IEnumerable<GetColumnTasksViewModel>>>
    {
        public int ColumnId { get; set; }
    }
    public class GetColumnTasksQueryHandler : IRequestHandler<GetColumnTasksQuery, Response<IEnumerable<GetColumnTasksViewModel>>>
    {
        private readonly ITaskRepositoryAsync _taskRepository;
        private readonly IMapper _mapper;
        public GetColumnTasksQueryHandler(ITaskRepositoryAsync taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetColumnTasksViewModel>>> Handle(GetColumnTasksQuery request, CancellationToken cancellationToken)
        {
            //TODO: implify filter
            var validFilter = _mapper.Map<GetColumnTasksParameter>(request);
            var task = await _taskRepository.GetTasksByDeskId(validFilter);
            var taskViewModel = _mapper.Map<IEnumerable<GetColumnTasksViewModel>>(task);
            return new Response<IEnumerable<GetColumnTasksViewModel>>(taskViewModel);
        }
    }
}
