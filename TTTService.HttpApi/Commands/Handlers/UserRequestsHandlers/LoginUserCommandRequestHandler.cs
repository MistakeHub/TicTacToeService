using MediatR;
using TTTService.Domain.Entities;
using TTTService.Domain.Interfaces;
using TTTService.HttpApi.Commands.Requests.UserRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;
using TTTService.infrastructure.InterfaceImpl;

namespace TTTService.HttpApi.Commands.Handlers.UserRequestsHandlers
{
    public class LoginUserCommandRequestHandler : IRequestHandler<LoginUserCommand, ReturnResult>
    {
        private IUserRepository _repository;

        public LoginUserCommandRequestHandler(IUserRepository repository) { _repository = repository; }
        public async Task<ReturnResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();
            if (!_repository.IsUserExists(request.Login).Result)
            {
                result.Data = $"Такой пользователь не существует";
                result.StatuseCode = (int)Statuse.NotFound;
                return result;
            }
            User user = await _repository.CheckPasswordAndUsser(request.Login, HashHelper.GetHashString(request.Password));
            if (user != null)
            {
                result.Data = user.Id;
                result.StatuseCode = (int)Statuse.Success;
            }
            else
            {
                result.Data = $"Неверный пароль";
                result.StatuseCode = (int)Statuse.BadGateway;
            }
            return result;
        }
    }
}
