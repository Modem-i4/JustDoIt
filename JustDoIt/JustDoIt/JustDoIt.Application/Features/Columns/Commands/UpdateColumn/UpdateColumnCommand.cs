using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Columns.Commands.UpdateColumn
{
    public class UpdateColumnCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public class UpdateColumnCommandHandler : IRequestHandler<UpdateColumnCommand, Response<int>>
        {
            private readonly IColumnRepositoryAsync _columnRepository;
            public UpdateColumnCommandHandler(IColumnRepositoryAsync columnRepository)
            {
                _columnRepository = columnRepository;
            }
            public async Task<Response<int>> Handle(UpdateColumnCommand command, CancellationToken cancellationToken)
            {
                var column = await _columnRepository.GetByIdAsync(command.Id);

                if (column == null)
                {
                    throw new ApiException($"Desk Not Found.");
                }
                else
                {
                    column.Title = command.Title;
                    await _columnRepository.UpdateAsync(column);
                    return new Response<int>(column.Id);
                }
            }
        }
    }
}
