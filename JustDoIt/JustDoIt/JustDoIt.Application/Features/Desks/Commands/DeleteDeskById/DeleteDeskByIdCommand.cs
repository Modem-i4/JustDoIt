using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Desks.Commands.DeleteProductById
{
    public class DeleteDeskByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteDeskByIdCommandHandler : IRequestHandler<DeleteDeskByIdCommand, Response<int>>
        {
            private readonly IDeskRepositoryAsync _deskRepository;
            public DeleteDeskByIdCommandHandler(IDeskRepositoryAsync deskRepository)
            {
                _deskRepository = deskRepository;
            }
            public async Task<Response<int>> Handle(DeleteDeskByIdCommand command, CancellationToken cancellationToken)
            {
                var desk = await _deskRepository.GetByIdAsync(command.Id);
                if (desk == null) throw new ApiException($"Desk Not Found.");
                await _deskRepository.DeleteAsync(desk);
                return new Response<int>(desk.Id);
            }
        }
    }
}
