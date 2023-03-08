using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Requests.GameLobbyRequests
{
    public class JoinToLobbyCommand:IRequest<ReturnResult>
    {
        public string Title { get; set; }

        public string? Password { get; set; }

    }
}
