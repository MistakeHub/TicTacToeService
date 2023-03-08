using MediatR;
using TTTService.HttpApi.Helpers;

namespace TTTService.HttpApi.Commands.Requests.UserRequests
{
    public class AddUserCommand: IRequest<ReturnResult>
    {
        public string Login { get; set; }

        public string Password { get; set; }

       
    }
}
