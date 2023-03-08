using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Requests.GameLobbyRequests
{
    public class AddGameLobbyCommand:IRequest<ReturnResult>
    {
        public string Title { get; set; }

        public string? Password { get; set; }

        public int Idhost { get; set; }
    }
}
