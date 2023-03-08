using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Queries.GameQueries
{
    public class GetAllGamesCommand: IRequest<ReturnResult>
    {
    }
}
