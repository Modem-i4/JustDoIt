using AutoMapper;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Queries
{
    public class GetUsersQuery : IRequest<Response<IEnumerable<GetUsersViewModel>>>
    {
        public string SearchQuery { get; set; }
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Response<IEnumerable<GetUsersViewModel>>>
        {
            private readonly IUserRepositoryAsync _userRepositoryAsync;
            private readonly IMapper _mapper;
            public GetUsersQueryHandler(IUserRepositoryAsync userRepositoryAsync, IMapper mapper)
            {
                _userRepositoryAsync = userRepositoryAsync;
                _mapper = mapper;
            }
            public async Task<Response<IEnumerable<GetUsersViewModel>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
            {
                var users = await _userRepositoryAsync.SearchForUsers(query.SearchQuery);
                var usersViewModel = _mapper.Map<IEnumerable<GetUsersViewModel>>(users);
                return new Response<IEnumerable<GetUsersViewModel>>(usersViewModel);
            }
        }
    }
}