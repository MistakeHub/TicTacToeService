using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Queries.MoveQueries
{
    public class GetMovesByIdCommand:IRequest<ReturnResult>
    {
        public int Idgame { get; set; }
    }
}
