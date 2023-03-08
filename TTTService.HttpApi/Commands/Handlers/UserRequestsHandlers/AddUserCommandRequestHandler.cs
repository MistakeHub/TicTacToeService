using MediatR;
using TTTService.Domain.Entities;
using TTTService.Domain.Interfaces;
using TTTService.HttpApi.Commands.Requests.UserRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;

using TTTService.infrastructure.InterfaceImpl;

namespace TTTService.HttpApi.Commands.Handlers.UserRequestsHandlers
{
    public class AddUserCommandRequestHandler : IRequestHandler<AddUserCommand, ReturnResult>
    {
        private IUserRepository _repository;

        public AddUserCommandRequestHandler(IUserRepository repository) { _repository= repository; }
        public async Task<ReturnResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();
         
            if (_repository.IsUserExists(request.Login).Result)
            {
                result.Data = $"Пользователь с логином {request.Login} уже существует";
                result.StatuseCode = (int)Statuse.Conflicted;
                return result;
            }
            int resultadd = await _repository.Add(new User() { Login = request.Login, Password = HashHelper.GetHashString( request.Password )});
            if(resultadd == 1)
            {
                result.Data = $"Пользователь с логином {request.Login} был успешно добавлен";
                result.StatuseCode = (int)Statuse.Success;
            }
            else
            {
                result.Data = $"Пользователь с логином {request.Login} не был добавлен";
                result.StatuseCode = (int)Statuse.Conflicted;
            }
            return result;
        }
    }
}
