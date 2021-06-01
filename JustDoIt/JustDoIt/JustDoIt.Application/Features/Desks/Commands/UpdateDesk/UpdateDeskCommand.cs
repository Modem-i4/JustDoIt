using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateDeskCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public class UpdateDeskCommandHandler : IRequestHandler<UpdateDeskCommand, Response<int>>
        {
            private readonly IDeskRepositoryAsync _deskRepository;
            public UpdateDeskCommandHandler(IDeskRepositoryAsync deskRepository)
            {
                _deskRepository = deskRepository;
            }
            public async Task<Response<int>> Handle(UpdateDeskCommand command, CancellationToken cancellationToken)
            {
                var desk = await _deskRepository.GetByIdAsync(command.Id);

                if (desk == null)
                {
                    throw new ApiException($"Desk Not Found.");
                }
                else
                {
                    desk.Title = command.Title;
                    desk.OwnerId = command.OwnerId;
                    desk.Description = command.Description;
                    await _deskRepository.UpdateAsync(desk);
                    return new Response<int>(desk.Id);
                }
            }
        }
    }
}
