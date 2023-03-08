using MediatR;
using TTTService.HttpApi.Commands.Requests.GameLobbyRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;
using TTTService.infrastructure.InterfaceImpl;

namespace TTTService.HttpApi.Commands.Handlers.GameLobbiesRequestsHandlers
{
    public class AddGameLobbiesCommandRequestHandler:IRequestHandler<AddGameLobbyCommand, ReturnResult>
    {
        private GameLobbyRepository _repository;

        public AddGameLobbiesCommandRequestHandler(GameLobbyRepository repository) { _repository= repository; }

        public Task<ReturnResult> Handle(AddGameLobbyCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();
           bool resultadd = _repository.CreateLobby(request.Title, request.Idhost, request.Password);

            if (resultadd)
            {
                result.Data = $"Лобби создано";
                result.StatuseCode = (int)Statuse.Success;
             
            }
            else
            {
                    result.Data = $"Невозможно создать лобби";
                    result.StatuseCode = (int)Statuse.Conflicted;

            }
            return Task.FromResult(result);
        }
    }
}
