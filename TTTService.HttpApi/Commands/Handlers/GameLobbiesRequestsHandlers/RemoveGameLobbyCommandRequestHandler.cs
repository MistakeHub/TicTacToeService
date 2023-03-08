using MediatR;
using TTTService.HttpApi.Commands.Requests.GameLobbyRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;
using TTTService.infrastructure;
using TTTService.infrastructure.InterfaceImpl;

namespace TTTService.HttpApi.Commands.Handlers.GameLobbiesRequestsHandlers
{
    public class RemoveGameLobbyCommandRequestHandler : IRequestHandler<RemoveGameLobbyCommand, ReturnResult>
    {
        private GameLobbyRepository _repository;

        public RemoveGameLobbyCommandRequestHandler(GameLobbyRepository repository) { _repository = repository; }
        public Task<ReturnResult> Handle(RemoveGameLobbyCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();
            int resultadd = _repository.RemoveLobby(request.Title, request.Iduser);
            switch (resultadd)
            {
                case (int)GameLobbyStatuse.NotAllowd:
                    {
                        result.Data = $"У вас нет прав";
                        result.StatuseCode = (int)Statuse.BadGateway;
                        break;
                    }
                case (int)GameLobbyStatuse.Success:
                    {
                        result.Data = $"Лобби удалено";
                        result.StatuseCode = (int)Statuse.Success;
                        break;
                    }
                case (int)GameLobbyStatuse.NotFound:
                    {
                        result.Data = $"Лобби не найдено";
                        result.StatuseCode = (int)Statuse.Conflicted;
                        break;
                    }
                default:
                    {
                        result.Data = $"Невозможно удалить лобби";
                        result.StatuseCode = (int)Statuse.Conflicted;
                        break;
                    }
            }
            return Task.FromResult(result);
        }
    }
}
