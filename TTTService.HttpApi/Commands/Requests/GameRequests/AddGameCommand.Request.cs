using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Requests.GameRequests
{
    public class AddGameCommand:IRequest<ReturnResult>
    {
        public int Iduser1 { get; set; }

        public int Iduser2 { get; set; }

    }
}
