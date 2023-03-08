using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Requests.GameLobbyRequests
{
    public class RemoveGameLobbyCommand : IRequest<ReturnResult>
    {
        public string Title { get; set; }

        public int Iduser { get; set; }

    }
}
