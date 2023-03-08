using MediatR;
using TTTService.Domain.Entities;
using TTTService.Domain.Interfaces;
using TTTService.HttpApi.Commands.Queries.GameQueries;
using TTTService.HttpApi.Commands.Requests.GameRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;
using TTTService.HttpApi.Helpers.ViewModels;

namespace TTTService.HttpApi.Commands.Handlers.GameQueriesHandlers
{
    public class GetAllGamesCommandQueryHandler:IRequestHandler<GetAllGamesCommand, ReturnResult>
    {

        private IBaseRepository<Game> _repository;

        public GetAllGamesCommandQueryHandler(IBaseRepository<Game> repository) { _repository = repository; }
        public async Task<ReturnResult> Handle(GetAllGamesCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();

            result.Data = _repository.GetAll().Result.Select(v=> new GameViewModel() {Id=v.Id, Iduser1 = v.Iduser1, Iduser2 = v.Iduser2, Dategamestart = v.Dategamestart, Idcurrentturn = v.Idcurrentturn, Idwinner = v.Idwinner, Isfinished = v.Isfinished });
            result.StatuseCode = (int)Statuse.Success;

            return result;
        }
    }
}
