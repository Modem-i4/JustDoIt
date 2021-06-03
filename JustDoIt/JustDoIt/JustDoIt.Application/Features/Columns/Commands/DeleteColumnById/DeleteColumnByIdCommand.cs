using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Columns.Products.Commands.DeleteColumnById
{
    public class DeleteColumnByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteColumnByIdCommandHandler : IRequestHandler<DeleteColumnByIdCommand, Response<int>>
        {
            private readonly IColumnRepositoryAsync _columnRepository;
            public DeleteColumnByIdCommandHandler(IColumnRepositoryAsync columnRepository)
            {
                _columnRepository = columnRepository;
            }
            public async Task<Response<int>> Handle(DeleteColumnByIdCommand command, CancellationToken cancellationToken)
            {
                var column = await _columnRepository.GetByIdAsync(command.Id);
                if (column == null) throw new ApiException($"Column Not Found.");
                await _columnRepository.DeleteAsync(column);
                return new Response<int>(column.Id);
            }
        }
    }
}
