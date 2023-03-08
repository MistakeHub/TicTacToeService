using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Requests.MoveRequests
{
    public class AddMoveCommand: IRequest<ReturnResult>
    {
        public int Idgame { get; set; }

        public int Iduser { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
