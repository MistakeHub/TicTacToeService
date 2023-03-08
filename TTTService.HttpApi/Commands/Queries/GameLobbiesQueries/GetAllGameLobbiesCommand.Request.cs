using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Queries.GameLobbiesQueries
{
    public class GetAllGameLobbiesCommand:IRequest<ReturnResult>
    {
    }
}
