using MediatR;
using TTTService.HttpApi.Commands.Queries.GameLobbiesQueries;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;
using TTTService.HttpApi.Helpers.ViewModels;
using TTTService.infrastructure.InterfaceImpl;

namespace TTTService.HttpApi.Commands.Handlers.GameLobbiesQueryHandlers
{
    public class GetAllGameLobbiesCommandQueryHandler : IRequestHandler<GetAllGameLobbiesCommand, ReturnResult>
    {
        private GameLobbyRepository _repository;

        public GetAllGameLobbiesCommandQueryHandler(GameLobbyRepository repository) { _repository = repository; }
        public Task<ReturnResult> Handle(GetAllGameLobbiesCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();
            var resultget = _repository.GetGameLobbies().Select(v=>new GameLobbyViewModel { Title=v.Title, Iduser=v.Idhost, IsPasswordDemand=v.Password != null});
            result.Data = resultget;
            result.StatuseCode = (int)Statuse.Success;

            return Task.FromResult(result);
        }
    }
}
