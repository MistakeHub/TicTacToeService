using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Queries.GameQueries
{
    public class GetGameByIdCommand: IRequest<ReturnResult>
    {
        public int Id { get; set; }

    }
}
