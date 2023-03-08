using MediatR;
using TTTService.Domain.Entities;
using TTTService.Domain.Interfaces;
using TTTService.HttpApi.Commands.Queries.GameQueries;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;
using TTTService.HttpApi.Helpers.ViewModels;

namespace TTTService.HttpApi.Commands.Handlers.GameQueriesHandlers
{
    public class GetGameByIdCommandQueryHandler: IRequestHandler<GetGameByIdCommand, ReturnResult>
    {
        private IBaseRepository<Game> _repository;

        public GetGameByIdCommandQueryHandler(IBaseRepository<Game> repository) { _repository = repository; }
        public async Task<ReturnResult> Handle(GetGameByIdCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();
            Game game = _repository.GetById(request.Id).Result;
            if (game != null)
            {
                result.Data =new GameViewModel() {Id = game.Id, Iduser1=game.Iduser1, Iduser2=game.Iduser2, Dategamestart=game.Dategamestart, Idcurrentturn=game.Idcurrentturn, Idwinner=game.Idwinner, Isfinished=game.Isfinished };
                result.StatuseCode = (int)Statuse.Success;
            }
            else
            {
                result.Data = "Такой игры - нет";
                result.StatuseCode = (int)Statuse.NotFound;
            }

            return result;
        }
    }
}
