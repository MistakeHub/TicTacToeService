using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using TTTService.HttpApi.Commands.Requests.GameLobbyRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;
using TTTService.infrastructure;
using TTTService.infrastructure.InterfaceImpl;

namespace TTTService.HttpApi.Commands.Handlers.GameLobbiesRequestsHandlers
{
    public class JoinToLobbyCommandRequestHandler : IRequestHandler<JoinToLobbyCommand, ReturnResult>
    {
        private GameLobbyRepository _repository;

        public JoinToLobbyCommandRequestHandler(GameLobbyRepository repository) { _repository = repository; }
        public Task<ReturnResult> Handle(JoinToLobbyCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();
            int resultjoin = _repository.JoinToLobby(request.Title, request.Password);
            switch (resultjoin) {
                case (int)GameLobbyStatuse.WrongPassword:
                    {
                        result.Data = $"Неверный пароль";
                        result.StatuseCode = (int)Statuse.BadGateway;
                        break;
                    }
                case (int)GameLobbyStatuse.Success:
                    {
                        result.Data = $"Вы вошли в лобби";
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
                        result.Data = $"Невозможно подключиться к лобби";
                        result.StatuseCode = (int)Statuse.Conflicted;
                        break; 
                    }
            }
      
            return Task.FromResult( result);
        }
    }
}
