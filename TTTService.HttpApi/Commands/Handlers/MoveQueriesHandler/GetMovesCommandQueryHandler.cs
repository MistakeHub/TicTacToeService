using MediatR;
using TTTService.Domain.Entities;
using TTTService.Domain.Interfaces;
using TTTService.HttpApi.Commands.Queries.MoveQueries;
using TTTService.HttpApi.Commands.Requests.MoveRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;
using TTTService.HttpApi.Helpers.ViewModels;
using TTTService.infrastructure;

namespace TTTService.HttpApi.Commands.Handlers.MoveQueriesHandler
{
    public class GetMovesCommandQueryHandler:IRequestHandler<GetMovesByIdCommand, ReturnResult>
    {
        private IGameMoveRepository _repository;

        public GetMovesCommandQueryHandler(IGameMoveRepository repository) { _repository = repository; }
        public async Task<ReturnResult> Handle(GetMovesByIdCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();
            var resultget = await _repository.GetAllMovesByGameID(request.Idgame);
           result.Data= resultget.Select(v => new MoveViewModel { Iduser = v.Iduser, Row = v.Row, Column = v.Column }).ToList();
            result.StatuseCode = (int)Statuse.Success;
            return result;
        }
    }
}
